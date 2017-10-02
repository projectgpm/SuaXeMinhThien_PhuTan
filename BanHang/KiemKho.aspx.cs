using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class KiemKho : System.Web.UI.Page
    {
        dtKiemKho data = new dtKiemKho();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 72) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    txtNguoiLapPhieu.Text = Session["TenDangNhap"].ToString();
                    cmbKho.Value = Session["IDKho"].ToString();
                }
                LoadGrid(IDPhieuKiemKho_Temp.Value.ToString());

            }
        }

        protected void gridDanhSachHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            data = new dtKiemKho();
            data.XoaPhieuKiemKho_Temp_ID(ID);
            e.Cancel = true;
            gridDanhSachHangHoa_Temp.CancelEdit();
            LoadGrid(IDPhieuKiemKho);
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data = new dtKiemKho();
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            data.XoaPhieuKiemKho_Temp_IDPhieuKiemKho(IDPhieuKiemKho);
            Response.Redirect("DanhSachKiemKho.aspx");
        }
        public void LoadGrid(string IDPhieuKiemKho)
        {
            data = new dtKiemKho();
            gridDanhSachHangHoa_Temp.DataSource = data.DanhSachKiemKhoTemp_IDPhieuKiemKho(IDPhieuKiemKho);
            gridDanhSachHangHoa_Temp.DataBind();
        }
        protected void gridDanhSachHangHoa_Temp_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            string ID = e.Keys[0].ToString();
            int ThucTe = Int32.Parse(e.NewValues["ThucTe"].ToString());
            if (ThucTe >= 0)
            {
                string IDHangHoa = e.NewValues["IDHangHoa"].ToString();
                int TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa,Session["IDKho"].ToString());
                data = new dtKiemKho();
                data.CapNhatPhieuKiemKho_Temp(ID, ThucTe, ThucTe - TonKho);
                e.Cancel = true;
                gridDanhSachHangHoa_Temp.CancelEdit();
                LoadGrid(IDPhieuKiemKho);
            }
            else
            {
                throw new Exception("Lỗi: Số lượng thực tế phải  >= 0");
            }
        }

        protected void cmbKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKe.Text != "")
            {
                // kiểm kho phải kiểm từng kệ.
                data = new dtKiemKho();
                data.XoaPhieuKiemKho_Temp_IDPhieuKiemKho(IDPhieuKiemKho_Temp.Value.ToString());
                Random ran = new Random();
                IDPhieuKiemKho_Temp.Value = ran.Next(100000, 999999).ToString();
                dtKe k = new dtKe();
                DataTable db = k.DanhSachChiTietKe(cmbKe.Value.ToString());
                if (db.Rows.Count > 0)
                {
                    string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
                    foreach (DataRow dr in db.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string MaHang = dr["MaHang"].ToString();
                        string IDonViTinh = dr["IDonViTinh"].ToString();
                        int TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
                        int ChechLech = -TonKho;
                        DataTable dt = data.KTChiTietPhieuKiemKho_Temp(IDHangHoa, IDPhieuKiemKho, cmbKe.Value.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            data = new dtKiemKho();
                            data.ThemPhieuKiemKho_Temp(IDPhieuKiemKho, IDHangHoa, TonKho, ChechLech, MaHang, IDonViTinh, cmbKe.Value.ToString());
                        }
                    }
                    LoadGrid(IDPhieuKiemKho);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Kệ chưa có hàng hóa.'); </script>");
                }
            }
        }

        protected void txtNgayLapPhieu_Init(object sender, EventArgs e)
        {
            txtNgayLapPhieu.Date = DateTime.Today;
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            DataTable db = data.DanhSachKiemKhoTemp_IDPhieuKiemKho(IDPhieuKiemKho);
            if (db.Rows.Count > 0)
            {
                string IDNguoiDung = Session["IDNhanVien"].ToString();
                DateTime NgayKiemKho = DateTime.Parse(txtNgayLapPhieu.Text.ToString());
                string IDKho = Session["IDKho"].ToString();
                string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                data = new dtKiemKho();
                object ID1 = data.ThemPhieu_Temp();
                if (ID1 != null)
                {
                    data.CapNhatPhieuKiemKho(ID1, IDNguoiDung, NgayKiemKho, IDKho, GhiChu);
                    foreach (DataRow dr in db.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string TonKho = dr["TonKho"].ToString();
                        string ChenhLech = dr["ChenhLech"].ToString();
                        string ThucTe = dr["ThucTe"].ToString();
                        string MaHang = dr["MaHang"].ToString();
                        string IDDonViTinh = dr["IDDonViTinh"].ToString();
                        string IDKe = dr["IDKe"].ToString();
                        data = new dtKiemKho();
                        data.ThemPhieuKiemKho(ID1, IDHangHoa, TonKho, ChenhLech, ThucTe, MaHang, IDDonViTinh, IDKe);
                    }
                    data = new dtKiemKho();
                    data.XoaPhieuKiemKho_Temp_IDPhieuKiemKho(IDPhieuKiemKho);
                    Response.Redirect("DanhSachKiemKho.aspx");
                    //dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Kiểm Kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");  
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Cập nhật không thành công.'); </script>");
                }
            }
            else
            {
                cmbKe.Focus();
                Response.Write("<script language='JavaScript'> alert('Danh sách kiểm kho rỗng.'); </script>");
            }
        }
    }
}
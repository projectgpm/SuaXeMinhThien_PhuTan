using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ThemDonHang : System.Web.UI.Page
    {
        dtThemDonHangKho data = new dtThemDonHangKho();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    IDThuMuaDatHang_Temp.Value = Session["IDNhanVien"].ToString();
                    TinhTongTien();
                    txtNguoiLap.Text = Session["TenDangNhap"].ToString();
                    txtSoDonHang.Text = DateTime.Now.ToString("ddMMyyyy-hhmmss");
                }
                LoadGrid(IDThuMuaDatHang_Temp.Value.ToString());
            }
        }
        private void LoadGrid(string p)
        {
            data = new dtThemDonHangKho();
            gridDanhSachHangHoa.DataSource = data.DanhSachDonDatHang_Temp(p);
            gridDanhSachHangHoa.DataBind();
        }
        public void CLear()
        {
            cmbHangHoa.Text = "";
            txtTonKho.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
        }
        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
                if (SoLuong > 0)
                {
                    string IDHangHoa = cmbHangHoa.Value.ToString();
                    string MaHangHoa = dtHangHoa.LayMaHang(IDHangHoa);
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    float DonGia = float.Parse(txtDonGia.Text);
                    string IDDonHang = IDThuMuaDatHang_Temp.Value.ToString();
                    DataTable db = dtThemDonHangKho.KTChiTietDonHang_Temp(IDHangHoa, IDDonHang);// kiểm tra hàng hóa
                    if (db.Rows.Count == 0)
                    {
                        data = new dtThemDonHangKho();
                        data.ThemChiTietDonHang_Temp(IDDonHang, IDHangHoa, MaHangHoa, IDDonViTinh, SoLuong, DonGia);
                        TinhTongTien();
                        CLear();
                    }
                    else
                    {
                        data = new dtThemDonHangKho();
                        data.CapNhatChiTietDonHang_temp(IDDonHang, IDHangHoa, SoLuong, DonGia);
                        TinhTongTien();
                        CLear();
                    }
                    LoadGrid(IDDonHang);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Số Lượng phải > 0.'); </script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn hàng hóa.'); </script>");
                return;
            }
        }
        public void TinhTongTien()
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            DataTable db = data.DanhSachDonDatHang_Temp(IDThuMuaDatHang);
            if (db.Rows.Count != 0)
            {
                double TongTien = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double ThanhTien = double.Parse(dr["ThanhTien"].ToString());
                    TongTien = TongTien + ThanhTien;
                }
                txtTongTien.Text = (TongTien).ToString();
            }
            else
            {
                txtTongTien.Text = "0";
            }
        }
        protected void btnThem_Click(object sender, EventArgs e)
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            DataTable dt = data.DanhSachDonDatHang_Temp(IDThuMuaDatHang);
            if (dt.Rows.Count != 0)
            {
                string SoDonHang = txtSoDonHang.Text.Trim();
                string IDNguoiLap = Session["IDNhanVien"].ToString();
                DateTime NgayLap = DateTime.Parse(txtNgayLap.Text);
                string TongTien = txtTongTien.Text;
                string IDChiNhanh = Session["IDKho"].ToString();
                string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                string IDNhaCungCap = cmbNhaCungCap.Text == "" ? "" : cmbNhaCungCap.Value.ToString();
                int TrangThai = 0;
                if (ckThanhToan.Checked == true)
                {
                    TrangThai = 1;
                }
                if (cmbNhaCungCap.Text != "" && ckThanhToan.Checked == false)
                {
                    data = new dtThemDonHangKho();
                    data.CongCongNoNCC(IDNhaCungCap, TongTien);

                }
                data = new dtThemDonHangKho();
                object ID = data.ThemPhieuDatHang();
                if (ID != null)
                {
                    data.CapNhatDonDatHang(ID, SoDonHang, IDNguoiLap, NgayLap, TongTien, GhiChu, IDNhaCungCap, TrangThai);
                    foreach (DataRow dr in dt.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string MaHangHoa = dr["MaHangHoa"].ToString();
                        string IDDonViTinh = dr["IDDonViTinh"].ToString();
                        string SoLuong = dr["SoLuong"].ToString();
                        string DonGia = dr["DonGia"].ToString();
                        string ThanhTien = dr["ThanhTien"].ToString();
                        data = new dtThemDonHangKho();
                        dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDChiNhanh); // cộng kho không qua bước duyệt
                        // ghi lịch sử
                        data.ThemChiTietDonHang(ID, IDHangHoa, MaHangHoa, IDDonViTinh, SoLuong, DonGia, ThanhTien);
                    }
                    data = new dtThemDonHangKho();
                    data.XoaChiTietDonHang_Nhap(IDThuMuaDatHang);

                   // dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Thêm đơn hàng", "Thêm đơn đặt hàng");

                    Response.Redirect("DanhSachPhieuDatHang.aspx");
                }
            }
            else
            {
                cmbHangHoa.Focus();
                Response.Write("<script language='JavaScript'> alert('Danh sách nguyên liệu rỗng.'); </script>");
            }
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            data.XoaChiTietDonHang_Nhap(IDThuMuaDatHang);
            Response.Redirect("DanhSachPhieuDatHang.aspx");
        }
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            data.XoaChiTietDonHang_Temp_ID(ID);
            TinhTongTien();
            LoadGrid(IDThuMuaDatHang);
        }
        protected void cmbHangHoa_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaMua, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID)";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaMua] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaMua, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang) OR GPM_HangHoa.TenHangHoa LIKE @TenHang)  AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                txtTonKho.Text = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), Session["IDKho"].ToString()) + "";
                txtDonGia.Text = dtCapNhatTonKho.GiaMua(cmbHangHoa.Value.ToString()) + "";
                txtSoLuong.Text = "0";
            }
        }

        protected void txtNgayLap_Init(object sender, EventArgs e)
        {
            txtNgayLap.Date = DateTime.Today;
        }
        protected void cmbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "")
            {
                ckThanhToan.Enabled = true;
            }
        }
    }
}
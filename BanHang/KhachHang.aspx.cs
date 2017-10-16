using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class KhachHang : System.Web.UI.Page
    {
        dtKhachHang data = new dtKhachHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //btnNhapExcel.Enabled = false;
               // gridKhachHang.Columns["chucnang"].Visible = false;
                LoadGrid();
            }
            //else
            //{
            //    Response.Redirect("Default.aspx");
            //}
        }
        
        public void LoadGrid()
        {
            data = new dtKhachHang();
            gridKhachHang.DataSource = data.LayDanhSachKhachHang(Session["IDKho"].ToString());
            gridKhachHang.DataBind();
        }

        protected void gridKhachHang_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            data = new dtKhachHang();
            int IDNhomKhachHang = Int32.Parse(e.NewValues["IDNhomKhachHang"].ToString());
            string TenKhachHang = e.NewValues["TenKhachHang"] == null ? "" : e.NewValues["TenKhachHang"].ToString();
            string NgaySinh = e.NewValues["NgaySinh"] == null ? "" : e.NewValues["NgaySinh"].ToString();
            string CMND = e.NewValues["CMND"] == null ? "" : e.NewValues["CMND"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            string IDChietKhau = e.NewValues["IDChietKhau"].ToString();
            if (dtKhachHang.KT_SDT_KH_CapNhat(DienThoai.Trim(), ID) == -1)
            {
                if (dtKhachHang.KT_SDT_KH(DienThoai.Trim()) == 1)
                {
                    throw new Exception("Lỗi: Số điện thoại đã tồn tại?");
                }
            }
            else
            {

                data.SuaThongTinKhachHang(Int32.Parse(ID), IDNhomKhachHang, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, GhiChu, IDChietKhau);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");   
            }
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            
        }

        protected void gridKhachHang_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dtKhachHang();
            int IDNhomKhachHang = Int32.Parse(e.NewValues["IDNhomKhachHang"].ToString());
            string TenKhachHang = e.NewValues["TenKhachHang"].ToString();
            DateTime NgaySinh = DateTime.Parse( e.NewValues["NgaySinh"] == null ? DateTime.Now.ToString() : e.NewValues["NgaySinh"].ToString());
            string CMND = e.NewValues["CMND"] == null ? "" : e.NewValues["CMND"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string MaKh = "";
            string Barcode = "";
            object ID;
            string IDChietKhau = e.NewValues["IDChietKhau"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (DienThoai != "")
            {
                if (dtKhachHang.KT_SDT_KH(DienThoai.Trim()) != -1)
                {
                    throw new Exception("Lỗi: Số điện thoại đã tồn tại?");
                }
                else
                {
                    ID = data.ThemKhachHang(IDNhomKhachHang, MaKh, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, Barcode, GhiChu, Session["IDKho"].ToString(), IDChietKhau);
                    if (ID != null)
                    {
                        if (e.NewValues["MaKhachHang"] == null)
                        {
                            data = new dtKhachHang();
                            data.CapNhatMaKhachHang(ID, (dtSetting.LayMaKho(Session["IDKho"].ToString()) + "." + (Int32.Parse(ID.ToString()) * 0.0001).ToString().Replace(".", "")).ToString(), (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001)).Replace(".", ""));
                            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                        }
                    }
                }
            }
            else
            {
                ID = data.ThemKhachHang(IDNhomKhachHang, MaKh, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, Barcode, GhiChu, Session["IDKho"].ToString(), IDChietKhau);
                if (ID != null)
                {
                    if (e.NewValues["MaKhachHang"] == null)
                    {
                        data = new dtKhachHang();
                        data.CapNhatMaKhachHang(ID, (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001).ToString().Replace(".", "")).ToString(), (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001)).Replace(".", ""));
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                    }
                }
            }
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            
        }

        protected void gridKhachHang_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKhachHang();
            data.XoaKhachHang(Int32.Parse(ID));
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + ID, Session["IDKho"].ToString(), "Danh Mục", "Xóa");  
        }
        
        protected void gridKhachHang_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["ChietKhau"] = "0";
        }

        protected void cmbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "")
            {
                cmbHinhThucThanhToan.Enabled = true;
                txtMaPhieu.Text = "";
                txtTienThanhToan.Text = "";
                txtNhapSoHoaDon.Enabled = true;
                MaPhieu();
                txtNoHienTai.Text = dtKhachHang.LayCongNoCuKhachHang(cmbKhachHang.Value.ToString()).ToString();
            }
        }
        public void MaPhieu()
        {
            data = new dtKhachHang();
            txtMaPhieu.DataSource = data.DanhSachSoDonHang(cmbKhachHang.Value.ToString());
            txtMaPhieu.TextField = "MaHoaDon";
            txtMaPhieu.ValueField = "ID";
            txtMaPhieu.DataBind();
        }
        protected void btnCongNo_Click(object sender, EventArgs e)
        {
            Clear();
            popup.ShowOnPageLoad = true;
        }
        public void Clear()
        {
            cmbKhachHang.Text = "";
            txtNoHienTai.Text = "";
            cmbHinhThucThanhToan.Text = "";
            txtMaPhieu.Text = "";
            txtTienThanhToan.Text = "";
            txtNhapSoHoaDon.Text = "";
            txtNoiDung.Text = "";
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Clear();
            cmbHinhThucThanhToan.Enabled = false;
            txtMaPhieu.Enabled = false;
            txtTienThanhToan.Enabled = false;
            txtNhapSoHoaDon.Enabled = false;
            popup.ShowOnPageLoad = false;
        }

        protected void btnCapNhatThanhToan_Click(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "" && cmbHinhThucThanhToan.Text != "" && txtTienThanhToan.Text != "")
            {
                data = new dtKhachHang();
                int KT = Int32.Parse(cmbHinhThucThanhToan.Value.ToString());
                string IDKhachHang = cmbKhachHang.Value.ToString();
                string HinhThucThanhToan = cmbHinhThucThanhToan.Text.ToString();
                string SoHoaDon = txtNhapSoHoaDon.Text == null ? "" : txtNhapSoHoaDon.Text;
                double SoTienThanhToan = double.Parse(txtTienThanhToan.Text);
                string NoiDung = txtNoiDung.Text == null ? "" : txtNoiDung.Text;
                DateTime NgayThanhToan = DateTime.Parse(dateNgayThanhToan.Text);
                if (KT == 0)
                {
                    if (double.Parse(txtNoHienTai.Text.ToString()) < double.Parse(txtTienThanhToan.Text.ToString()))
                    {
                        txtTienThanhToan.Text = "";
                        txtTienThanhToan.Focus();
                        Response.Write("<script language='JavaScript'> alert('Số tiền trả vượt quá số tiền nợ.'); </script>");
                    }
                    else
                    {

                        object ID = data.ThemChiTietCongNo(SoHoaDon, IDKhachHang, HinhThucThanhToan, "", SoTienThanhToan, NoiDung, NgayThanhToan);
                        if (ID != null)
                        {
                            data.CapNhatCongNo(IDKhachHang, SoTienThanhToan);
                            DataTable db = data.DanhSachSoDonHang(IDKhachHang);
                            if (db.Rows.Count != 0)
                            {
                                foreach (DataRow dr in db.Rows)
                                {
                                    float TienMaPhieu = float.Parse(dr["TongTien"].ToString());
                                    string IDHoaDon = dr["ID"].ToString();
                                    if (SoTienThanhToan > TienMaPhieu)
                                    {
                                        data = new dtKhachHang();
                                        data.CapNhatTinhTrang(IDHoaDon);
                                        SoTienThanhToan = SoTienThanhToan - TienMaPhieu;
                                    }
                                    else if (SoTienThanhToan > 0)
                                    {
                                        data = new dtKhachHang();
                                        data.CapNhatTinhTrang(IDHoaDon);
                                        SoTienThanhToan = 0;
                                    }
                                    Response.Redirect("ChiTietCongNoKhachHang.aspx");
                                }
                            }
                            Response.Redirect("ChiTietCongNoKhachHang.aspx");
                        }
                    }
                }
                if (KT == 1)
                {
                    string IDMaPhieu = txtMaPhieu.Value.ToString();
                    object ID = data.ThemChiTietCongNo(SoHoaDon, IDKhachHang, HinhThucThanhToan, IDMaPhieu, SoTienThanhToan, NoiDung, NgayThanhToan);
                    if (ID != null)
                    {
                        data.CapNhatCongNo(IDKhachHang, SoTienThanhToan);
                        data.CapNhatTinhTrang(IDMaPhieu);
                        Response.Redirect("ChiTietCongNoKhachHang.aspx");
                    }
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập đủ thông tin.'); </script>");
            }

           // dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Cập nhật công nợ nhà cung cấp", "Thanh toán công nợ.");
        
        }

        protected void cmbHinhThucThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHinhThucThanhToan.Text != "")
            {
                int KT = Int32.Parse(cmbHinhThucThanhToan.Value.ToString());
                if (KT == 0)
                {
                    txtMaPhieu.Enabled = false;
                    txtMaPhieu.Text = "";
                    txtTienThanhToan.Enabled = true;
                    txtTienThanhToan.Text = "";
                }
                if (KT == 1)
                {
                    txtMaPhieu.Enabled = true;
                    txtTienThanhToan.Enabled = false;
                    MaPhieu();
                }
            }
        }

        protected void txtMaPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text != "")
            {
                MaPhieu();
                txtTienThanhToan.Text = dtKhachHang.LayTienThanhToan_IDHoaDon(txtMaPhieu.Value.ToString()) + "";
            }
        }

        protected void dateNgayThanhToan_Init(object sender, EventArgs e)
        {
            dateNgayThanhToan.Date = DateTime.Now;
        }
    }
}
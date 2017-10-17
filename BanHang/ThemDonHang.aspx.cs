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
            txtBarcode.Text = "";
            txtSoLuong.Text = "";
        }
        //protected void btnThem_Temp_Click(object sender, EventArgs e)
        //{
        //    if (txtBarcode.Text != "")
        //    {
        //        int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
        //        if (SoLuong > 0)
        //        {
        //            string IDHangHoa = txtBarcode.Value.ToString();
        //            string MaHangHoa = dtHangHoa.LayMaHang(IDHangHoa);
        //            string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
        //            float DonGia = float.Parse(txtDonGia.Text);
        //            string IDDonHang = IDThuMuaDatHang_Temp.Value.ToString();
        //            DataTable db = dtThemDonHangKho.KTChiTietDonHang_Temp(IDHangHoa, IDDonHang);// kiểm tra hàng hóa
        //            if (db.Rows.Count == 0)
        //            {
        //                data = new dtThemDonHangKho();
        //                data.ThemChiTietDonHang_Temp(IDDonHang, IDHangHoa, MaHangHoa, IDDonViTinh, SoLuong, DonGia);
        //                TinhTongTien();
        //                CLear();
        //            }
        //            else
        //            {
        //                data = new dtThemDonHangKho();
        //                data.CapNhatChiTietDonHang_temp(IDDonHang, IDHangHoa, SoLuong, DonGia);
        //                TinhTongTien();
        //                CLear();
        //            }
        //            LoadGrid(IDDonHang);
        //        }
        //        else
        //        {
        //            Response.Write("<script language='JavaScript'> alert('Số Lượng phải > 0.'); </script>");
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("<script language='JavaScript'> alert('Vui lòng chọn hàng hóa.'); </script>");
        //        return;
        //    }
        //}
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
                    Response.Redirect("DanhSachPhieuDatHang.aspx");
                }
            }
            else
            {
                txtBarcode.Focus();
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
        protected void btnInsertHang_Click(object sender, EventArgs e)
        {
            try
            {
                dtBanHangLe dt = new dtBanHangLe();
                if (txtBarcode.Text.Trim() != "")
                {
                    DataTable tbThongTin;
                    if (txtBarcode.Value == null)
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Text.ToString(), Session["IDKho"].ToString());
                    }
                    else
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Value.ToString(), Session["IDKho"].ToString());
                    }

                    if (tbThongTin.Rows.Count > 0)
                    {
                        string IDKho = Session["IDKho"].ToString();
                        string IDDonHang = IDThuMuaDatHang_Temp.Value.ToString();
                        string IDHangHoa = tbThongTin.Rows[0]["ID"].ToString();
                        string MaHangHoa = tbThongTin.Rows[0]["MaHang"].ToString();
                        string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                        string HinhAnh = tbThongTin.Rows[0]["HinhAnh"].ToString();
                        int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
                        double DonGia = double.Parse(tbThongTin.Rows[0]["GiaMua"].ToString());
                        DataTable db = dtThemDonHangKho.KTChiTietDonHang_Temp(IDHangHoa, IDDonHang);// kiểm tra hàng hóa
                        if (db.Rows.Count == 0)
                        {
                            data.ThemChiTietDonHang_Temp(IDDonHang, IDHangHoa, MaHangHoa, IDDonViTinh, SoLuong, DonGia, HinhAnh);
                            TinhTongTien();
                            txtBarcode.Focus();
                        }
                        else
                        {
                            data.CapNhatChiTietDonHang_temp(IDDonHang, IDHangHoa, SoLuong, DonGia);
                            TinhTongTien();
                            txtBarcode.Focus();
                        }
                    }
                    else
                    {
                        txtBarcode.Focus();
                        Response.Write("<script language='JavaScript'> alert('Mã hàng không tồn tại !!!'); </script>");
                    }
                }
                txtBarcode.Focus();
                txtBarcode.Text = "";
                txtBarcode.Value = "";
                txtSoLuong.Text = "1";
            }
            catch (Exception ex)
            {
                Response.Write("<script language='JavaScript'> alert('Error: " + ex + "'); </script>");
            }
        }

        protected void txtBarcode_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang,GPM_HangHoa.HinhAnh, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBan, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID)";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void txtBarcode_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBan] , [TenDonViTinh],[HinhAnh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang,GPM_HangHoa.HinhAnh, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBan, GPM_DonViTinh.TenDonViTinh, 
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
    }
}
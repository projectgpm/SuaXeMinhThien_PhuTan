using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HangSanXuat : System.Web.UI.Page
    {
        dataHangSanXuat data = new dataHangSanXuat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    LoadGrid();
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                        gridDanhSach.Columns["chucnang"].Visible = false;
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void LoadGrid()
        {
            dataHangSanXuat data = new dataHangSanXuat();
            gridDanhSach.DataSource = data.getDanhSachHangSX();
            gridDanhSach.DataBind();
        }

        protected void gridDanhSach_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dataHangSanXuat();
            string TenNSX = e.NewValues["TenNSX"].ToString();
            string dienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string fax = e.NewValues["Fax"] == null ? "" : e.NewValues["Fax"].ToString();
            string email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string diaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string nguoiLienHe = e.NewValues["NguoiLienHe"] == null ? "" : e.NewValues["NguoiLienHe"].ToString();
            string maSoThue = e.NewValues["MaSoThue"] == null ? "" : e.NewValues["MaSoThue"].ToString();
            string linhVucKinhDoanh = e.NewValues["LinhVucKinhDoanh"] == null ? "" : e.NewValues["LinhVucKinhDoanh"].ToString();
            string MaNSX = e.NewValues["MaNSX"].ToString();
            DateTime NgayCapNhat = DateTime.Today.Date;
            string ghiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenNSX = dtSetting.convertDauSangKhongDau(TenNSX).ToUpper();
            if (dtSetting.IsNumber(MaNSX) == true)
            {
                if (dataHangSanXuat.KiemTraMaNSX(MaNSX) == false)
                {
                    data.ThemHangSX(MaNSX, TenNSX, dienThoai, fax, email, diaChi, nguoiLienHe, maSoThue, linhVucKinhDoanh, NgayCapNhat, ghiChu);
                    e.Cancel = true;
                    gridDanhSach.CancelEdit();
                    LoadGrid();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhà SX", Session["IDKho"].ToString(), "Danh mục", "Thêm: " + TenNSX); 
                }
                else
                {
                    throw new Exception("Lỗi: Mã nhà cung cấp đã tồn tại");
                }
            }
            else
            {
                throw new Exception("Lỗi: Mã nhà sản xuất phải là số.");
            }
        }

        protected void gridDanhSach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            dataHangSanXuat data = new dataHangSanXuat();
            string ID = e.Keys[0].ToString();
            data.deleteHangSX(ID);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hãng SX", Session["IDKho"].ToString(), "Danh mục", "Xóa: ID = " + ID); 
        }

        protected void gridDanhSach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            data = new dataHangSanXuat();
            string ID = e.Keys["ID"].ToString();
            string TenNSX = e.NewValues["TenNSX"].ToString();
            string dienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string fax = e.NewValues["Fax"] == null ? "" : e.NewValues["Fax"].ToString();
            string email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string diaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string nguoiLienHe = e.NewValues["NguoiLienHe"] == null ? "" : e.NewValues["NguoiLienHe"].ToString();
            string maSoThue = e.NewValues["MaSoThue"] == null ? "" : e.NewValues["MaSoThue"].ToString();
            string linhVucKinhDoanh = e.NewValues["LinhVucKinhDoanh"] == null ? "" : e.NewValues["LinhVucKinhDoanh"].ToString();
            string MaNSX = e.NewValues["MaNSX"].ToString();
            string ghiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenNSX = dtSetting.convertDauSangKhongDau(TenNSX).ToUpper();
            if (dtSetting.IsNumber(MaNSX) == true)
            {
                if (dataHangSanXuat.KiemTraMaNCC_ID(MaNSX, ID) == true)
                {
                    data.SuaThongTin(MaNSX, Int32.Parse(ID), TenNSX, dienThoai, fax, email, diaChi, nguoiLienHe, maSoThue, linhVucKinhDoanh, ghiChu);
                    e.Cancel = true;
                    gridDanhSach.CancelEdit();
                    LoadGrid();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhà Sản Xuất: " + TenNSX, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");
                }
                else
                {
                    if (dataHangSanXuat.KiemTraMaNSX(MaNSX) == false)
                    {
                        data.SuaThongTin(MaNSX, Int32.Parse(ID), TenNSX, dienThoai, fax, email, diaChi, nguoiLienHe, maSoThue, linhVucKinhDoanh, ghiChu);
                        e.Cancel = true;
                        gridDanhSach.CancelEdit();
                        LoadGrid();
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhà Sản Xuất: " + TenNSX, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");
                    }
                    else
                    {
                        throw new Exception("Lỗi: Mã nhà cung cấp đã tồn tại");
                    }
                }
            }
            else
            {
                throw new Exception("Lỗi: Mã nhà cung cấp phải là số");
            }

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hãng SX", Session["IDKho"].ToString(), "Danh mục", "Cập nhật: " + TenNSX); 
        }

        protected void gridDanhSach_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["MaNSX"] = dataHangSanXuat.Dem_Max();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BanHang.Data;
using DevExpress.Web;

namespace BanHang
{
    public partial class NhaCungCap : System.Web.UI.Page
    {
        dtNhaCungCap data = new dtNhaCungCap();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                //{
                    LoadGrid();
                    //if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                    //{
                    //    gridNhaCungCap.Columns["chucnang"].Visible = false;
                    //    btnNhapExcel.Enabled = false;
                    //}
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}
            }
        }
        public void LoadGrid()
        {
            data = new dtNhaCungCap();
            gridNhaCungCap.DataSource = data.LayDanhSachNhaCungCap();
            gridNhaCungCap.DataBind();
        }

        protected void gridNhaCungCap_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtNhaCungCap();
            data.XoaNhaCungCap(Int32.Parse(ID));
            e.Cancel = true;
            gridNhaCungCap.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhà Cung Cấp:" + ID, Session["IDKho"].ToString(), "Danh Mục", "Xóa");
        }

        protected void gridNhaCungCap_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dtNhaCungCap();
            string tenNhaCungCap = e.NewValues["TenNhaCungCap"].ToString();
            string dienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string fax = e.NewValues["Fax"] == null ? "" : e.NewValues["Fax"].ToString();
            string email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string diaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string nguoiLienHe = e.NewValues["NguoiLienHe"] == null ? "" : e.NewValues["NguoiLienHe"].ToString();
            string maSoThue = e.NewValues["MaSoThue"] == null ? "" : e.NewValues["MaSoThue"].ToString();
            string linhVucKinhDoanh = e.NewValues["LinhVucKinhDoanh"] == null ? "" : e.NewValues["LinhVucKinhDoanh"].ToString();
            string MaNCC = e.NewValues["MaNCC"].ToString();
            DateTime NgayCapNhat = DateTime.Today.Date;
            string ghiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                tenNhaCungCap = dtSetting.convertDauSangKhongDau(tenNhaCungCap).ToUpper();
            if (dtSetting.IsNumber(MaNCC) == true)
            {
                if (dtNhaCungCap.KiemTraMaNCC(MaNCC) == false)
                {
                    data.ThemNhaCungCap(MaNCC, tenNhaCungCap, dienThoai, fax, email, diaChi, nguoiLienHe, maSoThue, linhVucKinhDoanh, NgayCapNhat, ghiChu);
                    e.Cancel = true;
                    gridNhaCungCap.CancelEdit();
                    LoadGrid();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhà Cung Cấp:" + tenNhaCungCap, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                }
                else
                {
                    throw new Exception("Lỗi: Mã nhà cung cấp đã tồn tại");
                }
            }
            else
            {
                throw new Exception("Lỗi: Mã nhà cung cấp phải là số");
            }
        }

        protected void gridNhaCungCap_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            string tenNhaCungCap = e.NewValues["TenNhaCungCap"].ToString();
            string dienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string fax = e.NewValues["Fax"] == null ? "" : e.NewValues["Fax"].ToString();
            string email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string diaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string nguoiLienHe = e.NewValues["NguoiLienHe"] == null ? "" : e.NewValues["NguoiLienHe"].ToString();
            string maSoThue = e.NewValues["MaSoThue"] == null ? "" : e.NewValues["MaSoThue"].ToString();
            string linhVucKinhDoanh = e.NewValues["LinhVucKinhDoanh"] == null ? "" : e.NewValues["LinhVucKinhDoanh"].ToString();
            string MaNCC = e.NewValues["MaNCC"].ToString();
            string ghiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                tenNhaCungCap = dtSetting.convertDauSangKhongDau(tenNhaCungCap).ToUpper();
            if (dtSetting.IsNumber(MaNCC) == true)
            {
                if (dtNhaCungCap.KiemTraMaNCC_ID(MaNCC, ID) == true)
                {
                    data.SuaThongTinNhaCungCap(MaNCC, Int32.Parse(ID), tenNhaCungCap, dienThoai, fax, email, diaChi, nguoiLienHe, maSoThue, linhVucKinhDoanh, ghiChu);
                    e.Cancel = true;
                    gridNhaCungCap.CancelEdit();
                    LoadGrid();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhà Cung Cấp: " + tenNhaCungCap, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");
                }
                else
                {
                    if (dtNhaCungCap.KiemTraMaNCC(MaNCC) == false)
                    {
                        data.SuaThongTinNhaCungCap(MaNCC, Int32.Parse(ID), tenNhaCungCap, dienThoai, fax, email, diaChi, nguoiLienHe, maSoThue, linhVucKinhDoanh, ghiChu);
                        e.Cancel = true;
                        gridNhaCungCap.CancelEdit();
                        LoadGrid();
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhà Cung Cấp: " + tenNhaCungCap, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");
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
        }

        protected void btnXuatPDF_Click(object sender, EventArgs e)
        {
            XuatDuLieu.WritePdfToResponse();
        }

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            XuatDuLieu.WriteXlsToResponse();
        }

        protected void btnNhapExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImportExcel_NhaCungCap.aspx");
        }

        protected void gridNhaCungCap_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["MaNCC"] = dtNhaCungCap.Dem_Max();
        }
    }
}
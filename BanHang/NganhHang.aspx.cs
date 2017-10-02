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
    public partial class NganhHang : System.Web.UI.Page
    {
        dataNganhHang da = new dataNganhHang();
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
                        gridNganhHang.Columns["chucnang"].Visible = false;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
        public void LoadGrid()
        {
            gridNganhHang.DataSource = da.getDanhSachNganhHang();
            gridNganhHang.DataBind();
        }


        protected void gridNganhHang_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            da.XoaNganhHang(Int32.Parse(ID));
            e.Cancel = true;
            gridNganhHang.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Ngành Hàng", Session["IDKho"].ToString(), "Ngành Hàng", "Xóa ID = " + ID); 
        }

        protected void gridNganhHang_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            da = new dataNganhHang();
            string ID = e.Keys["ID"].ToString();
            string s = e.NewValues["TenNganhHang"].ToString();
            string MaNganh = e.NewValues["MaNganh"].ToString();
            string GhiChu  =  e.NewValues["GhiChu"] != null ? e.NewValues["GhiChu"].ToString() : "";
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                s = dtSetting.convertDauSangKhongDau(s).ToUpper();
            if (dtSetting.IsNumber(MaNganh) == true)
            {
                if (dataNganhHang.KiemTraMaNganhHang_ID(MaNganh, ID) == true)
                {
                    da.updateNganhHang(Int32.Parse(ID), MaNganh, s, GhiChu);
                    e.Cancel = true;
                    gridNganhHang.CancelEdit();
                    LoadGrid();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Ngành Hàng", Session["IDKho"].ToString(), "Ngành Hàng", "Cập Nhật ID = " + ID);
                }
                else
                {
                    if (dataNganhHang.KiemTraMaNganhHang(MaNganh) == false)
                    {
                        da.updateNganhHang(Int32.Parse(ID), MaNganh, s, GhiChu);
                        e.Cancel = true;
                        gridNganhHang.CancelEdit();
                        LoadGrid();
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Ngành Hàng", Session["IDKho"].ToString(), "Ngành Hàng", "Cập Nhật ID = " + ID);
                    }
                    else
                    {
                        throw new Exception("Lỗi: Mã ngành đã tồn tại");
                    }
                }
            }
            else
            {
                throw new Exception("Lỗi: Mã ngành phải là số");
            }
        }

        protected void gridNganhHang_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string s = e.NewValues["TenNganhHang"].ToString();
            string MaNganh = e.NewValues["MaNganh"].ToString();
            string GhiChu = e.NewValues["GhiChu"] != null ? e.NewValues["GhiChu"].ToString() : "";
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                s = dtSetting.convertDauSangKhongDau(s).ToUpper();
            if (dtSetting.IsNumber(MaNganh) == true)
            {
                if (dataNganhHang.KiemTraMaNganhHang(MaNganh) == false)
                {
                    da.insertNganhHang(MaNganh, s, GhiChu);
                    e.Cancel = true;
                    gridNganhHang.CancelEdit();
                    LoadGrid();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Ngành Hàng", Session["IDKho"].ToString(), "Ngành Hàng", "Thêm: " + s);
                }
                else
                {
                    throw new Exception("Lỗi: Mã ngành đã tồn tại");
                }
            }
            else
            {
                throw new Exception("Lỗi: Mã ngành phải là số");
            }
        }

       
        protected void gridNganhHang_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["MaNganh"] = dataNganhHang.Dem_Max();
        }

    }
}
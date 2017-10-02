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
    public partial class NhomHang : System.Web.UI.Page
    {
        dataNhomHang da = new dataNhomHang();
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
                //    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                //        gridNhomHang.Columns["chucnang"].Visible = false;
                //}
                //else
                //    Response.Redirect("Default.aspx");
            }
        }
        public void LoadGrid()
        {
            gridNhomHang.DataSource = da.getDanhSachNhomHang();
            gridNhomHang.DataBind();
        }

        protected void gridNhomHang_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            da.XoaNhomHang(Int32.Parse(ID));
            e.Cancel = true;
            gridNhomHang.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhóm Hàng", Session["IDKho"].ToString(), "Nhóm Hàng", "Xóa ID = " + ID); 
        }

        protected void gridNhomHang_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //int IDNganhHang = Int32.Parse(e.NewValues["IDNganhHang"].ToString());
            string MaNhom = e.NewValues["MaNhom"].ToString();
            string TenNhomHang = e.NewValues["TenNhomHang"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenNhomHang = dtSetting.convertDauSangKhongDau(TenNhomHang).ToUpper();

            string GhiChu = e.NewValues["GhiChu"] != null ? e.NewValues["GhiChu"].ToString() : "";

            if (dtSetting.IsNumber(MaNhom) == true)
            {
                if (dataNhomHang.KiemTraMaNhom(MaNhom) == false)
                {
                    da.insertNhomHang(MaNhom, TenNhomHang, GhiChu);
                    e.Cancel = true;
                    gridNhomHang.CancelEdit();
                    LoadGrid();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhóm Hàng", Session["IDKho"].ToString(), "Nhóm Hàng", "Thêm: " + TenNhomHang);
                }
                else
                {
                    throw new Exception("Lỗi: Mã nhóm đã tồn tại");
                }
            }
            else
            {
                throw new Exception("Lỗi: Mã nhóm phải là số");
            }
        }

        protected void gridNhomHang_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            string MaNhom = e.NewValues["MaNhom"].ToString();
            string TenNhomHang = e.NewValues["TenNhomHang"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenNhomHang = dtSetting.convertDauSangKhongDau(TenNhomHang).ToUpper();
            string GhiChu = e.NewValues["GhiChu"] != null ? e.NewValues["GhiChu"].ToString() : "";
            if (dtSetting.IsNumber(MaNhom) == true)
            {
                if (dataNhomHang.KiemTraMaNhom_ID(MaNhom, ID) == true)
                {
                    da.updateNhomHang(Int32.Parse(ID), MaNhom, TenNhomHang, GhiChu);
                    e.Cancel = true;
                    gridNhomHang.CancelEdit();
                    LoadGrid();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhóm Hàng", Session["IDKho"].ToString(), "Nhóm Hàng", "Cập nhật: " + ID);
                }
                else
                {
                    if (dataNhomHang.KiemTraMaNhom(MaNhom) == false)
                    {
                        da.updateNhomHang(Int32.Parse(ID), MaNhom, TenNhomHang, GhiChu);
                        e.Cancel = true;
                        gridNhomHang.CancelEdit();
                        LoadGrid();
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhóm Hàng", Session["IDKho"].ToString(), "Nhóm Hàng", "Cập nhật: " + ID);
                    }
                    else
                    {
                        throw new Exception("Lỗi: Mã nhóm đã tồn tại");
                    }
                }
            }
            else
            {
                throw new Exception("Lỗi: Mã nhốm phải là số");
            }
        }

        protected void gridNhomHang_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            e.NewValues["MaNhom"] = dataNhomHang.Dem_Max();
            //e.NewValues["IDNganhHang"] = 2;
        }
    }
}
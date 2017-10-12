using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HinhThucThanhToan : System.Web.UI.Page
    {
        dtHinhThucThanhToan data = new dtHinhThucThanhToan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid();
            }
        }
        public void LoadGrid()
        {
            data = new dtHinhThucThanhToan();
            gridHinhThucThanhToan.DataSource = data.LayDanhSachHinhThucThanhToan();
            gridHinhThucThanhToan.DataBind();
        }

        protected void gridHinhThucThanhToan_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtHinhThucThanhToan();
            data.XoaHinhThucThanhToan(Int32.Parse(ID));
            e.Cancel = true;
            gridHinhThucThanhToan.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hình Thức Thanh Toán", Session["IDKho"].ToString(), "Danh Mục", "Xóa");
        }

        protected void gridHinhThucThanhToan_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dtHinhThucThanhToan();
            string TenHinhThuc = e.NewValues["TenHinhThuc"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();

            data.ThemHinhThucThanhToan(TenHinhThuc, GhiChu);
            e.Cancel = true;
            gridHinhThucThanhToan.CancelEdit();
            LoadGrid();

           
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hình Thức Thanh Toán", Session["IDKho"].ToString(), "Danh Mục", "Thêm");
        }

        protected void gridHinhThucThanhToan_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

            data = new dtHinhThucThanhToan();
            string TenHinhThuc = e.NewValues["TenHinhThuc"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            int ID = int.Parse(e.Keys["ID"].ToString());
            data.SuaHinhThucThanhToan(ID, TenHinhThuc, GhiChu);
            e.Cancel = true;
            gridHinhThucThanhToan.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hình Thức Thanh Toán", Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");
        }
    }
}
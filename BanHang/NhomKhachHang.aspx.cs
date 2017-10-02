using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class NhomKhachHang : System.Web.UI.Page
    {
        dtNhomKhachHang data = new dtNhomKhachHang();
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
                //    gridNhomKhachHang.Columns["chucnang"].Visible = false;
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}
            }
        }
        public void LoadGrid()
        {
            data = new dtNhomKhachHang();
            gridNhomKhachHang.DataSource = data.LayDanhNhomKhachHang(Session["IDKho"].ToString());
            gridNhomKhachHang.DataBind();
        }

        protected void gridNhomKhachHang_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string TenNhomKhachHang = e.NewValues["TenNhomKhachHang"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            data = new dtNhomKhachHang();
            data.ThemNhomNhomHangMoi(TenNhomKhachHang, GhiChu, Session["IDKho"].ToString());
            e.Cancel = true;
            gridNhomKhachHang.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhóm Khách Hàng:" + TenNhomKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
        }

        protected void gridNhomKhachHang_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            string TenNhomKhachHang = e.NewValues["TenNhomKhachHang"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            data.SuaThongTinNhomKhachHang(Int32.Parse(ID), TenNhomKhachHang, GhiChu);
            e.Cancel = true;
            gridNhomKhachHang.CancelEdit();
            LoadGrid();


            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhóm Khách Hàng:" + TenNhomKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");
        }

        protected void gridNhomKhachHang_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtNhomKhachHang();
            data.XoaNhomKhachHang(Int32.Parse(ID));
            e.Cancel = true;
            gridNhomKhachHang.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhóm Khách Hàng:" + ID, Session["IDKho"].ToString(), "Danh Mục", "Xóa");   
        }
    }
}
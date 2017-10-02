using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhMucThue : System.Web.UI.Page
    {
        dtDanhMucThue data = new dtDanhMucThue();
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
                        gridDanhMucThue.Columns["chucnang"].Visible = false;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
        public void LoadGrid()
        {
            data = new dtDanhMucThue();
            gridDanhMucThue.DataSource = data.LayDanhSachDanhMucThue();
            gridDanhMucThue.DataBind();
        }

        protected void gridDanhMucThue_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            string TenThue = e.NewValues["TenThue"].ToString();
            string TiLe = e.NewValues["TiLe"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            data.SuaDanhMucThue(Int32.Parse(ID), TenThue, TiLe, GhiChu);
            e.Cancel = true;
            gridDanhMucThue.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh Mục Thuế:" + TenThue,Session["IDkho"].ToString(), "Danh Mục", "Cập Nhật"); 
        }

        protected void gridDanhMucThue_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string TenThue = e.NewValues["TenThue"].ToString();
            string TiLe = e.NewValues["TiLe"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            
            data = new dtDanhMucThue();
            data.ThemDanhMucThue(TenThue, TiLe, GhiChu);
            e.Cancel = true;
            gridDanhMucThue.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh Mục Thuế:" + TenThue, Session["IDkho"].ToString(), "Danh Mục", "Thêm"); 
        }

        protected void gridDanhMucThue_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtDanhMucThue();
            data.XoaDanhMucThue(Int32.Parse(ID));
            e.Cancel = true;
            gridDanhMucThue.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh Mục Thuế:" + ID, Session["IDkho"].ToString(), "Danh Mục", "Xóa"); 
        }
    }
}
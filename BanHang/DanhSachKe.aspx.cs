using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachKe : System.Web.UI.Page
    {
        dtKe data = new dtKe();

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
                    LoadGrid(Session["IDKho"].ToString());
                    //if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                    //{
                    //    gridKe.Columns["chucnang"].Visible = false;
                    //    btnThemHangHoaVaoKe.Enabled = false;
                    //}
                //}
                //else
                //    Response.Redirect("Default.aspx");
            }
        }

        private void LoadGrid(string p)
        {
            data = new dtKe();
            gridKe.DataSource = data.DanhSachKe(p);
            gridKe.DataBind();
        }
        

        protected void gridKe_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKe();
            data.XoaKe(ID);
            e.Cancel = true;
            gridKe.CancelEdit();
            LoadGrid(Session["IDKho"].ToString());
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh sách kệ", Session["IDKho"].ToString(), "Danh mục", "Xóa");
        }

        protected void gridKe_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        { 
            data = new dtKe();
            string TenKe = e.NewValues["TenKe"].ToString();
            string ViTri = e.NewValues["ViTri"].ToString();
            string MoTa = e.NewValues["MoTa"] == null ? "" : e.NewValues["MoTa"].ToString();
            data.ThemKe(TenKe, ViTri, MoTa, Session["IDKho"].ToString());
            e.Cancel = true;
            gridKe.CancelEdit();
            LoadGrid(Session["IDKho"].ToString());
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh sách kệ", Session["IDKho"].ToString(), "Danh mục", "Thêm");
        }

        protected void gridKe_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string TenKe = e.NewValues["TenKe"].ToString();
            string ViTri = e.NewValues["ViTri"].ToString();
            string MoTa = e.NewValues["MoTa"] == null ? "" : e.NewValues["MoTa"].ToString();
            data = new dtKe();
            data.CapNhatKe(ID, TenKe, ViTri, MoTa);
            e.Cancel = true;
            gridKe.CancelEdit();
            LoadGrid(Session["IDKho"].ToString());
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh sách kệ", Session["IDKho"].ToString(), "Danh mục", "Cập nhật");
        }
    }
}
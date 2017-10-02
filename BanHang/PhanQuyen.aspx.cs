using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class PhanQuyen : System.Web.UI.Page
    {
        dtPhanQuyen data = new dtPhanQuyen();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 36) == 1)
                //{
                    string IDNhomNguoiDung = Request.QueryString["IDNhomNguoiDung"];
                    if (IDNhomNguoiDung != null)
                    {
                        LoadGrid(Int32.Parse(IDNhomNguoiDung.ToString()));
                    }
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}
            }
        }

        private void LoadGrid(int IDNhomNguoiDung)
        {
            data = new dtPhanQuyen();
            gridPhanQuyen.DataSource = data.LayDanhSachMenu(IDNhomNguoiDung);
            gridPhanQuyen.DataBind();
        }

        protected void gridPhanQuyen_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            int TrangThai = Int32.Parse(e.NewValues["TrangThai"] == null ? "0" : e.NewValues["TrangThai"].ToString());
            int ChucNang = Int32.Parse(e.NewValues["ChucNang"] == null ? "0" : e.NewValues["ChucNang"].ToString());
            data = new dtPhanQuyen();
            data.CapNhatQuyen(Int32.Parse(Request.QueryString["IDNhomNguoiDung"]), ID, TrangThai, ChucNang);
            e.Cancel = true;
            gridPhanQuyen.CancelEdit();
            LoadGrid(Int32.Parse(Request.QueryString["IDNhomNguoiDung"]));

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phân Quyền:" + ID, Session["IDKho"].ToString(), "Hệ Thống", "Cập Nhật"); 
        }

        protected void gridPhanQuyen_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            string TrangThai = Convert.ToString(e.GetValue("Link"));
            if (TrangThai == "")
                e.Row.BackColor = color;
        }
    }
}
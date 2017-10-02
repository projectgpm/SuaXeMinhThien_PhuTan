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
    public partial class ChiTietDonHangXuLy1PhanThuMua : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHang = Request.QueryString["IDDonHang"];
                if (IDDonHang != null)
                {
                    LoadGrid(IDDonHang.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        dtDonHangHoanTatThuMua data = new dtDonHangHoanTatThuMua();
        private void LoadGrid(string IDDonHang)
        {

            data = new dtDonHangHoanTatThuMua();
            gridChiTiet.DataSource = data.DanhSachChiTietXuLy1Phan(IDDonHang);
            gridChiTiet.DataBind();
        }
        protected void gridChiTiet_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int SoLuong = Convert.ToInt32(e.GetValue("SoLuong"));
            if (SoLuong > 0)
                e.Row.BackColor = color;
        }
    }
}
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
    public partial class ChiTietDonHangChiNhanhXuLy1Phan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
                if (IDDonHangChiNhanh != null)
                {
                    LoadGrid(IDDonHangChiNhanh.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        dtChiTietDonHangChiNhanh data = new dtChiTietDonHangChiNhanh();
        private void LoadGrid(string IDDonHangChiNhanh)
        {

            data = new dtChiTietDonHangChiNhanh();
            gridChiTiet.DataSource = data.DanhSachChiTietXuLy1Phan(IDDonHangChiNhanh);
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
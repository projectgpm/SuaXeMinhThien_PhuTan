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
    public partial class TonKhoBanDau : System.Web.UI.Page
    {
        dtKhoHang data = new dtKhoHang();
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
        private void LoadGrid()
        {
            data = new dtKhoHang();
            gridTonKhoBanDau.DataSource = data.LayDanhSachHangTrongKho(Session["IDKho"].ToString());
            gridTonKhoBanDau.DataBind();
        }

        protected void gridTonKhoBanDau_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TonKhoTong = Convert.ToInt32(e.GetValue("SoLuongCon"));
            if (TonKhoTong < 2)
                e.Row.BackColor = color;
        }
    }
}
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
    public partial class ChiTietDonHangDuyetThuMua : System.Web.UI.Page
    {
        dtDuyetDonHangThuMua data = new dtDuyetDonHangThuMua();
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

        private void LoadGrid(string IDDonHangThuMua)
        {
            data = new dtDuyetDonHangThuMua();
            gridChiTiet.DataSource = data.DanhSachChiTiet_Duyet_ThuMua(IDDonHangThuMua);
            gridChiTiet.DataBind();
        }

        protected void gridChiTiet_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int ChenhLech = Convert.ToInt32(e.GetValue("ChenhLech"));
            if (ChenhLech > 0)
                e.Row.BackColor = color;
        }
    }
}
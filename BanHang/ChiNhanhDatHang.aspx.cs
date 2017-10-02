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
    public partial class ChiNhanhDatHang : System.Web.UI.Page
    {
        dtDonHangChiNhanh data = new dtDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }

            else
            {
                LoadGrid(Session["IDKho"].ToString());
            }
        }

        private void LoadGrid(string IDKho)
        {
            data = new dtDonHangChiNhanh();
            gridDonDatHang.DataSource = data.LayDanhSachDonHang(IDKho);
            gridDonDatHang.DataBind();
        }

        protected void gridDonDatHang_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int MucDoUuTien = Convert.ToInt32(e.GetValue("MucDoUuTien"));// lấy giá trị
            int TrangThai = Convert.ToInt32(e.GetValue("TrangThai"));
            if (MucDoUuTien == 1 && TrangThai ==0)
                e.Row.BackColor = color;
        }
    }
}
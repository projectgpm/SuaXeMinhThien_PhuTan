using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class LichSuKho : System.Web.UI.Page
    {
        dtLichSuKho dt = new dtLichSuKho();
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
            dt = new dtLichSuKho();
            gridLichSuKho.DataSource = dt.LayDanhSach();
            gridLichSuKho.DataBind();
        }
    }
}
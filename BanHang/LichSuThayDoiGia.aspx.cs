using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class LichSuThayDoiGia : System.Web.UI.Page
    {
        dtThayDoiGia data = new dtThayDoiGia();
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
            data = new dtThayDoiGia();
            gridLichSuThayDoiGia.DataSource = data.LayDanhSach();
            gridLichSuThayDoiGia.DataBind();
        }
    }
}
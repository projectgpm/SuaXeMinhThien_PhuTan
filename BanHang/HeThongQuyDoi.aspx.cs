using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HeThongQuyDoi : System.Web.UI.Page
    {
        dtHeThongQuyDoi data = new dtHeThongQuyDoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTBanLe"] == "GPMBanLe")
            {
                LoadDanhSach(Session["IDKho"].ToString());
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
           
        }
        public void LoadDanhSach(string IDKho)
        {
            data = new dtHeThongQuyDoi();
            gridHangHoa.DataBind();
        }
    }
}
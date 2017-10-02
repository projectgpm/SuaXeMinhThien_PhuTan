using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachGiaTheoGio : System.Web.UI.Page
    {
        dtGiaTheoGio data = new dtGiaTheoGio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                Loadgrid();
            }
        }

        private void Loadgrid()
        {
            data = new dtGiaTheoGio();
            gridHangHoa.DataSource = data.DanhSachHoanTat();
            gridHangHoa.DataBind();
        }
    }
}
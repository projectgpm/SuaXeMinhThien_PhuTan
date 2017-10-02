using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class keyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnKichHoat_Click(object sender, EventArgs e)
        {
            string TenDangNhap = txtNguoiKichHoat.Value;
            string key = txtKey.Value;
            if (dtSetting.setKeyCode(key, TenDangNhap) == 1)
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Key không tông tại.'); </script>");
            }
        }
    }
}
using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class InPhieuThanhToan3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Request.QueryString["ID"];

            rpPhieuThanhToan3 rp = new rpPhieuThanhToan3();
            rp.Parameters["ID"].Value = ID;
            rp.Parameters["ID"].Visible = false;
            reportView.Report = rp;
        }
    }
}
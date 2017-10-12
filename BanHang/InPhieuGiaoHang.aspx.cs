using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class InPhieuGiaoHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDHoaDon = Request.QueryString["IDHoaDon"];
            string KT = Request.QueryString["KT"];

            if (Int32.Parse(KT) == 1)
            {
                rpPhieuGiaoHang rp = new rpPhieuGiaoHang();
                rp.Parameters["ID"].Value = IDHoaDon;
                rp.Parameters["ID"].Visible = false;
                reportView.Report = rp;
            }
            else
            {
                rpPhieuGiaoHang1 rp = new rpPhieuGiaoHang1();
                rp.Parameters["ID"].Value = IDHoaDon;
                rp.Parameters["ID"].Visible = false;
                reportView.Report = rp;
            }
        }
    }
}
using BanHang.Data;
using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class TheKho_In : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NgayBD = Request.QueryString["ngayBD"];
            string NgayKT = Request.QueryString["NgayKT"];
            string MaHang = Request.QueryString["MaHang"];
            string IDKhoNhap = Request.QueryString["IDKhoNhap"];
            string strKhoNhap = "Tất cả các kho";

            dtKho dt1 = new dtKho();
            if (Int32.Parse(IDKhoNhap) != -1)
                strKhoNhap = dt1.LayTenKho_ID(IDKhoNhap);

            string strNgay = DateTime.Parse(NgayBD).ToString("dd-MM-yyyy") + " - " + DateTime.Parse(NgayKT).ToString("dd-MM-yyyy");

            rpTheKho rp = new rpTheKho();

            rp.Parameters["strNgay"].Value = strNgay;
            rp.Parameters["strNgay"].Visible = false;

            rp.Parameters["strKho"].Value = strKhoNhap;
            rp.Parameters["strKho"].Visible = false;

            rp.Parameters["NgayBD"].Value = NgayBD;
            rp.Parameters["NgayBD"].Visible = false;
            rp.Parameters["NgayKT"].Value = NgayKT;
            rp.Parameters["NgayKT"].Visible = false;

            rp.Parameters["MaHang"].Value = MaHang;
            rp.Parameters["MaHang"].Visible = false;

            rp.Parameters["IDKho"].Value = IDKhoNhap;
            rp.Parameters["IDKho"].Visible = false;
            viewerReport.Report = rp;
        }
    }
}
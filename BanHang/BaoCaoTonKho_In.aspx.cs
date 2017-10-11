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
    public partial class BaoCaoTonKho_In : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDHH = Request.QueryString["IDHH"];
            string IDNH = Request.QueryString["IDNH"];

            string strNhomHang = "Tất cả nhóm hàng";
            string strHangHoa = "Tất cả hàng hóa";

            dataNhomHang dt3 = new dataNhomHang();
            if (Int32.Parse(IDNH) != -1)
                strNhomHang = dt3.LayTenNhomHang_ID(IDNH);

            dataHangHoa dt4 = new dataHangHoa();
            if (Int32.Parse(IDHH) != -1)
                strHangHoa = dt4.LayTenHangHoa(IDHH);

            rpBaoCaoTonKho rp = new rpBaoCaoTonKho();
            rp.Parameters["IDHH"].Value = IDHH;
            rp.Parameters["IDHH"].Visible = false;
            rp.Parameters["IDNH"].Value = IDNH;
            rp.Parameters["IDNH"].Visible = false;

            rp.Parameters["strNhomHang"].Value = strNhomHang;
            rp.Parameters["strNhomHang"].Visible = false;
            rp.Parameters["strHangHoa"].Value = strHangHoa;
            rp.Parameters["strHangHoa"].Visible = false;
            viewerReport.Report = rp;
        }
    }
}
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
    public partial class BaoCaoBanHang_In : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
            //    //if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
            //    //{
            //    //    btnNhapExcel.Enabled = false;
            //    //    gridKhachHang.Columns["chucnang"].Visible = false;
            //    //}
            //    if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
            //    {
                    string NgayBD = Request.QueryString["NgayBD"];
                    string NgayKT = Request.QueryString["NgayKT"];

                    string IDKho = Request.QueryString["IDKho"];
                    string IDHH = Request.QueryString["IDHH"];
                    string IDNH = Request.QueryString["IDNH"];
                    string IDNganhH = Request.QueryString["IDNganhH"];
                    string strKho = "Tất cả các kho";
                    string strNganhHang = "Tất cả ngành hàng";
                    string strNhomHang = "Tất cả nhóm hàng";
                    string strHangHoa = "Tất cả hàng hóa";

                    dtKho dt1 = new dtKho();
                    if (Int32.Parse(IDKho) != -1)
                        strKho = dt1.LayTenKho_ID(IDKho);

                    dataNganhHang dt2 = new dataNganhHang();
                    if (Int32.Parse(IDNganhH) != -1)
                        strNganhHang = dt2.LayTenNganhHang_ID(IDNganhH);

                    dataNhomHang dt3 = new dataNhomHang();
                    if (Int32.Parse(IDNH) != -1)
                        strNhomHang = dt3.LayTenNhomHang_ID(IDNH);

                    dataHangHoa dt4 = new dataHangHoa();
                    if (Int32.Parse(IDHH) != -1)
                        strHangHoa = dt4.LayTenHangHoa_ID(IDHH);

                    string strNgay = DateTime.Parse(NgayBD).ToString("dd-MM-yyyy") + " - " + DateTime.Parse(NgayKT).ToString("dd-MM-yyyy");

                    rpBaoCaoBanHang rp = new rpBaoCaoBanHang();

                    rp.Parameters["NgayBD"].Value = NgayBD;
                    rp.Parameters["NgayBD"].Visible = false;
                    rp.Parameters["NgayKT"].Value = NgayKT;
                    rp.Parameters["NgayKT"].Visible = false;
                    rp.Parameters["strNgay"].Value = strNgay;
                    rp.Parameters["strNgay"].Visible = false;

                    rp.Parameters["IDKho"].Value = IDKho;
                    rp.Parameters["IDKho"].Visible = false;
                    rp.Parameters["IDHH"].Value = IDHH;
                    rp.Parameters["IDHH"].Visible = false;
                    rp.Parameters["IDNH"].Value = IDNH;
                    rp.Parameters["IDNH"].Visible = false;

                    rp.Parameters["IDNganhH"].Value = IDNganhH;
                    rp.Parameters["IDNganhH"].Visible = false;

                    rp.Parameters["strKho"].Value = strKho;
                    rp.Parameters["strKho"].Visible = false;
                    rp.Parameters["strNganhHang"].Value = strNganhHang;
                    rp.Parameters["strNganhHang"].Visible = false;
                    rp.Parameters["strNhomHang"].Value = strNhomHang;
                    rp.Parameters["strNhomHang"].Visible = false;
                    rp.Parameters["strHangHoa"].Value = strHangHoa;
                    rp.Parameters["strHangHoa"].Visible = false;
                    viewerReport.Report = rp;
            //    }
            //    else
            //    {
            //        Response.Redirect("Default.aspx");
            //    }
            }
        }
    }
}
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
    public partial class BaoCaoNhapHang_In : System.Web.UI.Page
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
                    string NgayBD = Request.QueryString["ngayBD"];
                    string NgayKT = Request.QueryString["NgayKT"];
                    string IDNhaCC = Request.QueryString["IDNhaCC"];
                    string IDKhoNhap = Request.QueryString["IDKhoNhap"];
                    string strNhaCC = "Tất cả nhà cung cấp";
                    string strKhoNhap = "Tất cả các kho";

                    dtNhaCungCap dt = new dtNhaCungCap();
                    if (Int32.Parse(IDNhaCC) != -1)
                        strNhaCC = dt.LayTenNhaCC_ID(IDNhaCC);

                    dtKho dt1 = new dtKho();
                    if (Int32.Parse(IDKhoNhap) != -1)
                        strKhoNhap = dt1.LayTenKho_ID(IDKhoNhap);

                    string strNgay = DateTime.Parse(NgayBD).ToString("dd-MM-yyyy") + " - " + DateTime.Parse(NgayKT).ToString("dd-MM-yyyy");

                    rpBaoCaoNhapHang rp = new rpBaoCaoNhapHang();

                    rp.Parameters["strNgay"].Value = strNgay;
                    rp.Parameters["strNgay"].Visible = false;

                    rp.Parameters["strNhaCC"].Value = strNhaCC;
                    rp.Parameters["strNhaCC"].Visible = false;

                    rp.Parameters["strKhoNhap"].Value = strKhoNhap;
                    rp.Parameters["strKhoNhap"].Visible = false;

                    rp.Parameters["NgayBD"].Value = NgayBD;
                    rp.Parameters["NgayBD"].Visible = false;
                    rp.Parameters["NgayKT"].Value = NgayKT;
                    rp.Parameters["NgayKT"].Visible = false;

                    rp.Parameters["IDNhaCC"].Value = IDNhaCC;
                    rp.Parameters["IDNhaCC"].Visible = false;

                    rp.Parameters["IDKhoNhap"].Value = IDKhoNhap;
                    rp.Parameters["IDKhoNhap"].Visible = false;
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
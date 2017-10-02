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
    public partial class BaoCaoChuyenKho_In : System.Web.UI.Page
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
                    string IDKhoXuat = Request.QueryString["IDKhoXuat"];
                    string IDKhoNhap = Request.QueryString["IDKhoNhap"];
                    string strKhoXuat = "Tất cả các kho";
                    string strKhoNhap = "Tất cả các kho";

                    dtKho dt = new dtKho();
                    if (Int32.Parse(IDKhoXuat) != -1)
                        strKhoXuat = dt.LayTenKho_ID(IDKhoXuat);
                    if (Int32.Parse(IDKhoNhap) != -1)
                        strKhoNhap = dt.LayTenKho_ID(IDKhoNhap);

                    string strNgay = DateTime.Parse(NgayBD).ToString("dd-MM-yyyy") + " - " + DateTime.Parse(NgayKT).ToString("dd-MM-yyyy");
                    rpBaoCaoChuyenKho rp = new rpBaoCaoChuyenKho();
                    rp.Parameters["strNgay"].Value = strNgay;
                    rp.Parameters["strNgay"].Visible = false;
                    rp.Parameters["strKhoXuat"].Value = strKhoXuat;
                    rp.Parameters["strKhoXuat"].Visible = false;
                    rp.Parameters["strKhoNhap"].Value = strKhoNhap;
                    rp.Parameters["strKhoNhap"].Visible = false;

                    rp.Parameters["NgayBD"].Value = NgayBD;
                    rp.Parameters["NgayBD"].Visible = false;
                    rp.Parameters["NgayKT"].Value = NgayKT;
                    rp.Parameters["NgayKT"].Visible = false;
                    rp.Parameters["IDKhoXuat"].Value = IDKhoXuat;
                    rp.Parameters["IDKhoXuat"].Visible = false;
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
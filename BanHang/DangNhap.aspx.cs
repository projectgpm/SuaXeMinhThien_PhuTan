using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DangNhap : System.Web.UI.Page
    {
        dtCheckDangNhap data = new dtCheckDangNhap();
        protected void Page_Load(object sender, EventArgs e)
        {
           // txtDangNhap.Focus();
            if (dtSetting.getKeyCode() == -1)
            {
                Response.Redirect("keyCode.aspx");
            }
        }
        protected void btnDangNhapQuanLy_Click(object sender, EventArgs e)
        {
            if (KiemTra() == true)
            {
                data = new dtCheckDangNhap();
                string TenDangNhap = txtDangNhap.Value.ToUpper();
                string MatKhau = dtSetting.GetSHA1HashData(txtMatKhau.Value.ToString());
                DataTable dt = data.KiemTraQuanLy(TenDangNhap,MatKhau);
                if(dt.Rows.Count != 0)
                {
                    DataRow dr = dt.Rows[0];
                    Session["TenDangNhap"] = dr["TenNguoiDung"].ToString();
                    Session["UserName"] = txtDangNhap.Value.ToUpper();
                    Session["KTDangNhap"] = "GPM";
                    Session["IDNhanVien"] = dr["ID"].ToString();
                    Session["IDNhom"] = dr["IDNhomNguoiDung"].ToString();
                    Session["IDKho"] = dr["IDKho"].ToString();
                    Session["KTBanLe"] = "GPMBanLe";
                    Session["TenThuNgan"] = dr["TenNguoiDung"].ToString();
                    Session["IDThuNgan"] = dr["ID"].ToString();
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đăng Nhập", Session["IDKho"].ToString(), "Đăng Nhập", "Đăng Nhập");
                    Response.Redirect("TonKhoBanDau.aspx");
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Đăng Nhập Không Thành Công.'); </script>");
                    //Response.Redirect("DangNhap.aspx");
                    txtDangNhap.Value = "";
                    txtMatKhau.Value = "";
                }
            }
        }
        protected void btnDangNhapBanHang_Click(object sender, EventArgs e)
        {
            if (KiemTra() == true)
            {
                data = new dtCheckDangNhap();
                string TenDangNhap = txtDangNhap.Value.ToString();
                string MatKhau = dtSetting.GetSHA1HashData(txtMatKhau.Value.ToString());
                DataTable dt = data.KiemTraBanHang(TenDangNhap, MatKhau);
                if (dt.Rows.Count != 0)
                {
                    DataRow dr = dt.Rows[0];
                    Session["TenThuNgan"] = dr["TenNguoiDung"].ToString();
                    Session["IDThuNgan"] = dr["ID"].ToString();
                    Session["IDNhom"] = dr["IDNhomNguoiDung"].ToString();
                    Session["IDKho"] = dr["IDKho"].ToString();
                    Session["KTBanLe"] = "GPMBanLe";
                    Response.Redirect("BanHangLe.aspx");
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Đăng Nhập Không Thành Công.'); </script>");
                    txtDangNhap.Value = "";
                    txtMatKhau.Value = "";
                }
            }
        }
        public bool KiemTra()
        {
            if (txtDangNhap.Value.ToString() == "" || txtMatKhau.Value.ToString() == "")
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng điền đầy đủ thông tin đăng nhập.'); </script>");
                return false;
            }
            return true;
        }
    }
}
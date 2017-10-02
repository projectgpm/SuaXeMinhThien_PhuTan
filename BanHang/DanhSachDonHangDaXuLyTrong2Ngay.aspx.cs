using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachDonHangDaXuLyTrong2Ngay : System.Web.UI.Page
    {
        dtCapNhatPhieuNhapHang data = new dtCapNhatPhieuNhapHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 64) == false)
                    Response.Redirect("Default.aspx");
                LoadGrid();
            }

        }
        private void LoadGrid()
        {
            data = new dtCapNhatPhieuNhapHang();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangDuyetTrong2Ngay(DateTime.Now,dtSetting.LaySoNgayDuocSuaDonHangDaXuLy());
            gridDonDatHang.DataBind();
        }
    }
}
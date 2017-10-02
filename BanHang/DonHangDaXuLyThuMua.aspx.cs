using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangDaXuLyThuMua : System.Web.UI.Page
    {
        dtDonHangHoanTatThuMua data = new dtDonHangHoanTatThuMua();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid();
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 66) == false)
                    btnTaoDonHang.Enabled = false;
            }
        }
        private void LoadGrid()
        {
            data = new dtDonHangHoanTatThuMua();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangDuyet();
            gridDonDatHang.DataBind();
        }
    }
}
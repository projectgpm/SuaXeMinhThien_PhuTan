using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachDonHangThuMuaHuy : System.Web.UI.Page
    {
        dtThuMuaDatHang data = new dtThuMuaDatHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid(Session["IDKho"].ToString());
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 66) == false)
                    btnTaoDonHang.Enabled = false;
            }
        }
        private void LoadGrid(string IDKho)
        {
            data = new dtThuMuaDatHang();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangDaHuy(IDKho);
            gridDonDatHang.DataBind();
        }
    }
}
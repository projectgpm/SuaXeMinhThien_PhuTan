using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangXuLy1PhanThuMua : System.Web.UI.Page
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
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 64) == false)
                {
                    btnDuyetDonHang.Enabled = false;
                    btncapnhatdonhang.Enabled = false;
                }
                LoadGrid();
            }
        }
        private void LoadGrid()
        {
            data = new dtThuMuaDatHang();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangXuLy1Phan();
            gridDonDatHang.DataBind();
        }
      

       
    }
}
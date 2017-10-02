using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangDaXuLy : System.Web.UI.Page
    {
        dtDuyetDonHangThuMua data = new dtDuyetDonHangThuMua();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }

            else
            {
               
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            data = new dtDuyetDonHangThuMua();
            gridDonDatHang.DataSource = data.DanhSachDuyet_ThuMua();
            gridDonDatHang.DataBind();
        }
    }
}
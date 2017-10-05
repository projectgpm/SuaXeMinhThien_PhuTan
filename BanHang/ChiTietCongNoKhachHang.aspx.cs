using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietCongNoKhachHang : System.Web.UI.Page
    {
        dtKhachHang data = new dtKhachHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtKhachHang();
            gridDanhSach.DataSource = data.DanhSachChiTietCongNo();
            gridDanhSach.DataBind();
        }
    }
}
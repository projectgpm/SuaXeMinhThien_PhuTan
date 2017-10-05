using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietThanhToanChietKhau : System.Web.UI.Page
    {
        dtThanhToanChietKhau data = new dtThanhToanChietKhau();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtThanhToanChietKhau();
            gridDanhSach.DataSource = data.DanhSachChiTietChietKhau();
            gridDanhSach.DataBind();
        }
    }
}
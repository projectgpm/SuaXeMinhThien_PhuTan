using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class CongNoKyThuat : System.Web.UI.Page
    {
        dtNhanVienKyThuat data = new dtNhanVienKyThuat();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtNhanVienKyThuat();
            gridDanhSach.DataSource = data.DanhSachChiTietCongNo();
            gridDanhSach.DataBind();
        }
    }
}
using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachPhieuDatHang : System.Web.UI.Page
    {
        dtDanhSachNhapHang data = new dtDanhSachNhapHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            data = new dtDanhSachNhapHang();
            gridDonDatHang.DataSource = data.LayDanhSachDonHang();
            gridDonDatHang.DataBind();

           // dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Danh sách phiếu nhập hàng", "Truy cập");
        }
    }
}
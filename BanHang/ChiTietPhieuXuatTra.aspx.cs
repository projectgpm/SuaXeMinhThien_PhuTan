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
    public partial class ChiTietPhieuXuatTra : System.Web.UI.Page
    {
        dtPhieuXuatTra data = new dtPhieuXuatTra();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDPhieuXuatTra = Request.QueryString["IDPhieuXuatTra"];
                if (IDPhieuXuatTra != null)
                {

                    LoadGrid(IDPhieuXuatTra.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        private void LoadGrid(string IDPhieuXuatTra)
        {
            data = new dtPhieuXuatTra();
            gridChiTietPhieuXuatTra.DataSource = data.DanhSachChiTietPhieuXuatTra_ID(IDPhieuXuatTra);
            gridChiTietPhieuXuatTra.DataBind();
        }
    }
}
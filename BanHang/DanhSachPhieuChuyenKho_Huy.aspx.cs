using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachPhieuChuyenKho_Huy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid();
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 71) == false)
                    Response.Redirect("Default.aspx");
            }
        }

        private void LoadGrid()
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            if (Session["IDKho"].ToString().CompareTo("1") == 0)
                gridPhieuChuyenKho.DataSource = data.DanhSachPhieuChuyenKho_Tong_Huy();
            else gridPhieuChuyenKho.DataSource = data.DanhSachPhieuChuyenKho_Client_Huy(Session["IDKho"].ToString());
            gridPhieuChuyenKho.DataBind();
        }
    }
}
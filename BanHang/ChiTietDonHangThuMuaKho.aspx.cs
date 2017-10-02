using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietDonHangThuMuaKho : System.Web.UI.Page
    {
        dtThuMuaDatHang data = new dtThuMuaDatHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
                if (IDDonHangThuMua != null)
                {
                    if (dtThuMuaDatHang.LayTrangThaiDonHang(IDDonHangThuMua) == 1 || dtThuMuaDatHang.DonHangHuy(IDDonHangThuMua) == 1)
                    {
                    
                        gridChiTiet.Columns["chucnang"].Visible = false;
                    }
                    LoadGrid(IDDonHangThuMua.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDDonHangThuMua)
        {
            data = new dtThuMuaDatHang();
            gridChiTiet.DataSource = data.DanhSachChiTiet(IDDonHangThuMua);
            gridChiTiet.DataBind();
        }
    }
}
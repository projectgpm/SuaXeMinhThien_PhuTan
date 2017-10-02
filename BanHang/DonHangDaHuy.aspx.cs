using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangDaHuy : System.Web.UI.Page
    {
        dtDonHangHuy data = new dtDonHangHuy();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 67) == false)
                {
                    btnDuyetDonHang.Enabled = false;
                }
                int IDKho = Int32.Parse(Session["IDKho"].ToString());
                if (IDKho != 1)
                {
                    gridDonDatHang.Columns["ChungTu"].Visible = false;
                    btnDuyetDonHang.Visible = false;
                    btnDonhangXuLy1Phan.Visible = false;
                    btnTaoDonHang.Visible = true;
                }
                else
                {
                    gridDonDatHang.Columns["ChungTu"].Visible = true;
                    btnDuyetDonHang.Visible = true;
                    btnTaoDonHang.Visible = false;
                    btnDonhangXuLy1Phan.Visible = true;
                }
                LoadGrid(Session["IDKho"].ToString());
            }

        }
        private void LoadGrid(string p)
        {
            data = new dtDonHangHuy();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangDuyet(p);
            gridDonDatHang.DataBind();
        }
    }
}
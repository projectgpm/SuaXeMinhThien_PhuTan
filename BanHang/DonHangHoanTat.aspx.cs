using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangHoanTat : System.Web.UI.Page
    {
        dtDonHangHoanTat data = new dtDonHangHoanTat();
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
                    btnTaoDonHang.Visible = true;
                    btnDonhangXuLy1Phan.Visible = false;
                }
                else
                {
                    gridDonDatHang.Columns["ChungTu"].Visible = true;
                    btnDuyetDonHang.Visible = true;
                    btnTaoDonHang.Visible = false;
                    btnDonhangXuLy1Phan.Visible = true;
                }
                LoadGrid(IDKho.ToString());
            }
        }
        private void LoadGrid(string p)
        {
            data = new dtDonHangHoanTat();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangDuyet(p);
            gridDonDatHang.DataBind();
        }
    }
}
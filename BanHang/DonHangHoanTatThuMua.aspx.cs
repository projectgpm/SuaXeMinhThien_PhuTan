using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangHoanTatThuMua : System.Web.UI.Page
    {
        dtDonHangHoanTatThuMua data = new dtDonHangHoanTatThuMua();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 64) == false)
                {
                    btnDuyetDonHang.Enabled = false;
                    btncapnhatdonhang.Enabled = false;
                }
                int IDKho = Int32.Parse(Session["IDKho"].ToString());
                if (IDKho != 1)
                {
                    // kiểm tra lại. thu mua k xem chứng từ của kho
                    gridDonDatHang.Columns["ChungTu"].Visible = false;
                    btnDuyetDonHang.Visible = false;
                }
                else
                {
                    gridDonDatHang.Columns["ChungTu"].Visible = true;
                    btnDuyetDonHang.Visible = true;
                }
                LoadGrid();
            }
        }
        private void LoadGrid()
        {
            data = new dtDonHangHoanTatThuMua();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangDuyet();
            gridDonDatHang.DataBind();
        }
    }
}
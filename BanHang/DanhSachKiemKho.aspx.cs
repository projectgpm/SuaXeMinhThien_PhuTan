using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachKiemKho : System.Web.UI.Page
    {
        dtKiemKho data = new dtKiemKho();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    LoadGrid();
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                        btnKiemKho.Enabled = false;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }

        private void LoadGrid()
        {
            data = new dtKiemKho();
            gridDanhSachKiemKho.DataSource = data.DanhSachKiemKho(Session["IDKho"].ToString());
            gridDanhSachKiemKho.DataBind();
        }
    }
}
using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachDonHangThuMua : System.Web.UI.Page
    {
        dtThuMuaDatHang data = new dtThuMuaDatHang();
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
                    LoadGrid(Session["IDKho"].ToString());
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                        btnTaoDonHang.Enabled = false;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }

        private void LoadGrid(string IDKho)
        {
            data = new dtThuMuaDatHang();
            gridDonDatHang.DataSource = data.LayDanhSachDonHang(IDKho);
            gridDonDatHang.DataBind();
        }
    }
}
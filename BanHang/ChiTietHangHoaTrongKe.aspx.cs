using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietHangHoaTrongKe : System.Web.UI.Page
    {
        dtKe data = new dtKe();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["KTDangNhap"] == "GPM")
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 53) == false)
                    gridChiTietHangHoa.Columns["chucnang"].Visible = false;
                string IDKe = Request.QueryString["IDKe"];
                if (IDKe != null)
                {
                    LoadGrid(IDKe.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        private void LoadGrid(string p)
        {
            data = new dtKe();
            gridChiTietHangHoa.DataSource = data.DanhSachChiTietKe(p);
            gridChiTietHangHoa.DataBind();
        }

        protected void gridChiTietHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKe();
            data.XoaChiTietKe(ID);
            e.Cancel = true;
            gridChiTietHangHoa.CancelEdit();
            LoadGrid(Request.QueryString["IDKe"].ToString());

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết hàng trong kệ", Session["IDKho"].ToString(), "Nhập xuất tồn", "Xóa");
        }
    }
}
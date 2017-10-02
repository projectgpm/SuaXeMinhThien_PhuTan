using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ThemQuyen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Random ran = new Random();
                    IDDonDatHang_Temp.Value = ran.Next(100000, 999999).ToString();
                }
                LoadGrid(IDDonDatHang_Temp.Value.ToString());
            }
        }

        private void LoadGrid(string p)
        {
            throw new NotImplementedException();
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {

        }

        protected void btnThem_Click(object sender, EventArgs e)
        {

        }

        protected void gridDanhSachHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void gridDanhSachHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void btnThem_temp_Click(object sender, EventArgs e)
        {

        }
    }
}
using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class GiaTheoGio : System.Web.UI.Page
    {
        dtGiaTheoGio data = new dtGiaTheoGio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 61) == false)
                    Response.Redirect("Default.aspx");
                Loadgrid();
            }
        }

        private void Loadgrid()
        {
            data = new dtGiaTheoGio();
            gridHangHoa.DataSource = data.DanhSach();
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtGiaTheoGio();
            data.Xoa(ID);
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            Loadgrid();
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            if (e.NewValues["GiaBan"] != null && e.NewValues["GiaBan1"] != null && e.NewValues["GiaBan2"] != null && e.NewValues["GiaBan3"] != null && e.NewValues["GiaBan4"] != null && e.NewValues["GiaBan5"] != null)
            {
                string GiaBan0 = e.NewValues["GiaBan"].ToString();
                string GiaBan1 = e.NewValues["GiaBan1"].ToString();
                string GiaBan2 = e.NewValues["GiaBan2"].ToString();
                string GiaBan3 = e.NewValues["GiaBan3"].ToString();
                string GiaBan4 = e.NewValues["GiaBan4"].ToString();
                string GiaBan5 = e.NewValues["GiaBan5"].ToString();
                data = new dtGiaTheoGio();
                data.CapNhatChiTiet(ID, GiaBan0, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5);
            }
            else
            {
                throw new Exception("Lỗi: Giá không được bỏ trống? Vui lòng kiểm tra lại.");
            }
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            Loadgrid();
        }
    }
}
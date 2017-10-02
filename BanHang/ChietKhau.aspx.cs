using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChietKhau : System.Web.UI.Page
    {
        dtChietKhau data = new dtChietKhau();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtChietKhau();
            gridDanhSach.DataSource = data.DanhSach();
            gridDanhSach.DataBind();
        }

        protected void gridDanhSach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string TenChietKhau = e.NewValues["TenChietKhau"].ToString();
            string TyLe = e.NewValues["TyLe"].ToString();
            data = new dtChietKhau();
            data.CapNhat(ID, TenChietKhau, TyLe);
            e.Cancel = true;
           
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhSach_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string TenChietKhau = e.NewValues["TenChietKhau"].ToString();
            string TyLe = e.NewValues["TyLe"].ToString();
            data = new dtChietKhau();
            e.Cancel = true;
            data.Them(TenChietKhau, TyLe);
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhSach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtChietKhau();
            data.Xoa(ID);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }
    }
}
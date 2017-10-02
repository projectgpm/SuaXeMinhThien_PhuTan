using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class NhanVienKyThuat : System.Web.UI.Page
    {
        dtNhanVienKyThuat data = new dtNhanVienKyThuat();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtNhanVienKyThuat();
            gridDanhSach.DataSource = data.DanhSach();
            gridDanhSach.DataBind();
        }

        protected void gridDanhSach_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string TenKyThuat = e.NewValues["TenKyThuat"].ToString();
            string IDChietKhau = e.NewValues["IDChietKhau"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            data = new dtNhanVienKyThuat();
            data.Them(TenKyThuat, IDChietKhau, DiaChi, DienThoai, GhiChu);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhSach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string TenKyThuat = e.NewValues["TenKyThuat"].ToString();
            string IDChietKhau = e.NewValues["IDChietKhau"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            data = new dtNhanVienKyThuat();
            data.CapNhat(ID, TenKyThuat, IDChietKhau, DiaChi, DienThoai, GhiChu);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhSach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtNhanVienKyThuat();
            data.Xoa(ID);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

    }
}
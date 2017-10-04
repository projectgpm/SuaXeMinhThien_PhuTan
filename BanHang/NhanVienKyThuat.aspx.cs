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

        protected void cmbKyThuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKyThuat.Text != "")
            {
                txtTienThanhToan.Enabled = true;
                txtNoHienTai.Text = dtNhanVienKyThuat.LayTongTienKyThuat(cmbKyThuat.Value.ToString()).ToString() ;
            }
        }

        protected void dateNgayThanhToan_Init(object sender, EventArgs e)
        {
            dateNgayThanhToan.Date = DateTime.Now;
        }

        protected void btnCapNhatThanhToan_Click(object sender, EventArgs e)
        {
            if (cmbKyThuat.Text != ""  && txtTienThanhToan.Text != "")
            {
                string IDKyThuat = cmbKyThuat.Value.ToString();
                double SoTienThanhToan = double.Parse(txtTienThanhToan.Text);
                string NoiDung = txtNoiDung.Text == null ? "" : txtNoiDung.Text;
                DateTime NgayThanhToan = DateTime.Parse(dateNgayThanhToan.Text);
                if (double.Parse(txtNoHienTai.Text.ToString()) < double.Parse(txtTienThanhToan.Text.ToString()))
                {
                    txtTienThanhToan.Text = "";
                    txtTienThanhToan.Focus();
                    Response.Write("<script language='JavaScript'> alert('Số tiền trả vượt quá số tiền nợ.'); </script>");
                }
                else
                {
                    data = new dtNhanVienKyThuat();
                    object ID = data.ThemChiTietCongNo(IDKyThuat, SoTienThanhToan, NoiDung, NgayThanhToan);
                    if (ID != null)
                    {
                        data.CapNhatCongNo(IDKyThuat, SoTienThanhToan);
                        Response.Redirect("CongNoKyThuat.aspx");
                    }
                }
            }
            else
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập đủ thông tin.'); </script>");
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            cmbKyThuat.Text = "";
            txtNoHienTai.Text = "";
            txtTienThanhToan.Text = "";
            txtNoiDung.Text = "";
            txtTienThanhToan.Enabled = false;
            popup.ShowOnPageLoad = false;
        }

        protected void btnCapNhatTien_Click(object sender, EventArgs e)
        {
            cmbKyThuat.Text = "";
            txtNoHienTai.Text = "";
            txtTienThanhToan.Text = "";
            txtNoiDung.Text = "";
            txtTienThanhToan.Enabled = false;
            popup.ShowOnPageLoad = true;
        }

    }
}
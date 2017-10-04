using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietCongNo : System.Web.UI.Page
    {
        dtCongNo data = new dtCongNo();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtCongNo();
            gridDanhSach.DataSource = data.DanhSachChiTietCongNo();
            gridDanhSach.DataBind();
        }

        protected void ASPxFormLayout1_E2_Click(object sender, EventArgs e)
        {
            popup.ShowOnPageLoad = true;

        }
        protected void dateNgayThanhToan_Init(object sender, EventArgs e)
        {
            dateNgayThanhToan.Date = DateTime.Today;
        }

        protected void cmbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "")
            {
                cmbHinhThucThanhToan.Enabled = true;
                txtMaPhieu.Text = "";
                txtTienThanhToan.Text = "";
                txtNhapSoHoaDon.Enabled = true;
                MaPhieu();
                txtNoHienTai.Text = dtNhaCungCap.LayCongNo_IDNCC(cmbNhaCungCap.Value.ToString()) + "";
            }
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            cmbNhaCungCap.Text = "";
            txtNoHienTai.Text = "";
            cmbHinhThucThanhToan.Text = "";
            txtMaPhieu.Text = "";
            txtTienThanhToan.Text = "";
            txtNhapSoHoaDon.Text = "";
            txtNoiDung.Text = "";
            cmbHinhThucThanhToan.Enabled = false;
            txtMaPhieu.Enabled = false;
            txtTienThanhToan.Enabled = false;
            txtNhapSoHoaDon.Enabled = false;
            popup.ShowOnPageLoad = false;
        }
        protected void cmbHinhThucThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHinhThucThanhToan.Text != "")
            {
                int KT = Int32.Parse(cmbHinhThucThanhToan.Value.ToString());
                if (KT == 0)
                {
                    txtMaPhieu.Enabled = false;
                    txtMaPhieu.Text = "";
                    txtTienThanhToan.Enabled = true;
                    txtTienThanhToan.Text = "";
                }
                if (KT == 1)
                {
                    txtMaPhieu.Enabled = true;
                    txtTienThanhToan.Enabled = false;
                    MaPhieu();
                }
            }
        }
        protected void txtMaPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text != "")
            {
                MaPhieu();
                txtTienThanhToan.Text = dtNhaCungCap.LayTienThanhToan_IDPhieuNhapKho(txtMaPhieu.Value.ToString()) + "";
            }
        }
        public void MaPhieu()
        {
            dtNhaCungCap data1 = new dtNhaCungCap();
            txtMaPhieu.DataSource = data1.DanhSachSoDonHang(cmbNhaCungCap.Value.ToString());
            txtMaPhieu.TextField = "SoDonHang";
            txtMaPhieu.ValueField = "ID";
            txtMaPhieu.DataBind();
        }
        protected void btnCapNhatThanhToan_Click(object sender, EventArgs e)
        {
            dtCongNo dt = new dtCongNo();
            if (cmbNhaCungCap.Text != "" && cmbHinhThucThanhToan.Text != "" && txtTienThanhToan.Text != "")
            {
                int KT = Int32.Parse(cmbHinhThucThanhToan.Value.ToString());
                string IDNhaCungCap = cmbNhaCungCap.Value.ToString();
                string HinhThucThanhToan = cmbHinhThucThanhToan.Text.ToString();
                string SoHoaDon = txtNhapSoHoaDon.Text == null ? "" : txtNhapSoHoaDon.Text;

                float SoTienThanhToan = float.Parse(txtTienThanhToan.Text);
                string NoiDung = txtNoiDung.Text == null ? "" : txtNoiDung.Text;
                DateTime NgayThanhToan = DateTime.Parse(dateNgayThanhToan.Text);

                if (KT == 0)
                {
                    if (double.Parse(txtNoHienTai.Text.ToString()) < double.Parse(txtTienThanhToan.Text.ToString()))
                    {
                        txtTienThanhToan.Text = "";
                        txtTienThanhToan.Focus();
                        Response.Write("<script language='JavaScript'> alert('Số tiền trả vượt quá số tiền nợ.'); </script>");
                    }
                    else
                    {
                        object ID = dt.ThemChiTietCongNo(SoHoaDon, IDNhaCungCap, HinhThucThanhToan, "", SoTienThanhToan, NoiDung, NgayThanhToan);
                        if (ID != null)
                        {
                            dt.CapNhatCongNo(IDNhaCungCap, SoTienThanhToan);
                            data = new dtCongNo();
                            DataTable db = data.DanhSachSoDonHang(cmbNhaCungCap.Value.ToString());
                            if (db.Rows.Count != 0)
                            {
                                foreach (DataRow dr in db.Rows)
                                {
                                    float TienMaPhieu = float.Parse(dr["TongTien"].ToString());
                                    string IDDonHang = dr["ID"].ToString();
                                    if (SoTienThanhToan > TienMaPhieu)
                                    {
                                        dt = new dtCongNo();
                                        dt.CapNhatTinhTrang(IDDonHang);
                                        SoTienThanhToan = SoTienThanhToan - TienMaPhieu;
                                    }
                                    else if (SoTienThanhToan > 0)
                                    {
                                        dt = new dtCongNo();
                                        dt.CapNhatTinhTrang(IDDonHang);
                                        SoTienThanhToan = 0;
                                        Response.Redirect("ChiTietCongNo.aspx");
                                    }
                                }
                            }
                        }
                    }
                }
                if (KT == 1)
                {
                    string IDMaPhieu = txtMaPhieu.Value.ToString();
                    object ID = dt.ThemChiTietCongNo(SoHoaDon, IDNhaCungCap, HinhThucThanhToan, IDMaPhieu, SoTienThanhToan, NoiDung, NgayThanhToan);
                    if (ID != null)
                    {
                        dt.CapNhatCongNo(IDNhaCungCap, SoTienThanhToan);
                        dt.CapNhatTinhTrang(IDMaPhieu);
                        Response.Redirect("ChiTietCongNo.aspx");
                    }
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập đủ thông tin.'); </script>");
            }

           // dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Cập nhật công nợ nhà cung cấp", "Thanh toán công nợ.");
        }
    }
}
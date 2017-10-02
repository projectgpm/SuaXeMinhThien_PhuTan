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
    public partial class TraCuuMaHang : System.Web.UI.Page
    {
        dtTraCuuMaHang data = new dtTraCuuMaHang();
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
                    if (txtMaHangTraCuu.Text != "")
                    {
                        string MaHang = txtMaHangTraCuu.Text.ToString().Trim();
                        DataTable db = data.TraCuuMaHang(MaHang);
                        if (db.Rows.Count > 0)
                        {
                            LoadDanhSachBarcode(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                            LoadDanhSachHangQuiDoi(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                        }
                    }
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
        public void LoadDanhSachBarcode(string IDHangHoa)
        {
            data = new dtTraCuuMaHang();
            gridBarcode.DataSource = data.DanhSachBarcode(IDHangHoa);
            gridBarcode.DataBind();
        }
        public void LoadDanhSachHangQuiDoi(string IDHangHoa)
        {
            data = new dtTraCuuMaHang();
            gridHangQuiDoi.DataSource = data.DanhSachHangHoaQuiDoi(IDHangHoa);
            gridHangQuiDoi.DataBind();
        }
        protected void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (txtMaHangTraCuu.Text != "")
            {
                string MaHang = txtMaHangTraCuu.Text.ToString().Trim();
                if (dtSetting.IsNumber(MaHang) == true)
                {
                    DataTable db = data.TraCuuMaHang(MaHang);
                    if (db.Rows.Count > 0)
                    {
                        DataRow dr = db.Rows[0];
                        cmbNhomHang.Value =  dr["IDNhomHang"].ToString();
                        txtMaHang.Text = dr["MaHang"].ToString();
                        txtTenHangHoa.Text = dr["TenHangHoa"].ToString();
                        cmbDonViTinh.Value = dr["IDDonViTinh"].ToString();
                        txtHeSo.Text = dr["HeSo"].ToString();
                        cmbHangSanXuat.Value = dr["IDHangSanXuat"].ToString();
                        cmbThue.Value = dr["IDThue"].ToString();
                        cmbNguoiDatHang.Value = dr["IDNhomDatHang"].ToString();
                        txtTrongLuong.Text = dr["TrongLuong"].ToString();
                        cmbTrangThaiHang.Value = dr["IDTrangThaiHang"].ToString();
                        txtHangSuDung.Text = dr["HanSuDung"].ToString();
                        txtGhiChu.Text = dr["GhiChu"].ToString();
                        LoadDanhSachBarcode(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                        LoadDanhSachHangQuiDoi(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                    }
                    else
                    {
                        Clear();
                        Response.Write("<script language='JavaScript'> alert('Không tìm thấy mã hàng " + MaHang + " .'); </script>");
                        return;
                    }
                }
                else
                {
                    Clear();
                    Response.Write("<script language='JavaScript'> alert('Mã hàng phải là số.'); </script>");
                    return;
                }
            }
            else
            {
                Clear();
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập mã hàng cần tìm.'); </script>");
                return;
            }
        }
        public void Clear()
        {
            txtMaHangTraCuu.Text = "";
            txtMaHangTraCuu.Focus();
            gridHangQuiDoi.DataSource = null;
            gridBarcode.DataSource = null;
            gridBarcode.DataBind();
            gridHangQuiDoi.DataBind();
            cmbNhomHang.Text = "";
            txtMaHang.Text = "";
            txtTenHangHoa.Text = "";
            cmbDonViTinh.Text = "";
            txtHeSo.Text = "";
            cmbHangSanXuat.Text = "";
            cmbThue.Text = "";
            cmbNguoiDatHang.Text = "";
            txtTrongLuong.Text = "";
            cmbTrangThaiHang.Text = "";
            txtHangSuDung.Text = "";
        }
    }
}
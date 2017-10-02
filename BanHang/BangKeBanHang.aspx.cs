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
    public partial class BangKeBanHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                //{
                //    btnNhapExcel.Enabled = false;
                //    gridKhachHang.Columns["chucnang"].Visible = false;
                //}
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    if (!IsPostBack)
                    {
                        string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
                        if (Session["IDThuNgan"] != null)
                            IDNhanVien = Session["IDThuNgan"].ToString();
                        if (Session["IDNhanVien"] != null)
                            IDNhanVien = Session["IDNhanVien"].ToString();

                        dtKho dt = new dtKho();
                        DataTable da = dt.LayDanhSachKho();
                        da.Rows.Add(-1, "", "Tất cả cửa hàng", null, null, null, null, null, null, null, null, null);

                        cmbKho.DataSource = da;
                        cmbKho.TextField = "TenCuaHang";
                        cmbKho.ValueField = "ID";
                        cmbKho.DataBind();
                        cmbKho.SelectedIndex = da.Rows.Count;

                        string IDKho = Session["IDKho"].ToString();
                        dtKhachHang dt1 = new dtKhachHang();
                        DataTable da1 = dt1.LayDanhSachKhachHang_BaoCao(IDKho);
                        da1.Rows.Add(-1, "Tất cả khách hàng");

                        cmbKhachHang.DataSource = da1;
                        cmbKhachHang.TextField = "TenKhachHang";
                        cmbKhachHang.ValueField = "ID";
                        cmbKhachHang.DataBind();
                        cmbKhachHang.SelectedIndex = da1.Rows.Count;
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void rbTheoNam_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTheoNam.Checked == true)
            {
                rbTuyChon.Checked = false;
                rbTheoThang.Checked = false;
                dateNgayBD.Enabled = false;
                dateNgayKT.Enabled = false;
            }
        }

        protected void rbTheoThang_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTheoThang.Checked == true)
            {
                rbTuyChon.Checked = false;
                rbTheoNam.Checked = false;
                dateNgayBD.Enabled = false;
                dateNgayKT.Enabled = false;
            }
        }

        protected void rbTuyChon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTuyChon.Checked == true)
            {
                rbTheoThang.Checked = false;
                rbTheoNam.Checked = false;
                dateNgayBD.Enabled = true;
                dateNgayKT.Enabled = true;
            }
        }

        protected void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            int thang = date.Month;
            int nam = date.Year;
            string ngayBD = ""; string ngayKT = "";
            if (rbTheoNam.Checked == true)
            {
                ngayBD = nam + "-01-01 ";
                ngayKT = nam + "-12-31 ";
            }
            else if (rbTheoThang.Checked == true)
            {
                ngayBD = nam + "-" + thang + "-01 ";
                ngayKT = nam + "-" + thang + "-" + dtSetting.tinhSoNgay(thang, nam) + " ";
            }
            else if (rbTuyChon.Checked == true)
            {
                ngayBD = DateTime.Parse(dateNgayBD.Value + "").ToString("yyyy-MM-dd ");
                ngayKT = DateTime.Parse(dateNgayKT.Value + "").ToString("yyyy-MM-dd ");
            }
            else Response.Write("<script language='JavaScript'> alert('Hãy chọn 1 hình thức báo cáo.'); </script>");

            ngayBD = ngayBD + "00:00:0.000";
            ngayKT = ngayKT + "23:59:59.999";

            string IDKhoNhap = cmbKho.Value + "";
            string IDKhachHang = cmbKhachHang.Value + "";

            popup.ContentUrl = "~/BangKeBanHang_In.aspx?ngayBD=" + ngayBD + "&ngayKT=" + ngayKT + "&IDKhoNhap=" + IDKhoNhap + "&IDKhachHang=" + IDKhachHang;
            popup.ShowOnPageLoad = true;
        }
    }
}
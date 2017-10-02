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
    public partial class BaoCaoTonKho : System.Web.UI.Page
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

                        dataNganhHang dtNH = new dataNganhHang();
                        DataTable daNH = dtNH.getDanhSachNganhHang();
                        daNH.Rows.Add(-1, null, "Tất cả ngành hàng", null, null, null);
                        cmbNganhHang.DataSource = daNH;
                        cmbNganhHang.TextField = "TenNganhHang";
                        cmbNganhHang.ValueField = "ID";
                        cmbNganhHang.DataBind();
                        cmbNganhHang.SelectedIndex = daNH.Rows.Count;

                        dataNhomHang dtNhomH = new dataNhomHang();
                        DataTable daNhomH = dtNhomH.getDanhSachNhomHang();
                        daNhomH.Rows.Add(-1, 1, null, "Tất cả nhóm hàng", null, null, null);

                        cmbNhomHang.DataSource = daNhomH;
                        cmbNhomHang.TextField = "TenNhomHang";
                        cmbNhomHang.ValueField = "ID";
                        cmbNhomHang.DataBind();
                        cmbNhomHang.SelectedIndex = daNhomH.Rows.Count;

                        dataHangHoa dtHH = new dataHangHoa();
                        DataTable daHH = dtHH.getDanhSachHangHoa_Ten_ID();
                        daHH.Rows.Add(-1, "Tất cả hàng hóa");

                        cmbHangHoa.DataSource = daHH;
                        cmbHangHoa.TextField = "TenHangHoa";
                        cmbHangHoa.ValueField = "ID";
                        cmbHangHoa.DataBind();
                        cmbHangHoa.SelectedIndex = daHH.Rows.Count;
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            string IDKho = cmbKho.Value + "";
            string IDHH = cmbHangHoa.Value + "";
            string IDNH = cmbNhomHang.Value + "";
            string IDNganhH = cmbNganhHang.Value + "";

            popup.ContentUrl = "~/BaoCaoTonKho_In.aspx?IDKho=" + IDKho + "&IDHH=" + IDHH + "&IDNH=" + IDNH + "&IDNganhH=" + IDNganhH;
            popup.ShowOnPageLoad = true;
        }

        protected void cmbNganhHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataNhomHang dtNhomH = new dataNhomHang();
            DataTable daNhomH = dtNhomH.getDanhSachNhomHang_IDNganhHang(cmbNganhHang.Value + "");
            daNhomH.Rows.Add(-1, 1, null, "Tất cả nhóm hàng", null, null, null);

            cmbNhomHang.DataSource = daNhomH;
            cmbNhomHang.TextField = "TenNhomHang";
            cmbNhomHang.ValueField = "ID";
            cmbNhomHang.DataBind();
            cmbNhomHang.SelectedIndex = daNhomH.Rows.Count;

            dataHangHoa dtHH = new dataHangHoa();
            DataTable daHH = dtHH.getDanhSachHangHoa_IDNganhHang(cmbNganhHang.Value + "");
            daHH.Rows.Add(-1, "Tất cả hàng hóa");

            cmbHangHoa.DataSource = daHH;
            cmbHangHoa.TextField = "TenHangHoa";
            cmbHangHoa.ValueField = "ID";
            cmbHangHoa.DataBind();
            cmbHangHoa.SelectedIndex = daHH.Rows.Count;
        }

        protected void cmbNhomHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Int32.Parse(cmbNhomHang.Value + "") == -1)
            {
                dataHangHoa dtHH = new dataHangHoa();
                DataTable daHH = dtHH.getDanhSachHangHoa_IDNganhHang(cmbNganhHang.Value + "");
                daHH.Rows.Add(-1, "Tất cả hàng hóa");

                cmbHangHoa.DataSource = daHH;
                cmbHangHoa.TextField = "TenHangHoa";
                cmbHangHoa.ValueField = "ID";
                cmbHangHoa.DataBind();
                cmbHangHoa.SelectedIndex = daHH.Rows.Count;
            }
            else
            {
                dataHangHoa dtHH = new dataHangHoa();
                DataTable daHH = dtHH.getDanhSachHangHoa_IDNhomHang(cmbNhomHang.Value + "");
                daHH.Rows.Add(-1, "Tất cả hàng hóa");

                cmbHangHoa.DataSource = daHH;
                cmbHangHoa.TextField = "TenHangHoa";
                cmbHangHoa.ValueField = "ID";
                cmbHangHoa.DataBind();
                cmbHangHoa.SelectedIndex = daHH.Rows.Count;
            }

        }
    }
}
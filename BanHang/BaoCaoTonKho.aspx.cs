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
            if (!IsPostBack)
            {
                dataNhomHang dtNhomH = new dataNhomHang();
                DataTable daNhomH = dtNhomH.getDanhSachNhomHang2();
                daNhomH.Rows.Add(-1,"Tất cả nhóm hàng");

                cmbNhomHang.DataSource = daNhomH;
                cmbNhomHang.TextField = "TenNhomHang";
                cmbNhomHang.ValueField = "ID";
                cmbNhomHang.DataBind();
                cmbNhomHang.SelectedIndex = daNhomH.Rows.Count;

                dataHangHoa dtHH = new dataHangHoa();
                DataTable daHH = dtHH.LayDanhSachHangHoa_Ten();
                daHH.Rows.Add(-1, "Tất cả hàng hóa");

                cmbHangHoa.DataSource = daHH;
                cmbHangHoa.TextField = "TenHangHoa";
                cmbHangHoa.ValueField = "ID";
                cmbHangHoa.DataBind();
                cmbHangHoa.SelectedIndex = daHH.Rows.Count;
            }
        }

        protected void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            string IDHH = cmbHangHoa.Value + "";
            string IDNH = cmbNhomHang.Value + "";

            popup.ContentUrl = "~/BaoCaoTonKho_In.aspx?IDHH=" + IDHH + "&IDNH=" + IDNH;
            popup.ShowOnPageLoad = true;
        }

        protected void cmbNhomHang_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (Int32.Parse(cmbNhomHang.Value + "") == -1)
            {
                dataHangHoa dtHH = new dataHangHoa();
                DataTable daHH = dtHH.LayDanhSachHangHoa_Ten();
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
                DataTable daHH = dtHH.LayDanhSachHangHoa_IDNhom(cmbNhomHang.Value + "");
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
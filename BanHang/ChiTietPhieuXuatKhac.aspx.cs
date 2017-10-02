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
    public partial class ChiTietPhieuXuatKhac : System.Web.UI.Page
    {
        dtPhieuXuatKhac data = new dtPhieuXuatKhac();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                if (Int32.Parse(Session["IDNhom"].ToString()) != 6)
                {
                    btnDuyetPhieuXuat.Enabled = false;
                }

                string IDPhieuXuatKhac = Request.QueryString["IDPhieuXuatKhac"];
                if (IDPhieuXuatKhac != null)
                {
                    if (dtPhieuXuatKhac.LayTrangThaiDonXuat(IDPhieuXuatKhac) == 1)
                    {
                        btnDuyetPhieuXuat.Enabled = false;
                    }
                    LoadGrid(IDPhieuXuatKhac.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDPhieuXuatKhac)
        {
            data = new dtPhieuXuatKhac();
            gridChiTietPhieuXuatKhac.DataSource = data.DanhSachChiTietPhieuXuatKhac_ID(IDPhieuXuatKhac);
            gridChiTietPhieuXuatKhac.DataBind();
        }

        protected void btnDuyetPhieuXuat_Click(object sender, EventArgs e)
        {
            string IDPhieuXuatKhac = Request.QueryString["IDPhieuXuatKhac"];
            if (IDPhieuXuatKhac != null)
            {
                data = new dtPhieuXuatKhac();
                DataTable db = data.DanhSachChiTietPhieuXuatKhac_ID(IDPhieuXuatKhac);
                if (db.Rows.Count > 0)
                {
                    foreach(DataRow dr in db.Rows)
                    {
                        string SoLuongXuat = dr["SoLuongXuat"].ToString();
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        if (Int32.Parse(SoLuongXuat) > 0)
                        {
                            object TheKho = dtTheKho.ThemTheKho(dtPhieuXuatKhac.LaySoDonXuat(IDPhieuXuatKhac), "Phiếu xuất khác ", "0", "", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString()) - Int32.Parse(SoLuongXuat)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Xuất", SoLuongXuat, "0", "0");
                            if (TheKho != null)
                            {
                                dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuongXuat, Session["IDKho"].ToString());
                            }
                        }
                    }
                    dtPhieuXuatKhac.CapNhatTrangThai(IDPhieuXuatKhac);
                    btnDuyetPhieuXuat.Enabled = false;
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa rỗng.'); </script>");
                    return;
                }
            }
          
        }
    }
}
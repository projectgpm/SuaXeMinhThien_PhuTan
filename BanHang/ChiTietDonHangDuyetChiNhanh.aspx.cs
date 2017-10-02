using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietDonHangDuyetChiNhanh : System.Web.UI.Page
    {
        dtDuyetDonHangChiNhanh data = new dtDuyetDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
                if (IDDonHangChiNhanh != null)
                {
                    // lấy trangthaiduyet
                    if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 67) == false)
                    {
                        btnChapNhanDonHang.Enabled = false;
                    }
                    if (dtDuyetDonHangChiNhanh.LayTrangThai(IDDonHangChiNhanh) == 1)
                    {
                        btnChapNhanDonHang.Visible = false;
                    }
                    LoadGrid(IDDonHangChiNhanh.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        private void LoadGrid(string IDDuyetHangChiNhanh)
        {
            data = new dtDuyetDonHangChiNhanh();
            gridChiTiet.DataSource = data.DanhSachChiTietDuyet(IDDuyetHangChiNhanh);
            gridChiTiet.DataBind();
        }

        protected void gridChiTiet_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TrangThai = Convert.ToInt32(e.GetValue("TrangThai"));
            if (TrangThai == 1)
                e.Row.BackColor = color;
        }

        protected void btnChapNhanDonHang_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            if (IDDonHangChiNhanh != null)
            {
                // cập nhật số lượng kho chi nhánh đặt hàng.
                data = new dtDuyetDonHangChiNhanh();
                DataTable db = data.DanhSachChiTietDuyet(IDDonHangChiNhanh);
                if (db.Rows.Count > 0)
                {
                    string IDKho = dtDuyetDonHangChiNhanh.LayIDKhoLapPhieu(IDDonHangChiNhanh);
                    // cập nhật số lượng
                    foreach (DataRow dr in db.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        int SoLuong = Int32.Parse(dr["ThucTe"].ToString());
                        if (SoLuong > 0)
                        {
                            object TheKho = dtTheKho.ThemTheKho(dtDuyetDonHangChiNhanh.LaySoDonHang(IDDonHangChiNhanh), "Xác nhận đơn hàng ", SoLuong.ToString(), "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString()) + SoLuong).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Nhập", "0", "0", "0");
                            if (TheKho != null)
                            {
                                dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong.ToString(), IDKho);
                            }
                        }
                       
                    }
                    // cập nhật xong đổi trạng thái thành đơn hàng hoàn tất
                    data = new dtDuyetDonHangChiNhanh();
                    data.CapNhatDonHangHoanTat(IDDonHangChiNhanh);
                    btnChapNhanDonHang.Enabled = false;

                }
                else
                {
                    // danh sách đơn hàng trống.
                    Response.Write("<script language='JavaScript'> alert('Đơn hàng trống. Vui lòng kiểm tra lại? '); </script>");
                    return;
                }
            }
        }
    }
}
using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietDonHangXuLyTrong2Ngay : System.Web.UI.Page
    {
        dtCapNhatPhieuNhapHang data = new dtCapNhatPhieuNhapHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHang = Request.QueryString["IDDonHang"];
                if (IDDonHang != null)
                {

                    LoadGrid(IDDonHang.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDDonHangThuMua)
        {
            data = new dtCapNhatPhieuNhapHang();
            gridChiTiet.DataSource = data.DanhSachChiTiet_Duyet_ThuMua(IDDonHangThuMua);
            gridChiTiet.DataBind();
        }
        protected void gridChiTiet_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {

            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int ChenhLech = Convert.ToInt32(e.GetValue("ChenhLech"));
            if (ChenhLech > 0)
                e.Row.BackColor = color;
        }

        protected void gridChiTiet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["ThucTe"] == null)
            {
                throw new Exception("Lỗi: Số lượng thực tế không được bỏ trống. ");
            }
            else
            {
                string IDDonHang = Request.QueryString["IDDonHang"];
                string ID = e.Keys[0].ToString();
                int SoLuongThucTeMoi = Int32.Parse(e.NewValues["ThucTe"].ToString());
                int SoLuongThucTeCu = dtCapNhatPhieuNhapHang.SoLuongThucTeCu(ID);
                if (SoLuongThucTeMoi >= 0)
                {
                    if (SoLuongThucTeMoi > SoLuongThucTeCu)
                    {
                        throw new Exception("Lỗi: Số lượng thực tế mới phải nhỏ hơn hoặc bằng số lượng thực tế củ. ");
                    }
                    else
                    {
                        if (SoLuongThucTeCu != SoLuongThucTeMoi)
                        {
                            // xử lý cập nhật ở đây
                            int SLThayDoi = SoLuongThucTeCu - SoLuongThucTeMoi;
                            data = new dtCapNhatPhieuNhapHang();
                            data.CapNhatChiTietDonHang(ID, SoLuongThucTeMoi);
                            //ghi nhật ký ở đây
                            dtCapNhatTonKho.TruTonKho(e.NewValues["IDHangHoa"].ToString(), SLThayDoi.ToString(), Session["IDKho"].ToString());
                        }
                    }
                }
                else
                {
                    throw new Exception("Lỗi: Số lượng không được âm? Vui lòng kiểm tra lại. ");
                }
                e.Cancel = true;
                gridChiTiet.CancelEdit();
                LoadGrid(IDDonHang.ToString());
            }
        }
    }
}
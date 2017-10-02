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
    public partial class ChiTietDonHangThuMua : System.Web.UI.Page
    {
        dtThuMuaDatHang data = new dtThuMuaDatHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
                if (IDDonHangThuMua != null)
                {
                    if (dtThuMuaDatHang.LayTrangThaiDonHang(IDDonHangThuMua) == 1 || dtThuMuaDatHang.DonHangHuy(IDDonHangThuMua) == 1)
                    {
                        btnHuyDonHang.Visible = false;
                        gridChiTiet.Columns["chucnang"].Visible = false;
                    }
                    


                    LoadGrid(IDDonHangThuMua.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDDonHangThuMua)
        {
            data = new dtThuMuaDatHang();
            gridChiTiet.DataSource = data.DanhSachChiTiet(IDDonHangThuMua);
            gridChiTiet.DataBind();
        }

        protected void gridChiTiet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];

            string MaHang = e.NewValues["MaHang"].ToString();
            int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
            float DonGia = float.Parse(e.NewValues["DonGia"].ToString());
            if (SoLuong >= 0 && DonGia >= 0)
            {
                string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                data = new dtThuMuaDatHang();
                data.CapNhatChiTietDonHang(IDDonHangThuMua, IDHangHoa, SoLuong, DonGia, DonGia * SoLuong);
                int GiaTri = dtThuMuaDatHang.LayTyLeChietKhau(IDDonHangThuMua);
                double TongTien = TinhTongTien();
                double Tylegiam = (GiaTri * (0.01));
                double TienGiam = TongTien * Tylegiam;
                double TienSauCK = (TongTien - TienGiam);
                data.CapNhat_TongTien_TongTrongLuong(IDDonHangThuMua, TinhTongTien().ToString(), TinhTrongLuong().ToString(), TienSauCK.ToString());
                e.Cancel = true;
                gridChiTiet.CancelEdit();
                LoadGrid(IDDonHangThuMua);

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết đơn hàng thu mua", Session["IDKho"].ToString(), "Nhập xuất tồn", "Cập nhật");
            }
            else
            {
                throw new Exception("Lỗi: Số lượng & Đơn giá phải >= 0 ?");
            }
        }
        public double TinhTongTien()
        {
            string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
            data = new dtThuMuaDatHang();
            DataTable db = data.DanhSachChiTiet(IDDonHangThuMua);
            if (db.Rows.Count != 0)
            {
                double TongTien = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double ThanhTien = double.Parse(dr["ThanhTien"].ToString());
                    TongTien = TongTien + ThanhTien;
                }
                return TongTien;
            }
            else
                return 0;
        }
        public double TinhTrongLuong()
        {
            string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
            data = new dtThuMuaDatHang();
            DataTable db = data.DanhSachChiTiet(IDDonHangThuMua);
            if (db.Rows.Count != 0)
            {
                double Tong = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                    int SoLuong = Int32.Parse(dr["SoLuong"].ToString());
                    Tong = Tong + (TrongLuong * SoLuong);
                }
                return Tong;
            }
            else
                return 0;
        }

        protected void btnHuyDonHang_Click(object sender, EventArgs e)
        {
            string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
            if (IDDonHangThuMua != null)
            {
                data = new dtThuMuaDatHang();
                data.CapNhatTrangThaiDonHang(IDDonHangThuMua);
                btnHuyDonHang.Enabled = false;
                gridChiTiet.Columns["chucnang"].Visible = false;
                LoadGrid(IDDonHangThuMua.ToString());
            }
        }
    }
}
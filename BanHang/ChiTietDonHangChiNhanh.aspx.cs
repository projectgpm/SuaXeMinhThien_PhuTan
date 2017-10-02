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
    public partial class ChiTietDonHangChiNhanh : System.Web.UI.Page
    {
        dtChiTietDonHangChiNhanh data = new dtChiTietDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
                if (IDDonHangChiNhanh != null)
                {
                    if (dtChiTietDonHangChiNhanh.LayTrangThaiDonHang(IDDonHangChiNhanh, Session["IDKho"].ToString()) == 1)
                    {
                        gridChiTiet.Columns["chucnang"].Visible = false;
                        btnGiamSat.Enabled = false;
                        btnCuaHangTruong.Enabled = false;
                    }
                    if (dtChiTietDonHangChiNhanh.LayIDKho(IDDonHangChiNhanh) != Int32.Parse(Session["IDKho"].ToString()))
                    {
                        gridChiTiet.Columns["chucnang"].Visible = false;
                        btnGiamSat.Visible = false;
                        btnCuaHangTruong.Visible = false;
                    }
                    if (dtChiTietDonHangChiNhanh.TrangThaiCuaHangTruong(IDDonHangChiNhanh) == 1)
                    {
                        gridChiTiet.Columns["chucnang"].Visible = false;
                        btnCuaHangTruong.Enabled = false;
                    }
                    if (dtChiTietDonHangChiNhanh.TrangThaiGiamSat(IDDonHangChiNhanh) == 1)
                    {
                        gridChiTiet.Columns["chucnang"].Visible = false;
                        btnGiamSat.Enabled = false;
                    }
                    LoadGrid(IDDonHangChiNhanh.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDDonHangChiNhanh)
        {

            data = new dtChiTietDonHangChiNhanh();
            gridChiTiet.DataSource = data.DanhSachChiTiet(IDDonHangChiNhanh);
            gridChiTiet.DataBind();
        }

        protected void gridChiTiet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
            if (SoLuong >= 0)
            {
                string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
                string MaHang = e.NewValues["MaHang"].ToString();
                string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
              
                data = new dtChiTietDonHangChiNhanh();
                data.CapNhatChiTietDonHang(IDDonHangChiNhanh, IDHangHoa, SoLuong, GhiChu);
                data.CapNhat_TongTrongLuong(IDDonHangChiNhanh, TinhTrongLuong().ToString());
                e.Cancel = true;
                gridChiTiet.CancelEdit();
                LoadGrid(IDDonHangChiNhanh);

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Cập nhật");
            }
            else
            {
                throw new Exception("Lỗi: Số lượng phải >= 0 ?");
            }
        }
        
        public double TinhTrongLuong()
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            data = new dtChiTietDonHangChiNhanh();
            DataTable db = data.DanhSachChiTiet(IDDonHangChiNhanh);
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

        protected void btnGiamSat_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            data = new dtChiTietDonHangChiNhanh();
            data.GiamSatDuyet(IDDonHangChiNhanh); // ghi nhật ký
            btnGiamSat.Enabled = false;
            gridChiTiet.Columns["chucnang"].Visible = false;
            LoadGrid(IDDonHangChiNhanh.ToString());
        }

        protected void btnCuaHangTruong_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            data = new dtChiTietDonHangChiNhanh();
            data.CuaHangTruongDuyet(IDDonHangChiNhanh);
            btnCuaHangTruong.Enabled = false;
            gridChiTiet.Columns["chucnang"].Visible = false;
            LoadGrid(IDDonHangChiNhanh.ToString());
            
        }
    }
}
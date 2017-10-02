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
    public partial class ChiTietPhieuChuyenKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
                if (IDPhieuChuyenKho != null)
                {
                    LoadGrid(IDPhieuChuyenKho);
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
            
        }
        private void LoadGrid(string IDPhieuChuyenKho)
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            DataTable da = data.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
            gridHangHoaChiTiet.DataSource = da;
            gridHangHoaChiTiet.DataBind();

            int IDKho = Int32.Parse(Session["IDKho"].ToString());
            int IDNhom = Int32.Parse(Session["IDNhom"].ToString());
            int IDNhanVien = Int32.Parse(Session["IDNhanVien"].ToString());

            DataTable daPhieu = data.DanhSachPhieuChuyenKho_Kho(IDPhieuChuyenKho);
            int IDKhoXuat = Int32.Parse(daPhieu.Rows[0]["IDKhoXuat"].ToString());
            int IDKhoNhan = Int32.Parse(daPhieu.Rows[0]["IDKhoNhap"].ToString());
            int IDTrangThai = Int32.Parse(daPhieu.Rows[0]["IDTrangThai"].ToString());
            int DaXoa = Int32.Parse(daPhieu.Rows[0]["DaXoa"].ToString());

            if (IDTrangThai == 4 || DaXoa == 1) btnXacNhanChuyenKho.Enabled = false;
            if (IDTrangThai > 1) gridHangHoaChiTiet.Columns["chucnang"].Visible = false;
            if (IDKho != IDKhoXuat) gridHangHoaChiTiet.Columns["chucnang"].Visible = false;
            
            if (IDKhoXuat == IDKho)
            {
                if (IDTrangThai != 1) btnXacNhanChuyenKho.Enabled = false;
                if (IDKho == 1 && IDTrangThai == 3) btnXacNhanChuyenKho.Enabled = true;
            }
            else if (IDKhoNhan == IDKho)
            {
                if (IDTrangThai != 2) btnXacNhanChuyenKho.Enabled = false;
                if (IDKho == 1 && IDTrangThai == 3) btnXacNhanChuyenKho.Enabled = true;
            }
            else if (IDKho == 1)
            {
                if (IDTrangThai != 3) btnXacNhanChuyenKho.Enabled = false;
            }

            if (IDNhom != 3 && IDNhom != 4)
            {
                btnXacNhanChuyenKho.Enabled = false;
            }
        }

        protected void gridHangHoaChiTiet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();

            dt.XoaChiTietPhieuChuyenKho_Update(ID + "");

            DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho);
            if (da.Rows.Count != 0)
            {
                string tongTrongLuong = da.Rows[0]["TongTrongLuong"].ToString();
                string tongSoLuong = da.Rows[0]["TongSoLuong"].ToString();

                dt.CapNhatPhieuChuyenKho_2(IDPhieuChuyenKho, tongSoLuong, tongTrongLuong);

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Xóa");
            }

            e.Cancel = true;
            gridHangHoaChiTiet.CancelEdit();
            LoadGrid(IDPhieuChuyenKho);
        }

        protected void gridHangHoaChiTiet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["MaHang"] != null && e.NewValues["SoLuong"] != null)
            {
                string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
                string MaHang = e.NewValues["MaHang"].ToString();
                string SoLuong = e.NewValues["SoLuong"].ToString();
                string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
                string IDHH = dtHangHoa.LayIDHangHoa_MaHang(MaHang);
                float TrongLuong = dtHangHoa.LayTrongLuong(IDHH) * Int32.Parse(SoLuong);

                dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
                DataTable dx = dt.KiemTraHangHoa(IDPhieuChuyenKho, IDHH);
                if (dx.Rows.Count == 0)
                {
                    dt.ThemChiTietPhieuChuyenKho(IDPhieuChuyenKho, IDHH, SoLuong, TrongLuong + "", GhiChu);
                    DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho);
                    if (da.Rows.Count != 0)
                    {
                        string tongTrongLuong = da.Rows[0]["TongTrongLuong"].ToString();
                        string tongSoLuong = da.Rows[0]["TongSoLuong"].ToString();

                        dt.CapNhatPhieuChuyenKho_2(IDPhieuChuyenKho, tongSoLuong, tongTrongLuong);
                    }
                }
                else
                {
                    dt.CapNhatChiTietPhieuChuyenKho(IDPhieuChuyenKho, IDHH, (Int32.Parse(dx.Rows[0]["SoLuong"].ToString()) + Int32.Parse(SoLuong)) + "", (float.Parse(dx.Rows[0]["TrongLuong"].ToString()) + TrongLuong) + "", GhiChu);
                    DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho);
                    if (da.Rows.Count != 0)
                    {
                        string tongTrongLuong = da.Rows[0]["TongTrongLuong"].ToString();
                        string tongSoLuong = da.Rows[0]["TongSoLuong"].ToString();

                        dt.CapNhatPhieuChuyenKho_2(IDPhieuChuyenKho, tongSoLuong, tongTrongLuong);
                    }
                }

                e.Cancel = true;
                gridHangHoaChiTiet.CancelEdit();
                LoadGrid(IDPhieuChuyenKho);
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");
        }

        protected void btnXacNhanChuyenKho_Click(object sender, EventArgs e)
        {
            int IDKho = Int32.Parse(Session["IDKho"].ToString());
            int IDNhom = Int32.Parse(Session["IDNhom"].ToString());
            int IDNhanVien = Int32.Parse(Session["IDNhanVien"].ToString());

            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            DataTable da = dt.DanhSachPhieuChuyenKho_Kho(IDPhieuChuyenKho);
            int IDKhoXuat = Int32.Parse(da.Rows[0]["IDKhoXuat"].ToString());
            int IDKhoNhan = Int32.Parse(da.Rows[0]["IDKhoNhap"].ToString());
            int IDTrangThai = Int32.Parse(da.Rows[0]["IDTrangThai"].ToString());
            int DaXoa = Int32.Parse(da.Rows[0]["DaXoa"].ToString());

            if (IDKhoXuat == IDKho && IDKho != 1)
            {
                if (IDNhom == 3)
                {
                    dt.DuyetChuyenKho_Xuat_CH1(IDPhieuChuyenKho, IDNhanVien + "");
                }
                else if (IDNhom == 4)
                {
                    dt.DuyetChuyenKho_Xuat_GS1(IDPhieuChuyenKho, IDNhanVien + "");
                } 
            }
            else if (IDKhoNhan == IDKho && IDKho != 1)
            {
                if (IDNhom == 3)
                {
                    dt.DuyetChuyenKho_Nhap_CH1(IDPhieuChuyenKho, IDNhanVien + "");
                }
                else if (IDNhom == 4)
                {
                    dt.DuyetChuyenKho_Nhap_GS1(IDPhieuChuyenKho, IDNhanVien + "");
                }
            }
            else if (IDKhoXuat == IDKho && IDKho == 1) // kho tổng là kho xuất.
            {
                if (IDTrangThai == 1)
                {
                    if (IDNhom == 3)
                    {
                        dt.DuyetChuyenKho_Xuat_CH1(IDPhieuChuyenKho, IDNhanVien + "");
                    }
                    else if (IDNhom == 4)
                    {
                        dt.DuyetChuyenKho_Xuat_GS1(IDPhieuChuyenKho, IDNhanVien + "");
                    }
                }
                else if (IDTrangThai == 3)
                {
                    dt.DuyetChuyenKho_HoanThanh(IDPhieuChuyenKho, IDNhanVien + "");

                    // Cộng, trừ tồn kho của 2 hệ thống...
                    DataTable dataChiTiet = dt.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
                    string SoPhieu = dtPhieuChuyenKho.MaPhieuChuyenKho(IDPhieuChuyenKho);
                    for (int i = 0; i < dataChiTiet.Rows.Count; i++)
                    {
                        string IDHangHoa = dataChiTiet.Rows[i]["IDHangHoa"].ToString();
                        string SoLuong = dataChiTiet.Rows[i]["SoLuong"].ToString();
                        //dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoXuat + "");
                        //dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoNhan + "");
                        if (Int32.Parse(SoLuong) > 0)
                        {
                            object TheKho1 = dtTheKho.ThemTheKho(SoPhieu, "Chuyển Kho " + dtTheKho.LayTenKho_ID(IDKhoXuat.ToString()) + " Sang " + dtTheKho.LayTenKho_ID(IDKhoNhan.ToString()), SoLuong, "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKhoNhan.ToString()).ToString()) + Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), IDKhoNhan + "", IDHangHoa, "Nhập", "0", "0", "0");
                            object TheKho2 = dtTheKho.ThemTheKho(SoPhieu, "Chuyển Kho " + dtTheKho.LayTenKho_ID(IDKhoXuat.ToString()) + " Sang " + dtTheKho.LayTenKho_ID(IDKhoNhan.ToString()), "0", SoLuong, (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKhoXuat.ToString()).ToString()) - Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), IDKhoXuat + "", IDHangHoa, "Xuất", "0", "0", "0");
                            if (TheKho1 != null && TheKho2 != null)
                            {
                                dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoXuat + "");
                                dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoNhan + "");
                            }
                        }
                    }
                }
            }
            else if (IDKhoNhan == IDKho && IDKho == 1)
            {
                if (IDTrangThai == 2)
                {
                    if (IDNhom == 3)
                    {
                        dt.DuyetChuyenKho_Xuat_CH1(IDPhieuChuyenKho, IDNhanVien + "");
                    }
                    else if (IDNhom == 4)
                    {
                        dt.DuyetChuyenKho_Xuat_GS1(IDPhieuChuyenKho, IDNhanVien + "");
                    }
                }
                else if (IDTrangThai == 3)
                {
                    dt.DuyetChuyenKho_HoanThanh(IDPhieuChuyenKho, IDNhanVien + "");

                    // Cộng, trừ tồn kho của 2 hệ thống...
                    DataTable dataChiTiet = dt.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
                    string SoPhieu = dtPhieuChuyenKho.MaPhieuChuyenKho(IDPhieuChuyenKho);
                    for (int i = 0; i < dataChiTiet.Rows.Count; i++)
                    {
                        string IDHangHoa = dataChiTiet.Rows[i]["IDHangHoa"].ToString();
                        string SoLuong = dataChiTiet.Rows[i]["SoLuong"].ToString();
                        //dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoXuat + "");
                        //dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoNhan + "");
                        object TheKho1 = dtTheKho.ThemTheKho(SoPhieu, "Chuyển Kho " + dtTheKho.LayTenKho_ID(IDKhoXuat.ToString()) + " Sang " + dtTheKho.LayTenKho_ID(IDKhoNhan.ToString()), SoLuong, "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKhoNhan.ToString()).ToString()) + Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Nhập", "0", "0", "0");
                        object TheKho2 = dtTheKho.ThemTheKho(SoPhieu, "Chuyển Kho " + dtTheKho.LayTenKho_ID(IDKhoXuat.ToString()) + " Sang " + dtTheKho.LayTenKho_ID(IDKhoNhan.ToString()), "0", SoLuong, (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKhoXuat.ToString()).ToString()) - Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Xuất", "0", "0", "0");
                        if (TheKho1 != null && TheKho2 != null)
                        {
                            dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoXuat + "");
                            dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoNhan + "");
                        }
                    }
                }
            }
            else if (IDKho == 1)
            {
                if (IDTrangThai == 3)
                {
                    dt.DuyetChuyenKho_HoanThanh(IDPhieuChuyenKho, IDNhanVien + "");

                    // Cộng, trừ tồn kho của 2 hệ thống...
                    DataTable dataChiTiet = dt.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
                    string SoPhieu = dtPhieuChuyenKho.MaPhieuChuyenKho(IDPhieuChuyenKho);
                    for (int i = 0; i < dataChiTiet.Rows.Count; i++)
                    {
                        string IDHangHoa = dataChiTiet.Rows[i]["IDHangHoa"].ToString();
                        string SoLuong = dataChiTiet.Rows[i]["SoLuong"].ToString();
                        //dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoXuat + "");
                        //dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoNhan + "");
                        object TheKho1 = dtTheKho.ThemTheKho(SoPhieu, "Chuyển Kho " + dtTheKho.LayTenKho_ID(IDKhoXuat.ToString()) + " Sang " + dtTheKho.LayTenKho_ID(IDKhoNhan.ToString()), SoLuong, "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKhoNhan.ToString()).ToString()) + Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Nhập", "0", "0", "0");
                        object TheKho2 = dtTheKho.ThemTheKho(SoPhieu, "Chuyển Kho " + dtTheKho.LayTenKho_ID(IDKhoXuat.ToString()) + " Sang " + dtTheKho.LayTenKho_ID(IDKhoNhan.ToString()), "0", SoLuong, (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKhoXuat.ToString()).ToString()) - Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Xuất", "0", "0", "0");
                        if (TheKho1 != null && TheKho2 != null)
                        {
                            dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoXuat + "");
                            dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoNhan + "");
                        }
                    }
                }
            }

            LoadGrid(IDPhieuChuyenKho);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt chuyển kho");
        }
    }
}
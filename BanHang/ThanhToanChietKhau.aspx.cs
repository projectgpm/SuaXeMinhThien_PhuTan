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
    public partial class ThanhToanChietKhau : System.Web.UI.Page
    {
        dtThanhToanChietKhau data = new dtThanhToanChietKhau();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        double TOngTien = 0;
        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "")
            {
                int KT = 0;
                double TongTienChietKhau = 0, TongTienTraHang = 0;
                foreach (var key in gridDanhSach.GetCurrentPageRowValues("ID"))
                {
                    if (gridDanhSach.Selection.IsRowSelectedByKey(key))
                    {
                        string ID = key.ToString();
                        if (Int32.Parse(ID) != -1)
                        {
                            string MaHoaDon = dtThanhToanChietKhau.LayMaHoaDon(ID);
                            double TyLe = dtThanhToanChietKhau.TyLeChietKhau(ID);
                            KT = 1;
                            TongTienChietKhau = TongTienChietKhau + dtThanhToanChietKhau.LayTienChietKhau(ID.ToString());
                            TongTienTraHang = TongTienTraHang + (dtThanhToanChietKhau.TongTienTra(MaHoaDon) * (TyLe * 0.01));
                        }
                    }

                }
                //thêm vào trả chiết khấu + cập nhật
                string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                if (KT == 1)
                {
                    double TienThanhToan = TongTienChietKhau - TongTienTraHang;
                    object ID = data.ThemThanhToanChietKhau(cmbKhachHang.Value.ToString(), TienThanhToan, GhiChu);
                    if (ID != null)
                    {
                        foreach (var key in gridDanhSach.GetCurrentPageRowValues("ID"))
                        {
                            if (gridDanhSach.Selection.IsRowSelectedByKey(key))
                            {
                                string IDHoaDon = key.ToString();
                                data = new dtThanhToanChietKhau();
                                data.CapNhatTinhTrang(IDHoaDon);
                            }

                        }
                        Response.Redirect("ChiTietThanhToanChietKhau.aspx");
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Không có đơn hàng để thanh toán chiết khấu.'); </script>");
                }
            }
            else
            {
                cmbKhachHang.Focus();
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn khách hàng.'); </script>");
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
        public void LoadGrid()
        {
            if (cmbKhachHang.Text != "")
            {
                string iDKhachHang = cmbKhachHang.Value.ToString();
                DataTable da = new DataTable();
                da.Columns.Add("NgayBan", typeof(DateTime));
                da.Columns.Add("TongTien", typeof(double));
                da.Columns.Add("TyLeChietKhauKhachHang", typeof(int));
                da.Columns.Add("TienChietKhauKhachHang", typeof(double));
                da.Columns.Add("ID", typeof(int));
                da.Columns.Add("TrangThaiDonHang", typeof(int));
                da.Columns.Add("MaHoaDon", typeof(string));
                DataTable db = data.DanhSachChuaChietKhau(iDKhachHang);
                if (db.Rows.Count > 0)
                {
                    foreach (DataRow dr in db.Rows)
                    {
                        int TyLeChietKhauKhachHang = Int32.Parse(dr["TyLeChietKhauKhachHang"].ToString());
                        int TrangThaiDonHang = Int32.Parse(dr["TrangThaiDonHang"].ToString());
                        int ID = Int32.Parse(dr["ID"].ToString());
                        string MaHoaDon = dr["MaHoaDon"].ToString();
                        DateTime NgayBan = DateTime.Parse(dr["NgayBan"].ToString());
                        double TongTien = double.Parse(dr["TongTien"].ToString());
                        double TienChietKhauKhachHang = double.Parse(dr["TienChietKhauKhachHang"].ToString());
                        da.Rows.Add(NgayBan, TongTien, TyLeChietKhauKhachHang, TienChietKhauKhachHang, ID, TrangThaiDonHang, MaHoaDon);
                        DataTable dbtt = data.DanhSachKhacHangTraHang(iDKhachHang, MaHoaDon, TyLeChietKhauKhachHang, ID);
                        TOngTien = TOngTien + TOngTien;
                        if (dbtt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dbtt.Rows)
                            {
                                double TongTienTraHang = double.Parse(dr1["TongTien"].ToString());
                                double TienChietKhauKhachHangTraHang = double.Parse(dr1["TienChietKhauKhachHang"].ToString());
                                DateTime NgayBanTraHang = DateTime.Parse(dr1["NgayBan"].ToString());
                                int TrangThaiDonHangTraHang = Int32.Parse(dr1["TrangThaiDonHang"].ToString());
                                TOngTien = TOngTien + TongTienTraHang;
                                da.Rows.Add(NgayBanTraHang, TongTienTraHang, TyLeChietKhauKhachHang, TienChietKhauKhachHangTraHang, -1, TrangThaiDonHangTraHang, MaHoaDon);
                            }
                        }
                        
                    }
                }
                gridDanhSach.DataSource = da;
                gridDanhSach.DataBind();
            }
        }

        protected void gridDanhSach_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TrangThaiDonHang = Convert.ToInt32(e.GetValue("TrangThaiDonHang"));
            if (TrangThaiDonHang == 1)
                e.Row.BackColor = color;
        }
    }
}
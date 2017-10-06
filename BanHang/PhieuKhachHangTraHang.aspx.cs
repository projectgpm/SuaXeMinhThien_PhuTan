﻿using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class PhieuKhachHangTraHang : System.Web.UI.Page
    {
        dtPhieuKhachHangTraHang data = new dtPhieuKhachHangTraHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    IDPhieuKhachHangTraHangTem_Temp.Value = Session["IDNhanVien"].ToString();
                    cmbNguoiLapPhieu.Text = Session["IDNhanVien"].ToString();
                }
                LoadGrid(IDPhieuKhachHangTraHangTem_Temp.Value.ToString());
            }
        }

        private void LoadGrid(string IDPhieuKhachHangTraHang)
        {
            data = new dtPhieuKhachHangTraHang();
            gridDanhSachHangHoa_Temp.DataSource = data.ChiTietPhieuKhachHangTraHang_Temp(IDPhieuKhachHangTraHang);
            gridDanhSachHangHoa_Temp.DataBind();

            //DataTable da = data.ChiTietTongSoLuongHangHoa(IDPhieuKhachHangTraHang);
            //if (da.Rows.Count != 0)
            //{
            //    txtTongTien.Value = float.Parse(da.Rows[0]["TongTien"].ToString());
            //    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
            //}
            //else
            //{
            //    txtTongTien.Value = 0;
            //    txtSoMatHang.Value = 0;
            //}
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            //string IDPhieuTraHang = IDPhieuKhachHangTraHangTem_Temp.Value + "";

            //if (cmbHoaDon.Value != null && cmbLyDoTra.Value != null)
            //{
            //    string IDHoaDon = cmbHoaDon.Value + "";
            //     data = new dtPhieuKhachHangTraHang();
            //     cmbHangHoa.DataSource = data.DanhSachHangHoa_HoaDon(IDHoaDon);
            //    cmbHangHoa.DataBind();

            //    string IDChiTiet = cmbHangHoa.Value + "";
            //    string IDHangHoa = 0 + "";
            //    int soLuongBan = 0;
            //    DataTable dta = dt.ChiTietHangHoa(IDChiTiet);
            //    if (dta.Rows.Count != 0)
            //    {
            //        DataRow dr = dta.Rows[0];
            //        IDHangHoa = dr["IDHangHoa"].ToString();
            //        soLuongBan = Int32.Parse(dr["SoLuong"].ToString());
            //    }

            //    DataTable tHH = dt.ChiTietHangHoa_ID(IDHangHoa, IDPhieuTraHang);
            //    if (tHH.Rows.Count == 0)
            //    {
            //        string GiaBan = txtGiaBan.Value + "";
            //        string SoLuong = txtSoLuong.Value + "";
            //        string tongTien = txtTongTienHH.Value + "";
            //        string lyDoTra = cmbLyDoTra.Text + "";
            //        dt.ThemChiTietPhieuKhachHangTraHang_Temp(IDPhieuTraHang, IDHangHoa, GiaBan, SoLuong, tongTien, lyDoTra);
            //    }
            //    else
            //    {
            //        string GiaBan = txtGiaBan.Value + "";
            //        string SoLuong = txtSoLuong.Value + "";
            //        string lyDoTra = cmbLyDoTra.Text + "";

            //        int SoLuong2 = Int32.Parse(tHH.Rows[0]["SoLuong"].ToString()) + Int32.Parse(SoLuong);
            //        if (soLuongBan < SoLuong2)
            //        {
            //            txtSoLuong.Value = soLuongBan - Int32.Parse(tHH.Rows[0]["SoLuong"].ToString());
            //            Response.Write("<script language='JavaScript'> alert('Không được vượt số lượng mua.'); </script>");
            //        }
            //        else
            //        {
            //            dt.CapNhatChiTietPhieuKhachHangTraHang_Temp(IDPhieuTraHang, IDHangHoa, GiaBan, SoLuong2 + "", (SoLuong2 * Int32.Parse(GiaBan)) + "", lyDoTra);
            //        }
            //    }
            //}
            //else
            //{
            //    Response.Write("<script language='JavaScript'> alert('Chọn hàng hóa và lý do trả.'); </script>");
            //}
            //LoadGrid(IDPhieuTraHang);
        }

        protected void cmbNgayLapPhieu_Init(object sender, EventArgs e)
        {
            cmbNgayLapPhieu.Date = DateTime.Now;
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
           // string IDChiTiet = cmbHangHoa.Value + "";
           // string IDHoaDon = cmbHoaDon.Value + "";
           //data = new dtPhieuKhachHangTraHang();
           //cmbHangHoa.DataSource = data.DanhSachHangHoa_HoaDon(IDHoaDon);
           // cmbHangHoa.DataBind();

           // DataTable dta = data.ChiTietHangHoa(IDChiTiet);
           // if (dta.Rows.Count != 0)
           // {
           //     DataRow dr = dta.Rows[0];
           //     cmbDonViTinh.Value = dr["IDDonViTinh"].ToString();
           //     txtGiaBan.Value = dr["GiaBan"].ToString();
           //     txtSoLuong.Value = dr["SoLuong"].ToString();
           //     txtTongTienHH.Value = dr["ThanhTien"].ToString();
           // }
        }

        protected void txtSoLuong_NumberChanged(object sender, EventArgs e)
        {
            if (cmbHoaDon.Text != "")
            {
                string IDHoaDon = cmbHoaDon.Value + "";
                data = new dtPhieuKhachHangTraHang();
                cmbHangHoa.DataSource = data.DanhSachHangHoa_HoaDon(IDHoaDon);
                cmbHangHoa.DataBind();

                int soLuong = Int32.Parse(txtSoLuong.Value + "");
                string IDChiTiet = cmbHangHoa.Value + "";
                DataTable dta = data.ChiTietHangHoa(IDChiTiet);
                int soLuong2 = 0;
                float giaBan = 0;
                if (dta.Rows.Count != 0)
                {
                    DataRow dr = dta.Rows[0];
                    soLuong2 = Int32.Parse(dr["SoLuong"].ToString());
                    giaBan = float.Parse(dr["GiaBan"].ToString());
                }

                if (soLuong2 < soLuong)
                {
                    txtSoLuong.Value = soLuong2;
                    Response.Write("<script language='JavaScript'> alert('Không được vượt số lượng mua.'); </script>");
                }
                else
                {
                    float gia = soLuong * giaBan;
                    txtTongTienHH.Value = gia;
                }
            }
        }

        protected void cmbHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHoaDon.Text != "")
            {
                string IDHoaDon = cmbHoaDon.Value + "";
                data = new dtPhieuKhachHangTraHang();
                DataTable da = data.HoaDon_ID(IDHoaDon);
                if (da.Rows.Count != 0)
                {
                    DataRow dr = da.Rows[0];
                    cmbNhanVienBanHang.Value = dr["IDNhanVien"].ToString();
                    cmbHangHoa.DataSource = data.DanhSachHangHoa_HoaDon(IDHoaDon);
                    cmbHangHoa.DataBind();
                    data.XoaChiTiet_Temp(IDHoaDon);
                }
            }
        }

        protected void btnThemPhieuKhachHangTraHang_Click(object sender, EventArgs e)
        {
            string ID = IDPhieuKhachHangTraHangTem_Temp.Value.ToString();
            dtPhieuKhachHangTraHang data = new dtPhieuKhachHangTraHang();
            if (cmbHoaDon.Value != null)
            {
                DataTable da = data.ChiTietPhieuKhachHangTraHang_Temp(ID);
                if (da.Rows.Count != 0)
                {
                    string IDHoaDon = cmbHoaDon.Value + "";
                    string IDNhanVien = Session["IDNhanVien"].ToString();
                    string IDKhachHang = cmbKhachHang.Value + "";
                    string TongHangHoaDoi = txtSoMatHang.Value + "";
                    string TongTienTra = txtTongTien.Value + "";
                    string GhiChu = txtGhiChu.Value + "";
                    data.CapNhatPhieuKhachHangTraHang(ID, IDHoaDon, IDNhanVien, IDKhachHang, TongHangHoaDoi, TongTienTra, GhiChu);

                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        DataRow dr = da.Rows[i];
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string GiaBan = dr["GiaBan"].ToString();
                        string SoLuong = dr["SoLuong"].ToString();
                        string ThanhTien = dr["ThanhTien"].ToString();
                        string LyDoDoi = dr["LyDoDoi"].ToString();
                        data.ThemChiTietPhieuKhachHangTraHang(ID, IDHangHoa, GiaBan, SoLuong, ThanhTien, LyDoDoi);

                        dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, Session["IDKho"].ToString());
                    }

                    data.XoaChiTiet_Temp(ID);

                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu khách hàng trả hàng", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");
                    Response.Redirect("DanhSachKhachHangTraHang.aspx");
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa không được rỗng.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Chọn hóa đơn.'); </script>");
            }
        }

        protected void btnHuyPhieuKhachHangTraHang_Click(object sender, EventArgs e)
        {
            dtPhieuKhachHangTraHang dt = new dtPhieuKhachHangTraHang();
            string IDHoaDon = IDPhieuKhachHangTraHangTem_Temp.Value + "";
            dt.XoaPhieu_(IDHoaDon);
            dt.XoaChiTiet_Temp(IDHoaDon);
            Response.Redirect("DanhSachKhachHangTraHang.aspx");
        }
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            string IDHoaDon = cmbHoaDon.Value + "";
            dtPhieuKhachHangTraHang dt = new dtPhieuKhachHangTraHang();
            cmbHangHoa.DataSource = dt.DanhSachHangHoa_HoaDon(IDHoaDon);
            cmbHangHoa.DataBind();

            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            dt.XoaChiTiet_ID(ID + "");
            LoadGrid(IDPhieuKhachHangTraHangTem_Temp.Value + "");
        }

        protected void cmbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "")
            {
                data = new dtPhieuKhachHangTraHang();
                cmbHoaDon.DataSource = data.DanhSachSoDonHang(cmbKhachHang.Value.ToString());
                cmbHoaDon.ValueField = "ID";
                cmbHoaDon.TextField = "MaHoaDon";
                cmbHoaDon.DataBind();
            }
        }

        protected void ckHoaDon_CheckedChanged(object sender, EventArgs e)
        {
            if (ckHoaDon.Checked == true)
            {
                cmbHoaDon.Enabled = true;
                if (cmbKhachHang.Text != "")
                {
                    data = new dtPhieuKhachHangTraHang();
                    cmbHoaDon.DataSource = data.DanhSachSoDonHang(cmbKhachHang.Value.ToString());
                    cmbHoaDon.ValueField = "ID";
                    cmbHoaDon.TextField = "MaHoaDon";
                    cmbHoaDon.DataBind();
                }
            }
            else
                cmbHoaDon.Enabled = false;

        }

        protected void cmbHangHoa_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
//            long value = 0;
//            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
//                return;
//            ASPxComboBox comboBox = (ASPxComboBox)source;
//            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBan, GPM_DonViTinh.TenDonViTinh 
//                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
//                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
//                                        WHERE (GPM_HangHoa.ID = @ID)";
//            dsHangHoa.SelectParameters.Clear();
//            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
//            comboBox.DataSource = dsHangHoa;
//            comboBox.DataBind();
        }
        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
//            ASPxComboBox comboBox = (ASPxComboBox)source;
//            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBan] , [TenDonViTinh]
//                                        FROM (
//	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBan, GPM_DonViTinh.TenDonViTinh, 
//	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
//	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
//                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
//	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang) OR GPM_HangHoa.TenHangHoa LIKE @TenHang)  AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
//	                                        ) as st 
//                                        where st.[rn] between @startIndex and @endIndex";
//            dsHangHoa.SelectParameters.Clear();
//            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
//            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
//            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
//            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
//            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
//            comboBox.DataSource = dsHangHoa;
//            comboBox.DataBind();
        }
    }
}
using BanHang.Data;
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
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            string IDPhieuTraHang = IDPhieuKhachHangTraHangTem_Temp.Value + "";
            if (cmbHangHoa.Value != null && cmbLyDoTra.Value != null)
            {
                data = new dtPhieuKhachHangTraHang();
                int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
                if (SoLuong > 0)
                {
                    string IDHangHoa = cmbHangHoa.Value.ToString();
                    DataTable tHH = data.ChiTietHangHoa_ID(IDHangHoa, IDPhieuTraHang);
                    if (tHH.Rows.Count == 0)
                    {
                        string GiaBan = txtGiaBan.Text.ToString();
                        string lyDoTra = cmbLyDoTra.Text.ToString();
                        data.ThemChiTietPhieuKhachHangTraHang_Temp(IDPhieuTraHang, IDHangHoa, GiaBan, SoLuong.ToString(), (double.Parse(GiaBan) * SoLuong).ToString(), lyDoTra);
                        Clear();
                    }
                    else
                    {
                        string GiaBan = txtGiaBan.Text.ToString();
                        string lyDoTra = cmbLyDoTra.Text.ToString();
                        data.CapNhatChiTietPhieuKhachHangTraHang_Temp(IDPhieuTraHang, IDHangHoa, GiaBan, SoLuong.ToString(), (double.Parse(GiaBan) * SoLuong).ToString(), lyDoTra);
                        Clear();
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Số lượng > 0.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Chọn hàng hóa và lý do trả.'); </script>");
            }
           
            LoadGrid(IDPhieuTraHang);
        }
        public void Clear()
        {
            cmbHangHoa.Text = "";
            txtSoLuong.Text = "0";
            cmbDonViTinh.Text = "";
            txtGiaBan.Text = "";
            cmbLyDoTra.Text = "";
        }
        protected void cmbNgayLapPhieu_Init(object sender, EventArgs e)
        {
            cmbNgayLapPhieu.Date = DateTime.Now;
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHoaDon.Text != "" && ckHoaDon.Checked == true)
            {
                string IDHangHoa = cmbHangHoa.Value.ToString();
                string IDHoaDon = cmbHoaDon.Value.ToString();
                data = new dtPhieuKhachHangTraHang();
                DataTable dta = data.ChiTietHangHoa(IDHangHoa);
                if (dta.Rows.Count != 0)
                {
                    DataRow dr = dta.Rows[0];
                    cmbDonViTinh.Value = dr["IDDonViTinh"].ToString();
                    txtGiaBan.Value = dr["GiaBan"].ToString();
                    txtSoLuong.Value = dr["SoLuong"].ToString();
                }
                txtSoLuong.Text = "0";
                txtGiaBan.Enabled = false;
            }
            else
            {
                txtSoLuong.Text = "0";
                txtGiaBan.Enabled = true;
                cmbDonViTinh.Value = dtHangHoa.LayIDDonViTinh(cmbHangHoa.Value.ToString());
                txtGiaBan.Text = dtCapNhatTonKho.GiaBan(cmbHangHoa.Value.ToString())+"";
            }
        }

        protected void txtSoLuong_NumberChanged(object sender, EventArgs e)
        {
            if (cmbHoaDon.Text != "" && ckHoaDon.Checked == true)
            {
                string IDHoaDon = cmbHoaDon.Value.ToString();
                data = new dtPhieuKhachHangTraHang();
                cmbHangHoa.DataSource = data.DanhSachHangHoa_HoaDon(IDHoaDon);
                cmbHangHoa.DataBind();

                int SoLuongDoi = Int32.Parse(txtSoLuong.Value.ToString());
                string IDHangHoa = cmbHangHoa.Value.ToString();
                
                data = new dtPhieuKhachHangTraHang();
                DataTable dta = data.DanhSachChiTietHoaDon(IDHangHoa, IDHoaDon);
                int soLuong2 = 0;
                float giaBan = 0;
                if (dta.Rows.Count != 0)
                {
                    DataRow dr = dta.Rows[0];
                    soLuong2 = Int32.Parse(dr["SoLuong"].ToString());
                    giaBan = float.Parse(dr["GiaBan"].ToString());
                }

                if (soLuong2 < SoLuongDoi)
                {
                    txtSoLuong.Value = soLuong2;
                    Response.Write("<script language='JavaScript'> alert('Không được vượt số lượng mua.'); </script>");
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
                LoadGrid(IDPhieuKhachHangTraHangTem_Temp.Value.ToString());
            }
        }

        protected void btnThemPhieuKhachHangTraHang_Click(object sender, EventArgs e)
        {
            string ID = IDPhieuKhachHangTraHangTem_Temp.Value.ToString();
            string IDNhanVien = Session["IDNhanVien"].ToString();
            string IDKhachHang = cmbKhachHang.Value.ToString();
            string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
            if (ckHoaDon.Checked == true && cmbHoaDon.Text != "")
            {
                // tính lại doanh thu hóa đơn, chiết khấu, giảm công nợ, cộng tồn kho
                DataTable da = data.ChiTietPhieuKhachHangTraHang_Temp(ID);
                if (da.Rows.Count != 0)
                {
                    double TongTien = 0;
                    foreach (DataRow dr in da.Rows)
                    {
                        double ThanhTien = double.Parse(dr["ThanhTien"].ToString());
                        TongTien = TongTien + ThanhTien;
                    }
                    object IDThem = data.ThemPhieuKhachHangTraHang(cmbHoaDon.Text.ToString(), IDNhanVien, IDKhachHang, TongTien.ToString(), GhiChu);
                    if (IDThem != null)
                    {
                        string IDHoaDon = cmbHoaDon.Value.ToString();
                        for (int i = 0; i < da.Rows.Count; i++)
                        {
                            DataRow dr = da.Rows[i];
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            string GiaBan = dr["GiaBan"].ToString();
                            string SoLuong = dr["SoLuong"].ToString();
                            string ThanhTien = dr["ThanhTien"].ToString();
                            string LyDoDoi = dr["LyDoDoi"].ToString();
                            data.ThemChiTietPhieuKhachHangTraHang(IDThem, IDHangHoa, GiaBan, SoLuong, ThanhTien, LyDoDoi);
                            dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, Session["IDKho"].ToString());// cộng tồn kho
                            // - Số lượng trong hóa đơn

                        }
                        //giảm công nợ khách hàng;
                        dtKhachHang dtkh = new dtKhachHang();
                        dtkh.CapNhatCongNo(IDKhachHang, TongTien);
                        data.XoaChiTiet_Temp(ID);
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu khách hàng trả hàng", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");
                        Response.Redirect("DanhSachKhachHangTraHang.aspx");
                    }
                }
                else
                {
                    Clear();
                    cmbHangHoa.Focus();
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa không được rỗng.'); </script>");
                }
            }
            else
            {
                // giảm công nợ, cộng tồn kho
                DataTable da = data.ChiTietPhieuKhachHangTraHang_Temp(ID);
                if (da.Rows.Count != 0)
                {
                    double TongTien = 0;
                    foreach (DataRow dr in da.Rows)
                    {
                        double ThanhTien = double.Parse(dr["ThanhTien"].ToString());
                        TongTien = TongTien + ThanhTien;
                    }
                    object IDThem = data.ThemPhieuKhachHangTraHang("", IDNhanVien, IDKhachHang, TongTien.ToString(), GhiChu);
                    if (IDThem != null)
                    {
                        for (int i = 0; i < da.Rows.Count; i++)
                        {
                            DataRow dr = da.Rows[i];
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            string GiaBan = dr["GiaBan"].ToString();
                            string SoLuong = dr["SoLuong"].ToString();
                            string ThanhTien = dr["ThanhTien"].ToString();
                            string LyDoDoi = dr["LyDoDoi"].ToString();
                            data.ThemChiTietPhieuKhachHangTraHang(IDThem, IDHangHoa, GiaBan, SoLuong, ThanhTien, LyDoDoi);
                            dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, Session["IDKho"].ToString());// cộng tồn kho
                        }
                        //giảm công nợ khách hàng;
                        if (Int32.Parse(IDKhachHang) != 1)
                        {
                            dtKhachHang dtkh = new dtKhachHang();
                            dtkh.CapNhatCongNo(IDKhachHang, TongTien);
                        }
                        data.XoaChiTiet_Temp(ID);
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu khách hàng trả hàng", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");
                        Response.Redirect("DanhSachKhachHangTraHang.aspx");
                    }
                }
                else
                {
                    Clear();
                    cmbHangHoa.Focus();
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa không được rỗng.'); </script>");
                }
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
                cmbHangHoa.Enabled = true;
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
            if (ckHoaDon.Checked == true && cmbHoaDon.Text != "")
            {
                cmbHangHoa.DataSource = data.DanhSachHangHoa_HoaDon(cmbHoaDon.Value.ToString());
                cmbHangHoa.ValueField = "IDHangHoa";
                cmbHangHoa.DataBind();
            }
            else
            {
                long value = 0;
                if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                    return;
                ASPxComboBox comboBox = (ASPxComboBox)source;
                dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBan, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID)";
                dsHangHoa.SelectParameters.Clear();
                dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
                comboBox.DataSource = dsHangHoa;
                comboBox.DataBind();
            }
        }
        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (ckHoaDon.Checked == true && cmbHoaDon.Text != "")
            {
                cmbHangHoa.DataSource = data.DanhSachHangHoa_HoaDon(cmbHoaDon.Value.ToString());
                cmbHangHoa.ValueField = "IDHangHoa";
                cmbHangHoa.DataBind();
            }
            else
            {
                ASPxComboBox comboBox = (ASPxComboBox)source;
                dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBan] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBan, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang) OR GPM_HangHoa.TenHangHoa LIKE @TenHang)  AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";
                dsHangHoa.SelectParameters.Clear();
                dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
                dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
                dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
                dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
                dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
                comboBox.DataSource = dsHangHoa;
                comboBox.DataBind();
            }
        }
    }
}
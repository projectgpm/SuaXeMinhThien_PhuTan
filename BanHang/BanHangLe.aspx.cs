using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using BanHang.Data;
using System.Data;

namespace BanHang
{
    public partial class BanHangLe1 : System.Web.UI.Page
    {
        public List<HoaDon> DanhSachHoaDon
        {
            get
            {
                if (ViewState["DanhSachHoaDon"] == null)
                    return new List<HoaDon>();
                else
                    return (List<HoaDon>)ViewState["DanhSachHoaDon"];
            }
            set
            {
                ViewState["DanhSachHoaDon"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTBanLe"] == "GPMBanLe")
            {
                //if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                //{
                    if (!IsPostBack)
                    {
                        DanhSachHoaDon = new List<HoaDon>();
                        ThemHoaDonMoi();
                        btnNhanVien.Text = Session["TenThuNgan"].ToString();
                        txtBarcode.Focus();
                    }
                    DanhSachKhachHang();
                //}
                //else
                //{
                //    Response.Redirect("DangNhap.aspx");
                //}
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        public void BindGridChiTietHoaDon()
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            UpdateSTT(MaHoaDon);
            gridChiTietHoaDon.DataSource = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon;
            gridChiTietHoaDon.DataBind();
            formLayoutThanhToan.DataSource = DanhSachHoaDon[MaHoaDon];
            ccbKhachHang.SelectedIndex = DanhSachHoaDon[MaHoaDon].IDKhachHang;
            formLayoutThanhToan.DataBind();
        }

        public void ThemHoaDonMoi()
        {
            HoaDon hd = new HoaDon();
            DanhSachHoaDon.Add(hd);
            Tab tabHoaDonNew = new Tab();
            int SoHoaDon = DanhSachHoaDon.Count;
            tabHoaDonNew.Name = SoHoaDon.ToString();
            tabHoaDonNew.Text = "Hóa đơn " + SoHoaDon.ToString();
            tabHoaDonNew.Index = SoHoaDon - 1;
            tabControlSoHoaDon.Tabs.Add(tabHoaDonNew);
            tabControlSoHoaDon.ActiveTabIndex = SoHoaDon - 1;
            BindGridChiTietHoaDon();
        }
        public void HuyHoaDon()
        {
            txtTienThoi.Text = "0";
            int indexTabActive = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon.RemoveAt(indexTabActive);
            tabControlSoHoaDon.Tabs.RemoveAt(indexTabActive);
            for (int i = 0; i < tabControlSoHoaDon.Tabs.Count; i++)
            {
                tabControlSoHoaDon.Tabs[i].Text = "Hóa đơn " + (i + 1).ToString();
            }
            if (DanhSachHoaDon.Count == 0)
            {
                ThemHoaDonMoi();
            }
            else
            {
                BindGridChiTietHoaDon();
            }
        }
        public void ThemHangVaoChiTietHoaDon(DataTable tbThongTin)
        {
            txtTienThoi.Text = "0"; txtKhachThanhToan.Text = "0";
            string MaHang = tbThongTin.Rows[0]["MaHang"].ToString();
            int IDHangHoa = Int32.Parse(tbThongTin.Rows[0]["ID"].ToString());
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.IDHangHoa == IDHangHoa);
            if (exitHang != null)
            {
                int SoLuong = exitHang.SoLuong + int.Parse(txtSoLuong.Text);
                double ThanhTienOld = exitHang.ThanhTien;
                exitHang.SoLuong = SoLuong;
                exitHang.HinhAnh = tbThongTin.Rows[0]["HinhAnh"].ToString();
                exitHang.DonGia = double.Parse(tbThongTin.Rows[0]["GiaBan"].ToString());
                exitHang.GiaKyThuat = double.Parse(tbThongTin.Rows[0]["GiaBan"].ToString());
                exitHang.TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), Session["IDKho"].ToString());
                exitHang.ThanhTien = SoLuong * exitHang.GiaKyThuat;
                DanhSachHoaDon[MaHoaDon].TongTien += SoLuong * exitHang.DonGia - ThanhTienOld;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien + DanhSachHoaDon[MaHoaDon].TienSuaXe; //- DanhSachHoaDon[MaHoaDon].GiamGia;
            }
            else
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.IDHangHoa = IDHangHoa;
                cthd.MaHang = MaHang;
                cthd.TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), Session["IDKho"].ToString());
                cthd.TenHang = tbThongTin.Rows[0]["TenHangHoa"].ToString();
                cthd.SoLuong = int.Parse(txtSoLuong.Text);
                cthd.DonViTinh = tbThongTin.Rows[0]["TenDonViTinh"].ToString();
                cthd.DonGia = double.Parse(tbThongTin.Rows[0]["GiaBan"].ToString());
                cthd.GiaKyThuat = double.Parse(tbThongTin.Rows[0]["GiaBan"].ToString());
                cthd.GiaMua = double.Parse(tbThongTin.Rows[0]["GiaMua"].ToString());
                cthd.HinhAnh = tbThongTin.Rows[0]["HinhAnh"].ToString();
                cthd.ThanhTien = int.Parse(txtSoLuong.Text) * double.Parse(cthd.GiaKyThuat.ToString());
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Add(cthd);
                DanhSachHoaDon[MaHoaDon].SoLuongHang++;
                DanhSachHoaDon[MaHoaDon].TongTien += cthd.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien + DanhSachHoaDon[MaHoaDon].TienSuaXe;//- DanhSachHoaDon[MaHoaDon].GiamGia;
            }
        }
        protected void btnInsertHang_Click(object sender, EventArgs e)
        {
            try
            {
                dtBanHangLe dt = new dtBanHangLe();
                if (txtBarcode.Text.Trim() != "")
                {
                    DataTable tbThongTin;
                    if (txtBarcode.Value == null)
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Text.ToString(), Session["IDKho"].ToString());
                    }
                    else
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Value.ToString(), Session["IDKho"].ToString());
                    }

                    if (tbThongTin.Rows.Count > 0)
                    {
                        // kiểm tra kho âm
                        string IDKho = Session["IDKho"].ToString();
                        if (dtSetting.KT_BanHang(Session["IDKho"].ToString()) == 1)
                        {
                            //1: bán âm , 0: bán không âm
                            ThemHangVaoChiTietHoaDon(tbThongTin);
                            BindGridChiTietHoaDon();
                        }
                        else
                        {
                            DataRow dr = tbThongTin.Rows[0];
                            int IDHangHoa = Int32.Parse(dr["ID"].ToString());
                            int SLMua = Int32.Parse(txtSoLuong.Text.ToString());
                            int SLTonKhoHienTai = 0;
                            SLTonKhoHienTai = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), IDKho);
                            // lấy sl có trong lưới
                            int MaHang = int.Parse(tbThongTin.Rows[0]["MaHang"].ToString());
                            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.IDHangHoa == IDHangHoa);
                            if (exitHang != null)
                            {
                                int SoLuong = exitHang.SoLuong;
                                SLTonKhoHienTai = SLTonKhoHienTai - SoLuong;
                            }
                            if (SLTonKhoHienTai >= SLMua)
                            {
                                ThemHangVaoChiTietHoaDon(tbThongTin);
                                BindGridChiTietHoaDon();
                            }
                            else
                            {
                                txtSoLuong.Text = SLTonKhoHienTai + "";
                                HienThiThongBao("Số lượng tồn kho không đủ bán? Vui lòng liên hệ kho tổng.");
                            }
                        }
                    }
                    else
                        HienThiThongBao("Mã hàng không tồn tại!!");
                }
                txtBarcode.Focus();
                txtBarcode.Text = "";
                txtBarcode.Value = "";
                txtSoLuong.Text = "1";
            }
            catch (Exception ex)
            {
                HienThiThongBao("Error: " + ex);
            }
        }
        public void HienThiThongBao(string thongbao)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + thongbao + "');", true);
        }
        protected void btnUpdateGridHang_Click(object sender, EventArgs e)
        {
            BatchUpdate();
            BindGridChiTietHoaDon();
        }
        private void BatchUpdate()
        {
            txtTienThoi.Text = "0"; txtKhachThanhToan.Text = "0";
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            string IDKho = Session["IDKho"].ToString();
            for (int i = 0; i < gridChiTietHoaDon.VisibleRowCount; i++)
            {
                object SoLuong = gridChiTietHoaDon.GetRowValues(i, "SoLuong");
                object GiaKyThuat = gridChiTietHoaDon.GetRowValues(i, "GiaKyThuat");
                ASPxSpinEdit spineditSoLuong = gridChiTietHoaDon.FindRowCellTemplateControl(i, (GridViewDataColumn)gridChiTietHoaDon.Columns["SoLuong"], "txtSoLuongChange") as ASPxSpinEdit;
                ASPxSpinEdit spineditGiaKyThuat = gridChiTietHoaDon.FindRowCellTemplateControl(i, (GridViewDataColumn)gridChiTietHoaDon.Columns["GiaKyThuat"], "txtGiaKyThuatChange") as ASPxSpinEdit;
                object SoLuongMoi = spineditSoLuong.Value;
                object GiaMoi = spineditGiaKyThuat.Value;
                if (SoLuong != SoLuongMoi || GiaKyThuat != GiaMoi)
                {
                    if (dtSetting.KT_BanHang(IDKho) == 1)
                    {
                        //bán âm
                        int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                        var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                        int SoLuongOld = exitHang.SoLuong;
                        double ThanhTienOld = exitHang.ThanhTien;
                        exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                        exitHang.GiaKyThuat = double.Parse(spineditGiaKyThuat.Value.ToString());
                        exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.GiaKyThuat;
                        DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                        DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                    }
                    else
                    {
                        //không bán âm
                        int MaHang = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "MaHang"));
                        dtHangHoa dtt = new dtHangHoa();
                        int IDHangHoa = Int32.Parse(dtHangHoa.LayIDHangHoa_MaHang(MaHang + ""));
                        int SLTonKhoHienTai = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), IDKho);
                        if (SLTonKhoHienTai >= Int32.Parse(SoLuongMoi + ""))
                        {
                            int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                            int SoLuongOld = exitHang.SoLuong;
                            double ThanhTienOld = exitHang.ThanhTien;
                            exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                            exitHang.GiaKyThuat = double.Parse(spineditGiaKyThuat.Value.ToString());
                            exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                            DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                        }
                        else
                        {
                            txtSoLuong.Text = SLTonKhoHienTai + "";
                            HienThiThongBao("Số lượng tồn kho không đủ bán? Vui lòng liên hệ kho tổng.");
                        }
                    }
                }
            }
        }

        protected void txtKhachThanhToan_TextChanged(object sender, EventArgs e)
        {

            float TienKhachThanhToan;
            bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
            if (!isNumeric)
            {
                txtKhachThanhToan.Text = "";
                txtKhachThanhToan.Focus();
                HienThiThongBao("Nhập không đúng số tiền !!"); return;
            }
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon[MaHoaDon].KhachThanhToan = TienKhachThanhToan;
            DanhSachHoaDon[MaHoaDon].TienThua = TienKhachThanhToan - DanhSachHoaDon[MaHoaDon].KhachCanTra;
            txtTienThoi.Text = DanhSachHoaDon[MaHoaDon].TienThua.ToString();
        }

        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            try
            {
                int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                int STT = Convert.ToInt32(((ASPxButton)sender).CommandArgument);
                var itemToRemove =  DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Single(r => r.STT == STT);
                DanhSachHoaDon[MaHoaDon].SoLuongHang--;
                DanhSachHoaDon[MaHoaDon].TongTien = DanhSachHoaDon[MaHoaDon].TongTien - itemToRemove.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien + DanhSachHoaDon[MaHoaDon].TienSuaXe;
                txtTienThoi.Text = "0";
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Remove(itemToRemove);
                BindGridChiTietHoaDon();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        protected void UpdateSTT(int MaHoaDon)
        {
            for (int i = 1; i <= DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count; i++)
            {
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon[i - 1].STT = i;
            }
        }
        protected void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            HuyHoaDon();
        }

        protected void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            ThemHoaDonMoi();
        }

        protected void tabControlSoHoaDon_ActiveTabChanged(object source, TabControlEventArgs e)
        {
            BindGridChiTietHoaDon();
        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            if (DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count > 0)
            {
                float TienKhachThanhToan;
                bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
                if (!isNumeric)
                {
                    HienThiThongBao("Nhập không đúng số tiền !!"); return;
                }

                DanhSachHoaDon[MaHoaDon].KhachThanhToan = TienKhachThanhToan;
                dtBanHangLe dt = new dtBanHangLe();
                string IDKho = Session["IDKho"].ToString();
                string IDNhanVien = Session["IDThuNgan"].ToString();
                int IDKhachHang = 1, IDKyThuat = 1;
                if (ccbKhachHang.Value != null)
                    IDKhachHang = Int32.Parse(ccbKhachHang.Value.ToString());
                if (cmbKyThuat.Value != null)
                    IDKyThuat = Int32.Parse(cmbKyThuat.Value.ToString());
                if (IDKhachHang == 1)// khách lẻ
                {
                    if (TienKhachThanhToan < DanhSachHoaDon[MaHoaDon].KhachCanTra)
                    {
                        txtKhachThanhToan.Text = "";
                        txtKhachThanhToan.Focus();
                        HienThiThongBao("Thanh toán chưa đủ số tiền !!"); return;
                    }
                    if (IDKyThuat != 1)
                    {
                        // có kỹ thuật
                        int TyLeChietKhauKyThuat = dtNhanVienKyThuat.TyLeChietKhauKyThuat(cmbKyThuat.Value.ToString());
                        double TienHuong = 0, TienHeThong = 0;
                        for (int i = 0; i < gridChiTietHoaDon.VisibleRowCount; i++)
                        {
                            int SoLuong = Int32.Parse(gridChiTietHoaDon.GetRowValues(i, "SoLuong").ToString());
                            double TongTienGiaGoc = double.Parse(gridChiTietHoaDon.GetRowValues(i, "DonGia").ToString());
                            TienHeThong = TienHeThong + (double.Parse(gridChiTietHoaDon.GetRowValues(i, "DonGia").ToString()) * SoLuong);
                            double TongTienBaoGia = double.Parse(gridChiTietHoaDon.GetRowValues(i, "GiaKyThuat").ToString());
                            TienHuong = TienHuong + ((TongTienBaoGia - TongTienGiaGoc) * SoLuong);
                        }
                        double TienChietKhau = TienHeThong * TyLeChietKhauKyThuat / (float)100;
                        double TongThucNhan = TienChietKhau + TienHuong;// cộng tiền vào công nợ kỹ thuật
                        object IDHoaDon = dt.InsertHoaDon(IDKho, IDNhanVien, IDKhachHang.ToString(), DanhSachHoaDon[MaHoaDon], IDKyThuat.ToString(), TongThucNhan.ToString(), "0", "0", "0", TyLeChietKhauKyThuat.ToString(), "1", "0", "0");
                        HuyHoaDon();
                        ccbKhachHang.Text = "";
                        cmbKyThuat.Text = "";

                        chitietbuilInLai.ContentUrl = "~/InPhieuGiaoHang.aspx?IDHoaDon=" + IDHoaDon + "&KT=" + 1;
                        chitietbuilInLai.ShowOnPageLoad = true;

                        txtBarcode.Focus();
                    }
                    else
                    {
                        //không có kỹ thuật, CK 0%// 
                        // không cộng tổng tiền cho kỹ thuật
                        object IDHoaDon = dt.InsertHoaDon(IDKho, IDNhanVien, IDKhachHang.ToString(), DanhSachHoaDon[MaHoaDon], IDKyThuat.ToString(), "0", "0", "0", "0", "0", "1", "0", "0");
                        HuyHoaDon();
                        ccbKhachHang.Text = "";
                        cmbKyThuat.Text = "";

                        chitietbuilInLai.ContentUrl = "~/InPhieuGiaoHang.aspx?IDHoaDon=" + IDHoaDon + "&KT=" + 1;
                        chitietbuilInLai.ShowOnPageLoad = true;

                        txtBarcode.Focus();
                    }
                }
                else// khách sỉ
                {
                    // tính chiết khấu khách sỉ
                    int TyLeChietKhauKhachHang = dtKhachHang.TyLeChietKhauKhachHang(IDKhachHang.ToString());
                    // nếu tiền chiết khấu lưu trong hóa đơn, tổng tiền còn lại thì cập nhật vào công nợ khách hàng
                    double CongNoCu = dtKhachHang.LayCongNoCuKhachHang(IDKhachHang.ToString());
                    double TongTienKhachHang = DanhSachHoaDon[MaHoaDon].KhachThanhToan - DanhSachHoaDon[MaHoaDon].KhachCanTra;//
                    double ChietKhauKhachHang = DanhSachHoaDon[MaHoaDon].TongTien * (TyLeChietKhauKhachHang / (float)100);
                    double CongNoMoi = CongNoCu + (TongTienKhachHang * -1);
                    object IDHoaDon = dt.InsertHoaDon(IDKho, IDNhanVien, IDKhachHang.ToString(), DanhSachHoaDon[MaHoaDon], IDKyThuat.ToString(), "0", ChietKhauKhachHang.ToString(), (TongTienKhachHang * -1).ToString(), TyLeChietKhauKhachHang.ToString(), "0", "0", CongNoCu.ToString(), CongNoMoi.ToString());
                    HuyHoaDon();
                    ccbKhachHang.Text = "";
                    cmbKyThuat.Text = "";

                    chitietbuilInLai.ContentUrl = "~/InPhieuGiaoHang.aspx?IDHoaDon=" + IDHoaDon + "&KT=" + 0;
                    chitietbuilInLai.ShowOnPageLoad = true;

                    txtBarcode.Focus();
                }
            }
            else
            {
                HienThiThongBao("Danh sách hàng hóa trống !!!");
                txtBarcode.Focus();
            }
        }
      
        protected void btnHuyKhachHang_Click(object sender, EventArgs e)
        {
            popupThemKhachHang.ShowOnPageLoad = false;
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            txtTenKhachHang.Text = "";
            cmbNhomKhachHang.Text = "";
            txtSoDienThoai.Text = "";
            txtDiaChi.Text = "";
            cmbChietKhau.Text = "";
            popupThemKhachHang.ShowOnPageLoad = true;
        }

        protected void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            if (cmbNhomKhachHang.Text != "" && txtTenKhachHang.Text != "" && cmbChietKhau.Text !="")
            {
                int IDNhom = Int32.Parse(cmbNhomKhachHang.Value.ToString());
                string TenKH = txtTenKhachHang.Text;
                string SDT = txtSoDienThoai.Text == null ? "" : txtSoDienThoai.Text;
                string DC = txtDiaChi.Text == null ? "" : txtDiaChi.Text;
                string IDChietKhau = cmbChietKhau.Value.ToString();
                dtKhachHang dtkh = new dtKhachHang();
                string MaKh = "";
                string Barcode = "";
                object ID = dtkh.ThemKhachHang(IDNhom, MaKh, TenKH, DateTime.Now, "", DC, SDT, "", Barcode, "", Session["IDKho"].ToString(), IDChietKhau);
                if (ID != null)
                {
                    dtkh = new dtKhachHang();
                    dtkh.CapNhatMaKhachHang(ID, (Session["IDKho"].ToString() + "." + ID).ToString(), (Session["IDKho"].ToString() + "." + ID).Replace(".", ""));
                }
                DanhSachKhachHang();
                txtTenKhachHang.Text = "";
                cmbNhomKhachHang.Text = "";
                txtSoDienThoai.Text = "";
                txtDiaChi.Text = "";
                cmbChietKhau.Text = "";
                HienThiThongBao("Thêm khách hàng thành công !!");
                popupThemKhachHang.ShowOnPageLoad = false; return;
            }
            else
            {
                HienThiThongBao("Vui lòng nhập thông tin đầy đủ (*) !!"); return;
            }
        }
        public void DanhSachKhachHang()
        {
            dtKhachHang dtkh = new dtKhachHang();
            ccbKhachHang.DataSource = dtkh.LayDanhSachKhachHang(Session["IDKho"].ToString());
            ccbKhachHang.TextField = "TenKhachHang";
            ccbKhachHang.ValueField = "ID";
            ccbKhachHang.DataBind();
        }

        protected void txtBarcode_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBan] , [TenDonViTinh],[HinhAnh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang,GPM_HangHoa.HinhAnh, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBan, GPM_DonViTinh.TenDonViTinh, 
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

        protected void txtBarcode_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang,GPM_HangHoa.HinhAnh, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBan, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID)";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void ccbKhachHang_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            sqlKhachHang.SelectCommand = @"SELECT ID,TenKhachHang,DienThoai,DiaChi
                                        FROM (
	                                            select ID,TenKhachHang, DienThoai,DiaChi, row_number()over(order by MaKhachHang) as [rn] 
	                                            FROM GPM_KhachHang
	                                            WHERE ((TenKhachHang LIKE @TenKhachHang) OR (DienThoai LIKE @DienThoai) OR (DiaChi LIKE @DiaChi)) AND (IDKho = @IDKho) AND (DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";
            sqlKhachHang.SelectParameters.Clear();
            sqlKhachHang.SelectParameters.Add("TenKhachHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("DienThoai", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("DiaChi", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            sqlKhachHang.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            sqlKhachHang.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = sqlKhachHang;
            comboBox.DataBind();
        }
        

        protected void ccbKhachHang_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            sqlKhachHang.SelectCommand = @"SELECT ID,TenKhachHang,DienThoai,DiaChi
                                        FROM GPM_KhachHang
                                        WHERE (GPM_KhachHang.ID = @ID)";
            sqlKhachHang.SelectParameters.Clear();
            sqlKhachHang.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = sqlKhachHang;
            comboBox.DataBind();
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "")
            {
                string TuKhoa = txtTimKiem.Text.ToString();
                dtBanHangLe dt = new dtBanHangLe();
                DataTable db = dt.LayThongHoaDon(TuKhoa);
                if (db.Rows.Count > 0)
                {
                    string IDKH = 1 + "";
                    if (Int32.Parse(db.Rows[0]["IDKhachHang"].ToString()) != 1)
                        IDKH = 0 + "";

                    chitietbuilInLai.ContentUrl = "~/InPhieuGiaoHang.aspx?IDHoaDon=" + db.Rows[0]["ID"].ToString() + "&KT=" + IDKH;
                    chitietbuilInLai.ShowOnPageLoad = true;
                }
                else
                {
                    txtTimKiem.Focus();
                    HienThiThongBao("Không tìm thấy dữ liệu cần tìm?");
                }
            }
            else
            {
                txtTimKiem.Focus();
                HienThiThongBao("Vui lòng nhập thông tin cần tìm?");
            }
        }

        protected void cmbKyThuat_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            sqlKyThuat.SelectCommand = @"SELECT ID,TenKyThuat,DienThoai,DiaChi
                                        FROM GPM_KyThuat
                                        WHERE (GPM_KyThuat.ID = @ID)";
            sqlKyThuat.SelectParameters.Clear();
            sqlKyThuat.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = sqlKyThuat;
            comboBox.DataBind();
        }

        protected void cmbKyThuat_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            sqlKyThuat.SelectCommand = @"SELECT ID,TenKyThuat,DienThoai,DiaChi
                                        FROM (
	                                            select ID,TenKyThuat, DienThoai,DiaChi, row_number()over(order by ID) as [rn] 
	                                            FROM GPM_KyThuat
	                                            WHERE ((TenKyThuat LIKE @TenKyThuat) OR (DienThoai LIKE @DienThoai))  AND (DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";
            sqlKyThuat.SelectParameters.Clear();
            sqlKyThuat.SelectParameters.Add("TenKyThuat", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKyThuat.SelectParameters.Add("DienThoai", TypeCode.String, string.Format("%{0}%", e.Filter));

            sqlKyThuat.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            sqlKyThuat.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = sqlKyThuat;
            comboBox.DataBind();
        }

        protected void ccbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ccbKhachHang.Text != "")
            {
                int ID = Int32.Parse(ccbKhachHang.Value.ToString());
                if (ID == 1)//khách lẻ
                {
                    cmbKyThuat.Enabled = true;
                    txtTienSuaXe.Enabled = true;
                    cmbKyThuat.Text = "";
                    txtTienSuaXe.Text = "0"; txtTienThoi.Text = "0"; txtKhachThanhToan.Text = "0";

                    int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                    DanhSachHoaDon[MaHoaDon].IDKhachHang = Int32.Parse(ccbKhachHang.SelectedIndex + "");
                }
                else// #khách lẻ
                {
                    txtKhachThanhToan.Text = "0";
                    cmbKyThuat.Text = "";
                    txtTienSuaXe.Text = "0";
                    cmbKyThuat.Enabled = false;
                    txtTienSuaXe.Enabled = false; txtTienThoi.Text = "0";

                    int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                    DanhSachHoaDon[MaHoaDon].IDKhachHang = Int32.Parse(ccbKhachHang.SelectedIndex + "");
                }
            }
        }

        protected void txtTienSuaXe_TextChanged(object sender, EventArgs e)
        {
            float TienSuaXe;
            bool isNumeric = float.TryParse(txtTienSuaXe.Text, out TienSuaXe);
            if (!isNumeric)
            {
                txtTienSuaXe.Text = "";
                txtTienSuaXe.Focus();
                HienThiThongBao("Nhập không đúng số tiền !!"); return;
            }
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon[MaHoaDon].TienSuaXe = TienSuaXe;
            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien + DanhSachHoaDon[MaHoaDon].TienSuaXe;
            DanhSachHoaDon[MaHoaDon].TienThua = DanhSachHoaDon[MaHoaDon].KhachCanTra;
            txtKhachCanTra.Text = DanhSachHoaDon[MaHoaDon].KhachCanTra.ToString();
            txtTienThoi.Text = "0";
        }

       
    }
    [Serializable]
    public class HoaDon
    {
        public int IDHoaDon { get; set; }
        public int IDKhachHang { get; set; }
        public int SoLuongHang { get; set; }
        public double TongTien { get; set; }
        public double GiamGia { get; set; }
        public double KhachCanTra { get; set; }
        public double KhachThanhToan { get; set; }
        public double TienThua { get; set; }
        public double TienSuaXe { get; set; }
        public List<ChiTietHoaDon> ListChiTietHoaDon { get; set; }
        public HoaDon()
        {
            TienSuaXe = 0;
            SoLuongHang = 0;
            TongTien = 0;
            GiamGia = 0;
            KhachCanTra = 0;
            KhachThanhToan = 0;
            TienThua = 0;
            ListChiTietHoaDon = new List<ChiTietHoaDon>();
        }
    }
    [Serializable]
    public class ChiTietHoaDon
    {
        public int STT { get; set; }
        public string MaHang { get; set; }
        public int IDHangHoa { get; set; }
        public string TenHang { get; set; }
        public int MaDonViTinh { get; set; }
        public int TonKho { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double GiaMua { get; set; }
        public double ThanhTien { get; set; }
        public double GiaKyThuat { get; set; }
        public string HinhAnh { get; set; }
        public ChiTietHoaDon()
        {
            TonKho = 0;
            SoLuong = 0;
            DonGia = 0;
            ThanhTien = 0;
        }
    }
}
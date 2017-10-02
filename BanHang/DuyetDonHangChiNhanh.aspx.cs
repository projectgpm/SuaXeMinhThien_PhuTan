using BanHang.Data;
using DevExpress.Web;
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
    public partial class DuyetDonHangChiNhanh : System.Web.UI.Page
    {
        dtDuyetDonHangChiNhanh data = new dtDuyetDonHangChiNhanh();
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
                    //data = new dtDuyetDonHangChiNhanh();
                    //data.Xoa_IDNhanVien(Session["IDNhanVien"].ToString());
                    data = new dtDuyetDonHangChiNhanh();
                    data.Xoa_Temp_ID(Session["IDNhanVien"].ToString());
                    txtNguoiDuyet.Text = Session["TenDangNhap"].ToString();
                    cmbChiNhanhDuyet.Value = Session["IDKho"].ToString();
                    cmbHangHoa.Enabled = false;
                    txtGhiChuHangHoa.Enabled = false;
                    btnThem_Temp.Enabled = false;
                    txtSoLuong.Enabled = false;
                }
                if (cmbSoDonHang.Text != "")
                {
                    string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                    string IDDonHangChiNhanh = cmbSoDonHang.Value.ToString();
                    LoadDanhSach(IDDonHangChiNhanh, IDTemp);
                }
            }
        }
        public void LoadDanhSach(string SoDonHang, string IDTemp)
        {
            data = new dtDuyetDonHangChiNhanh();
            gridDanhSachHangHoa.DataSource = data.DanhSachChiTiet_Temp(SoDonHang, IDTemp, Session["IDKho"].ToString());
            gridDanhSachHangHoa.DataBind();
        }
        protected void txtNgayDuyet_Init(object sender, EventArgs e)
        {
            txtNgayDuyet.Date = DateTime.Today;
        }

        protected void gridDanhSachHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                string ID = e.Keys[0].ToString();
                data = new dtDuyetDonHangChiNhanh();
                // lây trạng thái = 1 mới được xóa
                int TrangThai  = dtDuyetDonHangChiNhanh.LayTrangThai(ID);
                if (TrangThai == 1)
                {
                    data.Xoa_Temp(ID);
                }
                else
                {
                    throw new Exception("Lỗi: Chỉ xóa những cái Kho mới thêm? Vui lòng kiểm tra lại.  ");
                }
                e.Cancel = true;
                gridDanhSachHangHoa.CancelEdit();
                LoadDanhSach(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
            }
        }

        protected void gridDanhSachHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                if (e.NewValues["ThucTe"] == null)
                {
                    throw new Exception("Lỗi: Số lượng thực tế không được bỏ trống. ");
                }
                else
                {
                    string ID = e.Keys[0].ToString();
                    int SoLuongThucTe = Int32.Parse(e.NewValues["ThucTe"].ToString());
                    int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
                    int TonKhoTong = Int32.Parse(e.NewValues["TonKhoTong"].ToString());
                     string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                    if (SoLuongThucTe >= 0)
                    {
                        if (SoLuongThucTe > SoLuong)
                        {
                            throw new Exception("Lỗi: Số lượng thực tế phải nhỏ hơn hoặc bằng Số lượng đặt ");
                        }
                        else
                        {
                            string MaHang = e.NewValues["MaHang"].ToString();
                            string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                            data = new dtDuyetDonHangChiNhanh();
                           
                            data.CapNhatChiTietDonHang(ID, IDHangHoa, IDTemp, SoLuongThucTe, SoLuong - SoLuongThucTe);
                        }
                        if (SoLuongThucTe > TonKhoTong)
                        {
                            // cập nhật ghi chú
                            string GhiChu = e.NewValues["GhiChu"].ToString();
                            string aa = ", Tồn kho âm";
                            data = new dtDuyetDonHangChiNhanh();
                            data.CapNhatGhiChu(ID, IDTemp, (GhiChu + aa).ToString());
                        }
                        else
                        {
                            string GhiChu = e.NewValues["GhiChu"].ToString();
                            string aa = ", Tồn kho âm";
                            data = new dtDuyetDonHangChiNhanh();
                            data.CapNhatGhiChu(ID, IDTemp, (GhiChu).ToString().Replace(aa,""));
                        }
                    }
                    else
                    {
                        throw new Exception("Lỗi: Số lượng không được âm? Vui lòng kiểm tra lại. ");
                    }
                    e.Cancel = true;
                    gridDanhSachHangHoa.CancelEdit();
                    LoadDanhSach(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
                }
            }
        }
       
        public double TinhTrongLuong()
        {
            if (cmbSoDonHang.Text != "")
            {
                data = new dtDuyetDonHangChiNhanh();
                DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString(), Session["IDKho"].ToString());
                if (db.Rows.Count != 0)
                {
                    double Tong = 0;
                    foreach (DataRow dr in db.Rows)
                    {
                        double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                        Tong = Tong + (TrongLuong);
                    }
                    return Tong;
                }
                else
                    return 0;
            }
            return 0;
        }
        protected void cmbSoDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                data = new dtDuyetDonHangChiNhanh();
                cmbHangHoa.Enabled = true;
                txtGhiChuHangHoa.Enabled = true;
                btnThem_Temp.Enabled = true;
                txtSoLuong.Enabled = true;
                data.Xoa_Temp_ID(IDDonHangDuyet_Temp.Value.ToString());
                ///Random ran = new Random();
               // int Temp = Int32.Parse(Session["IDNhanVien"].ToString());//ran.Next(100000, 999999);
                IDDonHangDuyet_Temp.Value = Session["IDNhanVien"].ToString();
                string ID = cmbSoDonHang.Value.ToString();
                DataTable db = data.LayDanhSachDonHang_ID(ID);
                if (db.Rows.Count > 0)
                {
                    DataRow dr = db.Rows[0];
                    cmbNguoiLap.Value = dr["IDNguoiLap"].ToString();
                    txtNgayDatHang.Date = DateTime.Parse(dr["NgayDat"].ToString());
                    cmbChiNhanhLap.Value = dr["IDKho"].ToString();
                    txtGhiChu.Text = dr["GhiChu"].ToString();
                    
                    string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                    int TrangThaiDonHang = Int32.Parse(dr["IDTrangThaiDonHang"].ToString());
                    if (TrangThaiDonHang != 4)
                    {
                        //đơn hàng chưa xử lý lần nào
                        DataTable dt = data.DanhSachChiTiet(ID);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                string MaHang = dr1["MaHang"].ToString();
                                string IDHangHoa = dr1["IDHangHoa"].ToString();
                                string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                                string TrongLuong = dr1["TrongLuong"].ToString();
                                string SoLuong = dr1["SoLuong"].ToString();
                                string TonKho = dr1["TonKho"].ToString();
                                string GhiChu = dr1["GhiChu"].ToString();
                                string TrangThai = dr1["TrangThai"].ToString();
                                string IDDonHangChiNhanh = dr1["IDDonHangChiNhanh"].ToString();
                                string IDKho = dr1["IDKho"].ToString();
                                data = new dtDuyetDonHangChiNhanh();
                                DataTable dbt = data.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh, IDTemp);
                                if (dbt.Rows.Count == 0)
                                {
                                    data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, TonKho, GhiChu, TrangThai, IDKho, IDTemp, SoLuong);
                                }
                            }
                            LoadDanhSach(ID, IDTemp);
                        }
                        else
                        {
                            Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa trống.'); </script>");
                            return;
                        }
                    }
                    else
                    {
                        //đơn hàng đã xử lý 1 phần. lấy dữ liệu trong bảng log
                        data = new dtDuyetDonHangChiNhanh();
                        DataTable dt = data.DanhSachChiTiet_LOG(cmbSoDonHang.Text.ToString().Trim(), cmbSoDonHang.Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                string MaHang = dr1["MaHang"].ToString();
                                string IDHangHoa = dr1["IDHangHoa"].ToString();
                                string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                                string TrongLuong = dr1["TrongLuong"].ToString();
                                string SoLuong = dr1["SoLuong"].ToString();
                                string GhiChuHH = dr1["GhiChu"].ToString();
                                data = new dtDuyetDonHangChiNhanh();
                                data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, "0", GhiChuHH, "0", "1", IDTemp, SoLuong);
                            }
                            LoadDanhSach(ID, IDTemp);
                        }
                        else
                        {
                            Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa trống.'); </script>");
                        }
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Thông tin đơn hàng rỗng.'); </script>");
                    return;
                }
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbTrangThaiDonHang.Text != "" && txtNgayGiao.Text != "")
            {
                if (dtSetting.KT_ChuyenAm() == 1) // kiểm tra âm kho
                {
                    XuLyDuLieu();
                }
                else
                {
                    int kt = 0;
                    string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                    DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDTemp, Session["IDKho"].ToString());
                    foreach (DataRow dr1 in db.Rows)
                    {
                        string IDHangHoa = dr1["IDHangHoa"].ToString();
                        int SoLuong = Int32.Parse(dr1["ThucTe"].ToString());
                        int SLTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
                        if (SoLuong > SLTon)
                        {
                            kt = 1;
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Lỗi: Số lượng tồn kho không đủ:" + dtHangHoa.LayTenHangHoa(IDHangHoa) + "');", true);
                            return;
                        }
                    }
                    if (kt == 0)
                    {
                        XuLyDuLieu();
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Lỗi: Vui lòng nhập thông tin các trường có dấu (*)');", true);
                return;
            }
        }
        public void XuLyDuLieu()
        {
            if (cmbSoDonHang.Text != "")
            {
                string IDDonHang = cmbSoDonHang.Value.ToString();
                string SoDonHang = cmbSoDonHang.Text.Trim();
                string IDNguoiLap = cmbNguoiLap.Value.ToString();
                DateTime NgayDat = DateTime.Parse(txtNgayDatHang.Text);
                string IDNguoiDuyet = Session["IDNhanVien"].ToString();
                DateTime NgayDuyet = DateTime.Parse(txtNgayDuyet.Text);
                DateTime NgayGiao = DateTime.Parse(txtNgayGiao.Text.ToString());
                int TrangThaiXuLu = Int32.Parse(cmbTrangThaiDonHang.Value.ToString());
                string IDKhoLap = cmbChiNhanhLap.Value.ToString();
                string IDKhoDuyet = cmbChiNhanhDuyet.Value.ToString();
                string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                string TongTrongLuong = TinhTrongLuong().ToString();
                string ChungTu = "";
                if (Page.IsValid && uploadfile.HasFile)
                {
                    ChungTu = "ChungTu/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + uploadfile.FileName;
                    string filePath = MapPath(ChungTu);
                    uploadfile.SaveAs(filePath);
                }
                string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                if (TrangThaiXuLu != 4)
                {
                    // đơn hàng k treo. xử lý hết 1 lượt
                    data = new dtDuyetDonHangChiNhanh();
                    object ID = data.ThemDuyetDonHang(IDDonHang, SoDonHang, IDNguoiLap, NgayDat, IDNguoiDuyet, NgayDuyet, NgayGiao, TrangThaiXuLu.ToString(), IDKhoLap, IDKhoDuyet, GhiChu, TongTrongLuong, ChungTu);
                    if (ID != null)
                    {
                        DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDTemp, Session["IDKho"].ToString());
                        data = new dtDuyetDonHangChiNhanh();
                        foreach (DataRow dr1 in db.Rows)
                        {
                            int kt = 0;
                            string MaHang = dr1["MaHang"].ToString();
                            string IDHangHoa = dr1["IDHangHoa"].ToString();
                            string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                            string TrongLuong = dr1["TrongLuong"].ToString();
                            string SoLuong = dr1["SoLuong"].ToString();
                           
                            string TrangThai = dr1["TrangThai"].ToString();
                            string GhiChuHH = dr1["GhiChu"].ToString();
                            string ChenhLech = dr1["ChenhLech"].ToString();
                            string ThucTe = dr1["ThucTe"].ToString();
                            string IDKho = dr1["IDKho"].ToString();
                            data = new dtDuyetDonHangChiNhanh();
                            // trừ tồn kho tổng

                            
                            // xử lý hàng sẽ cho cửa hàng trước thấy và xác nhận. if không xác nhận thì đơn hàng tự đổi trạng thái và hàng vào kho, trừ kho tổng
                            data.ThemChiTietDonHang_Duyet(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, TrangThai, GhiChuHH, IDKho, ChenhLech, ThucTe);
                            object TheKho = dtTheKho.ThemTheKho(SoDonHang, "Xử Lý Đơn Hàng Chi Nhánh " + dtTheKho.LayTenKho_ID(IDKhoLap), "0", ThucTe, (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString()) - Int32.Parse(ThucTe)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Xuất", "0", "0", "0");
                            if (TheKho != null)
                            {
                                dtCapNhatTonKho.TruTonKho(IDHangHoa, ThucTe, Session["IDKho"].ToString());
                            }
                            if (Int32.Parse(SoLuong) != Int32.Parse(ThucTe))
                            {
                                kt = 1;
                            }
                            if (kt == 1)
                            {
                                data = new dtDuyetDonHangChiNhanh();
                                //cho biết đơn hàng xử lý có chênh lệch hay không
                                data.CapNhatChenhLech(ID);
                            }
                            DataTable LOG = data.KiemTra_LOG(SoDonHang, IDHangHoa, IDDonHang);
                            if (LOG.Rows.Count != 0)
                            {
                                // đơn hàng đang xử lý hoàn tất, nếu đơn hàng đang treo thì xóa đơn hàng đó.
                                data = new dtDuyetDonHangChiNhanh();
                                data.Xoa_LOG(SoDonHang, IDHangHoa, IDDonHang);
                            }
                        }
                        data = new dtDuyetDonHangChiNhanh();
                        data.CapNhatTrangThaiClient(cmbSoDonHang.Value.ToString(), TrangThaiXuLu);
                        data.Xoa_Temp_ID(IDTemp);
                        Response.Redirect("DonDatHangChiNhanh.aspx");
                        return;
                        // dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt");
                    }
                }
                else
                {
                    // đơn hàng treo, xử lý 1 phần, lưu vào bàng GPM_Log_DuyetHangChiNhanh
                    data = new dtDuyetDonHangChiNhanh();
                    object ID = data.ThemDuyetDonHang(IDDonHang, SoDonHang, IDNguoiLap, NgayDat, IDNguoiDuyet, NgayDuyet, NgayGiao, TrangThaiXuLu.ToString(), IDKhoLap, IDKhoDuyet, GhiChu, TongTrongLuong, ChungTu);
                    if (ID != null)
                    {
                        DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDTemp, Session["IDKho"].ToString());
                        data = new dtDuyetDonHangChiNhanh();
                        foreach (DataRow dr1 in db.Rows)
                        {
                            int kt = 0;
                            string MaHang = dr1["MaHang"].ToString();
                            string IDHangHoa = dr1["IDHangHoa"].ToString();
                            string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                            string TrongLuong = dr1["TrongLuong"].ToString();
                            string SoLuong = dr1["SoLuong"].ToString();
                            string TrangThai = dr1["TrangThai"].ToString();
                            string GhiChuHH = dr1["GhiChu"].ToString();
                            string ChenhLech = dr1["ChenhLech"].ToString();
                            string ThucTe = dr1["ThucTe"].ToString();
                            string IDKho = dr1["IDKho"].ToString();
                            if (Int32.Parse(SoLuong) != Int32.Parse(ThucTe))
                            {
                                kt = 1;
                            }
                            if (kt == 1)
                            {
                                data = new dtDuyetDonHangChiNhanh();
                                //cho biết đơn hàng xử lý có chênh lệch hay không
                                data.CapNhatChenhLech(ID);
                            }

                            data = new dtDuyetDonHangChiNhanh();
                            //trừ tồn kho tổng
                            // xử lý hàng sẽ cho cửa hàng trước thấy và xác nhận. if không xác nhận thì đơn hàng tự đổi trạng thái và hàng vào kho, trừ kho tổng
                            data.ThemChiTietDonHang_Duyet(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, TrangThai, GhiChuHH, IDKho, ChenhLech, ThucTe);
                            object TheKho = dtTheKho.ThemTheKho(SoDonHang, "Xử Lý Đơn Hàng Chi Nhánh " + dtTheKho.LayTenKho_ID(IDKhoLap), "0", ThucTe, (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString()) - Int32.Parse(ThucTe)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Xuất", "0", "0", "0");
                            if (TheKho != null)
                            {
                                dtCapNhatTonKho.TruTonKho(IDHangHoa, ThucTe, Session["IDKho"].ToString());
                            }

                            DataTable LOG = data.KiemTra_LOG(SoDonHang, IDHangHoa, IDDonHang);
                            //thêm dữ liệu vào bảng log
                            if (LOG.Rows.Count == 0 && Int32.Parse(TrangThai) == 0)
                            {
                                data = new dtDuyetDonHangChiNhanh();
                                data.ThemChiTietDonHang_LOG(SoDonHang, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, ChenhLech, IDDonHang, GhiChuHH);
                            }
                            else
                            {
                                // nếu tồn tại cập nhật số lượng lại
                                data = new dtDuyetDonHangChiNhanh();
                                data.CapNhatChiTietDonHang_LOG(SoDonHang, IDHangHoa, ChenhLech, IDDonHang);
                            }
                        }
                        data = new dtDuyetDonHangChiNhanh();
                        data.CapNhatTrangThaiClient(cmbSoDonHang.Value.ToString(), TrangThaiXuLu);
                        data.Xoa_Temp_ID(IDTemp);
                        Response.Redirect("DonDatHangChiNhanh.aspx");
                        return;
                        //        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt");
                    }
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn số đơn hàng để thực hiện thao tác? '); </script>");
            }
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
            data = new dtDuyetDonHangChiNhanh();
            data.Xoa_Temp_ID(IDTemp);
            Response.Redirect("DonDatHangChiNhanh.aspx");
        }
        protected void txtNgayGiao_Init(object sender, EventArgs e)
        {
            txtNgayGiao.Date = DateTime.Today;
        }

        protected void gridDanhSachHangHoa_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TrangThai = Convert.ToInt32(e.GetValue("TrangThai"));
            if (TrangThai == 1)
                e.Row.BackColor = color;

            int ThucTe = Convert.ToInt32(e.GetValue("ThucTe"));
            float TonKhoTong = Convert.ToInt32(e.GetValue("TonKhoTong"));
            if(ThucTe > TonKhoTong)
                e.Row.BackColor = color;
        }

        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
            string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
            string IDDonHangChiNhanh = cmbSoDonHang.Value.ToString();
            if (SoLuong > 0)
            {
                string IDHangHoa = cmbHangHoa.Value.ToString();
                string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                float TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa);
                string GhiChu = txtGhiChuHangHoa.Text == null ? "" : txtGhiChuHangHoa.Text.ToString();
                data = new dtDuyetDonHangChiNhanh();
                DataTable dbt = data.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh, IDTemp);
                if (dbt.Rows.Count == 0)
                {
                    string IDKho = "1";
                    DataTable db = data.LayDanhSachDonHang_ID(IDDonHangChiNhanh);
                    if (db.Rows.Count > 0)
                    {
                        DataRow dr = db.Rows[0];
                        IDKho = dr["IDKho"].ToString();
                    }
                    data.ThemMoiChiTiet(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, (TrongLuong * SoLuong).ToString(), SoLuong.ToString(), "0", GhiChu, "1", IDKho, IDTemp, "0", SoLuong.ToString());
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Hàng hóa đã tồn tại.Vui lòng kiểm tra lại? '); </script>");
                    cmbHangHoa.Text = "";
                    txtTonKho.Text = "";
                    txtSoLuong.Text = "";
                    txtGhiChuHangHoa.Text = "";
                    LoadDanhSach(IDDonHangChiNhanh, IDTemp);
                    return;
                }
                LoadDanhSach(IDDonHangChiNhanh, IDTemp);
                cmbHangHoa.Text = "";
                txtTonKho.Text = "";
                txtSoLuong.Text = "";
                txtGhiChuHangHoa.Text = "";
            }
            else
            {
                LoadDanhSach(IDDonHangChiNhanh, IDTemp);
                Response.Write("<script language='JavaScript'> alert('Số Lượng phải > 0.'); </script>");
                return;
            }
        }

        protected void cmbHangHoa_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh
                                        WHERE (GPM_HangHoa.ID = @ID) AND  (GPM_HangHoa.IDTrangThaiHang = 1) AND (GPM_HangHoa.IDNhomDatHang != 3)";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaMuaTruocThue] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_HangHoa.GiaMuaTruocThue, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh           
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang)) AND (GPM_HangHoa.DaXoa = 0) AND  ((GPM_HangHoa.IDTrangThaiHang = 1) OR (GPM_HangHoa.IDTrangThaiHang = 3) OR (GPM_HangHoa.IDTrangThaiHang = 6))
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            dsHangHoa.SelectParameters.Clear();
            // dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                int TonKho = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), Session["IDKho"].ToString());
                txtTonKho.Text = TonKho.ToString();
                txtSoLuong.Text = "0";
            }
        }

       
    }
}
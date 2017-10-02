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
    public partial class DuyetDonHangThuMua : System.Web.UI.Page
    {
        dtDuyetDonHangThuMua data = new dtDuyetDonHangThuMua();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 64) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    data = new dtDuyetDonHangThuMua();
                    data.Xoa_Temp_ID(Session["IDNhanVien"].ToString());
                    txtNguoiDuyet.Text = Session["TenDangNhap"].ToString();
                    btnThem.Enabled = false;
                }
                if (cmbSoDonHang.Text != "")
                {
                    LoadDanhSach(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
                }
            }
        }
        public void LoadDanhSach(string IDDonHangThuMua, string IDTemp)
        {
            data = new dtDuyetDonHangThuMua();
            gridDanhSachHangHoa.DataSource = data.DanhSachChiTiet_Temp(IDDonHangThuMua, IDTemp);
            gridDanhSachHangHoa.DataBind();
        }
       
        public double TinhTrongLuong()
        {
            if (cmbSoDonHang.Text != "")
            {
                data = new dtDuyetDonHangThuMua();
                DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
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
                btnThem.Enabled = true;
                data = new dtDuyetDonHangThuMua();
                data.Xoa_Temp_ID(IDDonHangDuyet_Temp.Value.ToString());
               // Random ran = new Random();
               // int Temp = ran.Next(100000, 999999);
                IDDonHangDuyet_Temp.Value = Session["IDNhanVien"].ToString();
                string ID = cmbSoDonHang.Value.ToString();
                DataTable db = data.LayDanhSachDonHang_ID(ID);
                if (db.Rows.Count > 0)
                {
                    DataRow dr = db.Rows[0];
                    cmbNguoiLap.Value = dr["IDNguoiLap"].ToString();
                    txtNgayDatHang.Date = DateTime.Parse(dr["NgayDat"].ToString());
                    cmbChiNhanhLap.Value = dr["IDKhoLap"].ToString();
                    txtGhiChu.Text = dr["GhiChu"].ToString();
                    cmbNhaCungCap.Value = dr["IDNhaCungCap"].ToString();
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
                                string GhiChu = dr1["GhiChu"].ToString();
                                data = new dtDuyetDonHangThuMua();
                                DataTable dbt = data.KTChiTietDonHang_Temp(IDHangHoa, ID, IDTemp);
                                if (dbt.Rows.Count == 0)
                                {
                                    data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, GhiChu, IDTemp);
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
                        data = new dtDuyetDonHangThuMua();
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
                                string GhiChu = dr1["GhiChu"].ToString();
                                data = new dtDuyetDonHangThuMua();
                                data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, GhiChu, IDTemp);
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
                            data = new dtDuyetDonHangThuMua();
                            data.CapNhatChiTietDonHang(ID, IDHangHoa, IDTemp, SoLuongThucTe, SoLuong - SoLuongThucTe);
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

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                if (cmbTrangThaiDonHang.Text != "" && txtNgayGiao.Text != "")
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
                    string IDNhaCungCap = cmbNhaCungCap.Value.ToString();
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
                        data = new dtDuyetDonHangThuMua();
                        object ID = data.ThemDuyetDonHang(IDDonHang, SoDonHang, IDNguoiLap, TongTrongLuong, IDNguoiDuyet, GhiChu, NgayDat, NgayDuyet, NgayGiao, IDNhaCungCap, TrangThaiXuLu, ChungTu);
                        if (ID != null)
                        {
                            DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDTemp);
                            if (db.Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in db.Rows)
                                {
                                    int kt = 0;
                                    string MaHang = dr1["MaHang"].ToString();
                                    string IDHangHoa = dr1["IDHangHoa"].ToString();
                                    string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                                    string TrongLuong = dr1["TrongLuong"].ToString();
                                    string SoLuong = dr1["SoLuong"].ToString();
                                    string ChenhLech = dr1["ChenhLech"].ToString();
                                    string GhiChuHH = dr1["GhiChu"].ToString();
                                    string ThucTe = dr1["ThucTe"].ToString();
                                    data = new dtDuyetDonHangThuMua();
                                    // cộng tồn kho tổng
                                    data.ThemChiTietDonHang_Duyet(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, GhiChuHH, ChenhLech, ThucTe);
                                    if (Int32.Parse(ThucTe) > 0)
                                    {
                                        object TheKho = dtTheKho.ThemTheKho(SoDonHang, "Phiếu nhập hàng", ThucTe, "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString()) + Int32.Parse(ThucTe)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Nhập", "0", "0", "0");
                                        if (TheKho != null)
                                        {
                                            dtCapNhatTonKho.CongTonKho(IDHangHoa, ThucTe, Session["IDKho"].ToString());
                                        }
                                    }
                                    if (Int32.Parse(SoLuong) != Int32.Parse(ThucTe))
                                    {
                                        kt = 1;
                                    }
                                    if (kt == 1)
                                    {
                                        data = new dtDuyetDonHangThuMua();
                                        //cho biết đơn hàng xử lý có chênh lệch hay không
                                        data.CapNhatChenhLech(ID);
                                    }
                                    DataTable LOG = data.KiemTra_LOG(SoDonHang, IDHangHoa, IDDonHang);
                                    if (LOG.Rows.Count != 0)
                                    {
                                        // đơn hàng đang xử lý hoàn tất, nếu đơn hàng đang treo thì xóa đơn hàng đó.
                                        data = new dtDuyetDonHangThuMua();
                                        data.Xoa_LOG(SoDonHang, IDHangHoa, IDDonHang);
                                    }
                                }

                                data = new dtDuyetDonHangThuMua();
                                data.CapNhatTrangThaiThuMua(cmbSoDonHang.Value.ToString(), TrangThaiXuLu);
                                data.Xoa_Temp_ID(IDTemp);
                                Response.Redirect("DanhSachPhieuDatHang.aspx");
                                return;
                            }
                            else
                            {
                                Response.Write("<script language='JavaScript'> alert('Danh sách rỗng vui lòng kiểm tra lại.? '); </script>");
                                return;
                            }
                            // dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt");
                        }
                    }
                    else
                    {
                        // đơn hàng treo, xử lý 1 phần, lưu vào bàng GPM_Log_DuyetHangChiNhanh
                        data = new dtDuyetDonHangThuMua();
                        object ID = data.ThemDuyetDonHang(IDDonHang, SoDonHang, IDNguoiLap, TongTrongLuong, IDNguoiDuyet, GhiChu, NgayDat, NgayDuyet, NgayGiao, IDNhaCungCap, TrangThaiXuLu, ChungTu);
                        if (ID != null)
                        {
                            DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDTemp);
                            if (db.Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in db.Rows)
                                {
                                    int kt = 0;
                                    string MaHang = dr1["MaHang"].ToString();
                                    string IDHangHoa = dr1["IDHangHoa"].ToString();
                                    string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                                    string TrongLuong = dr1["TrongLuong"].ToString();
                                    string SoLuong = dr1["SoLuong"].ToString();
                                    string ChenhLech = dr1["ChenhLech"].ToString();
                                    string GhiChuHH = dr1["GhiChu"].ToString();
                                    string ThucTe = dr1["ThucTe"].ToString();
                                    if (Int32.Parse(SoLuong) != Int32.Parse(ThucTe))
                                    {
                                        kt = 1;
                                    }
                                    if (kt == 1)
                                    {
                                        data = new dtDuyetDonHangThuMua();
                                        //cho biết đơn hàng xử lý có chênh lệch hay không
                                        data.CapNhatChenhLech(ID);
                                    }
                                    data = new dtDuyetDonHangThuMua();
                                    data.ThemChiTietDonHang_Duyet(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, GhiChuHH, ChenhLech, ThucTe);
                                    if (Int32.Parse(ThucTe) > 0)
                                    {
                                        object TheKho = dtTheKho.ThemTheKho(SoDonHang, "Phiếu nhập hàng", ThucTe, "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString()) + Int32.Parse(ThucTe)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Nhập", "0", "0", "0");
                                        if (TheKho != null)
                                        {
                                            dtCapNhatTonKho.CongTonKho(IDHangHoa, ThucTe, Session["IDKho"].ToString());
                                        }
                                    }
                                    DataTable LOG = data.KiemTra_LOG(SoDonHang, IDHangHoa, IDDonHang);
                                    //thêm dữ liệu vào bảng log
                                    if (LOG.Rows.Count == 0)
                                    {
                                        data = new dtDuyetDonHangThuMua();
                                        data.ThemChiTietDonHang_LOG(IDDonHang, SoDonHang, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, ChenhLech, GhiChuHH);
                                    }
                                    else
                                    {
                                        // nếu tồn tại cập nhật số lượng lại
                                        data = new dtDuyetDonHangThuMua();
                                        data.CapNhatChiTietDonHang_LOG(IDDonHang, SoDonHang, IDHangHoa, ChenhLech);
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("<script language='JavaScript'> alert('Danh sách rỗng vui lòng kiểm tra lại.? '); </script>");
                                return;
                            }
                            data = new dtDuyetDonHangThuMua();
                            data.CapNhatTrangThaiThuMua(cmbSoDonHang.Value.ToString(), TrangThaiXuLu);
                            data.Xoa_Temp_ID(IDTemp);
                            Response.Redirect("DanhSachPhieuDatHang.aspx");
                            return;
                            //        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt");
                        }
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Vui lòng chọn nhập trường có dấu (*)? '); </script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn số đơn hàng để thực hiện thao tác? '); </script>");
                return;
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
            data = new dtDuyetDonHangThuMua();
            data.Xoa_Temp_ID(IDTemp);
            Response.Redirect("DanhSachPhieuDatHang.aspx");
        }

        protected void txtNgayDuyet_Init(object sender, EventArgs e)
        {
            txtNgayDuyet.Date = DateTime.Now;
        }

        protected void txtNgayGiao_Init(object sender, EventArgs e)
        {
            txtNgayGiao.Date = DateTime.Now;
        }
    }
}
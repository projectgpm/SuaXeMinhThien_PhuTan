using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ThuaMuaDatHang : System.Web.UI.Page
    {
        dtThuMuaDatHang data = new dtThuMuaDatHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 66) == false)
                    Response.Redirect("Default.aspx");
               // ghi chú
                // TrangThaiDonHang : 1-> Đơn Hàng Hủy. 0-> Đơn Hàng Bình Thường. do thu mua cập nhật
                //TrangThai: 0-> Đơn hàng chưa được xử lý, 1-> đơn hàng đã được kho xử lý., kho cập nhật
                //IDTrangThaiDonHang: là lấy id trong bảng trạng thái hàng, do kho cập nhât
                if (!IsPostBack)
                {
                    // data = new dtThuMuaDatHang();
                    // object IDPhieuDatHang = data.ThemPhieuDatHang();
                    //Random ran = new Random();
                   // int Temp = ran.Next(100000, 999999);
                    IDThuMuaDatHang_Temp.Value = Session["IDNhanVien"].ToString();//Temp.ToString();
                    cmbKhoLap.Value = Session["IDKho"].ToString();
                    txtNguoiLap.Text = Session["TenDangNhap"].ToString();
                    txtChietKhau.Text = "0";
                    txtTongTien.Text = "0";
                    txtTongTienSauCk.Text = "0";
                    txtTongTrongLuong.Text = "0";
                    DateTime date = DateTime.Now;
                    int thang = date.Month;
                    int year = date.Year;
                    string ngayBD = year + "-" + thang + "-01 00:00:00.000";
                    string ngayKT = year + "-" + thang + "-" + dtSetting.tinhSoNgay(thang, year) + " 00:00:00.000";
                    txtSoDonHang.Text = (dtSetting.LayMaKho(Session["IDKho"].ToString()) + "-" + dtThuMuaDatHang.TongSoXuatTrongThang(ngayBD, ngayKT, Session["IDKho"].ToString()) + "-" + (DateTime.Now.ToString("ddMMyyyy")));
                }
                LoadGrid(IDThuMuaDatHang_Temp.Value.ToString());
            }
        }
        protected void txtNgayLap_Init(object sender, EventArgs e)
        {
            txtNgayLap.Date = DateTime.Today;
        }
        protected void cmbHangHoa_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaMuaTruocThue, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh
                                        WHERE (GPM_HangHoa.ID = @ID) AND  (GPM_HangHoa.IDTrangThaiHang = 1) AND (GPM_HangHoa.IDNhomDatHang != 3)";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }
        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaMuaTruocThue] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_HangHoa.GiaMuaTruocThue, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh           
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang)) AND (GPM_HangHoa.DaXoa = 0) AND  (GPM_HangHoa.IDTrangThaiHang = 1) AND (GPM_HangHoa.IDNhomDatHang != 3)
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            dsHangHoa.SelectParameters.Clear();
          //  dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
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
                txtTrongLuong.Text = dtHangHoa.LayTrongLuong(cmbHangHoa.Value.ToString()).ToString();
                txtTonKho.Text = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), Session["IDKho"].ToString()) + "";
                txtDonGia.Text = dtHangHoa.LayGiaMuaTruocThue(cmbHangHoa.Value.ToString())+"";
                txtSoLuong.Text = "0";
            }
        }
        public void CLear()
        {
            cmbHangHoa.Text = "";
            txtTonKho.Text = "";
            txtSoLuong.Text = "";
            txtTrongLuong.Text = "";
            txtDonGia.Text = "";
        }
        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "" && txtNgayLap.Text != "" && txtNgayDat.Text !="" && txtNgayGiaoDuKien.Text !="" && txtChietKhau.Text !="" && cmbThanhToan.Text !="" )
            {
                string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
                data = new dtThuMuaDatHang();
                DataTable dt = data.DanhSachDonDatHang_Temp(IDThuMuaDatHang);
                if (dt.Rows.Count != 0)
                {
                    string SoDonHang = txtSoDonHang.Text.Trim();
                    string IDNguoiLap = Session["IDNhanVien"].ToString();
                    DateTime NgayLap = DateTime.Parse(txtNgayLap.Text);
                    string TongTrongLuong = txtTongTrongLuong.Text;
                    string TongTien = txtTongTien.Text;
                    string IDKhoLap = Session["IDKho"].ToString();
                    string IDNhaCungCap = cmbNhaCungCap.Value.ToString();
                    string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                    DateTime NgayDat = DateTime.Parse(txtNgayDat.Text);
                    DateTime NgayGiaoDuKien = DateTime.Parse(txtNgayGiaoDuKien.Text);
                    string TongTienSauCk = txtTongTienSauCk.Text.ToString();
                    string ChietKhau = txtChietKhau.Text.ToString();
                    string IDThanhToan = cmbThanhToan.Value.ToString();
                    data = new dtThuMuaDatHang();
                    object ID = data.ThemPhieuDatHang(SoDonHang, IDNguoiLap, NgayLap, TongTrongLuong, TongTien, IDKhoLap, GhiChu, IDNhaCungCap, NgayDat, NgayGiaoDuKien, ChietKhau, TongTienSauCk, IDThanhToan);
                    if (ID != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            string MaHang = dr["MaHang"].ToString();
                            string IDDonViTinh = dr["IDDonViTinh"].ToString();
                            string TrongLuong = dr["TrongLuong"].ToString();
                            string SoLuong = dr["SoLuong"].ToString();
                            string DonGia = dr["DonGia"].ToString();
                            string ThanhTien = dr["ThanhTien"].ToString();
                            string GhiChuHangHoa = dr["GhiChu"].ToString();
                            data = new dtThuMuaDatHang();
                            data.ThemChiTietDonHang(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, ThanhTien, GhiChuHangHoa);
                        }
                        data = new dtThuMuaDatHang();
                        data.XoaChiTietDonHang_Nhap(IDThuMuaDatHang);
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Thu Mua Thêm Đặt Hàng", IDKhoLap, "Nhập xuất tồn", "Thêm");
                        Response.Redirect("DanhSachDonHangThuMua.aspx");
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa rỗng.'); </script>"); return;
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Không được bỏ trống trường có dấu (*).'); </script>"); return;
            }
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThuMuaDatHang();
            data.XoaChiTietDonHang_Temp(IDThuMuaDatHang);
            Response.Redirect("DanhSachDonHangThuMua.aspx");
        }
        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "" && UploadFileExcel.FileName.ToString() != "")
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chỉ chọn 1 hình thức thêm hàng hóa.'); </script>");
                CLear();
                return;
            }
            else if (UploadFileExcel.FileName.ToString() != "")
            {
                Import();
            }
            else if (cmbHangHoa.Text != "")
            {

                int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
                if (SoLuong > 0)
                {
                    string IDHangHoa = cmbHangHoa.Value.ToString();
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                    float TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa);
                    float DonGia = float.Parse(txtDonGia.Text);
                    string IDDonHangChiNhanh = IDThuMuaDatHang_Temp.Value.ToString();
                    string GhiChuHangHoa = txtGhiChuHangHoa.Text == null ? "" : txtGhiChuHangHoa.Text.ToString();
                    DataTable db = dtThuMuaDatHang.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh);// kiểm tra hàng hóa
                    if (db.Rows.Count == 0)
                    {
                        data = new dtThuMuaDatHang();
                        data.ThemChiTietDonHang_Temp(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, (SoLuong * TrongLuong).ToString(), SoLuong, DonGia, DonGia * SoLuong, GhiChuHangHoa);
                        TinhTongTien();
                        CLear();
                    }
                    else
                    {
                        data = new dtThuMuaDatHang();
                        data.CapNhatChiTietDonHang_temp(IDDonHangChiNhanh, IDHangHoa, SoLuong, DonGia, DonGia * SoLuong, GhiChuHangHoa, (SoLuong * TrongLuong).ToString());
                        TinhTongTien();
                        CLear();
                    }
                    LoadGrid(IDDonHangChiNhanh);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Số Lượng phải > 0.'); </script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn hàng hóa.'); </script>");
                return;
            }
        }
        public void TinhTongTien()
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThuMuaDatHang();
            DataTable db = data.DanhSachDonDatHang_Temp(IDThuMuaDatHang);
            if (db.Rows.Count != 0)
            {
                double TongTien = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double ThanhTien = double.Parse(dr["ThanhTien"].ToString());
                    TongTien = TongTien + ThanhTien;
                }
                txtTongTien.Text = (TongTien).ToString();
                TinhTrongLuong();
                TinhChietKhau();
            }
            else
            {
                txtTongTien.Text = "0";
            }
        }
        public void TinhTrongLuong()
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThuMuaDatHang();
            DataTable db = data.DanhSachDonDatHang_Temp(IDThuMuaDatHang);
            if (db.Rows.Count != 0)
            {
                double Tong = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                    Tong = Tong + (TrongLuong);
                }
                txtTongTrongLuong.Text = (Tong).ToString();
            }
            else
            {
                txtTongTrongLuong.Text = "0";
            }
        }
        private void LoadGrid(string IDDonHangChiNhanh)
        {
            data = new dtThuMuaDatHang();
            gridDanhSachHangHoa.DataSource = data.DanhSachDonDatHang_Temp(IDDonHangChiNhanh);
            gridDanhSachHangHoa.DataBind();
        }
        private void Import()
        {
            if (string.IsNullOrEmpty(UploadFileExcel.FileName))
            {
                Response.Write("<script language='JavaScript'> alert('Chưa chọn file.'); </script>");
                return;
            }
            UploadFile();
            string Excel = Server.MapPath("~/Uploads/") + strFileExcel;

            string excelConnectionString = string.Empty;
            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Excel + ";Extended Properties=Excel 8.0;";

            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
            OleDbCommand cmd = new OleDbCommand("Select * from [Sheet$]", excelConnection);
            excelConnection.Open();
            OleDbDataReader dReader = default(OleDbDataReader);
            dReader = cmd.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(dReader);
            int r = dataTable.Rows.Count;
            Import_Temp(dataTable);

        }
        private void UploadFile()
        {
            string folder = null;
            string filein = null;
            string ThangNam = null;

            ThangNam = string.Concat(System.DateTime.Now.Month.ToString(), System.DateTime.Now.Year.ToString());
            if (!Directory.Exists(Server.MapPath("~/Uploads/") + ThangNam))
            {
                Directory.CreateDirectory(Server.MapPath("~/Uploads/") + ThangNam);
            }
            folder = Server.MapPath("~/Uploads/" + ThangNam + "/");

            if (UploadFileExcel.HasFile)
            {
                strFileExcel = Guid.NewGuid().ToString();
                string theExtension = Path.GetExtension(UploadFileExcel.FileName);
                strFileExcel += theExtension;
                filein = folder + strFileExcel;
                UploadFileExcel.SaveAs(filein);
                strFileExcel = ThangNam + "/" + strFileExcel;
            }
        }
        private void Import_Temp(DataTable datatable)
        {
            int intRow = datatable.Rows.Count;
            if (datatable.Columns.Contains("MaHang") && datatable.Columns.Contains("TenHangHoa") && datatable.Columns.Contains("SoLuong") && datatable.Columns.Contains("DonViTinh") && datatable.Columns.Contains("DonGia") && datatable.Columns.Contains("GhiChu"))
            {
                if (intRow != 0)
                {
                    for (int i = 0; i <= intRow - 1; i++)
                    {
                        DataRow dr = datatable.Rows[i];
                        int SoLuong = Int32.Parse(dr["SoLuong"].ToString());
                        string MaHang = dr["MaHang"].ToString().Trim();
                        if (SoLuong > 0 && SoLuong.ToString() != "" && MaHang != "")
                        {
                            string TenHangHoa = dr["TenHangHoa"].ToString();
                            string GhiChu = dr["GhiChu"].ToString();
                            string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                            string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa.Trim());
                            string IDDonHangChiNhanh = IDThuMuaDatHang_Temp.Value.ToString();
                            float TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa);
                            float DonGia = float.Parse(dr["DonGia"].ToString());
                            if (DonGia == 0)
                            {
                                DonGia = dtHangHoa.LayGiaMuaSauThue(IDHangHoa);
                            }
                            DataTable db = dtThuMuaDatHang.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh);// kiểm tra hàng hóa
                            if (db.Rows.Count == 0)
                            {
                                data = new dtThuMuaDatHang();
                                if (dtHangHoa.TrangThaiHang(IDHangHoa) == 1 && dtHangHoa.TrangThaiNhomDatHang(IDHangHoa) != 3)
                                {
                                    data.ThemChiTietDonHang_Temp(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, (TrongLuong * SoLuong).ToString(), SoLuong, DonGia, DonGia * SoLuong, GhiChu);
                                    TinhTongTien();
                                    CLear();
                                }
                            }
                            else
                            {
                                data = new dtThuMuaDatHang();
                                data.CapNhatChiTietDonHang_temp(IDDonHangChiNhanh, IDHangHoa, SoLuong, DonGia, DonGia * SoLuong, GhiChu, (TrongLuong * SoLuong).ToString());
                                TinhTongTien();
                                CLear();
                            }
                            LoadGrid(IDDonHangChiNhanh);
                        }
                        else
                        {
                            Response.Write("<script language='JavaScript'> alert('Số lượng phải > 0.'); </script>"); return;
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Dữ liệu không chính xác? Vui lòng kiểm tra lại.'); </script>"); return;
            }
        }
        public string strFileExcel { get; set; }
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThuMuaDatHang();
            data.XoaChiTietDonHang_Temp_ID(ID);
            TinhTrongLuong();
            TinhTongTien();
            LoadGrid(IDThuMuaDatHang);
        }

        protected void txtChietKhau_NumberChanged(object sender, EventArgs e)
        {
            if (txtChietKhau.Text != "")
            {
                TinhChietKhau();
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập giá trị chiết khấu cho đơn hàng.'); </script>"); return;
            }
        }
        public void TinhChietKhau()
        {
            if (txtChietKhau.Text != "")
            {
                int GiaTri = Int32.Parse(txtChietKhau.Text.ToString());
                if (GiaTri >= 0)
                {
                    double TongTien = double.Parse(txtTongTien.Text.ToString());
                    double Tylegiam = (GiaTri * (0.01));
                    double TienGiam = TongTien * Tylegiam;
                    double TienSauCK = (TongTien - TienGiam);
                    txtTongTienSauCk.Text = TienSauCK.ToString();
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Tỷ lệ chiết khấu phải là số dương.Vui lòng kiểm tra lại? '); </script>");
                    return;
                }
            }
        }

        protected void txtNgayDat_Init(object sender, EventArgs e)
        {
            txtNgayDat.Date = DateTime.Today;
        }

        protected void txtNgayGiaoDuKien_Init(object sender, EventArgs e)
        {
            txtNgayGiaoDuKien.Date = DateTime.Today;
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
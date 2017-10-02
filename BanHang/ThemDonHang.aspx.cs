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
    public partial class ThemDonHang : System.Web.UI.Page
    {
        dtThemDonHangKho data = new dtThemDonHangKho();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //TinhTongTien();
                if (!IsPostBack)
                {
                    data = new dtThemDonHangKho();
                    object IDPhieuDatHang = data.ThemPhieuDatHang();
                    IDThuMuaDatHang_Temp.Value = IDPhieuDatHang.ToString();
                    cmbKhoLap.Value = Session["IDKho"].ToString();
                    txtNguoiLap.Text = Session["TenDangNhap"].ToString();
                    txtSoDonHang.Text = (Int32.Parse(Session["IDKho"].ToString())).ToString().Replace(".", "") + "-" + (DateTime.Now.ToString("ddMMyyyy-hhmmss"));
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
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaMuaSauThue, GPM_DonViTinh.TenDonViTinh 
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

            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaMuaSauThue] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_HangHoa.GiaMuaSauThue, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh           
	                                        WHERE ((GPM_HangHoa.TenHangHoa LIKE @TenHang) OR (GPM_HangHoa.MaHang LIKE @MaHang)) AND (GPM_HangHoa.DaXoa = 0) AND  (GPM_HangHoa.IDTrangThaiHang = 1) AND (GPM_HangHoa.IDNhomDatHang != 3)
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
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
                txtDonGia.Text = dtHangHoa.LayGiaMuaSauThue(cmbHangHoa.Value.ToString()) + "";
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
        public void TinhTongTien()
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
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
            }
        }
        public void TinhTrongLuong()
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            DataTable db = data.DanhSachDonDatHang_Temp(IDThuMuaDatHang);
            if (db.Rows.Count != 0)
            {
                double Tong = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                    int SoLuong = Int32.Parse(dr["SoLuong"].ToString());
                    Tong = Tong + (TrongLuong * SoLuong);
                }
                txtTongTrongLuong.Text = (Tong).ToString();
            }
        }
        private void LoadGrid(string IDDonHangChiNhanh)
        {
            data = new dtThemDonHangKho();
            gridDanhSachHangHoa.DataSource = data.DanhSachDonDatHang_Temp(IDDonHangChiNhanh);
            gridDanhSachHangHoa.DataBind();
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
                    string TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa).ToString();
                    float DonGia = float.Parse(txtDonGia.Text);
                    string IDDonHangChiNhanh = IDThuMuaDatHang_Temp.Value.ToString();
                    DataTable db = dtThemDonHangKho.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh);// kiểm tra hàng hóa
                    if (db.Rows.Count == 0)
                    {
                        data = new dtThemDonHangKho();
                        data.ThemChiTietDonHang_Temp(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, DonGia * SoLuong);
                        TinhTongTien();
                        CLear();
                    }
                    else
                    {
                        data = new dtThemDonHangKho();
                        data.CapNhatChiTietDonHang_temp(IDDonHangChiNhanh, IDHangHoa, SoLuong, DonGia, DonGia * SoLuong);
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

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "")
            {
                string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
                data = new dtThemDonHangKho();
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
                    data = new dtThemDonHangKho();
                    data.CapNhatDonDatHang(IDThuMuaDatHang, SoDonHang, IDNguoiLap, NgayLap, TongTrongLuong, TongTien, GhiChu, IDNhaCungCap);
                    foreach (DataRow dr in dt.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string MaHang = dr["MaHang"].ToString();
                        string IDDonViTinh = dr["IDDonViTinh"].ToString();
                        string TrongLuong = dr["TrongLuong"].ToString();
                        string SoLuong = dr["SoLuong"].ToString();
                        string DonGia = dr["DonGia"].ToString();
                        string ThanhTien = dr["ThanhTien"].ToString();
                        data = new dtThemDonHangKho();
                        dtCapNhatTonKho.CongTonKho(IDHangHoa,SoLuong,Session["IDKho"].ToString()); // cộng kho không qua bước duyệt
                        // ghi lịch sử
                        data.ThemChiTietDonHang(IDThuMuaDatHang, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, ThanhTien);
                    }
                    data = new dtThemDonHangKho();
                    data.XoaChiTietDonHang_Nhap(IDThuMuaDatHang);
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Kho Thêm Đặt Hàng", IDKhoLap, "Nhập xuất tồn", "Thêm");
                    Response.Redirect("DanhSachPhieuDatHang.aspx");
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa rỗng.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn nhà cung cấp.'); </script>");
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            data.XoaChiTietDonHang_Temp(IDThuMuaDatHang);
            Response.Redirect("DanhSachDonHangThuMua.aspx");
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
            if (datatable.Columns.Contains("MaHang") && datatable.Columns.Contains("TenHangHoa") && datatable.Columns.Contains("SoLuong") && datatable.Columns.Contains("DonViTinh") && datatable.Columns.Contains("DonGia"))
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
                            string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                            string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa.Trim());
                            string IDDonHangChiNhanh = IDThuMuaDatHang_Temp.Value.ToString();
                            string TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa) + "";
                            float DonGia = float.Parse(dr["DonGia"].ToString());
                            if (DonGia == 0)
                            {
                                DonGia = dtHangHoa.LayGiaMuaSauThue(IDHangHoa);
                            }
                            DataTable db = dtThemDonHangKho.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh);// kiểm tra hàng hóa
                            if (db.Rows.Count == 0)
                            {
                                data = new dtThemDonHangKho();
                                if (dtHangHoa.TrangThaiHang(IDHangHoa) == 1 && dtHangHoa.TrangThaiNhomDatHang(IDHangHoa) != 3)
                                {
                                    data.ThemChiTietDonHang_Temp(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, DonGia * SoLuong);
                                    TinhTongTien();
                                    CLear();
                                }
                            }
                            else
                            {
                                data = new dtThemDonHangKho();
                                data.CapNhatChiTietDonHang_temp(IDDonHangChiNhanh, IDHangHoa, SoLuong, DonGia, DonGia * SoLuong);
                                TinhTongTien();
                                CLear();
                            }
                            LoadGrid(IDDonHangChiNhanh);
                        }
                        else
                        {
                            Response.Write("<script language='JavaScript'> alert('Số lượng phải > 0.'); </script>");
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Dữ liệu không chính xác? Vui lòng kiểm tra lại.'); </script>");
            }
        }
        public string strFileExcel { get; set; }
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            data.XoaChiTietDonHang_Temp_ID(ID);
            TinhTrongLuong();
            TinhTongTien();
            LoadGrid(IDThuMuaDatHang);
        }
    }
}
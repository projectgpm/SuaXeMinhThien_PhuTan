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
    public partial class ThemDonHangChiNhanh : System.Web.UI.Page
    {
        dtDonHangChiNhanh data = new dtDonHangChiNhanh();
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
                    //Random ran = new Random();
                    IDDonDatHang_Temp.Value = Session["IDNhanVien"].ToString();//ran.Next(100000, 999999).ToString();
                    cmbKhoLap.Value = Session["IDKho"].ToString();
                    txtNguoiLap.Text = Session["TenDangNhap"].ToString();
                    txtTongTrongLuong.Text = "0";
                    cmbMucDoUuTien.SelectedIndex = 0;
                    DateTime date = DateTime.Now;
                    int thang = date.Month;
                    int year = date.Year;
                    string ngayBD = year + "-" + thang + "-01 00:00:00.000";
                    string ngayKT = year + "-" + thang + "-" + dtSetting.tinhSoNgay(thang, year) + " 00:00:00.000";
                    txtSoDonHang.Text = (dtSetting.LayMaKho(Session["IDKho"].ToString()) + "-" + dtDonHangChiNhanh.TongSoXuatTrongThang(ngayBD, ngayKT, Session["IDKho"].ToString()).ToString() + "-" + (DateTime.Now.ToString("ddMMyyyy")));
                }
                LoadGrid(IDDonDatHang_Temp.Value.ToString());
            }
        }

        private void LoadGrid(string p)
        {
            data = new dtDonHangChiNhanh();
            gridDanhSachHangHoa.DataSource = data.DanhSachDonDatHangClient_Temp(IDDonDatHang_Temp.Value.ToString());
            gridDanhSachHangHoa.DataBind();
        }

        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
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
        protected void cmbHangHoa_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
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
        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                txtTrongLuong.Text = dtHangHoa.LayTrongLuong(cmbHangHoa.Value.ToString()).ToString();
                int TonKho  = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), Session["IDKho"].ToString());
                txtTonKho.Text = TonKho.ToString();
                txtSoLuong.Text = "0";
                // tính tần suất bán hàng trong 10 ngày.
                int SoNgayBan = dtSetting.LaySoNgayBanHang();
                int SoLuongBan = dtDonHangChiNhanh.TuanSuatBanHang(DateTime.Now, cmbHangHoa.Value.ToString(), -SoNgayBan, Session["IDKho"].ToString());
                txtSoLuongGoiY.Text = (SoLuongBan - TonKho).ToString();
            }
        }
        public void CLear()
        {
            cmbHangHoa.Text = "";
            txtTonKho.Text = "";
            txtSoLuong.Text = "";
            txtTrongLuong.Text = "";
            txtSoLuongGoiY.Text = "";
            txtGhiChuHangHoa.Text = "";
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
                    string TonKho = txtTonKho.Text.ToString();
                    string GhiChu = txtGhiChuHangHoa.Text == null ? "":txtGhiChuHangHoa.Text.ToString();
                    string IDDonHangChiNhanh = IDDonDatHang_Temp.Value.ToString();
                    DataTable db = dtDonHangChiNhanh.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh);// kiểm tra hàng hóa
                    if (db.Rows.Count == 0)
                    {
                        data = new dtDonHangChiNhanh();
                        data.ThemChiTietDonHang_Temp(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, (SoLuong * TrongLuong).ToString(), SoLuong, TonKho, GhiChu);
                        CLear();
                        TinhTrongLuong();
                    }
                    else
                    {
                        data = new dtDonHangChiNhanh();
                        data.CapNhatChiTietDonHang_temp(IDDonHangChiNhanh, IDHangHoa, SoLuong,(SoLuong * TrongLuong).ToString(), TonKho, GhiChu);
                        CLear();
                        TinhTrongLuong();
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
            if (txtNgayDat.Text != "" && txtNgayGiaoDuKien.Text != "" && cmbMucDoUuTien.Text != "")
            {
                string IDDonHangChiNhanh = IDDonDatHang_Temp.Value.ToString();
                data = new dtDonHangChiNhanh();
                DataTable dt = data.DanhSachDonDatHangClient_Temp(IDDonHangChiNhanh);
                if (dt.Rows.Count != 0)
                {
                    string SoDonHang = txtSoDonHang.Text.Trim();
                    string IDNguoiLap = Session["IDNhanVien"].ToString();
                    DateTime NgayLap = DateTime.Parse(txtNgayLap.Text);
                    DateTime NgayDat = DateTime.Parse(txtNgayDat.Text.ToString());
                    DateTime NgayGiaoDuKien = DateTime.Parse(txtNgayGiaoDuKien.Text.ToString());
                    string TongTrongLuong = txtTongTrongLuong.Text;
                    string IDKho = Session["IDKho"].ToString();
                    string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                    string MucDoUuTien = cmbMucDoUuTien.Value.ToString();
                    data = new dtDonHangChiNhanh();
                    object ID = data.ThemPhieuDatHangClient();
                    if (ID != null)
                    {
                        data.CapNhatDonDatHangClient(ID, SoDonHang, IDNguoiLap, NgayLap, TongTrongLuong, IDKho, GhiChu, NgayDat, NgayGiaoDuKien, MucDoUuTien);
                        foreach (DataRow dr in dt.Rows)
                        {
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            string MaHang = dr["MaHang"].ToString();
                            string IDDonViTinh = dr["IDDonViTinh"].ToString();
                            string TrongLuong = dr["TrongLuong"].ToString();
                            string SoLuong = dr["SoLuong"].ToString();
                            string TonKho = dr["TonKho"].ToString();
                            string GhiChuHangHoa = dr["GhiChu"].ToString();
                            string TrangThai = dr["TrangThai"].ToString();
                            data = new dtDonHangChiNhanh();
                            data.ThemChiTietDonHangClient(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, TonKho, GhiChuHangHoa, TrangThai, IDKho);
                        }
                        data = new dtDonHangChiNhanh();
                        data.XoaChiTietDonHang_Nhap(IDDonHangChiNhanh);

                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi Nhánh Thêm Đặt Hàng", IDKho, "Nhập xuất tồn", "Thêm");
                        Response.Redirect("ChiNhanhDatHang.aspx");
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa rỗng.'); </script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập đầy đủ các trường có dấu (*).'); </script>");
                return;
            }
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = IDDonDatHang_Temp.Value.ToString();
            data = new dtDonHangChiNhanh();
            data.XoaChiTietDonHang_Temp(IDDonHangChiNhanh);
            Response.Redirect("ChiNhanhDatHang.aspx");
        }
        public void TinhTrongLuong()
        {
            string IDDonHangChiNhanh = IDDonDatHang_Temp.Value.ToString();
            data = new dtDonHangChiNhanh();
            DataTable db = data.DanhSachDonDatHangClient_Temp(IDDonHangChiNhanh);
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
            else
            {
                txtTongTrongLuong.Text = "";
            }
        }
        protected void gridDanhSachHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDDonHangChiNhanh = IDDonDatHang_Temp.Value.ToString();
            data = new dtDonHangChiNhanh();
            data.XoaChiTietDonHang_Temp_ID(ID);
            e.Cancel = true;
            gridDanhSachHangHoa.CancelEdit();
            TinhTrongLuong();
            LoadGrid(IDDonHangChiNhanh);
        }
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            string IDDonHangChiNhanh = IDDonDatHang_Temp.Value.ToString();
            data = new dtDonHangChiNhanh();
            data.XoaChiTietDonHang_Temp_ID(ID);
            TinhTrongLuong();
            LoadGrid(IDDonHangChiNhanh);
        }
        protected void txtNgayLap_Init(object sender, EventArgs e)
        {
            txtNgayLap.Date = DateTime.Now;
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
            if (datatable.Columns.Contains("MaHang") && datatable.Columns.Contains("TenHangHoa") && datatable.Columns.Contains("SoLuong") && datatable.Columns.Contains("DonViTinh") && datatable.Columns.Contains("GhiChu"))
            {
                if (intRow != 0)
                {
                    for (int i = 0; i <= intRow - 1; i++)
                    {
                        DataRow dr = datatable.Rows[i];
                        int SoLuong = Int32.Parse(dr["SoLuong"].ToString());
                        string MaHang = dr["MaHang"].ToString().Trim();
                        if (SoLuong > 0 && SoLuong.ToString() != "" && MaHang!="")
                        {
                            string TenHangHoa = dr["TenHangHoa"].ToString();
                            string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                            string DonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa.Trim());
                            string IDDonHangChiNhanh = IDDonDatHang_Temp.Value.ToString();
                            string TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa)+"";
                            int TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
                            string GhiChu = dr["GhiChu"].ToString();
                            //1: Hàng Hóa Thường, 3: Hàng Ngừng Nhập, 6: Đang Kinh Doanh , 2:Hàng Đang Chờ Xử Lý
                            DataTable db = dtDonHangChiNhanh.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh);// kiểm tra hàng hóa
                            if (db.Rows.Count == 0)
                            {
                                data = new dtDonHangChiNhanh();
                                if ((dtHangHoa.TrangThaiHang(IDHangHoa) == 1 || dtHangHoa.TrangThaiHang(IDHangHoa) == 3 || dtHangHoa.TrangThaiHang(IDHangHoa) == 6) && dtHangHoa.TrangThaiNhomDatHang(IDHangHoa) != 2)
                                {
                                    data.ThemChiTietDonHang_Temp(IDDonHangChiNhanh, MaHang, IDHangHoa, DonViTinh, (SoLuong * float.Parse(TrongLuong)).ToString(), SoLuong, TonKho.ToString(), GhiChu);
                                    CLear();
                                    TinhTrongLuong();
                                }
                            }
                            else
                            {
                                data = new dtDonHangChiNhanh();
                                data.CapNhatChiTietDonHang_temp(IDDonHangChiNhanh, IDHangHoa, SoLuong, (SoLuong * float.Parse(TrongLuong)).ToString(), TonKho.ToString(), GhiChu);
                                CLear();
                                TinhTrongLuong();
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

        protected void txtNgayGiaoDuKien_Init(object sender, EventArgs e)
        {
            txtNgayGiaoDuKien.Date = DateTime.Now;
        }

        protected void txtNgayDat_Init(object sender, EventArgs e)
        {
            txtNgayDat.Date = DateTime.Now;
        }
    }
}
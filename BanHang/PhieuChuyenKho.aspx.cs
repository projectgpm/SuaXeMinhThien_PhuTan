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
    public partial class PhieuChuyenKho : System.Web.UI.Page
    {
        private string strFileExcel = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    if (!IsPostBack)
                    {
                        dtPhieuChuyenKho data = new dtPhieuChuyenKho();
                        DataTable da = data.PhieuChuyenKho_Null(Session["IDKho"].ToString());
                        if (da.Rows.Count != 0)
                        {
                            IDPhieuChuyenKho.Value = da.Rows[0]["ID"].ToString();
                        }
                        else
                        {
                            object ID = data.ThemPhieuChuyenKho(Session["IDKho"].ToString());
                            IDPhieuChuyenKho.Value = ID.ToString();
                        }

                        cmbNguoiLapPhieu.Text = Session["IDNhanVien"].ToString();
                        cmbTrangThaiPhieu.SelectedIndex = 0;
                        cmbKhoXuat.Value = Session["IDKho"].ToString();
                    }
                    LoadGrid(IDPhieuChuyenKho.Value.ToString());
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void LoadGrid(string IDPhieuChuyenKho)
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            gridDanhSachHangHoa_Temp.DataSource = data.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
            gridDanhSachHangHoa_Temp.DataBind();

        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            if (cmbHangHoa.Value != null)
            {
                string IDHH = cmbHangHoa.Value + "";
                if (Int32.Parse(txtSoLuong.Value + "") <= Int32.Parse(txtTonKho.Value + ""))
                {
                    DataTable dx = dt.KiemTraHangHoa(IDPhieuChuyenKho.Value.ToString(), IDHH);
                    if (dx.Rows.Count == 0)
                    {
                        dt.ThemChiTietPhieuChuyenKho(IDPhieuChuyenKho.Value.ToString(), IDHH, txtSoLuong.Value + "", txtHHTrongLuong.Value + "", txtGhiChuHH.Value + "");
                        DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho.Value.ToString());
                        if (da.Rows.Count != 0)
                        {
                            txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                            txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                        }
                    }
                    else
                    {
                        dt.CapNhatChiTietPhieuChuyenKho(IDPhieuChuyenKho.Value.ToString(), IDHH, (Int32.Parse(dx.Rows[0]["SoLuong"].ToString()) + Int32.Parse(txtSoLuong.Value + "")) + "", (float.Parse(dx.Rows[0]["TrongLuong"].ToString()) + float.Parse(txtHHTrongLuong.Value + "")) + "", txtGhiChuHH.Value + "");
                        DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho.Value.ToString());
                        if (da.Rows.Count != 0)
                        {
                            txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                            txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                        }
                    }
                    LoadGrid(IDPhieuChuyenKho.Value.ToString());
                }
                else
                {
                    txtSoLuong.Value = txtTonKho.Value;
                    Response.Write("<script language='JavaScript'> alert('Hàng hóa không đủ số lượng.'); </script>");
                }

                cmbHangHoa.SelectedIndex = -1;
                txtHHTrongLuong.Value = 0;
                cmbDonViTinh.SelectedIndex = -1;
                txtSoLuong.Value = 1; ;
                txtTonKho.Value = 0;
            }
            else if (!string.IsNullOrEmpty(fileUpload.FileName))
            {
                Import();
            }
            else
                Response.Write("<script language='JavaScript'> alert('Chọn hàng hóa hoặc file Import.'); </script>");
        }

        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();

            dt.XoaChiTietPhieuChuyenKho_Delete(ID + "");
            DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho.Value.ToString());
            if (da.Rows.Count != 0)
            {
                txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
            }
            else
            {
                txtTrongLuong.Value = 0;
                txtSoMatHang.Value = 0;
            }

            LoadGrid(IDPhieuChuyenKho.Value.ToString());
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IDHH = cmbHangHoa.Value.ToString();
            dataHangHoa data = new dataHangHoa();
            DataTable da = data.getDanhSachHangHoa_TonKho_ID(IDHH, Session["IDKho"].ToString());
            if (da.Rows.Count != 0)
            {
                DataRow dr = da.Rows[0];
                string SoLuongCon = dr["SoLuongCon"].ToString();
                string IDDọnViTinh = dr["IDDonViTinh"].ToString();
                string TrongLuong = dr["TrongLuong"].ToString();
                txtHHTrongLuong.Value = TrongLuong;
                cmbDonViTinh.Value = IDDọnViTinh;
                txtSoLuong.Value = 1; ;
                txtTonKho.Value = SoLuongCon;
            }
        }

        protected void btnThemPhieuChuyenKho_Click(object sender, EventArgs e)
        {
            string ID = IDPhieuChuyenKho.Value.ToString();
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            if (cmbKhoNhap.Value != null && cmbKhoXuat.Value != null)
            {
                string IDKhoXuat = cmbKhoXuat.Value + "";
                string IDKhoNhap = cmbKhoNhap.Value + "";
                string IDNhanVienLap = Session["IDNhanVien"].ToString();
                string SoMatHang = txtSoMatHang.Value + "";
                string TrongLuong = txtTrongLuong.Value + "";
                string GhiChu = txtGhiChu.Value + "";
                string NguoiGiao = txtNguoiGiao.Value + "";

                DateTime date = DateTime.Now;
                int thang = date.Month;
                int year = date.Year;
                string ngayBD = year + "-" + thang + "-01 00:00:00.000";
                string ngayKT = year + "-" + thang + "-" + dtSetting.tinhSoNgay(thang, year) + " 00:00:00.000";

                dtKho dt = new dtKho();
                string MaKhoNhap = dt.LayMaKho_ID(IDKhoNhap);
                string MaKhoXuat = dt.LayMaKho_ID(IDKhoXuat);
                string ngaythangnam = date.ToString("ddMMyyyy");
                
                int soHDKhoNhap = Int32.Parse(data.TongSoHDCuaKhoNhan(ngayBD, ngayKT, IDKhoXuat, IDKhoNhap)) + 1;
                int soHDKhoXuat = Int32.Parse(data.TongSoHDCuaKhoNhan(ngayBD, ngayKT, IDKhoXuat, -1 + ""));
                
                string MaHD = MaKhoNhap + "-" + soHDKhoNhap + "-" + soHDKhoXuat + "-" + MaKhoXuat + "-" + ngaythangnam;

                string ChungTu = "";
                if (Page.IsValid && txtFileChungTu.HasFile)
                {
                    ChungTu = "ChuyenKho/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + txtFileChungTu.FileName;
                    string filePath = MapPath(ChungTu);
                    txtFileChungTu.SaveAs(filePath);
                }

                string MaSoPhieu = MaKhoXuat + "-" + soHDKhoXuat + "-" + ngaythangnam;

                data.CapNhatPhieuChuyenKho(ID, IDKhoXuat, IDKhoNhap, IDNhanVienLap, SoMatHang, TrongLuong, GhiChu, NguoiGiao, MaSoPhieu, MaHD, ChungTu);

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");
                Response.Redirect("DanhSachPhieuChuyenKho.aspx");

            
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Chọn kho nhận.'); </script>");
            }
        }

        protected void cmbNgayLapPhieu_Init(object sender, EventArgs e)
        {
            cmbNgayLapPhieu.Date = DateTime.Today;
        }

        protected void btnHuyPhieuChuyenKho_Click(object sender, EventArgs e)
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            data.XoaPhieuChuyenKho(IDPhieuChuyenKho.Value.ToString());
            data.XoaChiTietPhieuChuyenKho_Delete_Phieu(IDPhieuChuyenKho.Value.ToString());
            Response.Redirect("DanhSachPhieuChuyenKho.aspx");
        }


        private void Import()
        {
            if (string.IsNullOrEmpty(fileUpload.FileName))
            {
                return;
            }
            
            UploadFile();
            string Excel = Server.MapPath("~/Uploads/ChuyenKho") + strFileExcel;

            string excelConnectionString = string.Empty;
            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Excel + ";Extended Properties=Excel 8.0;";

            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
            OleDbCommand cmd = new OleDbCommand("Select * from [Sheet$]", excelConnection);
            excelConnection.Open();
            OleDbDataReader dReader = default(OleDbDataReader);
            dReader = cmd.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(dReader);
            Import_Temp(dataTable);
        }

        private void Import_Temp(DataTable dataTable)
        {
            int kt = 0;
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            int intRow = dataTable.Rows.Count;
            if (intRow != 0)
            {
                for (int i = 0; i <= intRow - 1; i++)
                {
                    DataRow dr = dataTable.Rows[i];
                    string MaHang = dr["Ma Hang"].ToString();
                    string soLuong = dr["So Luong"].ToString();
                    string IDHH = dtHangHoa.LayIDHangHoa_MaHang(MaHang);
                    float TrongLuong = dtHangHoa.LayTrongLuong(IDHH) * Int32.Parse(soLuong);

                    dataHangHoa d = new dataHangHoa();
                    DataTable dataHH = d.getDanhSachHangHoa_TonKho_ID(IDHH, Session["IDKho"].ToString());
                    if (dataHH.Rows.Count != 0)
                    {
                        if (Int32.Parse(soLuong) <= Int32.Parse(dataHH.Rows[0]["SoLuongCon"].ToString()))
                        {
                            DataTable dx = dt.KiemTraHangHoa(IDPhieuChuyenKho.Value.ToString(), IDHH);
                            if (dx.Rows.Count == 0)
                            {
                                dt.ThemChiTietPhieuChuyenKho(IDPhieuChuyenKho.Value.ToString(), IDHH, soLuong, TrongLuong + "", txtGhiChuHH.Value + "");
                                DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho.Value.ToString());
                                if (da.Rows.Count != 0)
                                {
                                    txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                                    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                                }
                            }
                            else
                            {
                                dt.CapNhatChiTietPhieuChuyenKho(IDPhieuChuyenKho.Value.ToString(), IDHH, (Int32.Parse(dx.Rows[0]["SoLuong"].ToString()) + Int32.Parse(soLuong)) + "", (float.Parse(dx.Rows[0]["TrongLuong"].ToString()) + TrongLuong) + "", txtGhiChuHH.Value + "");
                                DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho.Value.ToString());
                                if (da.Rows.Count != 0)
                                {
                                    txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                                    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                                }
                            }
                        }
                        else
                        {
                            kt = 1;
                            soLuong = dataHH.Rows[0]["SoLuongCon"].ToString();
                            if (Int32.Parse(soLuong) < 0) soLuong = "0";

                            DataTable dx = dt.KiemTraHangHoa(IDPhieuChuyenKho.Value.ToString(), IDHH);
                            if (dx.Rows.Count == 0)
                            {
                                dt.ThemChiTietPhieuChuyenKho(IDPhieuChuyenKho.Value.ToString(), IDHH, soLuong, TrongLuong + "", txtGhiChuHH.Value + "");
                                DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho.Value.ToString());
                                if (da.Rows.Count != 0)
                                {
                                    txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                                    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                                }
                            }
                            else
                            {
                                dt.CapNhatChiTietPhieuChuyenKho(IDPhieuChuyenKho.Value.ToString(), IDHH, (Int32.Parse(dx.Rows[0]["SoLuong"].ToString()) + Int32.Parse(soLuong)) + "", (float.Parse(dx.Rows[0]["TrongLuong"].ToString()) + TrongLuong) + "", txtGhiChuHH.Value + "");
                                DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho.Value.ToString());
                                if (da.Rows.Count != 0)
                                {
                                    txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                                    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                                }
                            }
                        }
                    }
                }

                if (kt == 1)
                    Response.Write("<script language='JavaScript'> alert('Có hàng hóa không đủ số lượng trong kho. Hệ thống lấy số lượng tối đa để cập nhật.'); </script>");
                LoadGrid(IDPhieuChuyenKho.Value.ToString());
            }
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

            if (fileUpload.HasFile)
            {
                strFileExcel = Guid.NewGuid().ToString();
                string theExtension = Path.GetExtension(fileUpload.FileName);
                strFileExcel += theExtension;

                filein = folder + strFileExcel;
                fileUpload.SaveAs(filein);
                strFileExcel = ThangNam + "/" + strFileExcel;
            }
        }

        protected void txtSoLuong_ValueChanged(object sender, EventArgs e)
        {
            string IDHH = cmbHangHoa.Value + "";
            float TrongLuong = dtHangHoa.LayTrongLuong(IDHH);
            int SoLuong = Int32.Parse(txtSoLuong.Value + "");
            txtHHTrongLuong.Value = SoLuong * TrongLuong;
        }
    }
}
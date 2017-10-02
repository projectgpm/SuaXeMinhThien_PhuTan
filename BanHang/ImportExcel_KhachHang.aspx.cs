using BanHang.Data;
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
    public partial class ImportExcel_KhachHang : System.Web.UI.Page
    {
        private string strFileExcel = "";
        Import_KhachHang data = new Import_KhachHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 49) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    data = new Import_KhachHang();
                    data.XoaDuLieuTemp();
                }
                LoadGrid();

            }
           
        }
        private void LoadGrid()
        {
            data = new Import_KhachHang();
            gridKhachHang_Temp.DataSource = data.DanhSachKhachHang_Import_Temp();
            gridKhachHang_Temp.DataBind();
        }
        protected void gridKhachHang_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
           
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("KhachHang.aspx");
        }

        protected void btnNhap_Click(object sender, EventArgs e)
        {
            Import();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            data = new Import_KhachHang();
            DataTable db = data.DanhSachKhachHang_Import_Temp();
            if (db.Rows.Count != 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string TenKhachHang = dr["TenKhachHang"].ToString();
                    string NgaySinh = dr["NgaySinh"].ToString();
                    string CMND = dr["CMND"].ToString();
                    string DiaChi = dr["DiaChi"].ToString();
                    string DienThoai = dr["DienThoai"].ToString();
                    string Email = dr["Email"].ToString();
                    string GhiChu = dr["GhiChu"].ToString();
                    int IDNhomKhachHang = Int32.Parse(dr["IDNhomKhachHang"].ToString());
                    string MaKH = "";
                    string Barcode = "";
                    object ID;
                    if (DienThoai != "")
                    {
                        if (dtKhachHang.KT_SDT_KH(DienThoai.Trim()) == -1)
                        {
                            dtKhachHang kh = new dtKhachHang();
                            //ID = kh.ThemKhachHang(IDNhomKhachHang, MaKH, TenKhachHang, DateTime.Parse(NgaySinh), CMND, DiaChi, DienThoai, Email, Barcode, GhiChu, Session["IDKho"].ToString());
                            //if (ID != null)
                            //{
                                
                            //    kh = new dtKhachHang();
                            //    kh.CapNhatMaKhachHang(ID, (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001).ToString().Replace(".", "")).ToString(), (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001)).Replace(".", ""));
                            //    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                               
                            //}
                        }
                    }
                    else
                    {
                        dtKhachHang kh = new dtKhachHang();
                       // ID = kh.ThemKhachHang(IDNhomKhachHang, MaKH, TenKhachHang, DateTime.Parse(NgaySinh), CMND, DiaChi, DienThoai, Email, Barcode, GhiChu, Session["IDKho"].ToString());
                        //if (ID != null)
                        //{
                        //    kh = new dtKhachHang();
                        //    kh.CapNhatMaKhachHang(ID, (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001).ToString().Replace(".", "")).ToString(), (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001)).Replace(".", ""));
                        //    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                        //}
                    }
                }
                
                Response.Redirect("KhachHang.aspx");
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Dữ liệu trống? Vui lòng kiểm tra lại.'); </script>");
            }
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
            if (
                    datatable.Columns.Contains("Nhóm KH")
                && datatable.Columns.Contains("Tên KH")
                && datatable.Columns.Contains("Ngày sinh")
                && datatable.Columns.Contains("CMND")
                && datatable.Columns.Contains("Địa chỉ")
                && datatable.Columns.Contains("Điện thoại")
                && datatable.Columns.Contains("Email")
                && datatable.Columns.Contains("Ghi chú")
                )
            {
                if (intRow != 0)
                {
                    for (int i = 0; i <= intRow - 1; i++)
                    {
                        DataRow dr = datatable.Rows[i];
                        int IDNhomKhachHang = 1;
                        string TenNhomHang = dr["Nhóm KH"].ToString();
                        string TenKhachHang = dr["Tên KH"].ToString();
                        string NgaySinh = dr["Ngày sinh"].ToString();
                        string CMND = dr["CMND"].ToString();
                        string DiaChi = dr["Địa chỉ"].ToString();
                        string DienThoai = dr["Điện thoại"].ToString();
                        string Email = dr["Email"].ToString();
                        string GhiChu = dr["Ghi chú"].ToString();
                        data = new Import_KhachHang();
                        DataTable dt = data.Lay_IDNhomHang(TenNhomHang);
                        if (dt.Rows.Count != 0)
                        {
                            IDNhomKhachHang = Int32.Parse(dt.Rows[0]["ID"].ToString());
                        }
                        data = new Import_KhachHang();
                        dt = data.KiemTraKhachHang_Import_Temp(TenKhachHang, CMND, DienThoai);
                        if (dt.Rows.Count == 0)
                        {
                            data.ThemKhachHang_Temp(IDNhomKhachHang, TenKhachHang, DateTime.Parse(NgaySinh.ToString()), CMND, DiaChi, DienThoai, Email, GhiChu);
                            LoadGrid();
                        }

                    }
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Dữ liệu không chính xác? Vui lòng kiểm tra lại.'); </script>");
            }

        }
       
        protected void gridKhachHang_Temp_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            data = new Import_KhachHang();
            data.XoaDuLieuTemp_ID(ID);
            e.Cancel = true;
            gridKhachHang_Temp.CancelEdit();
            LoadGrid();
        }
    }
}
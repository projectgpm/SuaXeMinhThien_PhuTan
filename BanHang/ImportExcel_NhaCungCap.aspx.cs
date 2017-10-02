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
    public partial class ImportExcel_NhaCungCap : System.Web.UI.Page
    {
        private string strFileExcel = "";
        Import_NhaCungCap data = new Import_NhaCungCap();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                if(dtSetting.LayChucNangCha(Session["IDNhom"].ToString(),51) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    data = new Import_NhaCungCap();
                    data.XoaDuLieuTemp();
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        private void LoadGrid()
        {
            data = new Import_NhaCungCap();
            gridNhaCungCap_Temp.DataSource = data.DanhSachNhaCungCap_Import_Temp();
            gridNhaCungCap_Temp.DataBind();
        }

        
        protected void btnNhap_Click(object sender, EventArgs e)
        {
            Import();
           
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            data = new Import_NhaCungCap();
            DataTable db = data.DanhSachNhaCungCap_Import_Temp();
            if (db.Rows.Count != 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string TenNhaCungCap = dr["TenNhaCungCap"].ToString();
                    string DienThoai = dr["DienThoai"].ToString();
                    string Fax = dr["Fax"].ToString();
                    string Email = dr["Email"].ToString();
                    string DiaChi = dr["DiaChi"].ToString();
                    string NguoiLienHe = dr["NguoiLienHe"].ToString();
                    string MaSoThue = dr["MaSoThue"].ToString();
                    string LinhVucKinhDoanh = dr["LinhVucKinhDoanh"].ToString();
                    string GhiChu = dr["GhiChu"].ToString();
                    DateTime NgayCapNhat = DateTime.Today.Date;

                    data = new Import_NhaCungCap();
                    DataTable dt = data.KiemTraNhaCungCap_Import(TenNhaCungCap, MaSoThue, DienThoai);
                    if (dt.Rows.Count == 0)
                    {
                        data.ThemNhaCungCap(TenNhaCungCap, DienThoai, Fax, Email, DiaChi, NguoiLienHe, MaSoThue, LinhVucKinhDoanh, NgayCapNhat, GhiChu);
                    }
                }

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Nhà cung cấp", Session["IDKho"].ToString(), "Danh mục", "Import");
                Response.Redirect("NhaCungCap.aspx");
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Dữ liệu trống? Vui lòng kiểm tra lại.'); </script>");
            }
            
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data = new Import_NhaCungCap();
            data.XoaDuLieuTemp();
            Response.Redirect("NhaCungCap.aspx");
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

        private void Import_Temp(DataTable datatable)
        {
            int intRow = datatable.Rows.Count;
            if (
                    datatable.Columns.Contains("Nhà Cung Cấp")
                && datatable.Columns.Contains("Điện Thoại")
                && datatable.Columns.Contains("Fax")
                && datatable.Columns.Contains("Email")
                && datatable.Columns.Contains("Địa chỉ")
                && datatable.Columns.Contains("Người Liên Hệ")
                && datatable.Columns.Contains("Mã số thuế")
                && datatable.Columns.Contains("Lĩnh Vực Kinh Doanh")
                && datatable.Columns.Contains("Ghi chú")
                )
            {
                if (intRow != 0)
                {
                    for (int i = 0; i <= intRow - 1; i++)
                    {
                        DataRow dr = datatable.Rows[i];
                        string TenNhaCungCap = dr["Nhà Cung Cấp"].ToString();
                        string DienThoai = dr["Điện Thoại"].ToString();
                        string Fax = dr["Fax"].ToString();
                        string Email = dr["Email"].ToString();
                        string DiaChi = dr["Địa Chỉ"].ToString();
                        string NguoiLienHe = dr["Người Liên Hệ"].ToString();
                        string MaSoThue = dr["Mã số thuế"].ToString();
                        string LinhVucKinhDoanh = dr["Lĩnh Vực Kinh Doanh"].ToString();
                        string GhiChu = dr["Ghi chú"].ToString();

                        data = new Import_NhaCungCap();
                        DataTable dt = data.KiemTraNhaCungCap_Import_Temp(TenNhaCungCap, MaSoThue, DienThoai);
                        if (dt.Rows.Count == 0)
                        {
                            data.ThemNhaCungCap_Temp(TenNhaCungCap, DienThoai, Fax, Email, DiaChi, NguoiLienHe, MaSoThue, LinhVucKinhDoanh, GhiChu);
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

        protected void gridNhaCungCap_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            data = new Import_NhaCungCap();
            data.XoaDuLieuTemp_ID(ID);
            e.Cancel = true;
            gridNhaCungCap_Temp.CancelEdit();
            LoadGrid();
        }
    }
}
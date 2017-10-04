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
    public partial class ThemHangHoaTrongKe : System.Web.UI.Page
    {
        dtKe data = new dtKe();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 53) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    //Random ran = new Random();
                    IDTemp.Value = Session["IDNhanVien"].ToString(); //ran.Next(100000, 999999).ToString();
                }
                LoadGrid(IDTemp.Value.ToString());
                
            }
        }
        protected void cmbHangHoa_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh
                                        WHERE (GPM_HangHoa.ID = @ID) ";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
	                                        WHERE ((GPM_HangHoa.TenHangHoa LIKE @TenHang) OR (GPM_HangHoa.MaHang LIKE @MaHang)) AND (GPM_HangHoa.DaXoa = 0) 
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

        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            if (cmbKe.Text != "")
            {
                if (cmbHangHoa.Text != "" && UploadFileExcel.FileName.ToString() != "")
                {
                    Response.Write("<script language='JavaScript'> alert('Vui lòng chỉ chọn 1 hình thức thêm hàng hóa.'); </script>");
                    return;
                }
                else if (UploadFileExcel.FileName.ToString() != "")
                {
                    Import();
                }
                else if (cmbHangHoa.Text != "")
                {
                    string IDHangHoa = cmbHangHoa.Value.ToString();
                    string IDKe = cmbKe.Value.ToString();
                    data = new dtKe();
                    string Temp = IDTemp.Value.ToString();
                    DataTable db = dtKe.KTHangTrongKe_Temp(IDHangHoa, IDKe, Temp);
                    if (db.Rows.Count == 0)
                    {
                        data.ThemHangVaoKe_Temp(IDHangHoa, IDKe, Temp);
                    }
                    else
                    {
                        Response.Write("<script language='JavaScript'> alert('Hàng hóa đã tồn tại trong kệ này.'); </script>");
                    }
                    cmbHangHoa.Text = "";
                    LoadGrid(Temp);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Vui lòng chọn hàng hóa.'); </script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn kệ hàng.'); </script>");
                return;
            }
        }

        private void LoadGrid(string IDTemp)
        {
            data = new dtKe();
            gridDanhSachHangHoa.DataSource = data.DanhSachKe_Temp(IDTemp);
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
            if (datatable.Columns.Contains("MaHang") && datatable.Columns.Contains("TenHangHoa"))
            {
                string Temp = IDTemp.Value.ToString();
                if (intRow != 0)
                {
                    for (int i = 0; i <= intRow - 1; i++)
                    {
                        DataRow dr = datatable.Rows[i];
                        string MaHang = dr["MaHang"].ToString().Trim();
                        string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                        string IDKe = cmbKe.Value.ToString();
                        DataTable db = dtKe.KTHangTrongKe_Temp(IDHangHoa, IDKe, Temp);
                        if (db.Rows.Count == 0)
                        {
                            data = new dtKe();
                            data.ThemHangVaoKe_Temp(IDHangHoa, IDKe, Temp);
                            cmbHangHoa.Text = "";
                        }
                    }
                    LoadGrid(Temp);
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Dữ liệu không chính xác? Vui lòng kiểm tra lại.'); </script>");
            }

        }
        public string strFileExcel { get; set; }
        protected void gridDanhSachHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKe();
            data.XoaKe_Temp(ID);
            e.Cancel = true;
            gridDanhSachHangHoa.CancelEdit();
            LoadGrid(IDTemp.Value.ToString());
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            data = new dtKe();
            DataTable db = data.DanhSachKe_Temp_ALL(IDTemp.Value.ToString());
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string IDHangHoa = dr["IDHangHoa"].ToString();
                    string IDKe = dr["IDKe"].ToString();
                    string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    DataTable dt = dtKe.KTHangTrongKe(IDHangHoa, cmbKe.Value.ToString());
                    if (dt.Rows.Count == 0)
                    {
                        data = new dtKe();
                        data.ThemHangVaoKe(IDHangHoa, IDKe, IDDonViTinh, MaHang);
                    }
                }
                data = new dtKe();
                data.XoaKe_IDke_Temp(IDTemp.Value.ToString());
                Response.Redirect("DanhSachKe.aspx");
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa trống.'); </script>");
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data = new dtKe();
            data.XoaALL_Temp(IDTemp.Value.ToString());
            Response.Redirect("DanhSachKe.aspx");
        }
    }
}
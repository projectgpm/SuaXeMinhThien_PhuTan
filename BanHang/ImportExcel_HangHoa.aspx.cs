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
    public partial class ImportExcel_HangHoa : System.Web.UI.Page
    {
        private string strFileExcel = "";
        dtImportHangHoa data = new dtImportHangHoa();
        protected void Page_Load(object sender, EventArgs e)
        {
            
                //if (Session["KTDangNhap"] == "GPM")
                //{
                //    if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 7) != 1)
                //    {
                        if (!IsPostBack)
                        {
                            // xóa dữ liệu bảng temp
                            data = new dtImportHangHoa();
                            data.XoaDuLieuTemp();

                        }
                        LoadGrid();
                //    }
                //    else
                //    {
                //        Response.Redirect("Default.aspx");
                //    }
                //}
                //else
                //{
                //    Response.Redirect("DangNhap.aspx");
                //}
           

        }

        private void LoadGrid()
        {
            data = new dtImportHangHoa();
            gridHangHoa_Temp.DataSource = data.DanhSachHangHoa_Import_Temp();
            gridHangHoa_Temp.DataBind();
        }

        protected void gridHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            data = new dtImportHangHoa();
            data.XoaDuLieuTemp_ID(ID);
            e.Cancel = true;
            gridHangHoa_Temp.CancelEdit();
            LoadGrid();
        }

        protected void btnNhap_Click(object sender, EventArgs e)
        {
            Import();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            data = new dtImportHangHoa();
            DataTable dt = data.DanhSachHangHoa_Import_Temp();
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string IDNhomHang = dr["IDNhomHang"].ToString();
                    string MaHang = dr["MaHang"].ToString();
                    string TenHangHoa = dr["TenHangHoa"].ToString();
                    string IDDonViTinh = dr["IDDonViTinh"].ToString();
                    string HeSo = dr["HeSo"].ToString();
                    string IDHangSanXuat = dr["IDHangSanXuat"].ToString();
                    string IDThue = dr["IDThue"].ToString();
                    //string HangQuyDoi = dr["Hang Quy Doi"].ToString();
                    string IDNhomDatHang = dr["IDNhomDatHang"].ToString();
                    string GiaMuaTruocThue = dr["GiaMuaTruocThue"].ToString();
                    string GiaBanTruocThue = dr["GiaBanTruocThue"].ToString();
                    string GiaMuaSauThue = dr["GiaMuaSauThue"].ToString();
                    string GiaBanSauThue = dr["GiaBanSauThue"].ToString();
                    string GiaBan1 = dr["GiaBan1"].ToString();
                    string GiaBan2 = dr["GiaBan2"].ToString();
                    string GiaBan3 = dr["GiaBan3"].ToString();
                    string GiaBan4 = dr["GiaBan4"].ToString();
                    string GiaBan5 = dr["GiaBan5"].ToString();
                    string TrongLuong = dr["TrongLuong"].ToString();
                    string HanSuDung = dr["HanSuDung"].ToString();
                    string IDTrangThaiHang = dr["IDTrangThaiHang"].ToString();
                    string GhiChu = dr["GhiChu"].ToString();
                    string IDTrangThaiBarcode = dr["IDTrangThaiBarcode"].ToString();
                    string Barcode = dr["Barcode"].ToString();

                    object IDHangHoa = -1;
                    dataHangHoa hh = new dataHangHoa();
                    DataTable dd = hh.KiemTraHangHoa_MaHang(MaHang);
                    if (dd.Rows.Count == 0)
                    {
                        //IDHangHoa = hh.insertHangHoa(IDNhomHang, MaHang, TenHangHoa, IDDonViTinh, HeSo, IDHangSanXuat, IDThue, -1 + "", IDNhomDatHang, GiaMuaTruocThue, GiaBanTruocThue, GiaMuaSauThue, GiaBanSauThue, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5, TrongLuong, HanSuDung, IDTrangThaiHang, GhiChu);
                        // Thêm hàng hóa vào các kho....
                        dtKho dtx = new dtKho();
                        DataTable dtax = dtx.LayDanhSachKho();
                        for (int i = 0; i < dtax.Rows.Count; i++)
                        {
                            DataRow drx = dtax.Rows[i];
                            string IDKho = drx["ID"].ToString();
                            dataHangHoa da = new dataHangHoa();
                            da.ThemHangVaoTonKho(IDKho, IDHangHoa + "", 0 + "",GiaBanSauThue,GiaBan1,GiaBan2,GiaBan3,GiaBan4,GiaBan5);
                        }
                    }
                    if ((int)IDHangHoa != -1)
                    {
                        //DataTable d3 = hh.KiemTraBarcode(IDHangHoa + "", Barcode);
                        //if (d3.Rows.Count == 0)
                        //{
                        //    hh.ThemBarCode(IDHangHoa, IDTrangThaiBarcode, Barcode);
                        //}
                    }
                    else
                    {
                        DataRow drx = dd.Rows[0];
                        int ID = Int32.Parse(drx["ID"].ToString());
                        //DataTable d3 = hh.KiemTraBarcode(ID + "", Barcode);
                        //if (d3.Rows.Count == 0)
                        //{
                        //    hh.ThemBarCode(ID, IDTrangThaiBarcode, Barcode);
                        //}
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string HangQuyDoi = dr["HangQuyDoi"].ToString();
                    string MaHang = dr["MaHang"].ToString();

                    string IDHH = dataHangHoa.LayIDHangHoa(HangQuyDoi);
                    if (IDHH.CompareTo("-1") != 0)
                    {
                        dataHangHoa da = new dataHangHoa();
                        da.SuaThongTinHangHoa_HangQuyDoi(MaHang, IDHH);
                    }
                    else
                    {
                        IDHH = dataHangHoa.LayIDHangHoa_MaHang(MaHang);
                        dataHangHoa da = new dataHangHoa();
                        da.SuaThongTinHangHoa_HangQuyDoi(MaHang, IDHH);
                    }
                }

                data.XoaDuLieuTemp();
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa", Session["IDKho"].ToString(), "Danh mục", "Import hàng hóa");
                Response.Redirect("HangHoa.aspx");
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data.XoaDuLieuTemp();
            Response.Redirect("HangHoa.aspx");
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
            Import_Temp(dataTable);

        }

        private void Import_Temp(DataTable dataTable)
        {
            int intRow = dataTable.Rows.Count;
            if (intRow != 0)
            {
                for (int i = 0; i <= intRow - 1; i++)
                {
                    DataRow dr = dataTable.Rows[i];
                    dataNhomHang d = new dataNhomHang();
                    string IDNhomHang = dataNhomHang.LayIDNhom(dr["Nhom Hang"].ToString());
                    string MaHang = dr["Ma Hang"].ToString();
                    string TenHangHoa = dr["Ten Hang Hoa"].ToString();
                    string IDDonViTinh = dtDonViTinh.LayIDDonViTinh(dr["Don Vi Tinh"].ToString());
                    string HeSo = dtDonViTinh.LayIDDonViTinh(dr["He So"].ToString());
                    string IDHangSanXuat = dataHangSanXuat.LayIDHangSanXuat(dr["Hang San Xuat"].ToString());
                    string IDThue = dtDanhMucThue.LayIDThue(dr["Thue"].ToString());
                    string HangQuyDoi = dr["Hang Quy Doi"].ToString();
                    string IDNhomDatHang = dtNhomDatHang.LayIDNhomDatHang(dr["Nhom Dat Hang"].ToString());
                    string GiaMuaTruocThue = dr["Gia Mua Truoc Thue"].ToString();
                    string GiaBanTruocThue = dr["Gia Ban Truoc Thue"].ToString();
                    string GiaMuaSauThue = dr["Gia Mua Sau Thue"].ToString();
                    string GiaBanSauThue = dr["Gia Ban Sau Thue"].ToString();
                    string GiaBan1 = dr["Gia Ban1"].ToString();
                    string GiaBan2 = dr["Gia Ban2"].ToString();
                    string GiaBan3 = dr["Gia Ban3"].ToString();
                    string GiaBan4 = dr["Gia Ban4"].ToString();
                    string GiaBan5 = dr["Gia Ban5"].ToString();
                    string TrongLuong = dr["Trong Luong"].ToString();
                    string HanSuDung = dr["Han Su Dung"].ToString();
                    string IDTrangThaiHang = dataHangHoa.LayIDTrangThaiHang(dr["Trang Thai Hang"].ToString());
                    string GhiChu = dr["Ghi Chu"].ToString();
                    string IDTrangThaiBarcode = dataHangHoa.LayIDTrangThaiBarcode(dr["Trang Thai Barcode"].ToString());
                    string Barcode = dr["Barcode"].ToString();

                    data = new dtImportHangHoa();
                    data.insertHangHoa_temp(IDNhomHang, MaHang, TenHangHoa, IDDonViTinh, HeSo, IDHangSanXuat, IDThue, HangQuyDoi, IDNhomDatHang, GiaMuaTruocThue, GiaBanTruocThue, GiaMuaSauThue, GiaBanSauThue, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5, TrongLuong, HanSuDung, IDTrangThaiHang, GhiChu, IDTrangThaiBarcode, Barcode);
                }
                LoadGrid();
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

    }
}
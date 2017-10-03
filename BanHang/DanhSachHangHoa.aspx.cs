using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachHangHoa : System.Web.UI.Page
    {
        dataHangHoa data = new dataHangHoa();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 7) == 1)
                //{
                //    gridHangHoa.Columns["iconaction"].Visible = false;
                //    btnNhapExel.Enabled = false;
                //}

                //if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 7) == 1)
                //{
                    LoadGrid();
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}
            }
        }

        private void LoadGrid()
        {
            data = new dataHangHoa();
            gridHangHoa.DataSource = data.LayDanhSachHangHoa();
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dataHangHoa();
            data.XoaHangHoa(ID);
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid();
        }

        protected void gridHangHoa_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dataHangHoa();
            List<string> ListBarCode = GetListBarCode();
            string MaHang = e.NewValues["MaHang"].ToString();
            DataTable dd = data.KiemTraHangHoa(MaHang);
            if (dd.Rows.Count == 0)
            {
                string IDNhomHang = e.NewValues["IDNhomHang"].ToString();
                string TenHangHoa = e.NewValues["TenHangHoa"].ToString();
                TenHangHoa = dtSetting.convertDauSangKhongDau(TenHangHoa).ToUpper();
                string IDDonViTinh =e.NewValues["IDDonViTinh"].ToString();
                float GiaMua = float.Parse(e.NewValues["GiaMua"].ToString());
                float GiaBan = float.Parse(e.NewValues["GiaBan"].ToString());
                string GhiChu = e.NewValues["GhiChu"] != null ? e.NewValues["GhiChu"].ToString() : "";
                e.NewValues["HinhAnh"] = Session["data"];
                string HinhAnh = e.NewValues["HinhAnh"] != null ? e.NewValues["HinhAnh"].ToString() : "";
               // ASPxGridView grid = (sender as ASPxGridView);
               // ASPxUploadControl fileUpLoad = ((ASPxGridView)gridHangHoa).FindEditFormTemplateControl("UploadHinhAnh") as ASPxUploadControl;
                //string = Session["data"];
                //if (Page.IsValid && fileUpLoad.HasFile)
                //{
                //    HinhAnh = "HinhAnh/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + fileUpLoad.FileName;
                //    string filePath = MapPath(HinhAnh);
                //    fileUpLoad.SaveAs(filePath);
                //}

                object IDHangHoa = data.ThemHangHoa(IDNhomHang, MaHang, TenHangHoa, IDDonViTinh, GiaMua, GiaBan, GhiChu, HinhAnh);
                if (IDHangHoa != null)
                {
                    data.ThemDanhSachBarCode(IDHangHoa, ListBarCode);
                    DataTable dta = data.LayDanhSachCuaHang();
                    for (int i = 0; i < dta.Rows.Count; i++)
                    {
                        DataRow dr = dta.Rows[i];
                        int IDKho = Int32.Parse(dr["ID"].ToString());

                        data.ThemHangVaoTonKho(IDKho, (int)IDHangHoa, 0);
                    }
                }
                e.Cancel = true;
                gridHangHoa.CancelEdit();
                LoadGrid();
            }
            else Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại.'); </script>");
        }
        protected List<string> GetListBarCode()
        {
            ASPxTokenBox tkbListBarCode = gridHangHoa.FindEditFormTemplateControl("tkbListBarCode") as ASPxTokenBox;
            List<string> ListBarCode = new List<string>();
            foreach (string barCode in tkbListBarCode.Tokens)
            {
                ListBarCode.Add(barCode);
            }
            return ListBarCode;
        }

        protected TokenCollection LoadListBarCode(object ID)
        {
            TokenCollection listBarCode = new TokenCollection();
            if (ID != null)
            {
                DataTable dt = data.GetListBarCode(ID);
                foreach (DataRow row in dt.Rows)
                {
                    listBarCode.Add(row["Barcode"].ToString());
                }
            }
            return listBarCode;
        }
        protected void gridBarCode_Init(object sender, EventArgs e)
        {
            data = new dataHangHoa();
            ASPxGridView gridBarCode = sender as ASPxGridView;
            object IDHangHoa = gridBarCode.GetMasterRowKeyValue();
            gridBarCode.DataSource = data.GetListBarCode(IDHangHoa);
            gridBarCode.DataBind();
        }

        protected void gridBarCode_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            data = new dataHangHoa();
            ASPxGridView gridBarCode = sender as ASPxGridView;
            int ID = Int32.Parse(e.Keys["ID"].ToString());
            object IDHangHoa = gridBarCode.GetMasterRowKeyValue();
            string BarCode = e.NewValues["Barcode"] != null ? e.NewValues["Barcode"].ToString() : "";
            data.CapNhatBarCode(ID, IDHangHoa, BarCode);
            e.Cancel = true;
            gridBarCode.CancelEdit();
            gridBarCode.DataSource = data.GetListBarCode(IDHangHoa);
            gridBarCode.DataBind();
        }

        protected void gridBarCode_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dataHangHoa();
            ASPxGridView gridBarCode = sender as ASPxGridView;
            object IDHangHoa = gridBarCode.GetMasterRowKeyValue();
            string BarCode = e.NewValues["Barcode"].ToString();
            if (dataHangHoa.KiemTraBarcode(BarCode) == true)
            {

                data.ThemBarCode(IDHangHoa, BarCode);
            }
            else
            {
                throw new Exception("Lỗi:Barcode đã tồn tại");
            }
            e.Cancel = true;
            gridBarCode.CancelEdit();
            gridBarCode.DataSource = data.GetListBarCode(IDHangHoa);
            gridBarCode.DataBind();
        }

        protected void gridBarCode_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            data = new dataHangHoa();
            int ID = Int32.Parse(e.Keys["ID"].ToString());
            data.XoaBarCode(ID);
            e.Cancel = true;
            ASPxGridView gridBarCode = sender as ASPxGridView;
            object IDHangHoa = gridBarCode.GetMasterRowKeyValue();
            gridBarCode.DataSource = data.GetListBarCode(IDHangHoa);
            gridBarCode.DataBind();
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            data = new dataHangHoa();
            List<string> ListBarCode = GetListBarCode();
            string MaHang = e.NewValues["MaHang"].ToString();
            string IDNhomHang = e.NewValues["IDNhomHang"].ToString();
            string TenHangHoa = e.NewValues["TenHangHoa"].ToString();
            TenHangHoa = dtSetting.convertDauSangKhongDau(TenHangHoa).ToUpper();
            string IDDonViTinh = e.NewValues["IDDonViTinh"].ToString();
            float GiaMua = float.Parse(e.NewValues["GiaMua"].ToString());
            float GiaBan = float.Parse(e.NewValues["GiaBan"].ToString());
            string GhiChu = e.NewValues["GhiChu"] != null ? e.NewValues["GhiChu"].ToString() : "";
            string HinhAnh = e.NewValues["HinhAnh"] != null ? e.NewValues["HinhAnh"].ToString() : "";
            string ID = e.Keys[0].ToString();
            data.SuaThongTinHangHoa(ID, IDNhomHang, MaHang, TenHangHoa, IDDonViTinh, GiaMua, GiaBan, GhiChu, HinhAnh);
            data.SuaDanhSachBarCode(e.Keys["ID"] as object, ListBarCode);
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid();

        }

        protected void gridHangHoa_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["MaHang"] = dataHangHoa.Dem_Max();
        }

        protected void ASPxUploadControl1_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.IsValid)
            {
              
                string path = Page.MapPath("~/HinhAnh/") + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_")  + e.UploadedFile.FileName;
                if (File.Exists(path))
                    File.Delete(path);
                e.UploadedFile.SaveAs(path);
                Session["data"] = path;
            }
        }
    }
}
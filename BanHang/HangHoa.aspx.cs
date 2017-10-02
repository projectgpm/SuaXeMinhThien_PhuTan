using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.IO;
using DevExpress.XtraRichEdit.API.Native;

namespace BanHang
{
    public partial class HangHoa : System.Web.UI.Page
    {
        //dataHangHoa data = new dataHangHoa();
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
                    LoadGrid();
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                    {
                        gridHangHoa.Columns["chucnang1"].Visible = false;
                        gridHangHoa.Columns["chucnang2"].Visible = false;
                        btnTheMoi.Enabled = false;
                        btnNhapExel.Enabled = false;
                    }
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
        public void LoadGrid()
        {
            dataHangHoa data = new dataHangHoa();
            gridHangHoa.DataSource = data.getDanhSachHangHoa();
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            
        }

        //protected void gridBarCode_Init(object sender, EventArgs e)
        //{
        //    dataHangHoa data = new dataHangHoa();
        //    ASPxGridView gridBarCode = sender as ASPxGridView;
        //    object IDHangHoa = gridBarCode.GetMasterRowKeyValue();
        //    gridBarCode.DataSource = data.GetListBarCode(IDHangHoa);
        //    gridBarCode.DataBind();
        //}

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            HangHoaExport1.DataSource = data.getDanhSachHangHoa_Full_Barcode();
            HangHoaExport1.DataBind();
            HangHoaExport.WriteXlsToResponse();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa", Session["IDKho"].ToString(), "Danh mục", "Xuất excel");
        }

        protected void btnNhapExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImportExcel_HangHoa.aspx");
        }

        protected void XuatFilePDF_Click(object sender, EventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            HangHoaExport1.DataSource = data.getDanhSachHangHoa_Full_Barcode();
            HangHoaExport1.DataBind();
            HangHoaExport.WritePdfToResponse();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa", Session["IDKho"].ToString(), "Danh mục", "Xuất pdf");
        }

        protected void gridHangHoa_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            int ID = Int32.Parse(e.Keys["ID"].ToString());
            data.XoaHangHoa(ID);
            e.Cancel = true;
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa", Session["IDKho"].ToString(), "Danh mục", "Xóa");
        }

        protected void btnTheMoi_Click(object sender, EventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            Response.Redirect("HangHoa_Page.aspx");
        }

    }
}
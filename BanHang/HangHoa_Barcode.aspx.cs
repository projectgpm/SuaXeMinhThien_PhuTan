using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HangHoa_Barcode : System.Web.UI.Page
    {
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
                    Load();
                    if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 58) == false)
                        gridHangHoaBarcode.Columns["chucnang"].Visible = false;
                }
            }
        }

        public void Load()
        {
            dataHangHoa data = new dataHangHoa();
            string IDHangHoa = Request.QueryString["IDHangHoa"];
            gridHangHoaBarcode.DataSource = data.GetListBarCode(IDHangHoa);
            gridHangHoaBarcode.DataBind();
        }

        protected void gridHangHoaBarcode_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangHoa data = new dataHangHoa();
            data.deleteHangHoa_Barcode(ID);
            e.Cancel = true;
            gridHangHoaBarcode.CancelEdit();
            Load();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa barcode", Session["IDKho"].ToString(), "Danh mục", "Xóa");
        }

        protected void gridHangHoaBarcode_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["IDTrangThaiBarcode"] != null && e.NewValues["Barcode"] != null)
            {
                string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
                string Barcode = e.NewValues["Barcode"].ToString();

                string IDHangHoa = Request.QueryString["IDHangHoa"];
                dataHangHoa data = new dataHangHoa();
                DataTable da = data.KiemTraBarcode(Barcode);
                if (da.Rows.Count == 0)
                {
                    data.insertHangHoa_Barcode(IDHangHoa, IDTrangThaiBarcode, Barcode);
                    e.Cancel = true;
                    gridHangHoaBarcode.CancelEdit();
                }
                else throw new Exception("Barcode đã tồn tại.");


            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa barcode", Session["IDKho"].ToString(), "Danh mục", "Thêm");
        }

        protected void gridHangHoaBarcode_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["IDTrangThaiBarcode"] != null && e.NewValues["Barcode"] != null)
            {
                string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
                string Barcode = e.NewValues["Barcode"].ToString();
                string ID = e.Keys[0].ToString();

                dataHangHoa data = new dataHangHoa();
                DataTable da = data.KiemTraBarcode(Barcode);
                if (da.Rows.Count == 0)
                {
                    data.updateHangHoa_Barcode(ID, IDTrangThaiBarcode, Barcode);
                    e.Cancel = true;
                    gridHangHoaBarcode.CancelEdit();
                }
                else throw new Exception("Barcode đã tồn tại.");
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa barcode", Session["IDKho"].ToString(), "Danh mục", "Cập nhật");
        }
    }
}
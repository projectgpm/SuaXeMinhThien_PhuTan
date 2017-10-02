using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HangHoa_GiaTheoSL : System.Web.UI.Page
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
                        gridHangHoaGiaTheoSL.Columns["chucnang"].Visible = false;
                }
            }
        }

        public void Load()
        {
            dataHangHoa data = new dataHangHoa();
            string IDHangHoa = Request.QueryString["IDHangHoa"];
            gridHangHoaGiaTheoSL.DataSource = data.GetListHangHoa_GiaTheoSL(IDHangHoa);
            gridHangHoaGiaTheoSL.DataBind();
        }

        protected void gridHangHoaGiaTheoSL_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangHoa data = new dataHangHoa();
            data.deleteHangHoa_TheoSL(ID);
            e.Cancel = true;
            gridHangHoaGiaTheoSL.CancelEdit();
            Load();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa giá theo SL", Session["IDKho"].ToString(), "Danh mục", "Xóa");
        }

        protected void gridHangHoaGiaTheoSL_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["SoLuongBD"] != null && e.NewValues["SoLuongKT"] != null && e.NewValues["GiaBan"] != null)
            {
                string SoLuongBD = e.NewValues["SoLuongBD"].ToString();
                string SoLuongKT = e.NewValues["SoLuongKT"].ToString();
                string GiaBan = e.NewValues["GiaBan"].ToString();

                string IDHangHoa = Request.QueryString["IDHangHoa"];
                dataHangHoa data = new dataHangHoa();
                data.insertHangHoa_GiaTheoSL(IDHangHoa, SoLuongBD, SoLuongKT, GiaBan);

                e.Cancel = true;
                gridHangHoaGiaTheoSL.CancelEdit();
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa giá theo SL", Session["IDKho"].ToString(), "Danh mục", "Thêm");
        }

        protected void gridHangHoaGiaTheoSL_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["SoLuongBD"] != null && e.NewValues["SoLuongKT"] != null && e.NewValues["GiaBan"] != null)
            {
                string SoLuongBD = e.NewValues["SoLuongBD"].ToString();
                string SoLuongKT = e.NewValues["SoLuongKT"].ToString();
                string GiaBan = e.NewValues["GiaBan"].ToString();
                string ID = e.Keys[0].ToString();

                dataHangHoa data = new dataHangHoa();
                data.updateHangHoa_GiaTheoSL(ID, SoLuongBD, SoLuongKT, GiaBan);

                e.Cancel = true;
                gridHangHoaGiaTheoSL.CancelEdit();
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa giá theo SL", Session["IDKho"].ToString(), "Danh mục", "Cập nhật");
        }
    }
}
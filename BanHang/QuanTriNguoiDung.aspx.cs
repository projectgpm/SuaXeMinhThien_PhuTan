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
    public partial class QuanTriNguoiDung : System.Web.UI.Page
    {
        dtQuanTriNguoiDung data = new dtQuanTriNguoiDung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
               
                   
                    LoadGrid();
                
            }
        }
        public void LoadGrid()
        {
            data = new dtQuanTriNguoiDung();
            gridQuanTriNguoiDung.DataSource = data.LayDanhSachNguoiDung();
            gridQuanTriNguoiDung.DataBind();
        }
        protected void gridQuanTriNguoiDung_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtQuanTriNguoiDung();
            data.XoaNguoiDung(Int32.Parse(ID));
            e.Cancel = true;
            gridQuanTriNguoiDung.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Quản Trị người dùng", Session["IDKho"].ToString(), "Hệ Thống", "Xóa");
         
        }

        protected void gridQuanTriNguoiDung_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
           
            data = new dtQuanTriNguoiDung();
            string TenNguoiDung = e.NewValues["TenNguoiDung"].ToString();
            int IDNhomNguoiDung = Int32.Parse(e.NewValues["IDNhomNguoiDung"].ToString());
            string IDKho = e.NewValues["IDKho"].ToString();
            string SDT = e.NewValues["SDT"].ToString();
            string MatKhau = e.NewValues["MatKhau"].ToString();
            MatKhau = dtSetting.GetSHA1HashData(MatKhau);
            string TenDangNhap = e.NewValues["TenDangNhap"].ToString().ToUpper();

            if (dtQuanTriNguoiDung.KiemTraNguoiDung(TenDangNhap.Trim()) != -1)
            {
                throw new Exception("Lỗi: Tên đăng nhập đã tồn tại");
            }
            else
            {
                data.ThemNguoiDung(TenNguoiDung, TenDangNhap, IDNhomNguoiDung, SDT, MatKhau, IDKho);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Quản Trị người dùng", Session["IDKho"].ToString(), "Hệ Thống", "Thêm");
            }
            e.Cancel = true;
            gridQuanTriNguoiDung.CancelEdit();
            LoadGrid();   
        }

        protected void gridQuanTriNguoiDung_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            string TenNguoiDung = e.NewValues["TenNguoiDung"].ToString();
            int IDNhomNguoiDung = Int32.Parse(e.NewValues["IDNhomNguoiDung"].ToString());
            string IDKho = e.NewValues["IDKho"].ToString();
            string SDT = e.NewValues["SDT"].ToString();
            string MatKhau = e.NewValues["MatKhau"].ToString();
            MatKhau = dtSetting.GetSHA1HashData(MatKhau);
            string TenDangNhap = e.NewValues["TenDangNhap"].ToString().ToUpper();
            if (dtQuanTriNguoiDung.KT_Tendangnhap_CapNhat(TenDangNhap.Trim(), ID) == -1)
            {
                if (dtQuanTriNguoiDung.KiemTraNguoiDung(TenDangNhap.Trim()) == 1)
                {
                    throw new Exception("Lỗi: Tên đăng nhập đã tồn tại");
                }
                else
                {
                    data.SuaNguoiDung(Int32.Parse(ID), TenNguoiDung, TenDangNhap, IDNhomNguoiDung, SDT, MatKhau, IDKho);
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Quản Trị người dùng", Session["IDKho"].ToString(), "Hệ Thống", "Cập Nhật");
                }
            }
            else
            {
                data.SuaNguoiDung(Int32.Parse(ID), TenNguoiDung, TenDangNhap, IDNhomNguoiDung, SDT, MatKhau, IDKho);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Quản Trị người dùng", Session["IDKho"].ToString(), "Hệ Thống", "Cập Nhật");
            }
            e.Cancel = true;
            gridQuanTriNguoiDung.CancelEdit();
            LoadGrid();
        }

        protected void gridQuanTriNguoiDung_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["IDKho"] = 1;
            e.NewValues["IDNhomNguoiDung"] = 1;
             
        }
    }
}
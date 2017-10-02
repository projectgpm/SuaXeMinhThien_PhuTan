using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class KhachHang : System.Web.UI.Page
    {
        dtKhachHang data = new dtKhachHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //btnNhapExcel.Enabled = false;
               // gridKhachHang.Columns["chucnang"].Visible = false;
                LoadGrid();
            }
            //else
            //{
            //    Response.Redirect("Default.aspx");
            //}
        }
        
        public void LoadGrid()
        {
            data = new dtKhachHang();
            gridKhachHang.DataSource = data.LayDanhSachKhachHang(Session["IDKho"].ToString());
            gridKhachHang.DataBind();
        }

        protected void gridKhachHang_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            data = new dtKhachHang();
            int IDNhomKhachHang = Int32.Parse(e.NewValues["IDNhomKhachHang"].ToString());
            string TenKhachHang = e.NewValues["TenKhachHang"] == null ? "" : e.NewValues["TenKhachHang"].ToString();
            string NgaySinh = e.NewValues["NgaySinh"] == null ? "" : e.NewValues["NgaySinh"].ToString();
            string CMND = e.NewValues["CMND"] == null ? "" : e.NewValues["CMND"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            string ChietKhau = e.NewValues["ChietKhau"].ToString();
            if (dtKhachHang.KT_SDT_KH_CapNhat(DienThoai.Trim(), ID) == -1)
            {
                if (dtKhachHang.KT_SDT_KH(DienThoai.Trim()) == 1)
                {
                    throw new Exception("Lỗi: Số điện thoại đã tồn tại?");
                }
            }
            else
            {

                data.SuaThongTinKhachHang(Int32.Parse(ID), IDNhomKhachHang, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, GhiChu, ChietKhau);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");   
            }
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            
        }

        protected void gridKhachHang_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dtKhachHang();
            int IDNhomKhachHang = Int32.Parse(e.NewValues["IDNhomKhachHang"].ToString());
            string TenKhachHang = e.NewValues["TenKhachHang"].ToString();
            DateTime NgaySinh = DateTime.Parse( e.NewValues["NgaySinh"] == null ? DateTime.Now.ToString() : e.NewValues["NgaySinh"].ToString());
            string CMND = e.NewValues["CMND"] == null ? "" : e.NewValues["CMND"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string MaKh = "";
            string Barcode = "";
            object ID;
            string ChietKhau = e.NewValues["ChietKhau"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (DienThoai != "")
            {
                if (dtKhachHang.KT_SDT_KH(DienThoai.Trim()) != -1)
                {
                    throw new Exception("Lỗi: Số điện thoại đã tồn tại?");
                }
                else
                {
                    ID = data.ThemKhachHang(IDNhomKhachHang, MaKh, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, Barcode, GhiChu, Session["IDKho"].ToString(), ChietKhau);
                    if (ID != null)
                    {
                        if (e.NewValues["MaKhachHang"] == null)
                        {
                            data = new dtKhachHang();
                            data.CapNhatMaKhachHang(ID, (dtSetting.LayMaKho(Session["IDKho"].ToString()) + "." + (Int32.Parse(ID.ToString()) * 0.0001).ToString().Replace(".", "")).ToString(), (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001)).Replace(".", ""));
                            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                        }
                    }
                }
            }
            else
            {
                ID = data.ThemKhachHang(IDNhomKhachHang, MaKh, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, Barcode, GhiChu, Session["IDKho"].ToString(), ChietKhau);
                if (ID != null)
                {
                    if (e.NewValues["MaKhachHang"] == null)
                    {
                        data = new dtKhachHang();
                        data.CapNhatMaKhachHang(ID, (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001).ToString().Replace(".", "")).ToString(), (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001)).Replace(".", ""));
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                    }
                }
            }
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            
        }

        protected void gridKhachHang_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKhachHang();
            data.XoaKhachHang(Int32.Parse(ID));
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + ID, Session["IDKho"].ToString(), "Danh Mục", "Xóa");  
        }

        protected void btnXuatPDF_Click(object sender, EventArgs e)
        {
            XuatDuLieu.WritePdfToResponse();
        }

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            XuatDuLieu.WriteXlsToResponse();
        }

        protected void btnNhapExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImportExcel_KhachHang.aspx");
        }

        protected void gridKhachHang_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["ChietKhau"] = "0";
        }
    }
}
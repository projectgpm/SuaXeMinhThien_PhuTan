using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class Vung : System.Web.UI.Page
    {
        dtVung data = new dtVung();
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
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                    {
                        gridVung.Columns["chucnang"].Visible = false;
                    }
                    LoadGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        private void LoadGrid()
        {
            data = new dtVung();
            gridVung.DataSource = data.LayDanhSach();
            gridVung.DataBind();
        }

        protected void gridVung_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string MaVung = e.NewValues["MaVung"].ToString();
            string TenVung = e.NewValues["TenVung"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (dtSetting.IsNumber(MaVung) == true)
            {
                data = new dtVung();
                data.Sua(ID, MaVung, TenVung, GhiChu);
                e.Cancel = true;
                gridVung.CancelEdit();
                LoadGrid();

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Vung" + TenVung, Session["IDKho"].ToString(), "Hệ thống", "Cập nhật");
            }
            else
            {
                throw new Exception("Lỗi: Mã vùng phải là số");
            }
        }

        protected void gridVung_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dtVung();
            string MaVung = e.NewValues["MaVung"].ToString();
            string TenVung = e.NewValues["TenVung"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (dtSetting.IsNumber(MaVung) == true)
            {
                data = new dtVung();
                data.Them(MaVung, TenVung, GhiChu);
                e.Cancel = true;
                gridVung.CancelEdit();
                LoadGrid();

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Vung" + TenVung, Session["IDKho"].ToString(), "Hệ thống", "Thêm");
            }
            else
            {
                throw new Exception("Lỗi: Mã vùng phải là số");
            }
        }

        protected void gridVung_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            data = new dtVung();
            string ID = e.Keys[0].ToString();
            data.Xoa(ID);
            e.Cancel = true;
            gridVung.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Vung" + ID, Session["IDKho"].ToString(), "Hệ thống", "Xóa"); 
        }

        protected void gridVung_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            int Max = dtVung.LayID_Max();
            e.NewValues["MaVung"] = ((Max + 1) * 0.0001).ToString().Replace(".", "");
        }
    }
}
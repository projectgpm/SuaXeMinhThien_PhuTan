using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachGiaTheoVung : System.Web.UI.Page
    {
        dtGiaTheoVung data = new dtGiaTheoVung();
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
                        cmbChiNhanh.Enabled = false;
                        btnGiaTheoKho.Enabled = false;
                        btnThayDoiGiaTheoGio.Enabled = false;
                    }
                    if (cmbChiNhanh.Text != "")
                    {
                        LoadGrid(cmbChiNhanh.Value.ToString());
                    }
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }

        private void LoadGrid(string IDKho)
        {
            data = new dtGiaTheoVung();
            gridHangHoa.DataSource = data.DanhSachHangHoa_IDKho(IDKho);
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["GiaBan"] != null && e.NewValues["GiaBan1"] != null && e.NewValues["GiaBan2"] != null && e.NewValues["GiaBan3"] != null && e.NewValues["GiaBan4"] != null && e.NewValues["GiaBan5"] != null)
            {
                string IDKho = cmbChiNhanh.Value.ToString();
                string ID = e.Keys[0].ToString();
                string GiaBan0 = e.NewValues["GiaBan"].ToString();
                string GiaBan1 = e.NewValues["GiaBan1"].ToString();
                string GiaBan2 = e.NewValues["GiaBan2"].ToString();
                string GiaBan3 = e.NewValues["GiaBan3"].ToString();
                string GiaBan4 = e.NewValues["GiaBan4"].ToString();
                string GiaBan5 = e.NewValues["GiaBan5"].ToString();
                string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(e.NewValues["MaHang"].ToString());
                float giacu0 = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKho);
                float giacu1 = dtCapNhatTonKho.GiaBan1_KhoChiNhanh(IDHangHoa, IDKho);
                float giacu2 = dtCapNhatTonKho.GiaBan2_KhoChiNhanh(IDHangHoa, IDKho);
                float giacu3 = dtCapNhatTonKho.GiaBan3_KhoChiNhanh(IDHangHoa, IDKho);
                float giacu4 = dtCapNhatTonKho.GiaBan4_KhoChiNhanh(IDHangHoa, IDKho);
                float giacu5 = dtCapNhatTonKho.GiaBan5_KhoChiNhanh(IDHangHoa, IDKho);
                string MaHang = e.NewValues["MaHang"].ToString();
                string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                string IDNhanVien = Session["IDNhanVien"].ToString();
                if (float.Parse(GiaBan0) != giacu0)
                {
                    dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu0.ToString(), GiaBan0, IDNhanVien, "Thay đổi giá theo vùng(GiaBan)");
                }
                if (float.Parse(GiaBan1) != giacu1)
                {
                    dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu1.ToString(), GiaBan1, IDNhanVien, "Thay đổi giá theo vùng(GiaBan1)");
                }
                if (float.Parse(GiaBan2) != giacu2)
                {
                    dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu2.ToString(), GiaBan2, IDNhanVien, "Thay đổi giá theo vùng(GiaBan2)");
                }
                if (float.Parse(GiaBan3) != giacu3)
                {
                    dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu3.ToString(), GiaBan3, IDNhanVien, "Thay đổi giá theo vùng(GiaBan3)");
                }
                if (float.Parse(GiaBan4) != giacu4)
                {
                    dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu4.ToString(), GiaBan4, IDNhanVien, "Thay đổi giá theo vùng(GiaBan4)");
                }
                if (float.Parse(GiaBan5) != giacu5)
                {
                    dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu5.ToString(), GiaBan5, IDNhanVien, "Thay đổi giá theo vùng(GiaBan5)");
                }
                dtGiaTheoVung.CapNhat_GiaTheoVung(ID, IDKho, GiaBan0, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5);
                e.Cancel = true;
                gridHangHoa.CancelEdit();
                LoadGrid(IDKho);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Kho", Session["IDKho"].ToString(), "Danh Mục", "Cập nhật giá theo vùng");
            }
            else
            {
                throw new Exception("Lỗi: Giá không được bỏ trống? Vui lòng kiểm tra lại.");
            }
        }

        protected void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.Text != "")
            {
                string IDKho = cmbChiNhanh.Value.ToString();
                LoadGrid(IDKho);
            }
        }
    }
}
using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ThanhToanChietKhau : System.Web.UI.Page
    {
        dtThanhToanChietKhau data = new dtThanhToanChietKhau();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "")
            {
                int KT = 0;
                double TongTienChietKhau = 0;
                foreach (var key in gridDanhSach.GetCurrentPageRowValues("ID"))
                {
                    if (gridDanhSach.Selection.IsRowSelectedByKey(key))
                    {
                        string ID = key.ToString();
                        KT = 1;
                        TongTienChietKhau = TongTienChietKhau + dtThanhToanChietKhau.LayTienChietKhau(ID.ToString());
                    }

                }
                //thêm vào trả chiết khấu + cập nhật
                string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                if (KT == 1)
                {
                    object ID = data.ThemThanhToanChietKhau(cmbKhachHang.Value.ToString(), TongTienChietKhau, GhiChu);
                    if (ID != null)
                    {
                        foreach (var key in gridDanhSach.GetCurrentPageRowValues("ID"))
                        {
                            if (gridDanhSach.Selection.IsRowSelectedByKey(key))
                            {
                                string IDHoaDon = key.ToString();
                                data = new dtThanhToanChietKhau();
                                data.CapNhatTinhTrang(IDHoaDon);
                            }

                        }
                        Response.Redirect("ChiTietThanhToanChietKhau.aspx");
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Không có đơn hàng để thanh toán chiết khấu.'); </script>");
                }
            }
            else
            {
                cmbKhachHang.Focus();
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn khách hàng.'); </script>");
            }
        }

        protected void cmbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "")
            {
                string iDKhachHang = cmbKhachHang.Value.ToString();
                gridDanhSach.DataSource = data.DanhSachChuaChietKhau(iDKhachHang);
                gridDanhSach.DataBind();
            }
        }
    }
}
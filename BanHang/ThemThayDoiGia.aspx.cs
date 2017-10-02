using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ThemThayDoiGia : System.Web.UI.Page
    {
        dtGiaTheoGio dt = new dtGiaTheoGio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 61) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    Random ran = new Random();
                    ID_temp.Value = ran.Next(100000, 999999).ToString();
                    DanhSachVung();
                }
                LoadGrid(ID_temp.Value.ToString());
            }
        }
        private void DanhSachVung()
        {
            dtGiaTheoVung data = new dtGiaTheoVung();
            data = new dtGiaTheoVung();
            DanhSachKho.DataSource = data.LayDanhSachKho();
            DanhSachKho.ValueField = "ID";
            DanhSachKho.TextField = "TenCuaHang";
            DanhSachKho.DataBind();
            DanhSachKho.SelectAll();

            dtVung dt1 = new dtVung();
            DataTable d2x = dt1.LayDanhSach();
            d2x.Rows.Add(-1, 0, "Tất cả Vùng", 0);
            cmbVung.DataSource = d2x;
            cmbVung.ValueField = "ID";
            cmbVung.TextField = "TenVung";
            cmbVung.DataBind();
            cmbVung.SelectedIndex = cmbVung.Items.Count;


        }
        private void LoadGrid(string IDtemp)
        {
            dt = new dtGiaTheoGio();
            gridHangHoa.DataSource = dt.DanhSachHangHoa(IDtemp);
            gridHangHoa.DataBind();
        }
        protected void cmbVung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVung.Text != "")
            {
                int ID = Int32.Parse(cmbVung.Value.ToString());
                dtGiaTheoVung data = new dtGiaTheoVung();
                DanhSachKho.DataSource = data.LayDanhSachKho_IDVung(ID);
                DanhSachKho.ValueField = "ID";
                DanhSachKho.TextField = "TenCuaHang";
                DanhSachKho.DataBind();
                DanhSachKho.SelectAll();
            }
        }
      

        protected void cmbMaHang_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            SqlMaHang.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
	                                                            INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
                                            WHERE (GPM_HangHoa.MaHang LIKE @MaHang) AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";
            SqlMaHang.SelectParameters.Clear();
            SqlMaHang.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            //SqlMaHang.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            SqlMaHang.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            SqlMaHang.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            SqlMaHang.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = SqlMaHang;
            comboBox.DataBind();
        }

        protected void cmbMaHang_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            SqlMaHang.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                             INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID)";
            SqlMaHang.SelectParameters.Clear();
            SqlMaHang.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = SqlMaHang;
            comboBox.DataBind();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbMaHang.Text != "" && txtGioThayDoi.Text != "")
            {
                if (DanhSachKho.SelectedItems.Count != 0)
                {
                    string IDHangHoa = cmbMaHang.Value.ToString();
                    float GiaBan = dtHangHoa.GiaBan0(IDHangHoa, Session["IDKho"].ToString());
                    float GiaBan1 = dtHangHoa.GiaBan1(IDHangHoa, Session["IDKho"].ToString());
                    float GiaBan2 = dtHangHoa.GiaBan2(IDHangHoa, Session["IDKho"].ToString());
                    float GiaBan3 = dtHangHoa.GiaBan3(IDHangHoa, Session["IDKho"].ToString());
                    float GiaBan4 = dtHangHoa.GiaBan4(IDHangHoa, Session["IDKho"].ToString());
                    float GiaBan5 = dtHangHoa.GiaBan5(IDHangHoa, Session["IDKho"].ToString());
                    string IDTemp = ID_temp.Value.ToString();
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                    DateTime GioThayDoi = DateTime.Parse(txtGioThayDoi.Text.ToString());
                    for (int i = DanhSachKho.Items.Count - 1; i >= 0; i--)
                    {
                        if (DanhSachKho.Items[i].Selected == true)
                        {
                            string IDKhoApDung = DanhSachKho.Items[i].Value.ToString();
                            // thêm temp
                            dt = new dtGiaTheoGio();
                            DataTable db = dt.KT_ThemChiTiet_Temp(IDHangHoa, IDTemp, IDKhoApDung, GioThayDoi);
                            if (db.Rows.Count == 0)
                            {
                                dt.ThemChiTiet_Temp(IDTemp, MaHang, IDHangHoa, IDDonViTinh, GiaBan.ToString(), GiaBan1.ToString(), GiaBan2.ToString(), GiaBan3.ToString(), GiaBan4.ToString(), GiaBan5.ToString(), GioThayDoi, IDKhoApDung);
                            }
                            else
                            {
                                Response.Write("<script language='JavaScript'> alert('Mã hàng đã tại trong kho này.'); </script>");
                            }
                        }
                    }
                    LoadGrid(IDTemp);
                }
                else
                    Response.Write("<script language='JavaScript'> alert('Vui lòng chọn kho để áp dụng.'); </script>");
            }
            else
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập trường có dấu (*).'); </script>");
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dt = new dtGiaTheoGio();
            dt.Xoa_temp(ID);
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(ID_temp.Value.ToString());
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            dt = new dtGiaTheoGio();
            dt.XoaAll_temp(ID_temp.Value.ToString());
            Response.Redirect("GiaTheoGio.aspx");
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            if (e.NewValues["GiaBan"] != null && e.NewValues["GiaBan1"] != null && e.NewValues["GiaBan2"] != null && e.NewValues["GiaBan3"] != null && e.NewValues["GiaBan4"] != null && e.NewValues["GiaBan5"] != null)
            {
                string GiaBan0 = e.NewValues["GiaBan"].ToString();
                string GiaBan1 = e.NewValues["GiaBan1"].ToString();
                string GiaBan2 = e.NewValues["GiaBan2"].ToString();
                string GiaBan3 = e.NewValues["GiaBan3"].ToString();
                string GiaBan4 = e.NewValues["GiaBan4"].ToString();
                string GiaBan5 = e.NewValues["GiaBan5"].ToString();
                DateTime GioThayDoi = DateTime.Parse(e.NewValues["GioThayDoi"].ToString());
                dt = new dtGiaTheoGio();
                dt.CapNhatChiTiet_Temp(ID, GiaBan0, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5, GioThayDoi);
            }
            else
            {
                throw new Exception("Lỗi: Giá không được bỏ trống? Vui lòng kiểm tra lại.");
            }
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(ID_temp.Value.ToString());
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            dt = new dtGiaTheoGio();
            DataTable db = dt.DanhSachHangHoa(ID_temp.Value.ToString());
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string MaHang = dr["MaHang"].ToString();
                    string IDHangHoa = dr["IDHangHoa"].ToString();
                    string IDDonViTinh = dr["IDDonViTinh"].ToString();
                    string GiaBan = dr["GiaBan"].ToString();
                    string GiaBan1 = dr["GiaBan1"].ToString();
                    string GiaBan2 = dr["GiaBan2"].ToString();
                    string GiaBan3 = dr["GiaBan3"].ToString();
                    string GiaBan4 = dr["GiaBan4"].ToString();
                    string GiaBan5 = dr["GiaBan5"].ToString();
                    string GioThayDoi = dr["GioThayDoi"].ToString();
                    string IDKho = dr["IDKho"].ToString();
                    string IDNhanVien = Session["IDNhanVien"].ToString();
                    dt = new dtGiaTheoGio();
                    //kiểm tra
                    DataTable db1 = dt.KT_ThemChiTiet(IDHangHoa, IDKho, DateTime.Parse(GioThayDoi));
                    if (db1.Rows.Count == 0)
                    {
                        dt.ThemChiTiet(MaHang, IDHangHoa, IDDonViTinh, GiaBan, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5, DateTime.Parse(GioThayDoi), IDKho, IDNhanVien);
                    }
                }
                dt = new dtGiaTheoGio();
                dt.XoaAll_temp(ID_temp.Value.ToString());
                Response.Redirect("GiaTheoGio.aspx");
            }
            else
            {
                cmbMaHang.Focus();
                Response.Write("<script language='JavaScript'> alert('Danh sách thay đổi giá trống.'); </script>");
            }
        }
    }
}
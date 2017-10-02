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
    public partial class PhieuXuatKhac : System.Web.UI.Page
    {
        dtPhieuXuatKhac data = new dtPhieuXuatKhac();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 70) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {

                    DateTime date = DateTime.Now;
                    int thang = date.Month;
                    int year = date.Year;
                    string ngayBD = year + "-" + thang + "-01 00:00:00.000";
                    string ngayKT = year + "-" + thang + "-" + dtSetting.tinhSoNgay(thang, year) + " 00:00:00.000";

                    //Random ran = new Random();
                    IDPhieuXuatKhac_Temp.Value = Session["IDNhanVien"].ToString();// ran.Next(100000, 999999).ToString();
                    txtSoDonXuat.Text = (dtSetting.LayMaKho(Session["IDKho"].ToString()) + "-" + dtPhieuXuatKhac.TongSoXuatTrongThang(ngayBD, ngayKT, Session["IDKho"].ToString()) + "-" + (DateTime.Now.ToString("ddMMyyyy")));
                    cmbNguoiLapPhieu.Text = Session["IDNhanVien"].ToString();
                    cmbKho.Text = Session["IDKho"].ToString();
                }
                LoadGrid(IDPhieuXuatKhac_Temp.Value.ToString());
            }
        }
       
        public void Clear()
        {
            cmbHangHoa.Text = "";
            txtSoLuong.Text = "";
            txtTonKho.Text = "";
            cmbDonViTinh.Text = "";
            txtGhiChuHH.Text = "";
          
        }
        protected void cmbNgayLapPhieu_Init(object sender, EventArgs e)
        {
            cmbNgayLapPhieu.Date = DateTime.Today;
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                txtTonKho.Text = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), Session["IDKho"].ToString()) + "";
                cmbDonViTinh.Value = dtHangHoa.LayIDDonViTinh(cmbHangHoa.Value.ToString());
                txtSoLuong.Text = "0";
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbHangHoa.Value != null && txtSoLuong.Text != "")
            {
                int SoLuong = Int32.Parse(txtSoLuong.Value.ToString());
                if (SoLuong > 0)
                {
                    int SLTon = Int32.Parse(txtTonKho.Text);
                    string IDHangHoa = cmbHangHoa.Value.ToString();
                    string IDPhieuXuatKhac = IDPhieuXuatKhac_Temp.Value.ToString();
                    string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    string TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa).ToString();
                    string TonKho = txtTonKho.Text.ToString();
                    string GhiChuHH = txtGhiChuHH.Text == null ? "" : txtGhiChuHH.Text.ToString();
                    if (dtSetting.KT_ChuyenAm() == 0)
                    {
                        if (SLTon < SoLuong)
                        {
                            txtSoLuong.Text = SLTon.ToString();
                            Response.Write("<script language='JavaScript'> alert('Hàng hóa tồn kho không đủ.'); </script>");
                        }
                        else
                        {
                            DataTable db = data.KTChiTietPhieuXuatKhac_Temp(IDHangHoa, IDPhieuXuatKhac);// kiểm tra hàng hóa
                            if (db.Rows.Count == 0)
                            {
                                data = new dtPhieuXuatKhac();
                                data.ThemPhieuXuatKhac_Temp(IDPhieuXuatKhac, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, TonKho, SoLuong.ToString(), GhiChuHH);
                                Clear();
                            }
                            else
                            {
                                data = new dtPhieuXuatKhac();
                                data.UpdatePhieuXuatKhac_temp(IDPhieuXuatKhac, IDHangHoa, SoLuong);
                                Clear();
                            }
                            LoadGrid(IDPhieuXuatKhac);
                        }
                    }
                    else
                    {
                        DataTable db = data.KTChiTietPhieuXuatKhac_Temp(IDHangHoa, IDPhieuXuatKhac);// kiểm tra hàng hóa
                        if (db.Rows.Count == 0)
                        {
                            data = new dtPhieuXuatKhac();
                            data.ThemPhieuXuatKhac_Temp(IDPhieuXuatKhac, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, TonKho, SoLuong.ToString(), GhiChuHH);
                            Clear();
                        }
                        else
                        {
                            data = new dtPhieuXuatKhac();
                            data.UpdatePhieuXuatKhac_temp(IDPhieuXuatKhac, IDHangHoa, SoLuong);
                            Clear();
                        }
                        if (SLTon < SoLuong)
                        {
                            Response.Write("<script language='JavaScript'> alert('Số hàng tồn trong kho hiện tại không đủ.'); </script>");
                        }
                        LoadGrid(IDPhieuXuatKhac);
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Số Lượng phải > 0.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Bạn chưa chọn hàng hóa.'); </script>");
            }
        }

        private void LoadGrid(string IDPhieuXuatKhac)
        {
            data = new dtPhieuXuatKhac();
            gridDanhSachHangHoa_Temp.DataSource = data.LayDanhSachPhieuXuatKhac(IDPhieuXuatKhac);
            gridDanhSachHangHoa_Temp.DataBind();

        }

        protected void btnHuyPhieuXuatKhac_Click(object sender, EventArgs e)
        {
            data = new dtPhieuXuatKhac();
            int ID = Int32.Parse(IDPhieuXuatKhac_Temp.Value.ToString());
            data.XoaChiTietPhieuXuatKhac_Temp(IDPhieuXuatKhac_Temp.Value.ToString());
            Response.Redirect("DanhSachPhieuXuatKhac.aspx");

        }

        protected void btnThemPhieuXuatKhac_Click(object sender, EventArgs e)
        {
            if (cmbLyDoXuat.Text != "")
            {
                string IDPhieuXuatKhac = IDPhieuXuatKhac_Temp.Value.ToString();
                DataTable db = data.LayDanhSachPhieuXuatKhac(IDPhieuXuatKhac);
                if (db.Rows.Count != 0)
                {
                    string IDNguoiLapPhieu = cmbNguoiLapPhieu.Value.ToString();
                    DateTime NgayLapPhieu = DateTime.Parse(cmbNgayLapPhieu.Text.ToString());
                    string IDKho = Session["IDKho"].ToString();
                    string IDLyDoXuat = cmbLyDoXuat.Value.ToString();
                    string GhiChu = txtGhiChu == null ? "" : txtGhiChu.Text.ToString();
                    string SoDonXuat = txtSoDonXuat.Text.ToString();
                    string IDNhanVien = Session["IDNhanVien"].ToString();
                    string ChungTu = "";
                    if (Page.IsValid && uploadfile.HasFile)
                    {
                        ChungTu = "ChungTu/" + DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + uploadfile.FileName;
                        string filePath = MapPath(ChungTu);
                        uploadfile.SaveAs(filePath);
                    }
                    float TongTrongLuong = 0;
                    foreach (DataRow dr in db.Rows)
                    {
                        float TrongLuong = float.Parse(dr["TrongLuong"].ToString());
                        int SL = Int32.Parse(dr["SoLuongXuat"].ToString());
                        TongTrongLuong = TongTrongLuong + (TrongLuong * SL);
                    }
                    data = new dtPhieuXuatKhac();
                    object ID = data.ThemPhieuXuatKhac(IDKho, IDNhanVien, IDLyDoXuat, GhiChu, NgayLapPhieu, SoDonXuat, ChungTu, TongTrongLuong.ToString());
                    if (ID != null)
                    {
                        foreach (DataRow dr in db.Rows)
                        {
                            string MaHang = dr["MaHang"].ToString();
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            string IDDonViTinh = dr["IDDonViTinh"].ToString();
                            string TrongLuong = dr["TrongLuong"].ToString();
                            string TonKho = dr["TonKho"].ToString();
                            string SoLuongXuat = dr["SoLuongXuat"].ToString();
                            string GhiChuHH = dr["GhiChu"].ToString();
                            data = new dtPhieuXuatKhac();
                            data.ThemChiTietPhieuXuatKhac(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, TonKho, SoLuongXuat, GhiChuHH);
                           // dtLichSuKho.ThemLichSu(IDHangHoa, Session["IDNhanVien"].ToString(), SoLuong, "Phiếu xuất khác", Session["IDKho"].ToString());
                           // dtLichSuKho.ThemLichSuXuat(IDHangHoa, Session["IDNhanVien"].ToString(), SoLuong, Session["IDKho"].ToString());

                            // xuất khác qua giám đốc duyệt
                            //object TheKho = dtTheKho.ThemTheKho(SoDonXuat, "Phiếu xuất khác ", "0", SoLuongXuat, (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString()) - Int32.Parse(SoLuongXuat)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa);
                            //if (TheKho != null)
                            //{
                            //    dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuongXuat, Session["IDKho"].ToString());
                            //}
                        }
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu Xuất Khác", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");
                        data = new dtPhieuXuatKhac();
                        data.XoaChiTietPhieuXuatKhac_Temp(IDPhieuXuatKhac);
                        Response.Redirect("DanhSachPhieuXuatKhac.aspx");
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa rỗng.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn lý do để xuất.'); </script>");
            }
        }

        protected void gridDanhSachHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            data = new dtPhieuXuatKhac();
            data.XoaChiTietPhieuXuatKhac_Temp_ID(ID);
            e.Cancel = true;
            gridDanhSachHangHoa_Temp.CancelEdit();
            LoadGrid(IDPhieuXuatKhac_Temp.Value.ToString());
        }

        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            // <5 vì hàng combo không xuất được
            sqlHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh           
	                                        WHERE (GPM_HangHoa.MaHang LIKE @MaHang) AND (GPM_HangHoa.DaXoa = 0) AND  GPM_HangHoa.IDTrangThaiHang < 5
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            sqlHangHoa.SelectParameters.Clear();
            // dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            sqlHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            sqlHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = sqlHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            sqlHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh
                                        WHERE (GPM_HangHoa.ID = @ID) AND  (GPM_HangHoa.IDTrangThaiHang < 5 )";
            sqlHangHoa.SelectParameters.Clear();
            sqlHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = sqlHangHoa;
            comboBox.DataBind();
        }

        protected void txtSoLuong_NumberChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text != "")
            {
                int SoLuong = Int32.Parse(txtSoLuong.Value.ToString());
                int SLCon = Int32.Parse(txtTonKho.Text);
                if (dtSetting.KT_ChuyenAm() != 0)
                {
                    if (SLCon < SoLuong)
                    {
                        txtSoLuong.Text = SLCon.ToString();
                        Response.Write("<script language='JavaScript'> alert('Số hàng trong kho không đủ.'); </script>");
                    }
                }
            }
        }

    }
}
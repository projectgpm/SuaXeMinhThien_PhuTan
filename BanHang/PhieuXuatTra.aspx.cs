using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class PhieuXuatTra : System.Web.UI.Page
    {
        dtPhieuXuatTra data = new dtPhieuXuatTra();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 69) == false)
                //    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    IDPhieuXuatTra_Temp.Value = Session["IDNhanVien"].ToString();
                    txtNguoiLapPhieu.Text = Session["TenDangNhap"].ToString();
                    txtSoDonXuat.Text = DateTime.Now.ToString("ddMMyyyy-hhmmss");
                }
                LoadGrid(IDPhieuXuatTra_Temp.Value.ToString());
            }
        }
       
        protected void cmbNgayLapPhieu_Init(object sender, EventArgs e)
        {
            cmbNgayLapPhieu.Date = DateTime.Today;
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                txtTonKho.Text = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(),Session["IDKho"].ToString())+"";
                txtSoLuong.Text = "0";
                cmbDonViTinh.Value = dtHangHoa.LayIDDonViTinh(cmbHangHoa.Value.ToString());
                txtDonGia.Text = dtCapNhatTonKho.GiaMua(cmbHangHoa.Value.ToString()) + "";
                
            }  
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "" && txtSoLuong.Text != "" && txtDonGia.Text != "")
            {
                int SoLuong = Int32.Parse(txtSoLuong.Value.ToString());
                if (SoLuong > 0)
                {
                    int SLTon = Int32.Parse(txtTonKho.Text);
                    string IDHangHoa = cmbHangHoa.Value.ToString();
                    string IDPhieuXuatTra = IDPhieuXuatTra_Temp.Value.ToString();
                    string GhiChu = txtGhiChuHH.Text == null ? "" : txtGhiChuHH.Text.ToString();
                    string TonKho = txtTonKho.Text.ToString();
                    double GiaMua = double.Parse(txtDonGia.Text.ToString());
                    if (dtSetting.KT_ChuyenAm() == 0)
                    {
                        if (SLTon < SoLuong)
                        {
                            txtSoLuong.Text = SLTon.ToString();
                            Response.Write("<script language='JavaScript'> alert('Hàng hóa tồn kho không đủ.'); </script>");
                        }
                        else
                        {
                            
                            DataTable db = data.KTChiTietPhieuXuatTra_Temp(IDHangHoa, IDPhieuXuatTra);
                            if (db.Rows.Count == 0)
                            {
                                data = new dtPhieuXuatTra();
                                data.ThemChiTietPhieuXuatTra_Temp(IDPhieuXuatTra, IDHangHoa, cmbDonViTinh.Value.ToString(), SoLuong, GhiChu, dtHangHoa.LayMaHang(IDHangHoa), TonKho, GiaMua);
                                Clear();
                            }
                            else
                            {
                                data = new dtPhieuXuatTra();
                                data.UpdatePhieuXuatTra_temp(IDPhieuXuatTra, IDHangHoa, SoLuong, GhiChu, GiaMua);             
                                Clear();
                            }

                            LoadGrid(IDPhieuXuatTra);

                        }
                    }
                    else
                    {
                        DataTable db = data.KTChiTietPhieuXuatTra_Temp(IDHangHoa, IDPhieuXuatTra);
                        if (db.Rows.Count == 0)
                        {
                            data = new dtPhieuXuatTra();
                            data.ThemChiTietPhieuXuatTra_Temp(IDPhieuXuatTra, IDHangHoa, cmbDonViTinh.Value.ToString(), SoLuong, GhiChu, dtHangHoa.LayMaHang(IDHangHoa), TonKho, GiaMua);
                            Clear();
                        }
                        else
                        {
                            data = new dtPhieuXuatTra();
                            data.UpdatePhieuXuatTra_temp(IDPhieuXuatTra, IDHangHoa, SoLuong, GhiChu, GiaMua);
                            Clear();
                        }
                        if (SLTon < SoLuong)
                        {
                            Response.Write("<script language='JavaScript'> alert('Số hàng tồn trong kho hiện tại không đủ.'); </script>");
                        }
                        LoadGrid(IDPhieuXuatTra);
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

        private void LoadGrid(string IDPhieuXuatTra)
        {
            data = new dtPhieuXuatTra();
            gridDanhSachHangHoa_Temp.DataSource = data.LayDanhSachPhieuXuatTra_Temp(IDPhieuXuatTra);
            gridDanhSachHangHoa_Temp.DataBind();
        }

        protected void gridDanhSachHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            data = new dtPhieuXuatTra();
            data.XoaChiTietPhieuXuatTra_Temp_ID(ID);
            e.Cancel = true;
            gridDanhSachHangHoa_Temp.CancelEdit();
            LoadGrid(IDPhieuXuatTra_Temp.Value.ToString());
        }

        protected void btnThemPhieuXuat_Click(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "" && txtNgayXuat.Text != "")
            {
                string IDPhieuXuatTra = IDPhieuXuatTra_Temp.Value.ToString();
                data = new dtPhieuXuatTra();
                DataTable db = data.LayDanhSachPhieuXuatTra_Temp(IDPhieuXuatTra);
                if (db.Rows.Count != 0)
                {
                    string SoDonXuat = txtSoDonXuat.Text.ToString();
                    string IDNhaCungCap = cmbNhaCungCap.Value.ToString();
                    string IDNhanVien = Session["IDNhanVien"].ToString();
                    string IDKhoLap = Session["IDKho"].ToString();
                    DateTime NgayLapPhieu = DateTime.Parse(cmbNgayLapPhieu.Text);
                    DateTime NgayXuat = DateTime.Parse(txtNgayXuat.Text);
                    string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                    double TongTien = 0;
                    foreach (DataRow dr in db.Rows)
                    {
                        double ThanhTien = float.Parse(dr["ThanhTien"].ToString());
                        TongTien = TongTien + ThanhTien;
                    }
                    data = new dtPhieuXuatTra();
                    object ID = data.ThemPhieuXuatTra_Temp(SoDonXuat, IDNhanVien, NgayLapPhieu, NgayXuat, TongTien.ToString(), GhiChu, IDNhaCungCap);
                    if (ID != null)
                    {
                        dtCongNo dt1 = new dtCongNo();
                        dt1.CapNhatCongNo(IDNhaCungCap, TongTien);// trừ công nợ NCC
                        foreach (DataRow dr in db.Rows)
                        {
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            string SoLuong = dr["SoLuong"].ToString();
                            string MaHang = dr["MaHang"].ToString();
                            string DonGia = dr["DonGia"].ToString();
                            string ThanhTien = dr["ThanhTien"].ToString();
                            string GhiChuHH = dr["GhiChu"].ToString();
                            string TonKho = dr["TonKho"].ToString();
                            string IDDonViTinh = dr["IDDonViTinh"].ToString();
                            data = new dtPhieuXuatTra();
                            data.ThemChiTietPhieuXuatTra(ID, IDHangHoa, IDDonViTinh, SoLuong, MaHang, DonGia, GhiChu, TonKho, ThanhTien);
                            if (Int32.Parse(SoLuong) > 0)
                            {
                                //object TheKho = dtTheKho.ThemTheKho(SoDonXuat, "Phiếu xuất trả ", "0", "", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString()) - Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Xuất", "0", SoLuong, "0");
                                //if (TheKho != null)
                                //{
                                    dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, Session["IDKho"].ToString());

                                //}
                            }
                        }
                       // dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu Xuất Trả", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");
                        data = new dtPhieuXuatTra();
                        data.XoaChiTietPhieuXuatTra_Temp(IDPhieuXuatTra);
                        Response.Redirect("DanhSachPhieuXuatTra.aspx");
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa rỗng.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn Nhà cung cấp.'); </script>");
            }
        }

        protected void btnHuyPhieuXuat_Click(object sender, EventArgs e)
        {
            data = new dtPhieuXuatTra();
            int ID = Int32.Parse(IDPhieuXuatTra_Temp.Value.ToString());
            data.XoaPhieuXuatTra_Temp(ID);
            data.XoaChiTietPhieuXuatTra_Temp(IDPhieuXuatTra_Temp.Value.ToString());
            Response.Redirect("DanhSachPhieuXuatTra.aspx");

        }
        public void Clear()
        {
            txtDonGia.Text = "";
            cmbHangHoa.Text = "";
            txtSoLuong.Text = "";
            txtTonKho.Text = "";
            cmbDonViTinh.Text = "";
            txtGhiChuHH.Text = "";
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
	                                        WHERE (GPM_HangHoa.MaHang LIKE @MaHang OR GPM_HangHoa.TenHangHoa LIKE @TenHang) AND (GPM_HangHoa.DaXoa = 0) 
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex ORDER BY TenHangHoa ASC ";

            sqlHangHoa.SelectParameters.Clear();
            sqlHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
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
                                        WHERE (GPM_HangHoa.ID = @ID) ORDER BY TenHangHoa ASC ";
            sqlHangHoa.SelectParameters.Clear();
            sqlHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = sqlHangHoa;
            comboBox.DataBind();
        }

        protected void txtNgayXuat_Init(object sender, EventArgs e)
        {
            txtNgayXuat.Date = DateTime.Now;
        }

        protected void gridDanhSachHangHoa_Temp_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TonKho = Convert.ToInt32(e.GetValue("TonKho"));
            int SoLuong = Convert.ToInt32(e.GetValue("SoLuong"));
            if (TonKho < SoLuong)
                e.Row.BackColor = color;
        }
    }
}
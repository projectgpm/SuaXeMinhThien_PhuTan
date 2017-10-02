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
    public partial class ThemHangHoaCombo : System.Web.UI.Page
    {
        dtHangCombo data = new dtHangCombo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 60) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                   // Random ran = new Random();
                    IDHangHoaComBo_Temp.Value = Session["IDNhanVien"].ToString();//ran.Next(100000, 999999).ToString();
                    txtSoLuong.Text = "0";
                    txtMaHang.Text = dtHangCombo.Dem_Max().ToString();
                }
                LoadGrid(Int32.Parse(IDHangHoaComBo_Temp.Value.ToString()));
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data = new dtHangCombo();
            int IDHangHoaComBo = Int32.Parse(IDHangHoaComBo_Temp.Value.ToString());
            data.XoaHangHoa_Temp_IDHangCombo(IDHangHoaComBo);
            Response.Redirect("DanhMucCombo.aspx");
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text != "" && txtHanSuDung.Text !="" && txtMaHang.Text != "" && cmbDonViTinh.Text != "" && txtTenHangHoa.Text != "" && txtGiaBanTong.Text != "" && cmbNhomHang.Text != "")
            {
                data = new dtHangCombo();
                int IDHangHoaComBo = Int32.Parse(IDHangHoaComBo_Temp.Value.ToString());
                DataTable dt = data.DanhSachHangHoaCombo_Temp(IDHangHoaComBo);
                if (dt.Rows.Count != 0)
                {
                    string MaHang = txtMaHang.Text.Trim();
                    string txtTenHangComBo = txtTenHangHoa.Text.ToString();
                    if (dtSetting.kiemTraChuyenDoiDau() == 1)
                        txtTenHangComBo = dtSetting.convertDauSangKhongDau(txtTenHangComBo).ToUpper();
                    string IDDonViTinh = cmbDonViTinh.Value.ToString();
                    string IDNhomHang = cmbNhomHang.Value.ToString();
                    string TongGiaMuaTruocThue = txtGiaMuaTruocThue.Text.ToString();
                    string TongGiaMuaSauThue = txtGiaMuaSauThue.Text.ToString();
                    string TongGiaBanTruocThue = txtGiaBanTruocThue.Text.ToString();
                    string TongGiaBanSauThue = txtGiaBanSauThue.Text.ToString();
                    string GiaBanTong = txtGiaBanTong.Text.ToString();
                    string TongTrongLuong = txtTrongLuong.Text;
                    string Barcode = txtBarcode.Text.Trim();
                    string HanSuDung = txtHanSuDung.Text.ToString();
                    string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                    if ((dtHangHoa.KiemTraMaHang(MaHang))  ==  false)
                    {
                        if (dtHangHoa.KiemTraBarcode(Barcode) == false)
                        {
                            data = new dtHangCombo();
                            object ID = data.ThemIDHangHoa_Temp();
                            if (ID != null)
                            {
                                data.CapNhatHangHoa(ID, MaHang, txtTenHangComBo, IDNhomHang, IDDonViTinh, TongGiaMuaTruocThue, TongGiaBanTruocThue, TongGiaMuaSauThue, TongGiaBanSauThue, TongTrongLuong, GhiChu, HanSuDung);
                                data.ThemBarCode(ID, Barcode);
                                //Thêm hàng hóa vào các kho....
                                DataTable dta = data.LayDanhSachKho();
                                for (int i = 0; i < dta.Rows.Count; i++)
                                {
                                    DataRow dr = dta.Rows[i];
                                    int IDKho = Int32.Parse(dr["ID"].ToString());
                                    data = new dtHangCombo();
                                    data.ThemHangVaoTonKho(IDKho, (int)ID, "0", GiaBanTong, GiaBanTong, GiaBanTong, GiaBanTong, GiaBanTong, GiaBanTong);
                                }

                                foreach (DataRow dr in dt.Rows)
                                {
                                    string IDHangHoa1 = dr["IDHangHoa"].ToString();
                                    string SoLuong1 = dr["SoLuong"].ToString();
                                    string GiaBanTruocThue1 = dr["GiaBanTruocThue"].ToString();
                                    string ThanhTien1 = dr["ThanhTien"].ToString();
                                    string IDDonViTinh1 = dr["IDDonViTinh"].ToString();
                                    string MaHang1 = dr["MaHang"].ToString();
                                    string TrongLuong1 = dr["TrongLuong"].ToString();
                                    string GiaBanSauThue1 = dr["GiaBanSauThue"].ToString();
                                    string GiaMuaTruocThue1 = dr["GiaMuaTruocThue"].ToString();
                                    string GiaMuaSauThue1 = dr["GiaMuaSauThue"].ToString();
                                    string GhiChu1 = dr["GhiChu"].ToString();
                                    data = new dtHangCombo();
                                    data.ThemHangHoa(ID, IDHangHoa1, SoLuong1, GiaBanTruocThue1, ThanhTien1, IDDonViTinh1, MaHang1, TrongLuong1, GiaBanSauThue1, GiaMuaTruocThue1, GiaMuaSauThue1, GhiChu1);
                                }
                                data.XoaHangHoa_Temp_IDHangCombo(IDHangHoaComBo);
                                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa combo", Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                                Response.Redirect("DanhMucCombo.aspx");
                            }
                        }
                        else
                        {
                            Response.Write("<script language='JavaScript'> alert('Mã Barcode đã tồn tại. Vui lòng kiểm tra lại?'); </script>"); return;
                        }
                    }
                    else
                    {
                        Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại.Vui lòng kiểm tra lại'); </script>"); return;
                    }
                }
                else
                {
                    cmbHangHoa.Focus();
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa combo rỗng.'); </script>"); return;
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Trường có dấu (*) không được bỏ trống.'); </script>"); return;
            }
        }
        public void TinhTongTien()
        {
            data = new dtHangCombo();
            DataTable db = data.DanhSachHangHoaCombo_Temp(Int32.Parse(IDHangHoaComBo_Temp.Value.ToString()));
            if (db.Rows.Count != 0)
            {
                double GiaBanTruocThue = 0, GiaBanSauThue = 0, GiaMuaTruocThue = 0, GiaMuaSauThue=0;
                foreach (DataRow dr in db.Rows)
                {
                    double ThanhTien1 = double.Parse(dr["GiaMuaTruocThue"].ToString());
                    GiaMuaTruocThue = GiaMuaTruocThue + ThanhTien1;
                    double ThanhTien2 = double.Parse(dr["GiaMuaSauThue"].ToString());
                    GiaMuaSauThue = GiaMuaSauThue + ThanhTien2;
                    double ThanhTien3 = double.Parse(dr["GiaBanTruocThue"].ToString());
                    GiaBanTruocThue = GiaBanTruocThue + ThanhTien3;
                     double ThanhTien4 = double.Parse(dr["ThanhTien"].ToString());
                    GiaBanSauThue = GiaBanSauThue + ThanhTien4;
                }
              
                txtGiaMuaTruocThue.Text = GiaMuaTruocThue.ToString();
                txtGiaMuaSauThue.Text = GiaMuaSauThue.ToString();
                txtGiaBanTruocThue.Text = GiaBanTruocThue.ToString();
                txtGiaBanSauThue.Text = GiaBanSauThue.ToString();
                txtGiaBanTong.Text = GiaBanSauThue.ToString();
                TinhTrongLuong();
            }
            else
            {
                txtGiaMuaTruocThue.Text = "0";
                txtGiaMuaSauThue.Text = "0";
                txtGiaBanTruocThue.Text = "0";
                txtGiaBanSauThue.Text = "0";
                txtGiaBanTong.Text = "0";
            }
        }
        public void TinhTrongLuong()
        {
            data = new dtHangCombo();
            DataTable db = data.DanhSachHangHoaCombo_Temp(Int32.Parse(IDHangHoaComBo_Temp.Value.ToString()));
            if (db.Rows.Count != 0)
            {
                double Tong = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                    Tong = Tong + TrongLuong;
                }
                txtTrongLuong.Text = (Tong).ToString();
            }
            else
            {
                txtTrongLuong.Text = "0";
            }
        }
        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.Text != "" && cmbHangHoa.Text != "")
            {
                int SL = Int32.Parse(txtSoLuong.Text);
                int SLTonKho = Int32.Parse(txtTonKho.Text);
                if (SL > 0)
                {

                    int IDHangHoaComBo = Int32.Parse(IDHangHoaComBo_Temp.Value.ToString());
                    float GiaBanSauThue = float.Parse(txtGiaBanST.Text.ToString());
                    float GiaBanTruocThue = dtHangHoa.LayGiaBanTruocThue(cmbHangHoa.Value.ToString());
                    float GiaMuaSauThue = dtHangHoa.LayGiaMuaSauThue(cmbHangHoa.Value.ToString());
                    float GiaMuaTruocThue = dtHangHoa.LayGiaMuaTruocThue(cmbHangHoa.Value.ToString());
                    string MaHang = dtHangHoa.LayMaHang(cmbHangHoa.Value.ToString());
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(cmbHangHoa.Value.ToString());
                    float TrongLuong = dtHangHoa.LayTrongLuong(cmbHangHoa.Value.ToString());
                    string GhiChu = txtGhiChuHangHoa.Text == null ? "" : txtGhiChuHangHoa.Text.ToString();
                    data = new dtHangCombo();
                    DataTable db = data.KTHangHoa_Temp(cmbHangHoa.Value.ToString(), IDHangHoaComBo);// kiểm tra hàng hóa
                    float TongTrongLuong = SL * TrongLuong;
                    float TongGiaMuaSauThue = SL * GiaMuaSauThue;
                    float TongGiaMuaTruocThue = SL * GiaMuaTruocThue;
                    float TongGiaBanTruocThue = SL * GiaBanTruocThue;
                    float TongGiaBanSauThue = SL * GiaBanSauThue;
                    if (db.Rows.Count == 0)
                    {
                        data = new dtHangCombo();
                        data.ThemHangHoa_Temp(IDHangHoaComBo, cmbHangHoa.Value.ToString(), SL, TongGiaBanTruocThue, TongGiaBanSauThue, MaHang, IDDonViTinh, TongTrongLuong.ToString(), GiaBanSauThue, TongGiaMuaTruocThue, TongGiaMuaSauThue, GhiChu);
                        TinhTongTien();
                        Clear();
                    }
                    else
                    {
                        data = new dtHangCombo();
                        data.UpdateHangHoa_temp(IDHangHoaComBo, cmbHangHoa.Value.ToString(), SL, TongGiaBanTruocThue, TongGiaBanSauThue, MaHang, IDDonViTinh, TongTrongLuong.ToString(), GiaBanSauThue, TongGiaMuaTruocThue, TongGiaMuaSauThue, GhiChu);
                        TinhTongTien();
                        Clear();
                    }
                    LoadGrid(IDHangHoaComBo);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Số lượng > 0.'); </script>"); return;
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Bạn chưa chọn hàng hóa hoặc số lượng.'); </script>"); return;
            }
        }
        public void Clear()
        {
            txtTL.Text = "";
            cmbHangHoa.Text = "";
            txtSoLuong.Text = "0";
            txtTonKho.Text = "";
            txtGiaBanST.Text = "0";
            txtGhiChuHangHoa.Text = "";
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                txtTL.Text = dtHangHoa.LayTrongLuong(cmbHangHoa.Value.ToString()) + "";
                txtTonKho.Text = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), Session["IDKho"].ToString()) + "";
                txtGiaBanST.Text = dtHangHoa.LayGiaBanSauThue(cmbHangHoa.Value.ToString(),Session["IDKho"].ToString()).ToString();
            }
            else
            {
                txtTonKho.Text = "";
                txtSoLuong.Text = "";
                Response.Write("<script language='JavaScript'> alert('Bạn chưa chọn hàng hóa.'); </script>"); return;
            }
        }
        private void LoadGrid(int IDHangHoaComBo)
        {
            data = new dtHangCombo();
            gridDanhSachHangHoa.DataSource = data.DanhSachHangHoaCombo_Temp(IDHangHoaComBo);
            gridDanhSachHangHoa.DataBind();

        }
        
        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            //IDTrangThaiHang = 1  Hàng Hóa Thường
            //IDTrangThaiHang = 3 Hàng Ngừng Nhập
            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBan] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoaTonKho.GiaBan, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
	                                        WHERE (GPM_HangHoa.MaHang LIKE @MaHang) AND (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3 ) AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoaTonKho.GiaBan, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID) AND (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3 ) AND GPM_HangHoaTonKho.IDKho = @IDKho";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

     
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            string IDHangHoaComBo = IDHangHoaComBo_Temp.Value.ToString();
            data = new dtHangCombo();
            data.XoaHangHoa_Temp_ID(ID);
            TinhTrongLuong();
            TinhTongTien();
            LoadGrid(Int32.Parse(IDHangHoaComBo));
        }
    }
}
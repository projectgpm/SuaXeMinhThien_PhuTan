using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtKhachHang
    {
        public DataTable DanhSachChiTietCongNo()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT TOP 1000 * FROM [GPM_ChiTietCongNoKhachHang]  ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatTinhTrang(string IDHoaDon)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HoaDon] SET [TrangThaiCongNoKhachHang] = 1 WHERE [ID] = " + IDHoaDon;
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình duyệt dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void CapNhatCongNo(string ID, double SoTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_KhachHang] SET [CongNo] = [CongNo] - '" + SoTien + "' WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình duyệt dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public object ThemChiTietCongNo(string SoHoaDon, string IDKhachHang, string HinhThucThanhToan, string IDMaPhieu, double SoTienThanhToan, string NoiDung, DateTime NgayThanhToan)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object ID = null;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietCongNoKhachHang] ([SoHoaDon], [IDKhachHang], [HinhThucThanhToan], [IDMaPhieu], [SoTienThanhToan], [NoiDung], [NgayThanhToan], [NgayCapNhat],[SoTienBangChu]) OUTPUT INSERTED.ID VALUES (@SoHoaDon, @IDKhachHang, @HinhThucThanhToan, @IDMaPhieu, @SoTienThanhToan, @NoiDung, @NgayThanhToan, getdate(),@SoTienBangChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoHoaDon", SoHoaDon);
                        myCommand.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                        myCommand.Parameters.AddWithValue("@HinhThucThanhToan", HinhThucThanhToan);
                        myCommand.Parameters.AddWithValue("@IDMaPhieu", IDMaPhieu);
                        myCommand.Parameters.AddWithValue("@SoTienThanhToan", SoTienThanhToan);
                        myCommand.Parameters.AddWithValue("@NoiDung", NoiDung);
                        myCommand.Parameters.AddWithValue("@NgayThanhToan", NgayThanhToan);
                        myCommand.Parameters.AddWithValue("@SoTienBangChu", dtSetting.Conver_TienChu(SoTienThanhToan));
                        ID = myCommand.ExecuteScalar();
                    }

                    myConnection.Close();
                    return ID;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public static double LayTienThanhToan_IDHoaDon(string IDHoaDon)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TongTien FROM [GPM_HoaDon] WHERE [ID] = " + IDHoaDon;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Double.Parse(dr["TongTien"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public DataTable DanhSachSoDonHang(string IDKhachHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HoaDon] WHERE IDKhachHang = '" + IDKhachHang + "' AND TrangThaiCongNoKhachHang = 0 AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static double LayCongNoCuKhachHang(string IDKhachHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT CongNo FROM [GPM_KhachHang] WHERE [ID] = " + IDKhachHang;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Double.Parse(dr["CongNo"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public static int TyLeChietKhauKhachHang(string IDKhachHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GPM_ChietKhau.TyLe FROM GPM_KhachHang, GPM_ChietKhau WHERE GPM_KhachHang.IDChietKhau = GPM_ChietKhau.ID AND GPM_KhachHang.ID = '" + IDKhachHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TyLe"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public static int KT_SDT_KH_CapNhat(string SDT,string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT DienThoai FROM [GPM_KHACHHANG] WHERE [DienThoai] = '" + SDT + "' AND ID = " + ID;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return 1;
                    }
                    else return -1;
                }
            }
        }
        public static int KT_SDT_KH(string SDT)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT DienThoai FROM [GPM_KHACHHANG] WHERE [DienThoai] = '" + SDT + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return 1;
                    }
                    else return -1;
                }
            }
        }

        public string LayTenKhachHang_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenKhachHang FROM [GPM_KhachHang] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["TenKhachHang"].ToString();
                }
            }
        }

        public DataTable LayDanhSachKhachHang( string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_KHACHHANG] WHERE [GPM_KHACHHANG].DaXoa = 0  AND ('" + IDKho + "' = 1 OR IDKho = '" + IDKho + "') ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable LayDanhSachKhachHang_BaoCao(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID,TenKhachHang FROM [GPM_KHACHHANG] WHERE [GPM_KHACHHANG].DaXoa = 0  AND ('" + IDKho + "' = 1 OR IDKho = '" + IDKho + "') ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public object ThemKhachHang(int IDNhomKhachHang, string MaKhachHang, string TenKhachHang, DateTime NgaySinh, string CMND, string DiaChi, string DienThoai, string Email, string Barcode, string GhiChu, string IDKho, string IDChietKhau)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object ID = null;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_KHACHHANG] ([IDNhomKhachHang],[MaKhachHang], [TenKhachHang], [NgaySinh], [CMND], [DiaChi], [DienThoai], [Email], [Barcode], [GhiChu], [NgayCapNhat],[IDKho],[IDChietKhau])  OUTPUT INSERTED.ID VALUES (@IDNhomKhachHang,@MaKhachHang, @TenKhachHang, @NgaySinh, @CMND, @DiaChi, @DienThoai, @Email, @Barcode, @GhiChu, getdate(),@IDKho,@IDChietKhau)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDChietKhau", IDChietKhau);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDNhomKhachHang", IDNhomKhachHang);
                        myCommand.Parameters.AddWithValue("@TenKhachHang", TenKhachHang);
                        myCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                        myCommand.Parameters.AddWithValue("@CMND", CMND);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
                        myCommand.Parameters.AddWithValue("@Email", Email);
                        myCommand.Parameters.AddWithValue("@Barcode", Barcode); 
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
     
                        myCommand.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);
                       // myCommand.ExecuteNonQuery();
                        ID = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return ID;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void CapNhatMaKhachHang(object ID, string MaKhachHang, string Barcode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_KHACHHANG] SET [MaKhachHang] = @MaKhachHang,[Barcode] = @Barcode WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);
                        myCommand.Parameters.AddWithValue("@Barcode", Barcode);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void XoaKhachHang(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_KHACHHANG] SET [DAXOA] = 1 WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }


        public void SuaThongTinKhachHang(int ID, int IDNhomKhachHang, string TenKhachHang, string NgaySinh, string CMND, string DiaChi, string DienThoai, string Email, string GhiChu, string IDChietKhau)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_KHACHHANG] SET [IDChietKhau] =@IDChietKhau,[IDNhomKhachHang] = @IDNhomKhachHang,[TenKhachHang] = @TenKhachHang, [NgaySinh] = @NgaySinh, [CMND] = @CMND, [DiaChi] = @DiaChi, [DienThoai] = @DienThoai, [Email] = @Email, [GhiChu] = @GhiChu, [NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDChietKhau", IDChietKhau);
                        myCommand.Parameters.AddWithValue("@IDNhomKhachHang", IDNhomKhachHang);
                        myCommand.Parameters.AddWithValue("@TenKhachHang", TenKhachHang);
                        myCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                        myCommand.Parameters.AddWithValue("@CMND", CMND);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
                        myCommand.Parameters.AddWithValue("@Email", Email);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtPhieuXuatTra
    {
        public static string TongSoXuatTrongThang(string NgayBD, string NgayKT, string IDKhoLap)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select [IDKhoLap], count(IDKhoLap) as Tong from [GPM_PhieuXuatTra] where [NgayLap] >= '" + DateTime.Parse(NgayBD).ToString("yyyy-MM-dd hh:mm:ss tt") + "' and [NgayLap] <= '" + DateTime.Parse(NgayKT).ToString("yyyy-MM-dd hh:mm:ss tt") + "' and [IDKhoLap] = '" + IDKhoLap + "' group by IDKhoLap";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                        return (Int32.Parse(tb.Rows[0]["Tong"].ToString()) + 1).ToString();
                    return "1";
                }
            }
        }

        public DataTable DanhSachPhieuXuatTra()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuXuatTra] WHERE [SoDonXuat] is not null ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachChiTietPhieuXuatTra_ID(string IDPhieuXuatTra)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT [GPM_ChiTietPhieuXuatTra].* FROM [GPM_ChiTietPhieuXuatTra] WHERE [IDPhieuXuatTra] = '" + IDPhieuXuatTra + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public object ThemPhieuXuatTra_Temp(string SoDonXuat, string IDNhanVien, DateTime NgayLap, DateTime NgayXuat, string TongTien, string GhiChu, string IDNhaCungCap)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuNhapSi = null;
                    string cmdText = "INSERT INTO [GPM_PhieuXuatTra] ([IDNhaCungCap],[GhiChu],[NgayXuat],[NgayLap],[IDNhanVien],[SoDonXuat],[NgayCapNhat],[TongTien]) OUTPUT INSERTED.ID VALUES  (@IDNhaCungCap,@GhiChu, @NgayXuat,@NgayLap,@IDNhanVien,@SoDonXuat,getdate(),@TongTien)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@SoDonXuat", SoDonXuat);
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.Parameters.AddWithValue("@NgayLap", NgayLap);
                        myCommand.Parameters.AddWithValue("@NgayXuat", NgayXuat);
                        myCommand.Parameters.AddWithValue("@IDNhaCungCap", IDNhaCungCap);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        IDPhieuNhapSi = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuNhapSi;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void XoaPhieuXuatTra_Temp(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhieuXuatTra] WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void XoaChiTietPhieuXuatTra_Temp(string IDPhieuXuatTra)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ChiTietPhieuXuatTra_Temp] WHERE [IDPhieuXuatTra] = '" + IDPhieuXuatTra + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

          
        public DataTable LayDanhSachPhieuXuatTra_Temp(string IDPhieuXuatTra)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_ChiTietPhieuXuatTra_Temp] WHERE [IDPhieuXuatTra] = '" + IDPhieuXuatTra + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        
        public DataTable KTChiTietPhieuXuatTra_Temp(string IDHangHoa, string IDPhieuXuatTra)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ChiTietPhieuXuatTra_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "' AND [IDPhieuXuatTra] = '" + IDPhieuXuatTra + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void ThemChiTietPhieuXuatTra_Temp(string IDPhieuXuatTra, string IDHangHoa, string IDDonViTinh, int SoLuong, string GhiChu, string MaHang, string TonKho, double DonGia)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietPhieuXuatTra_Temp] ([IDPhieuXuatTra],[IDHangHoa],[IDDonViTinh],[SoLuong],[GhiChu],[MaHang],[TonKho],[DonGia],[ThanhTien]) VALUES (@IDPhieuXuatTra,@IDHangHoa,@IDDonViTinh,@SoLuong,@GhiChu,@MaHang,@TonKho,@DonGia,@ThanhTien)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ThanhTien", SoLuong * DonGia);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@IDPhieuXuatTra", IDPhieuXuatTra);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);

                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void UpdatePhieuXuatTra_temp(string IDPhieuXuatTra, string IDHangHoa, int SoLuong, string GhiChu, double DonGia)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ChiTietPhieuXuatTra_Temp] SET [ThanhTien] = @ThanhTien,[DonGia] = @DonGia,[SoLuong] = @SoLuong,[GhiChu] = @GhiChu WHERE [IDHangHoa] = @IDHangHoa AND [IDPhieuXuatTra] = @IDPhieuXuatTra";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", DonGia * SoLuong);
                        myCommand.Parameters.AddWithValue("@IDPhieuXuatTra", IDPhieuXuatTra);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }
        public void XoaChiTietPhieuXuatTra_Temp_ID(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ChiTietPhieuXuatTra_Temp] WHERE ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void ThemChiTietPhieuXuatTra(object IDPhieuXuatTra, string IDHangHoa, string IDDonViTinh, string SoLuong, string MaHang, string DonGia, string GhiChu, string TonKho, string ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietPhieuXuatTra] ([IDPhieuXuatTra],[IDHangHoa],[IDDonViTinh],[SoLuong],[MaHang],[DonGia],[GhiChu],[TonKho],[ThanhTien]) VALUES (@IDPhieuXuatTra,@IDHangHoa,@IDDonViTinh,@SoLuong,@MaHang,@DonGia,@GhiChu,@TonKho,@ThanhTien)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        myCommand.Parameters.AddWithValue("@IDPhieuXuatTra", IDPhieuXuatTra);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

       
    }
}
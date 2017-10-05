using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtCongNo
    {
        public DataTable DanhSachSoDonHang(string IDNhaCungCap)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_DonDatHang] WHERE IDNhaCungCap = '" + IDNhaCungCap + "' AND TrangThaiCongNo = 0 AND IDNhaCungCap > 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachChiTietCongNo()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_ChiTietCongNo]  ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
 
        public void CapNhatTinhTrang(string IDMaPhieu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_DonDatHang] SET [TrangThaiCongNo] = 1 WHERE [ID] = " + IDMaPhieu;
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
                    string strSQL = "UPDATE [GPM_NhaCungCap] SET [CongNo] = [CongNo] - '" + SoTien + "' WHERE [ID] = @ID";
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
        public object ThemChiTietCongNo(string SoHoaDon, string IDNhaCungCap, string HinhThucThanhToan, string IDMaPhieu, float SoTienThanhToan, string NoiDung, DateTime NgayThanhToan)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object ID = null;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietCongNo] ([SoHoaDon], [IDNhaCungCap], [HinhThucThanhToan], [IDMaPhieu], [SoTienThanhToan], [NoiDung], [NgayThanhToan], [NgayCapNhat],[TienBangChu]) OUTPUT INSERTED.ID VALUES (@SoHoaDon, @IDNhaCungCap, @HinhThucThanhToan, @IDMaPhieu, @SoTienThanhToan, @NoiDung, @NgayThanhToan, getdate(),@TienBangChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoHoaDon", SoHoaDon);
                        myCommand.Parameters.AddWithValue("@IDNhaCungCap", IDNhaCungCap);
                        myCommand.Parameters.AddWithValue("@HinhThucThanhToan", HinhThucThanhToan);
                        myCommand.Parameters.AddWithValue("@IDMaPhieu", IDMaPhieu);
                        myCommand.Parameters.AddWithValue("@SoTienThanhToan", SoTienThanhToan);
                        myCommand.Parameters.AddWithValue("@NoiDung", NoiDung);
                        myCommand.Parameters.AddWithValue("@NgayThanhToan", NgayThanhToan);
                        myCommand.Parameters.AddWithValue("@TienBangChu", dtSetting.Conver_TienChu(SoTienThanhToan));
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
    }
}
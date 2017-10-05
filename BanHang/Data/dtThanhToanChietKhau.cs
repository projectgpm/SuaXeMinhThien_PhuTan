using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtThanhToanChietKhau
    {
        public DataTable DanhSachChiTietChietKhau()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT TOP 1000 * FROM [GPM_ThanhToanChietKhau]  ORDER BY [ID] DESC";
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
                    string strSQL = "UPDATE [GPM_HoaDon] SET [TrangThai] = 1 WHERE [ID] = " + IDHoaDon;
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
        public object ThemThanhToanChietKhau(string IDKhachHang, double SoTienThanhToan, string NoiDung)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object ID = null;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ThanhToanChietKhau] ([IDKhachHang], [SoTienThanhToan], [NoiDung], [NgayCapNhat], [TienBangChu]) OUTPUT INSERTED.ID VALUES (@IDKhachHang, @SoTienThanhToan, @NoiDung, getdate(), @TienBangChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                        myCommand.Parameters.AddWithValue("@SoTienThanhToan", SoTienThanhToan);
                        myCommand.Parameters.AddWithValue("@NoiDung", NoiDung);
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
        public static double LayTienChietKhau(string IDHoaDon)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TienChietKhauKhachHang FROM [GPM_HoaDon] WHERE [ID] = " + IDHoaDon;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Double.Parse(dr["TienChietKhauKhachHang"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public DataTable DanhSachChuaChietKhau(string IDKhachHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HoaDon] WHERE IDKhachHang = '" + IDKhachHang + "' AND TrangThai = 0 AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtLichSuKho
    {
        public DataTable LayDanhSach()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "  SELECT [GPM_LichSuKho].*,[GPM_HangHoa].MaHang FROM [GPM_LichSuKho],[GPM_HangHoa] WHERE [GPM_HangHoa].ID = [GPM_LichSuKho].IDHangHoa ORDER BY [ID] DESC ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static void ThemLichSu(string IDHangHoa, string IDNhanVien, string SoL, string NoiDung, string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    DataTable da = new DataTable(); ;
                    myConnection.Open();
                    string cmdText1 = "SELECT SoLuongCon FROM [GPM_HangHoaTonKho] WHERE IDHangHoa = '" + IDHangHoa + "' AND IDKho = " + IDKho;
                    using (SqlCommand command = new SqlCommand(cmdText1, myConnection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        da.Load(reader);
                    }
                    int SoLuong = 0;
                    int SoluongMoi = 0;
                    if (da.Rows.Count != 0)
                    {
                        DataRow dr = da.Rows[0];
                        SoLuong = Int32.Parse(dr["SoLuongCon"].ToString());
                        int aa = Int32.Parse(SoL);
                        SoluongMoi = SoLuong - aa;
                    }
                    string TenMay = Environment.MachineName; // tên máy tinh
                    string cmdText = "INSERT INTO [GPM_LichSuKho] ([IDKho],[IDHangHoa], [IDNhanVien], [SoLuong], [SoLuongMoi], [NoiDung],[NgayCapNhat],[TenMay]) VALUES (@IDKho,@IDHangHoa, @IDNhanVien, @SoLuong, @SoLuongMoi, @NoiDung,getDate(),'" + TenMay + "')";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@SoLuongMoi", SoluongMoi);
                        myCommand.Parameters.AddWithValue("@NoiDung", NoiDung);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
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


        public static void ThemLichSuXuat(string IDHangHoa, string IDKhachHang, string SoLuong, string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_LichSuXuatHang] ([IDHangHoa],[IDKhachHang], [SoLuong], [NgayNhap], [IDKho]) VALUES (@IDHangHoa,@IDKhachHang, @SoLuong, getdate(), '" + IDKho + "')";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
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
        public static void ThemLichSuNhap(int IDHangHoa, int IDNhaCungCap, int SoLuong, string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_LichSuNhapHang] ([IDHangHoa],[IDNhaCungCap], [SoLuong], [NgayNhap], [IDKho]) VALUES (@IDHangHoa,@IDNhaCungCap, @SoLuong, getdate(), '" + IDKho + "')";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDNhaCungCap", IDNhaCungCap);

                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
  
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
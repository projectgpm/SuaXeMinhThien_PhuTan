using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtThayDoiGia
    {
        public static void ThemLichSu(string MaHang, string IDHangHoa, string IDDonViTinh, string GiaCu, string GiaMoi, string IDNguoiDung, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                string myServer = Environment.MachineName; // tên máy tinh
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_LichSuThayDoiGia] ([MaHang], [IDHangHoa], [IDDonViTinh], [GiaCu],[TenMay],[GiaMoi],[IDNguoiDung], [NgayThayDoi],[GhiChu]) VALUES (@MaHang, @IDHangHoa,@IDDonViTinh, @GiaCu,@TenMay,@GiaMoi,@IDNguoiDung, getdate(),@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@GiaCu", GiaCu);
                        myCommand.Parameters.AddWithValue("@TenMay", myServer);
                        myCommand.Parameters.AddWithValue("@GiaMoi", GiaMoi);
                        myCommand.Parameters.AddWithValue("@IDNguoiDung", IDNguoiDung);
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
        public DataTable LayDanhSach()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT TOP 1000 * FROM [GPM_LichSuThayDoiGia] ORDER BY [ID] DESC";
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
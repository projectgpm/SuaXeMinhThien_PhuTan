using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtNhanVienKyThuat
    {
        public void CapNhat(string ID,string TenKyThuat, string IDChietKhau, string DiaChi, string DienThoai, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_KyThuat] SET [TenKyThuat] = @TenKyThuat,[IDChietKhau] = @IDChietKhau,[DiaChi] = @DiaChi,[DienThoai] = @DienThoai,[GhiChu] = @GhiChu WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@TenKyThuat", TenKyThuat);
                        myCommand.Parameters.AddWithValue("@IDChietKhau", IDChietKhau);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
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
        public void Xoa(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_KyThuat] SET [DAXOA] = 1 WHERE [ID] = @ID";
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
        public void Them(string TenKyThuat, string IDChietKhau, string DiaChi, string DienThoai, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_KyThuat] ([TenKyThuat],[IDChietKhau],[DiaChi],[DienThoai],[GhiChu]) VALUES (@TenKyThuat,@IDChietKhau,@DiaChi,@DienThoai,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TenKyThuat", TenKyThuat);
                        myCommand.Parameters.AddWithValue("@IDChietKhau", IDChietKhau);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
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
        public DataTable DanhSach()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_KyThuat] WHERE [DAXOA] = 0";
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
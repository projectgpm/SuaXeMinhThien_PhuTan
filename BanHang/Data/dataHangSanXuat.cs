using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dataHangSanXuat
    {
        public DataTable getData(string cmd)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(cmd, con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
                catch (Exception) { return new DataTable(); }
            }
        }

        public DataTable getDanhSachHangSX()
        {
            string cmd = "SELECT * FROM [GPM_HangSanXuat] WHERE [DAXOA] = 0 AND ID != 1";
            return getData(cmd);
        }
        public static bool KiemTraMaNCC_ID(string MaNSX, string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaNSX FROM [GPM_HangSanXuat] WHERE [MaNSX] = '" + MaNSX + "' AND ID =  " + ID;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        public void SuaThongTin(string MaNSX, int ID, string TenNSX, string DienThoai, string Fax, string Email, string DiaChi, string NguoiLienHe, string MaSoThue, string LinhVucKinhDoanh, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangSanXuat] SET [MaNSX] = @MaNSX, [TenNSX] = @TenNSX, [DienThoai] = @DienThoai, [Fax] = @Fax, [Email] = @Email, [DiaChi] = @DiaChi, [NguoiLienHe] = @NguoiLienHe, [MaSoThue] = @MaSoThue, [LinhVucKinhDoanh] = @LinhVucKinhDoanh, [NgayCapNhat] = getdate(), [GhiChu] = @GhiChu WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@MaNSX", MaNSX);
                        myCommand.Parameters.AddWithValue("@TenNSX", TenNSX);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
                        myCommand.Parameters.AddWithValue("@Fax", Fax);
                        myCommand.Parameters.AddWithValue("@Email", Email);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@NguoiLienHe", NguoiLienHe);
                        myCommand.Parameters.AddWithValue("@MaSoThue", MaSoThue);
                        myCommand.Parameters.AddWithValue("@LinhVucKinhDoanh", LinhVucKinhDoanh);
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
        public static string Dem_Max()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                int STTV = 0;
                string So;
                string GPM = "00000";
                string cmdText = "SELECT * FROM [GPM_HangSanXuat]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    STTV = tb.Rows.Count + 1;
                    int DoDaiHT = STTV.ToString().Length;
                    string DoDaiGPM = GPM.Substring(0, 5 - DoDaiHT);
                    So = DoDaiGPM + STTV;
                    return So;
                }
            }
        }
        public static bool KiemTraMaNSX(string MaNSX)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaNSX FROM [GPM_HangSanXuat] WHERE [MaNSX] = " + MaNSX;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        public void ThemHangSX(string MaNSX, string TenNSX, string DienThoai, string Fax, string Email, string DiaChi, string NguoiLienHe, string MaSoThue, string LinhVucKinhDoanh, DateTime NgayCapNhat, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangSanXuat] ([MaNSX],[TenNSX], [DienThoai], [Fax], [Email], [DiaChi], [NguoiLienHe], [MaSoThue], [LinhVucKinhDoanh], [NgayCapNhat], [GhiChu]) VALUES (@MaNSX,@TenNSX, @DienThoai, @Fax, @Email, @DiaChi, @NguoiLienHe, @MaSoThue, @LinhVucKinhDoanh, @NgayCapNhat, @GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaNSX", MaNSX);
                        myCommand.Parameters.AddWithValue("@TenNSX", TenNSX);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
                        myCommand.Parameters.AddWithValue("@Fax", Fax);
                        myCommand.Parameters.AddWithValue("@Email", Email);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@NguoiLienHe", NguoiLienHe);
                        myCommand.Parameters.AddWithValue("@MaSoThue", MaSoThue);
                        myCommand.Parameters.AddWithValue("@LinhVucKinhDoanh", LinhVucKinhDoanh);

                        myCommand.Parameters.AddWithValue("@NgayCapNhat", NgayCapNhat);
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
        public void deleteHangSX(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangSanXuat] SET [DaXoa] = 1 WHERE [ID] = @ID";
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
        public static string LayIDHangSanXuat(string TenHangSanXuat)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_HANGSANXUAT] WHERE [TenHangSanXuat] = N'" + TenHangSanXuat + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    else return 1 + "";
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class Import_NhaCungCap
    {
        public void XoaDuLieuTemp_ID(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_NhaCungCap_Import_Temp] WHERE [ID] = '" + ID + "'";
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
        public void ThemNhaCungCap_Temp(string TenNhaCungCap, string DienThoai, string Fax, string Email, string DiaChi, string NguoiLienHe, string MaSoThue, string LinhVucKinhDoanh, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_NhaCungCap_Import_Temp] ([TenNhaCungCap], [DienThoai], [Fax], [Email], [DiaChi], [NguoiLienHe], [MaSoThue], [LinhVucKinhDoanh], [GhiChu]) VALUES (@TenNhaCungCap, @DienThoai, @Fax, @Email, @DiaChi, @NguoiLienHe, @MaSoThue, @LinhVucKinhDoanh, @GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TenNhaCungCap", TenNhaCungCap);
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
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public DataTable KiemTraNhaCungCap_Import_Temp(string TenNhaCungCap, string MaSoThue, string DienThoai)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_NhaCungCap_Import_Temp] WHERE [GPM_NhaCungCap_Import_Temp].TenNhaCungCap = N'" + TenNhaCungCap + "' AND  [GPM_NhaCungCap_Import_Temp].MaSoThue = N'" + MaSoThue + "' AND [GPM_NhaCungCap_Import_Temp].DienThoai = N'" + DienThoai + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void ThemNhaCungCap(string TenNhaCungCap, string DienThoai, string Fax, string Email, string DiaChi, string NguoiLienHe, string MaSoThue, string LinhVucKinhDoanh, DateTime NgayCapNhat, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_NHACUNGCAP] ([TenNhaCungCap], [DienThoai], [Fax], [Email], [DiaChi], [NguoiLienHe], [MaSoThue], [LinhVucKinhDoanh], [NgayCapNhat], [GhiChu]) VALUES (@TenNhaCungCap, @DienThoai, @Fax, @Email, @DiaChi, @NguoiLienHe, @MaSoThue, @LinhVucKinhDoanh, @NgayCapNhat, @GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TenNhaCungCap", TenNhaCungCap);
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
        public DataTable KiemTraNhaCungCap_Import(string TenNhaCungCap, string MaSoThue, string DienThoai)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_NhaCungCap] WHERE [GPM_NhaCungCap].TenNhaCungCap = N'" + TenNhaCungCap + "' AND  [GPM_NhaCungCap].MaSoThue = N'" + MaSoThue + "' AND [GPM_NhaCungCap].DienThoai = N'" + DienThoai + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachNhaCungCap_Import_Temp()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_NhaCungCap_Import_Temp]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void XoaDuLieuTemp()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_NhaCungCap_Import_Temp]";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class Import_KhachHang
    {
        public void XoaDuLieuTemp_ID(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_Import_KhachHang] WHERE [ID] = '" + ID + "'";
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
        public void ThemKhachHang_Temp(int IDNhomKhachHang, string TenKhachHang, DateTime NgaySinh, string CMND, string DiaChi, string DienThoai, string Email, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_Import_KhachHang] ([IDNhomKhachHang], [TenKhachHang], [NgaySinh], [CMND], [DiaChi], [DienThoai], [Email], [GhiChu]) " +
                    "VALUES (@IDNhomKhachHang, @TenKhachHang, @NgaySinh, @CMND, @DiaChi, @DienThoai, @Email, @GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

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
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public DataTable Lay_IDNhomHang(string TenNhomHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_NhomKhachHang] WHERE [TenNhomKhachHang] = N'" + TenNhomHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
       
        public DataTable KiemTraKhachHang_Import_Temp(string TenKhachHang, string CMND, string DienThoai)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_Import_KhachHang] WHERE [GPM_Import_KhachHang].TenKhachHang = N'" + TenKhachHang + "' AND  [GPM_Import_KhachHang].CMND = N'" + CMND + "' AND [GPM_Import_KhachHang].DienThoai = N'" + DienThoai + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachKhachHang_Import_Temp()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_Import_KhachHang]";
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
                    string strSQL = "DELETE [GPM_Import_KhachHang]";
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
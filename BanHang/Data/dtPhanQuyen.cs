using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtPhanQuyen
    {

        public DataTable LayDanhSachMenu(int IDNhomNguoiDung)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_Menu].Link,[GPM_Menu].TenDanhMuc,[GPM_PhanQuyen].ID, [GPM_PhanQuyen].TrangThai,[GPM_PhanQuyen].ChucNang FROM [GPM_PhanQuyen],[GPM_Menu] WHERE [GPM_PhanQuyen].IDMenu = [GPM_Menu].ID AND [GPM_PhanQuyen].IDNhomNguoiDung = '" + IDNhomNguoiDung + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatQuyen(int IDNhomNguoiDung, int ID, int TrangThai,int ChucNang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_PhanQuyen] SET [TrangThai] = @TrangThai,[ChucNang] = @ChucNang WHERE [ID] = @ID AND [IDNhomNguoiDung] = @IDNhomNguoiDung";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ChucNang", ChucNang);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDNhomNguoiDung", IDNhomNguoiDung);
                      
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

        public void CapNhatQuyen_Full(int ID, int IDNhomNguoiDung, int IDMenu, int TrangThai, int ChucNang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_PhanQuyen] SET [IDMenu] = @IDMenu,[TrangThai] = @TrangThai,[ChucNang] = @ChucNang,[IDNhomNguoiDung] = @IDNhomNguoiDung WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ChucNang", ChucNang);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDMenu", IDMenu);
                        myCommand.Parameters.AddWithValue("@IDNhomNguoiDung", IDNhomNguoiDung);

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

        public void Insert_Full(int ID, int IDNhomNguoiDung, int IDMenu, int TrangThai, int ChucNang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_PhanQuyen] ([IDMenu],[TrangThai],[ChucNang],[IDNhomNguoiDung],[NgayCapNhap]) VALUE (@IDMenu,@TrangThai,@ChucNang,@IDNhomNguoiDung,@NgayCapNhap)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ChucNang", ChucNang);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDMenu", IDMenu);
                        myCommand.Parameters.AddWithValue("@IDNhomNguoiDung", IDNhomNguoiDung);

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
    }
}
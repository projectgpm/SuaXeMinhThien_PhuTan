using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtKho
    {
        public void ThemHangHoaTonKho(string IDHangHoa, float GiaBan, float GiaBan1, float GiaBan2, float GiaBan3, float GiaBan4, float GiaBan5, string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoaTonKho] ([IDHangHoa],[GiaBan],[GiaBan1],[GiaBan2],[GiaBan3],[GiaBan4],[GiaBan5],[IDKho],[SoLuongCon],[NgayCapNhat]) VALUES (@IDHangHoa,@GiaBan,@GiaBan1,@GiaBan2,@GiaBan3,@GiaBan4,@GiaBan5,@IDKho,0,getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@GiaBan1", GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", GiaBan5);
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
        public DataTable DanhSachHangHoaTonKhoTong()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_HangHoaTonKho] WHERE [DAXOA] = 0 AND [IDKho] = 1";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static int LayID_Max()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT Max(MaKho) as IDMax FROM [GPM_Kho] ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["IDMax"].ToString());
                    }
                    else return -1;
                }
            }
        }
        public DataTable LayDanhSachKho()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_Kho] WHERE [DAXOA] = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public string LayTenKho_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenCuaHang FROM [GPM_Kho] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["TenCuaHang"].ToString();
                }
            }
        }
        public string LayMaKho_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaKho FROM [GPM_Kho] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["MaKho"].ToString();
                }
            }
        }

        public DataTable LayDanhSachKho_2()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_Kho]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public object ThemKho(string MaKho, string TenCuaHang, string SoSerial, string DiaChi, string DienThoai, DateTime NgayMo, string IDVung, string TrangThaiBanHang, string GiaApDung)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object ID = null;
                    string cmdText = "INSERT INTO [GPM_Kho] ([TenCuaHang], [SoSerial], [DiaChi], [DienThoai], [NgayMo],[MaKho],[IDVung],[TrangThaiBanHang],[GiaApDung],[NgayCapNhat])  OUTPUT INSERTED.ID  VALUES (@TenCuaHang, @SoSerial, @DiaChi, @DienThoai, @NgayMo,@MaKho,@IDVung,@TrangThaiBanHang,@GiaApDung,getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TrangThaiBanHang", TrangThaiBanHang);
                        myCommand.Parameters.AddWithValue("@GiaApDung", GiaApDung);
                        myCommand.Parameters.AddWithValue("@TenCuaHang", TenCuaHang);
                        myCommand.Parameters.AddWithValue("@SoSerial", SoSerial);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
                        myCommand.Parameters.AddWithValue("@MaKho", MaKho);
                        myCommand.Parameters.AddWithValue("@NgayMo", NgayMo);
                        myCommand.Parameters.AddWithValue("@IDVung", IDVung);
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

        public void CapNhatKho(string ID, string MaKho, string TenCuaHang, string SoSerial, string DiaChi, string DienThoai, DateTime NgayMo, string IDVung, string TrangThaiBanHang, string GiaApDung)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE GPM_KHO SET [GiaApDung] = @GiaApDung,[NgayCapNhat] = getdate(),[TrangThaiBanHang] = @TrangThaiBanHang,TenCuaHang = @TenCuaHang, SoSerial = @SoSerial, DiaChi = @DiaChi, DienThoai = @DienThoai, MaKho = @MaKho, NgayMo = @NgayMo, IDVung = @IDVung WHERE ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TrangThaiBanHang", TrangThaiBanHang);
                        myCommand.Parameters.AddWithValue("@GiaApDung", GiaApDung);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@TenCuaHang", TenCuaHang);
                        myCommand.Parameters.AddWithValue("@SoSerial", SoSerial);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
                        myCommand.Parameters.AddWithValue("@MaKho", MaKho);
                        myCommand.Parameters.AddWithValue("@NgayMo", NgayMo);
                        myCommand.Parameters.AddWithValue("@IDVung", IDVung);
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

        public void Xoakho(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_KHO] SET [DAXOA] =  1 WHERE [ID] = @ID";
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
    }
}
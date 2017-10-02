using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dataNhomHang
    {
        public static bool KiemTraMaNhom_ID(string MaNhom, string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaNhom FROM [GPM_NHOMHANG] WHERE [MaNhom] = '" + MaNhom + "' AND ID =  " + ID;
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
        public static bool KiemTraMaNhom(string MaNhom)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaNhom FROM [GPM_NHOMHANG] WHERE [MaNhom] = " + MaNhom;
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

        public static string LayIDNhom(string TenNhom)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_NHOMHANG] WHERE [TenNhomHang] = '" + TenNhom + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    return 1 + "";
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
                string GPM = "0000";
                string cmdText = "SELECT * FROM [GPM_NHOMHANG]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    STTV = tb.Rows.Count + 1;
                    int DoDaiHT = STTV.ToString().Length;
                    string DoDaiGPM = GPM.Substring(0, 4 - DoDaiHT);
                    So = DoDaiGPM + STTV;
                    return So;
                }
            }
        }
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

        public DataTable getDanhSachNhomHang()
        {
            string cmd = "SELECT * FROM [GPM_NHOMHANG] WHERE [DAXOA] = 0 AND ID != 1";
            return getData(cmd);
        }

        public string LayTenNhomHang_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenNhomHang FROM [GPM_NhomHang] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["TenNhomHang"].ToString();
                }
            }
        }

        public DataTable getDanhSachNhomHang_IDNganhHang(string IDNganhHang)
        {
            string cmd = "SELECT * FROM [GPM_NHOMHANG] WHERE [DAXOA] = 0 AND (('" + IDNganhHang + "' = -1) OR (IDNganhHang = '" + IDNganhHang + "'))";
            return getData(cmd);
        }

        public void XoaNhomHang(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_NHOMHANG] SET [DAXOA] = 1 WHERE [ID] = @ID";
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

        public void updateNhomHang(int ID, int IDNganhHang, string MaNhom, string TenNhomHang, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_NHOMHANG] SET [IDNganhHang] = @IDNganhHang,[MaNhom] = @MaNhom,[TenNhomHang]=@TenNhomHang,[GhiChu] = @GhiChu, [NgayCapNhat] = getDATE() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDNganhHang", IDNganhHang);
                        myCommand.Parameters.AddWithValue("@MaNhom", MaNhom);
                        myCommand.Parameters.AddWithValue("@TenNhomHang", TenNhomHang);
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


        public void insertNhomHang(int IDNganhHang, string MaNhom, string TenNhomHang, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_NHOMHANG] ([IDNganhHang], [MaNhom],[TenNhomHang],[GhiChu],[NgayCapNhat]) VALUES (@IDNganhHang, @MaNhom,@TenNhomHang,@GhiChu,getDATE())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNganhHang", IDNganhHang);
                        myCommand.Parameters.AddWithValue("@MaNhom", MaNhom);
                        myCommand.Parameters.AddWithValue("@TenNhomHang", TenNhomHang);
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
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtHangHoa
    {
        
        public static bool KiemTraMaHang(string MaHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaHang FROM [GPM_HangHoa] WHERE [MaHang] = '" + MaHang + "'";
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

        public static bool KiemTraBarcode(string Barcode)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT Barcode FROM [GPM_HangHoa_Barcode] WHERE [Barcode] = '" + Barcode + "'";
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
        public static bool KiemTraMaHang_HangQuyDoi(string IDHangHoa,string IDHangQuyDoi)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select * from GPM_HangHoa where (IDTrangThaiHang = 1 or IDTrangThaiHang = 3) and DaXoa = 0 and ID = '" + IDHangQuyDoi + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        cmdText = "select * from GPM_HangHoa_QuyDoi where IDHangHoa = '" + IDHangHoa + "' and IDHangQuyDoi = '" + IDHangQuyDoi + "'";
                        using (SqlCommand command1 = new SqlCommand(cmdText, con))
                        using (SqlDataReader reader1 = command1.ExecuteReader())
                        {
                            DataTable tb1 = new DataTable();
                            tb1.Load(reader1);
                            if (tb1.Rows.Count != 0)
                                return true;
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        //public static void ThemLichSuThayDoiGia(string IDHangHoa, string IDDonViTinh, float GiaCu, float GiaMoi, string IDNguoiDung, string MaHang)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            string TenMay = Environment.MachineName; // tên máy tinh
        //            myConnection.Open();
        //            string cmdText = "INSERT INTO [GPM_LichSuThayDoiGia] ([IDHangHoa],[IDDonViTinh],[GiaCu],[GiaMoi],[IDNguoiDung],[MaHang],[TenMay],[NgayThayDoi]) VALUES(@IDHangHoa, @IDDonViTinh,@GiaCu,@GiaMoi,@IDNguoiDung,@MaHang,@TenMay, getdate())";
        //            using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@MaHang", MaHang.Trim());
        //                myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
        //                myCommand.Parameters.AddWithValue("@GiaCu", GiaCu);
        //                myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
        //                myCommand.Parameters.AddWithValue("@GiaMoi", GiaMoi);
        //                myCommand.Parameters.AddWithValue("@IDNguoiDung", IDNguoiDung);
        //                myCommand.Parameters.AddWithValue("@TenMay", TenMay);
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
        //        }
        //    }
        //}
        public static string LayIDHangHoa_MaHang(string MaHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_HangHoa] WHERE [MaHang] = '" + MaHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        string ID = dr["ID"].ToString().Trim();
                        return ID;
                    }
                    return null;
                }
            }
        }
        public static string LayMaHang(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaHang FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        string ID = dr["MaHang"].ToString().Trim();
                        return ID;
                    }
                    return null;
                }
            }
        }
        public static string LayTenHangHoa(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenHangHoa FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        string ID = dr["TenHangHoa"].ToString().Trim();
                        return ID;
                    }
                    return null;
                }
            }
        }
        public static string LayIDDonViTinh(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDDonViTinh FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        string ID = dr["IDDonViTinh"].ToString().Trim();
                        return ID;
                    }
                    return null;
                }
            }
        }
        public static float LayTrongLuong(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrongLuong FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["TrongLuong"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float LayGiaBanSauThue(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho]  = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float LayGiaBanTruocThue(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBanTruocThue FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBanTruocThue"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float LayGiaMuaSauThue(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaMuaSauThue FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaMuaSauThue"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float LayGiaMuaTruocThue(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaMuaTruocThue FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaMuaTruocThue"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float GiaBan0(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho] = '" + IDKho + "'  ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float GiaBan1(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan1 FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho] = '" + IDKho + "'  ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan1"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float GiaBan2(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan2 FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho] = '" + IDKho + "'  ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan2"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float GiaBan3(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan3 FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho] = '" + IDKho + "'  ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan3"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float GiaBan4(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan4 FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho] = '" + IDKho + "'  ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan4"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static float GiaBan5(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan5 FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho] = '" + IDKho + "'  ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan5"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static int TrangThaiHang(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDTrangThaiHang FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["IDTrangThaiHang"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static int TrangThaiNhomDatHang(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDNhomDatHang FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["IDNhomDatHang"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static int HangHoaQuiDoi(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDHangQuyDoi FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["IDHangQuyDoi"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static int HeSoQuyDoi(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT HeSo FROM [GPM_HangHoa] WHERE [ID] = " + IDHangHoa;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["HeSo"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        //public static int KiemTraQuyDoi(string IDHangHoaQuiDoi, string IDKho, string IDNguoiDung, string IDHangHoa, int MaHoaDon)
        //{
        //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        con.Open();
        //        string cmdText = "SELECT * FROM [GPM_LOG_QuiDoi] WHERE [MaHoaDon] = '" + MaHoaDon + "'  AND [IDHangHoa] = '" + IDHangHoa + "' AND [IDHangHoaQuiDoi] = '" + IDHangHoaQuiDoi + "' AND [IDKho] = '" + IDKho + "' AND [IDNguoiDung] = '" + IDNguoiDung + "'";
        //        using (SqlCommand command = new SqlCommand(cmdText, con))
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            DataTable tb = new DataTable();
        //            tb.Load(reader);
        //            if (tb.Rows.Count != 0)
        //            {
        //                return 1;
        //            }
        //            return 0;
        //        }
        //    }
        //}


        //public static void XoaQuyDoi(string IDHangHoaQuiDoi, string IDKho, string IDNguoiDung, int MaHoaDon, int IDHangHoa)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string strSQL = "DELETE [GPM_LOG_QuiDoi] WHERE [IDHangHoa] = '" + IDHangHoa + "'  AND [MaHoaDon] = '" + MaHoaDon + "'  AND [IDHangHoaQuiDoi] = '" + IDHangHoaQuiDoi + "' AND [IDKho] = '" + IDKho + "' AND [IDNguoiDung] = '" + IDNguoiDung + "'";
        //            using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
        //            {
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
        //        }
        //    }
        //}

        //public static int SLConQuyDoi(string IDHangHoa,string IDKho,string IDNguoiDung)
        //{
        //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        con.Open();
        //        string cmdText = "SELECT SoLuongCon FROM [GPM_LOG_QuiDoi] WHERE [IDNguoiDung] = '" + IDNguoiDung + "' AND [IDKho] = '" + IDKho + "' AND  [IDHangHoa] = " + IDHangHoa;
        //        using (SqlCommand command = new SqlCommand(cmdText, con))
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            DataTable tb = new DataTable();
        //            tb.Load(reader);
        //            if (tb.Rows.Count != 0)
        //            {
        //                DataRow dr = tb.Rows[0];
        //                return Int32.Parse(dr["SoLuongCon"].ToString().Trim());
        //            }
        //            return 0;
        //        }
        //    }
        //}
        //public static int SLHienTaiQuyDoi(int IDHangHoa,string IDKho)
        //{
        //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        con.Open();
        //        string cmdText = "SELECT SoLuong FROM [GPM_LOG_QuiDoi] WHERE  [IDKho] = '" + IDKho + "'AND  [IDHangHoa] = " + IDHangHoa;
        //        using (SqlCommand command = new SqlCommand(cmdText, con))
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            DataTable tb = new DataTable();
        //            tb.Load(reader);
        //            if (tb.Rows.Count != 0)
        //            {
        //                DataRow dr = tb.Rows[0];
        //                return Int32.Parse(dr["SoLuong"].ToString().Trim());
        //            }
        //            return 0;
        //        }
        //    }
        //}
        //public static int SLConLaiQuyDoi(int IDHangHoa, string IDKho)
        //{
        //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        con.Open();
        //        string cmdText = "SELECT SoLuongCon FROM [GPM_LOG_QuiDoi] WHERE  [IDKho] = '" + IDKho + "'AND  [IDHangHoa] = " + IDHangHoa;
        //        using (SqlCommand command = new SqlCommand(cmdText, con))
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            DataTable tb = new DataTable();
        //            tb.Load(reader);
        //            if (tb.Rows.Count != 0)
        //            {
        //                DataRow dr = tb.Rows[0];
        //                return Int32.Parse(dr["SoLuongCon"].ToString().Trim());
        //            }
        //            return 0;
        //        }
        //    }
        //}
        public static int KTSoNguyen(int number1, int number2)
        {
            if (number1 % number2 == 0)
            {
                return number1 / number2;
            }
            else
            {
                float x = number1 / (float)number2;
                int a = (int)(number1 / (float)number2);
                if (a < x)
                    return (a + 1);
                else return a;
            }
        }
    }
}
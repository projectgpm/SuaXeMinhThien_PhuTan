using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Web;

namespace BanHang.Data
{
    public class dtSetting
    {
        public static string Conver_TienChu(double number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            double decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDouble(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            return str + "đồng chẵn";
        }
        public static int tinhSoNgay(int thang, int nam)
        {
            if (thang == 1 || thang == 3 || thang == 5 || thang == 7 || thang == 8 || thang == 10 || thang == 12)
                return 31;
            if (thang == 4 || thang == 6 || thang == 9 || thang == 11)
                return 30;

            if (nam % 4 == 0 && nam % 100 != 0 || nam % 400 == 0)
                return 29;
            else return 28;
        }

        public static int LaySoNgayDuocSuaDonHangDaXuLy()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT CapNhatDonHangXuLy FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["CapNhatDonHangXuLy"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public static int SoNgayHuyDonHangThuMua()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT HuyDonHangThuMua FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["HuyDonHangThuMua"].ToString());
                    }
                    else return 7;
                }
            }
        }
        public static int KT_ChuyenAm()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ChuyenAm FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["ChuyenAm"].ToString());
                    }
                    else return -1;
                }
            }
        }

        public static string LayMaKho(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaKho FROM [GPM_Kho] WHERE ID = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return dr["MaKho"].ToString();
                    }
                    else return null;
                }
            }
        }
        public static string LayTenKho(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenCuaHang FROM [GPM_Kho] WHERE ID = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return dr["TenCuaHang"].ToString();
                    }
                    else return null;
                }
            }
        }
        public static int LaySoNgayBanHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TuanSuatBanHang FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TuanSuatBanHang"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public static int KT_BanHang(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThaiBanHang FROM [GPM_Kho] WHERE [ID] = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TrangThaiBanHang"].ToString());
                    }
                    else return -1;
                }
            }
        }


        public static int LaySoNgayTraHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SoNgayTraHang FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["SoNgayTraHang"].ToString());
                    }
                    else return 0;
                }
            }
        }

        public static bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public DataTable LayTenDatabase()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string Database;
                string cmdText = "SELECT DatabaseName FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static bool LayChucNangCha(string IDNhomNguoiDung, int IDMenu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ChucNang FROM [GPM_PhanQuyen] WHERE [IDNhomNguoiDung] = '" + IDNhomNguoiDung + "' AND [IDMenu] = '" + IDMenu + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        if ((Int32.Parse(dr["ChucNang"].ToString())) == 1)
                            return true;
                        return false;

                    }
                    else return false;
                }
            }
        }
        //public static bool LayChucNang_HienThi(string IDNhomNguoiDung)
        //{
        //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        con.Open();
        //        string Link = (HttpContext.Current.Request.Url.AbsolutePath).Replace("/","");
        //        string cmdText = "SELECT [GPM_PhanQuyen].TrangThai,[GPM_Menu].Link FROM [GPM_PhanQuyen],[GPM_Menu] WHERE [GPM_PhanQuyen].[IDNhomNguoiDung] = '" + IDNhomNguoiDung + "' AND [GPM_PhanQuyen].IDMenu = [GPM_Menu].ID AND  [GPM_Menu].Link = '" + Link + "'";
        //        using (SqlCommand command = new SqlCommand(cmdText, con))
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            DataTable tb = new DataTable();
        //            tb.Load(reader);
        //            if (tb.Rows.Count != 0)
        //            {
        //                DataRow dr = tb.Rows[0];
        //                if (Int32.Parse(dr["TrangThai"].ToString()) == 1)
        //                    return true;
        //                return false;
        //            }
        //            else return false;
        //        }
        //    }
        //}
        public static bool LayChucNang_ThemXoaSua(string IDNhomNguoiDung)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string Link = (HttpContext.Current.Request.Url.AbsolutePath).Replace("/", "");
                string cmdText = "SELECT [GPM_PhanQuyen].ChucNang,[GPM_Menu].Link FROM [GPM_PhanQuyen],[GPM_Menu] WHERE [GPM_PhanQuyen].[IDNhomNguoiDung] = '" + IDNhomNguoiDung + "' AND [GPM_PhanQuyen].IDMenu = [GPM_Menu].ID AND  [GPM_Menu].Link = '" + Link + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        int KT = 0;
                        if (Int32.Parse(dr["ChucNang"].ToString()) == 1)
                            return true;
                        return false;

                    }
                    else return false;
                }
            }
        }
        public static string convertDauSangKhongDau(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToUpper();
        }
        public static int LayIDKho(int IDKho)
        {
            return IDKho;
        }
        public static int kiemTraChuyenDoiDau()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ChuyenDoiDau FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return Int32.Parse(tb.Rows[0]["ChuyenDoiDau"].ToString());
                    }
                    else return -1;
                }
            }
        }

        public static PhysicalAddress GetMacAddress()
        {
            NetworkInterface[] nic1 = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.Name.ToString().CompareTo("Wi-Fi") == 0 || nic.Name.ToString().CompareTo("Ethernet") == 0)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        // -----------
        public static string GetSHA1HashData(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            byte[] hashData = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data + 123));

            System.Text.StringBuilder returnValue = new System.Text.StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString("x"));
            }

            return returnValue.ToString();
        }
        public static void setData_Setting(string KeyKichHoat, string user)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [Setting] SET [KeyKichHoat] = @KeyKichHoat, [NguoiKichHoat] = @NguoiKichHoat";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@KeyKichHoat", KeyKichHoat);
                        myCommand.Parameters.AddWithValue("@NguoiKichHoat", user);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public static DataTable getData_Setting()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    con.Open();
                    string cmdText = "SELECT KeyKichHoat FROM [Setting]";
                    using (SqlCommand command = new SqlCommand(cmdText, con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        // Key code..
        public static int setKeyCode(string Key, string user)
        {
            PhysicalAddress address = GetMacAddress();
            string strAddress = address.ToString();

            if (Key.CompareTo("1231123") == 0)
            {
                string sha1Address = GetSHA1HashData(strAddress);
                setData_Setting(sha1Address, user);
                return 1;
            }
            return -1;

        }
        public static int getKeyCode()
        {
            PhysicalAddress address = GetMacAddress();
            string strAddress = address.ToString();
            string sha1Address = GetSHA1HashData(strAddress);

            DataTable da = getData_Setting();
            if (da.Rows.Count != 0)
            {
                DataRow dr = da.Rows[0];
                string macAddress = dr["KeyKichHoat"].ToString();
                if (macAddress.CompareTo(sha1Address) == 0)
                    return 1;
            }
            return -1;
        }
    }
}
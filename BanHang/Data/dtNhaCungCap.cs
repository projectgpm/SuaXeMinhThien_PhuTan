using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BanHang.Data
{
    public class dtNhaCungCap
    {
        public DataTable DanhSachSoDonHang(string IDNhaCungCap)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_DonDatHang] WHERE IDNhaCungCap = '" + IDNhaCungCap + "' AND TrangThaiCongNo = 0 AND IDNhaCungCap > 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static double LayTienThanhToan_IDPhieuNhapKho(string IDPhieuNhapKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TongTien FROM [GPM_DonDatHang] WHERE [ID] = " + IDPhieuNhapKho;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Double.Parse(dr["TongTien"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public static double LayCongNo_IDNCC(string IDNCC)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT CongNo FROM [GPM_NhaCungCap] WHERE [ID] = " + IDNCC;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Double.Parse(dr["CongNo"].ToString());
                    }
                    else return 0;
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
                string cmdText = "SELECT * FROM [GPM_NHACUNGCAP]";
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
        public static bool KiemTraMaNCC_ID(string MaNCC, string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaNCC FROM [GPM_NHACUNGCAP] WHERE [MaNCC] = '" + MaNCC + "' AND ID =  " + ID;
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
        public static bool KiemTraMaNCC(string MaNCC)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaNCC FROM [GPM_NHACUNGCAP] WHERE [MaNCC] = " + MaNCC;
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
		public DataTable LayDanhSachNhaCungCap()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_NHACUNGCAP] WHERE [DAXOA] = 0 and ID != 1";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public string LayTenNhaCC_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenNhaCungCap FROM [GPM_NhaCungCap] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["TenNhaCungCap"].ToString();
                }
            }
        }

        public void ThemNhaCungCap(string MaNCC , string TenNhaCungCap, string DienThoai, string Fax, string Email, string DiaChi, string NguoiLienHe, string MaSoThue, string LinhVucKinhDoanh, DateTime NgayCapNhat, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_NHACUNGCAP] ([MaNCC],[TenNhaCungCap], [DienThoai], [Fax], [Email], [DiaChi], [NguoiLienHe], [MaSoThue], [LinhVucKinhDoanh], [NgayCapNhat], [GhiChu]) VALUES (@MaNCC,@TenNhaCungCap, @DienThoai, @Fax, @Email, @DiaChi, @NguoiLienHe, @MaSoThue, @LinhVucKinhDoanh, @NgayCapNhat, @GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaNCC", MaNCC);
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
        public void XoaNhaCungCap(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_NHACUNGCAP] SET [DAXOA] = 1 WHERE [ID] = @ID";
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

        public void SuaThongTinNhaCungCap(string MaNCC, int ID, string TenNhaCungCap, string DienThoai, string Fax, string Email, string DiaChi, string NguoiLienHe, string MaSoThue, string LinhVucKinhDoanh, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_NHACUNGCAP] SET [MaNCC] = @MaNCC, [TenNhaCungCap] = @TenNhaCungCap, [DienThoai] = @DienThoai, [Fax] = @Fax, [Email] = @Email, [DiaChi] = @DiaChi, [NguoiLienHe] = @NguoiLienHe, [MaSoThue] = @MaSoThue, [LinhVucKinhDoanh] = @LinhVucKinhDoanh, [NgayCapNhat] = getdate(), [GhiChu] = @GhiChu WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@MaNCC", MaNCC);
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
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
    }
}
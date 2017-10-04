using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtPhieuXuatKhac
    {
        public static string TongSoXuatTrongThang(string NgayBD, string NgayKT, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select [IDKho], count(IDKho) as Tong from [GPM_PhieuXuatKhac] where [NgayLapPhieu] >= '" + DateTime.Parse(NgayBD).ToString("yyyy-MM-dd hh:mm:ss tt") + "' and [NgayLapPhieu] <= '" + DateTime.Parse(NgayKT).ToString("yyyy-MM-dd hh:mm:ss tt") + "' and [IDKho] = '" + IDKho + "' group by IDKho";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                        return (Int32.Parse(tb.Rows[0]["Tong"].ToString()) + 1).ToString();
                    return "1";
                }
            }
        }
        public static void CapNhatTrangThai(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_PhieuXuatKhac] SET [TrangThai] = 1 WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                       
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
        public static string LaySoDonXuat(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SoDonXuat FROM [GPM_PhieuXuatKhac] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["SoDonXuat"].ToString();
                }
            }
        }
        public static int LayTrangThaiDonXuat(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThai FROM [GPM_PhieuXuatKhac] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return Int32.Parse(tb.Rows[0]["TrangThai"].ToString());
                }
            }
        }
        public object ThemPhieuXuatKhac(string IDKho, string IDNhanVien, string IDTrangThaiPhieuXuatKhac, string GhiChu, DateTime NgayLapPhieu, string SoDonXuat, string ChungTu, string TongTrongLuong)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuNhapSi = null;
                    string cmdText = "INSERT INTO [GPM_PhieuXuatKhac] ([NgayCapNhat],[IDKho],[IDNhanVien],[IDTrangThaiPhieuXuatKhac],[GhiChu],[NgayLapPhieu],[SoDonXuat],[ChungTu],[TongTrongLuong]) OUTPUT INSERTED.ID VALUES (getdate(),@IDKho,@IDNhanVien,@IDTrangThaiPhieuXuatKhac,@GhiChu,@NgayLapPhieu,@SoDonXuat,@ChungTu,@TongTrongLuong)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiPhieuXuatKhac", IDTrangThaiPhieuXuatKhac);
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@NgayLapPhieu", NgayLapPhieu);
                        myCommand.Parameters.AddWithValue("@SoDonXuat", SoDonXuat);
                        myCommand.Parameters.AddWithValue("@ChungTu", ChungTu);
                        myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
                        IDPhieuNhapSi = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuNhapSi;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        
        public void XoaChiTietPhieuXuatKhac_Temp(string IDPhieuXuatKhac)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhieuXuatKhac_ChiTiet_Temp] WHERE [IDPhieuXuatKhac] = '" + IDPhieuXuatKhac + "'";
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
        public DataTable KTChiTietPhieuXuatKhac_Temp(string IDHangHoa, string IDPhieuXuatKhac)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_PhieuXuatKhac_ChiTiet_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "' AND [IDPhieuXuatKhac] = " + IDPhieuXuatKhac;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void ThemPhieuXuatKhac_Temp(string IDPhieuXuatKhac, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string TonKho, string SoLuongXuat, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_PhieuXuatKhac_ChiTiet_Temp] ([IDPhieuXuatKhac],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[TonKho],[SoLuongXuat],[GhiChu]) VALUES (@IDPhieuXuatKhac,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@TonKho,@SoLuongXuat,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuXuatKhac", IDPhieuXuatKhac);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@SoLuongXuat", SoLuongXuat);
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
        public void UpdatePhieuXuatKhac_temp(string IDPhieuXuatKhac, string IDHangHoa, int SoLuong)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_PhieuXuatKhac_ChiTiet_Temp] SET [IDPhieuXuatKhac] = @IDPhieuXuatKhac,[SoLuong] = @SoLuong WHERE [IDHangHoa] = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuXuatKhac", IDPhieuXuatKhac);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
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
        public DataTable LayDanhSachPhieuXuatKhac(string IDPhieuXuatKhac)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuXuatKhac_ChiTiet_Temp] WHERE [IDPhieuXuatKhac] = '" + IDPhieuXuatKhac + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public string LayTenLyDo_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenTrangThai FROM [GPM_TrangThaiPhieuXuatKhac] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["TenTrangThai"].ToString();
                }
            }
        }

        public void ThemChiTietPhieuXuatKhac(object IDPhieuXuatKhac, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string TonKho, string SoLuongXuat, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_PhieuXuatKhac_ChiTiet] ([IDPhieuXuatKhac],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[TonKho],[SoLuongXuat],[GhiChu]) VALUES (@IDPhieuXuatKhac,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@TonKho,@SoLuongXuat,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@IDPhieuXuatKhac", IDPhieuXuatKhac);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@SoLuongXuat", SoLuongXuat);
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

        public void XoaChiTietPhieuXuatKhac_Temp_ID(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhieuXuatKhac_ChiTiet_Temp] WHERE ID = @ID";
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
        public DataTable DanhSachPhieuXuatKhac()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuXuatKhac] WHERE IDNhanVien is not null ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachLyDoXuat()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_TrangThaiPhieuXuatKhac]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachChiTietPhieuXuatKhac_ID(string IDPhieuXuatKhac)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_PhieuXuatKhac_ChiTiet] WHERE [IDPhieuXuatKhac] = '" + IDPhieuXuatKhac + "'";
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
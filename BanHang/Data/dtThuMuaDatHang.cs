using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtThuMuaDatHang
    {
        public static string TongSoXuatTrongThang(string NgayBD, string NgayKT, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select [IDKhoLap], count(IDKhoLap) as Tong from [GPM_ThuMua_DonHang] where [NgayLap] >= '" + DateTime.Parse(NgayBD).ToString("yyyy-MM-dd hh:mm:ss tt") + "' and [NgayLap] <= '" + DateTime.Parse(NgayKT).ToString("yyyy-MM-dd hh:mm:ss tt") + "' and [IDKhoLap] = '" + IDKho + "' group by IDKhoLap";
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
        public DataTable LayDanhSachDonHangXuLy1Phan()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang] WHERE [TrangThai] = 1 AND IDTrangThaiDonHang = 4  AND [IDNguoiLap] is not null ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatTrangThaiDonHang(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ThuMua_DonHang] SET [TrangThaiDonHang] = 1, [NgayCapNhat] = getdate() WHERE [ID] = '" + ID + "'";
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
        public static int LayTyLeChietKhau(string IDDonHangThuMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ChietKhau FROM [GPM_ThuMua_DonHang] WHERE [ID] = '" + IDDonHangThuMua + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["ChietKhau"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public void CapNhat_TongTien_TongTrongLuong(string ID, string TongTien, string TongTrongLuong, string TongTienSauChietKhau)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ThuMua_DonHang] SET [TongTienSauChietKhau] = @TongTienSauChietKhau,[TongTien] = @TongTien,[TongTrongLuong] = @TongTrongLuong, [NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
                        myCommand.Parameters.AddWithValue("@TongTienSauChietKhau", TongTienSauChietKhau);
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
        public void CapNhatChiTietDonHang(string IDDonHangThuMua, string IDHangHoa, int SoLuong, float DonGia, float ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ThuMua_DonHang_ChiTiet] SET [SoLuong] = @SoLuong,[DonGia] = @DonGia,[ThanhTien] = @ThanhTien WHERE [IDHangHoa] = @IDHangHoa AND [IDDonHangThuMua] = @IDDonHangThuMua";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonHangThuMua", IDDonHangThuMua);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
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
        public DataTable DanhSachChiTiet(string IDDonHangThuMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang_ChiTiet] WHERE IDDonHangThuMua =" + IDDonHangThuMua;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static int LayTrangThaiDonHang(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThai FROM [GPM_ThuMua_DonHang] WHERE [ID] = " + ID;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TrangThai"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public static int DonHangHuy(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThaiDonHang FROM [GPM_ThuMua_DonHang] WHERE [ID] = " + ID;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TrangThaiDonHang"].ToString().Trim());
                    }
                    return 0;
                }
            }
        }
        public DataTable LayDanhSachDonHang(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang] WHERE TrangThai = 0 AND TrangThaiDonHang = 0  AND [SoDonHang] is not null AND [IDKhoLap] = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachDonHangDaHuy(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang] WHERE TrangThaiDonHang = 1  AND [SoDonHang] is not null AND [IDKhoLap] = '" + IDKho + "' ORDER BY [ID] DESC ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void XoaChiTietDonHang_Nhap(string IDDonHangThuMua)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ThuMua_Temp] WHERE [IDDonHangThuMua] = " + IDDonHangThuMua;
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
        public void ThemChiTietDonHang(object IDDonHangThuMua, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string DonGia, string ThanhTien, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ThuMua_DonHang_ChiTiet] ([IDDonHangThuMua],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[DonGia],[ThanhTien],[GhiChu]) VALUES (@IDDonHangThuMua,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@DonGia,@ThanhTien,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@IDDonHangThuMua", IDDonHangThuMua);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
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
        public object ThemPhieuDatHang(string SoDonHang, string IDNguoiLap, DateTime NgayLap, string TongTrongLuong, string TongTien, string IDKhoLap, string GhiChu, string IDNhaCungCap, DateTime NgayDat, DateTime NgayGiaoDuKien, string ChietKhau, string TongTienSauChietKhau, string IDThanhToan)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuKiemKho = null;
                    string cmdText = "INSERT INTO [GPM_ThuMua_DonHang] ([NgayCapNhat],[SoDonHang],[IDNguoiLap],[NgayLap],[TongTrongLuong],[TongTien],[IDKhoLap],[GhiChu],[IDNhaCungCap],[NgayDat],[NgayGiaoDuKien],[ChietKhau],[TongTienSauChietKhau],[IDThanhToan]) OUTPUT INSERTED.ID VALUES (getdate(),@SoDonHang,@IDNguoiLap,@NgayLap,@TongTrongLuong,@TongTien,@IDKhoLap,@GhiChu,@IDNhaCungCap,@NgayDat,@NgayGiaoDuKien,@ChietKhau,@TongTienSauChietKhau,@IDThanhToan)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDThanhToan", IDThanhToan);
                        myCommand.Parameters.AddWithValue("@IDNhaCungCap", IDNhaCungCap);
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@IDNguoiLap", IDNguoiLap);
                        myCommand.Parameters.AddWithValue("@NgayLap", NgayLap);
                        myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@IDKhoLap", IDKhoLap);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@NgayDat", NgayDat);
                        myCommand.Parameters.AddWithValue("@NgayGiaoDuKien", NgayGiaoDuKien);
                        myCommand.Parameters.AddWithValue("@ChietKhau", ChietKhau);
                        myCommand.Parameters.AddWithValue("@TongTienSauChietKhau", TongTienSauChietKhau);
                        IDPhieuKiemKho = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuKiemKho;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        //public void CapNhatDonDatHang(string ID, string SoDonHang, string IDNguoiLap, DateTime NgayLap, string TongTrongLuong, string TongTien, string IDKhoLap, string GhiChu, string IDNhaCungCap, DateTime NgayDat, DateTime NgayGiaoDuKien, string ChietKhau, string TongTienSauChietKhau, string IDThanhToan)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();

        //            string cmdText = "UPDATE [GPM_ThuMua_DonHang] SET [IDThanhToan] = @IDThanhToan, [IDNhaCungCap] = @IDNhaCungCap,[SoDonHang] = @SoDonHang,[IDNguoiLap] = @IDNguoiLap,[NgayLap] = @NgayLap,[TongTrongLuong] = @TongTrongLuong,[TongTien] = @TongTien,[IDKhoLap] = @IDKhoLap,[GhiChu] = @GhiChu,[NgayDat] = @NgayDat,[NgayGiaoDuKien] = @NgayGiaoDuKien,[ChietKhau] = @ChietKhau , [TongTienSauChietKhau] = @TongTienSauChietKhau WHERE ID = @ID";
        //            using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@ID", ID);
        //                myCommand.Parameters.AddWithValue("@IDThanhToan", IDThanhToan);
        //                myCommand.Parameters.AddWithValue("@IDNhaCungCap", IDNhaCungCap);
        //                myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
        //                myCommand.Parameters.AddWithValue("@IDNguoiLap", IDNguoiLap);
        //                myCommand.Parameters.AddWithValue("@NgayLap", NgayLap);
        //                myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
        //                myCommand.Parameters.AddWithValue("@TongTien", TongTien);
        //                myCommand.Parameters.AddWithValue("@IDKhoLap", IDKhoLap);
        //                myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
        //                myCommand.Parameters.AddWithValue("@NgayDat", NgayDat);
        //                myCommand.Parameters.AddWithValue("@NgayGiaoDuKien", NgayGiaoDuKien);
        //                myCommand.Parameters.AddWithValue("@ChietKhau", ChietKhau);
        //                myCommand.Parameters.AddWithValue("@TongTienSauChietKhau", TongTienSauChietKhau);
        //                myCommand.ExecuteNonQuery();
        //            }
        //            myConnection.Close();
        //        }
        //        catch
        //        {
        //            throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
        //        }
        //    }
        //}
        public void XoaChiTietDonHang_Temp(string IDDonHangThuMua)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ThuMua_Temp] WHERE [IDDonHangThuMua] = " + IDDonHangThuMua;
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    strSQL = "DELETE [GPM_ThuMua_DonHang] WHERE [ID] = " + IDDonHangThuMua;
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
        public void XoaChiTietDonHang_Temp_ID(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ThuMua_Temp] WHERE ID = @ID";
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
        
        public DataTable DanhSachDonDatHang_Temp(string IDDonHangThuMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_Temp] WHERE [IDDonHangThuMua] = " + IDDonHangThuMua;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatChiTietDonHang_temp(string IDDonHangThuMua, string IDHangHoa, int SoLuong, float DonGia, float ThanhTien, string GhiChu, string TrongLuong)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ThuMua_Temp] SET [TrongLuong] = @TrongLuong,[GhiChu] = @GhiChu,[SoLuong] = @SoLuong,[DonGia] = @DonGia,[ThanhTien] = @ThanhTien WHERE [IDHangHoa] = @IDHangHoa AND [IDDonHangThuMua] = @IDDonHangThuMua";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@IDDonHangThuMua", IDDonHangThuMua);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
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
        public void ThemChiTietDonHang_Temp(string IDDonHangThuMua, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, int SoLuong, float DonGia, float ThanhTien, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ThuMua_Temp] ([IDDonHangThuMua],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[DonGia],[ThanhTien],[GhiChu]) VALUES (@IDDonHangThuMua,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@DonGia,@ThanhTien,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHangThuMua", IDDonHangThuMua);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
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
        public static DataTable KTChiTietDonHang_Temp(string IDHangHoa, string IDDonHangThuMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "' AND [IDDonHangThuMua] = '" + IDDonHangThuMua + "' ";
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
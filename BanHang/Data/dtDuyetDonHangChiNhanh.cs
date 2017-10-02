using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtDuyetDonHangChiNhanh
    {
        public static string LaySoDonHang(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SoDonHang FROM [GPM_DuyetHangChiNhanh] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return dr["SoDonHang"].ToString();
                    }
                    else return null;
                }
            }
        }
        public static string LayIDKhoLapPhieu(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDKhoLap FROM [GPM_DuyetHangChiNhanh] WHERE ID  = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return dr["IDKhoLap"].ToString();
                    }
                    else return "1";
                }
            }
        }
        public void CapNhatDonHangHoanTat(string IDDonHang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangChiNhanh] SET [TrangThaiDuyet] = 1 WHERE [ID] = '" + IDDonHang + "'";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
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
        public static int TrangThaiDuyet(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThaiDuyet FROM [GPM_DuyetHangChiNhanh] WHERE ID  = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TrangThaiDuyet"].ToString());
                    }
                    else return -1;
                }
            }
        }
        public DataTable DanhSachChiTiet_LOG(string SoDonHang, string IDSoDonHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_Log_DuyetHangChiNhanh] WHERE SoDonHang =N'" + SoDonHang + "' AND [IDSoDonHang] = '" + IDSoDonHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatChiTietDonHang_LOG(string SoDonHang, string IDHangHoa, string SoLuong,  string IDSoDonHang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_Log_DuyetHangChiNhanh] SET [SoLuong] = @SoLuong WHERE [IDHangHoa] = @IDHangHoa AND [SoDonHang] = @SoDonHang AND [IDSoDonHang] = @IDSoDonHang";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@IDSoDonHang", IDSoDonHang);
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
        public void ThemChiTietDonHang_LOG(string SoDonHang, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string IDSoDonHang, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_Log_DuyetHangChiNhanh] ([SoDonHang],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[IDSoDonHang],[GhiChu]) VALUES (@SoDonHang,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@IDSoDonHang,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@IDSoDonHang", IDSoDonHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
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
        public void Xoa_LOG(string SoDonHang, string IDHangHoa, string IDSoDonHang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "DELETE [GPM_Log_DuyetHangChiNhanh]  WHERE [IDHangHoa] = '" + IDHangHoa + "' AND  [SoDonHang] = N'" + SoDonHang + "' AND [IDSoDonHang] = '" + IDSoDonHang + "'";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }
        public DataTable KiemTra_LOG(string SoDonHang, string IDHangHoa, string IDSoDonHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_Log_DuyetHangChiNhanh] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND SoDonHang = N'" + SoDonHang + "' AND [IDSoDonHang] ='" + IDSoDonHang + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatChenhLech(object ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangChiNhanh] SET [TrangThai] = 1 WHERE [ID] = " + ID;
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }
        public static int LayTrangThai(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThaiDuyet FROM [GPM_DuyetHangChiNhanh] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TrangThaiDuyet"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public DataTable DanhSachChiTietDuyet(string IDDuyetHangChiNhanh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangChiNhanh_ChiTiet] WHERE IDDuyetHangChiNhanh =" + IDDuyetHangChiNhanh;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachDonHangDuyet(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangChiNhanh] WHERE ( '" + IDKho + "' = 1 OR [IDKhoLap] = '" + IDKho + "') AND [GPM_DuyetHangChiNhanh].IDTrangThaiXuLy != 2 AND [GPM_DuyetHangChiNhanh].TrangThaiDuyet = 0  ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatTrangThaiClient(string ID, int TrangThaiXuLu)
        {

            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DonHangChiNhanh] SET [TrangThai] = 1 , [IDTrangThaiDonHang] = '" + TrangThaiXuLu + "' WHERE [ID] = " + ID;
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    //cmdText = " UPDATE [GPM_DonHangChiNhanh_ChiTiet] SET [TrangThai] = 1 WHERE [IDDonHangChiNhanh] = " + ID;
                    //using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    //{
                    //    myCommand.ExecuteNonQuery();
                    //}
                    myConnection.Close();
                }
                catch(Exception ex)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }

        public object ThemDuyetDonHang(string IDDonHang, string SoDonHang, string IDNguoiLap, DateTime NgayDat, string IDNguoiDuyet, DateTime NgayDuyet, DateTime NgayGiao, string IDTrangThaiXuLy, string IDKhoLap, string IDKhoDuyet, string GhiChu, string TongTrongLuong, string ChungTu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuKiemKho = null;
                    string cmdText = "INSERT INTO [GPM_DuyetHangChiNhanh] ([IDDonHang],[SoDonHang],[IDNguoiLap],[NgayDat],[IDNguoiDuyet],[NgayDuyet],[NgayGiao],[IDTrangThaiXuLy],[IDKhoLap],[IDKhoDuyet],[GhiChu],[TongTrongLuong],[ChungTu],[NgayCapNhat]) OUTPUT INSERTED.ID VALUES (@IDDonHang,@SoDonHang,@IDNguoiLap,@NgayDat,@IDNguoiDuyet,@NgayDuyet,@NgayGiao,@IDTrangThaiXuLy,@IDKhoLap,@IDKhoDuyet,@GhiChu,@TongTrongLuong,@ChungTu,getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHang", IDDonHang);
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@IDNguoiLap", IDNguoiLap);
                        myCommand.Parameters.AddWithValue("@NgayDat", NgayDat);
                        myCommand.Parameters.AddWithValue("@IDNguoiDuyet", IDNguoiDuyet);
                        myCommand.Parameters.AddWithValue("@NgayDuyet", NgayDuyet);
                        myCommand.Parameters.AddWithValue("@NgayGiao", NgayGiao);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiXuLy", IDTrangThaiXuLy);
                        myCommand.Parameters.AddWithValue("@IDKhoLap", IDKhoLap);
                        myCommand.Parameters.AddWithValue("@IDKhoDuyet", IDKhoDuyet);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
                        myCommand.Parameters.AddWithValue("@ChungTu", ChungTu);
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
        public void Xoa_ALL_Temp()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = " DELETE [GPM_DuyetHangChiNhanh_Temp]";
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
        public void Xoa_Temp_ID(string IDTemp)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = " DELETE [GPM_DuyetHangChiNhanh_Temp] WHERE [IDTemp] = '" + IDTemp + "'";
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
        public void CapNhatChiTietDonHang(string ID, string IDHangHoa, string IDTemp, int ThucTe, int ChenhLech)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangChiNhanh_Temp] SET [ThucTe] = @ThucTe,[ChenhLech] = @ChenhLech WHERE [IDHangHoa] = @IDHangHoa AND [ID] = @ID AND [IDTemp] = @IDTemp";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@ThucTe", ThucTe);
                        myCommand.Parameters.AddWithValue("@ChenhLech", ChenhLech);
                        myCommand.Parameters.AddWithValue("@IDTemp", IDTemp);
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
        public void CapNhatGhiChu(string ID, string IDTemp, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangChiNhanh_Temp] SET [GhiChu] = @GhiChu WHERE [ID] = @ID AND [IDTemp] = @IDTemp";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                       
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@IDTemp", IDTemp);
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
        public void ThemChiTietDonHang_Temp(string IDDonHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string TonKho, string GhiChu, string TrangThai, string IDKho, string IDTemp, string ChenhLech)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangChiNhanh_Temp] ([ChenhLech],[IDDonHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[TonKho],[GhiChu],[TrangThai],[IDKho],[IDTemp]) VALUES (@ChenhLech,@IDDonHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@TonKho,@GhiChu,@TrangThai,@IDKho,@IDTemp)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@ChenhLech", ChenhLech);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDTemp", IDTemp);
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
        public void ThemMoiChiTiet(string IDDonHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string TonKho, string GhiChu, string TrangThai, string IDKho, string IDTemp, string ChenhLech, string ThucTe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangChiNhanh_Temp] ([IDDonHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[TonKho],[GhiChu],[TrangThai],[IDKho],[IDTemp],[ChenhLech],[ThucTe]) VALUES (@IDDonHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@TonKho,@GhiChu,@TrangThai,@IDKho,@IDTemp,@ChenhLech,@ThucTe)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDTemp", IDTemp);
                        myCommand.Parameters.AddWithValue("@ChenhLech", ChenhLech);
                        myCommand.Parameters.AddWithValue("@ThucTe", ThucTe);
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
        public DataTable KTChiTietDonHang_Temp(string IDHangHoa, string IDDonHangChiNhanh, string IDTemp)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_DuyetHangChiNhanh_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "' AND [IDDonHangChiNhanh] = '" + IDDonHangChiNhanh + "' AND [IDTemp] = '" + IDTemp + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void ThemChiTietDonHang_Duyet(object IDDuyetHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string TrangThai, string GhiChu, string IDKho, string ChenhLech, string ThucTe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangChiNhanh_ChiTiet] ([IDDuyetHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[TrangThai],[GhiChu],[IDKho],[ChenhLech],[ThucTe]) VALUES (@IDDuyetHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@TrangThai,@GhiChu,@IDKho,@ChenhLech,@ThucTe)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDuyetHangChiNhanh", IDDuyetHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@ChenhLech", ChenhLech);
                        myCommand.Parameters.AddWithValue("@ThucTe", ThucTe);
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
        public void CapNhat_temp(string IDDonHangChiNhanh, string IDHangHoa, int SoLuong, float DonGia, float ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangChiNhanh_Temp] SET [SoLuong] = @SoLuong,[DonGia] = @DonGia,[ThanhTien] = @ThanhTien WHERE [IDHangHoa] = @IDHangHoa AND [IDDonHangChiNhanh] = @IDDonHangChiNhanh";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
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
        public void Xoa_Temp(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "DELETE GPM_DuyetHangChiNhanh_Temp WHERE ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
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
        public DataTable DanhSachChiTiet(string IDDonHangChiNhanh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh_ChiTiet] WHERE IDDonHangChiNhanh ='" + IDDonHangChiNhanh + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachChiTiet_Temp(string IDDonHangChiNhanh, string IDTemp, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_DuyetHangChiNhanh_Temp].*,[GPM_HangHoaTonKho].SoLuongCon As TonKhoTong FROM [GPM_DuyetHangChiNhanh_Temp],[GPM_HangHoaTonKho] WHERE   [GPM_HangHoaTonKho].IDKho = '" + IDKho + "' AND [GPM_HangHoaTonKho].IDHangHoa = [GPM_DuyetHangChiNhanh_Temp].IDHangHoa  AND [GPM_DuyetHangChiNhanh_Temp].IDDonHangChiNhanh ='" + IDDonHangChiNhanh + "' AND [GPM_DuyetHangChiNhanh_Temp].[IDTemp] = '" + IDTemp + "' ORDER BY [GPM_DuyetHangChiNhanh_Temp].TrangThai ASC ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachChiTiet_Temp_LuuLOG(string IDDonHangChiNhanh, string IDTemp, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_DuyetHangChiNhanh_Temp].*,[GPM_HangHoaTonKho].SoLuongCon As TonKhoTong FROM [GPM_DuyetHangChiNhanh_Temp],[GPM_HangHoaTonKho] WHERE   [GPM_HangHoaTonKho].IDKho = '" + IDKho + "' AND [GPM_HangHoaTonKho].IDHangHoa = [GPM_DuyetHangChiNhanh_Temp].IDHangHoa  AND [GPM_DuyetHangChiNhanh_Temp].IDDonHangChiNhanh ='" + IDDonHangChiNhanh + "' AND [GPM_DuyetHangChiNhanh_Temp].[IDTemp] = '" + IDTemp + "' AND [GPM_DuyetHangChiNhanh_Temp].TrangThai = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachDonHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh] WHERE [TrangThai] = 0 AND [GiamSatDuyet] = 1  AND [IDNguoiLap] is not null ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachDonHangXuLy1Phan()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh] WHERE [TrangThai] = 1 AND IDTrangThaiDonHang = 4 AND [GiamSatDuyet] = 1  AND [IDNguoiLap] is not null ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachDonHang_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh] WHERE ID = '" + ID + "' ";
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
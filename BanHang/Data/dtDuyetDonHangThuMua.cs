using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtDuyetDonHangThuMua
    {
        public DataTable KTChiTietDonHang_Temp(string IDHangHoa, string IDDonHangThuMua, string IDTemp)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_DuyetHangThuMua_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "' AND [IDDonHangThuMua] = '" + IDDonHangThuMua + "' AND [IDTemp] = '" + IDTemp + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
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
                    string strSQL = " DELETE [GPM_DuyetHangThuMua_Temp] WHERE [IDTemp] = '" + IDTemp + "'";
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
        public void Xoa_IDCu_NhanVien(string IDNhanVien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_DuyetHangThuMua_Temp] WHERE [IDNhanVien] = '" + IDNhanVien + "'";
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
        public DataTable DanhSachChiTiet_Duyet_ThuMua(string IDDonHangThuMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangThuMua_ChiTiet] WHERE IDDonHangThuMua =" + IDDonHangThuMua;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachDuyet_ThuMua()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang] WHERE IDNguoiLap is not null AND NgayLap is not null AND TrangThaiDonHang = 0 AND IDTrangThaiDonHang = 3 AND TrangThai =0  ";
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
                    string cmdText = "UPDATE [GPM_DuyetHangThuMua] SET [TrangThai] = 1 WHERE [ID] = " + ID;
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
        public void CapNhatTrangThaiThuMua(string ID, int IDTrangThaiDonHang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ThuMua_DonHang] SET [TrangThai] = 1 , [IDTrangThaiDonHang] = '" + IDTrangThaiDonHang + "' WHERE  [ID] = " + ID;
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
                string cmdText = "SELECT * FROM [GPM_Log_DuyetHangThuMua] WHERE [IDSoDonHang] = '" + IDSoDonHang + "' AND [IDHangHoa] = '" + IDHangHoa + "' AND SoDonHang = N'" + SoDonHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
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
                    string cmdText = "DELETE [GPM_Log_DuyetHangThuMua]  WHERE [IDSoDonHang] = '" + IDSoDonHang + "' AND [IDHangHoa] = '" + IDHangHoa + "' AND  [SoDonHang] = N'" + SoDonHang + "'";
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
        public void ThemChiTietDonHang_LOG(string IDSoDonHang, string SoDonHang, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_Log_DuyetHangThuMua] ([IDSoDonHang],[SoDonHang],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[GhiChu]) VALUES (@IDSoDonHang,@SoDonHang,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@IDSoDonHang", IDSoDonHang);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
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
        public void CapNhatChiTietDonHang_LOG(string IDSoDonHang,string SoDonHang, string IDHangHoa, string SoLuong)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_Log_DuyetHangThuMua] SET [SoLuong] = @SoLuong WHERE [IDHangHoa] = @IDHangHoa AND [SoDonHang] = @SoDonHang AND [IDSoDonHang] = @IDSoDonHang";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@IDSoDonHang", IDSoDonHang);;
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
        public void ThemChiTietDonHang_Duyet(object IDDonHangThuMua, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string GhiChu, string ChenhLech, string ThucTe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangThuMua_ChiTiet] ([IDDonHangThuMua],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[GhiChu],[ChenhLech],[ThucTe]) VALUES (@IDDonHangThuMua,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@GhiChu,@ChenhLech,@ThucTe)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ThucTe", ThucTe);
                        myCommand.Parameters.AddWithValue("@ChenhLech", ChenhLech);
                        myCommand.Parameters.AddWithValue("@IDDonHangThuMua", IDDonHangThuMua);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
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
        public object ThemDuyetDonHang(string IDDonHang, string SoDonHang, string IDNguoiLap, string TongTrongLuong, string IDNguoiDuyet, string GhiChu, DateTime NgayDat, DateTime NgayDuyet, DateTime NgayGiao, string IDNhaCungCap, int IDTrangThaiXuLy, string ChungTu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuKiemKho = null;
                    string cmdText = "INSERT INTO [GPM_DuyetHangThuMua] ([IDDonHang],[SoDonHang],[NgayCapNhat],[IDNguoiLap],[TongTrongLuong],[IDNguoiDuyet],[GhiChu],[NgayDat],[NgayDuyet],[NgayGiao],[IDNhaCungCap],[IDTrangThaiXuLy],[ChungTu]) OUTPUT INSERTED.ID VALUES (@IDDonHang,@SoDonHang,getdate(),@IDNguoiLap,@TongTrongLuong,@IDNguoiDuyet,@GhiChu,@NgayDat,@NgayDuyet,@NgayGiao,@IDNhaCungCap,@IDTrangThaiXuLy,@ChungTu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHang", IDDonHang);
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@IDNguoiLap", IDNguoiLap);
                        myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
                        //myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@IDNguoiDuyet", IDNguoiDuyet);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@NgayDat", NgayDat);
                        myCommand.Parameters.AddWithValue("@NgayDuyet", NgayDuyet);
                        myCommand.Parameters.AddWithValue("@NgayGiao", NgayGiao);
                        myCommand.Parameters.AddWithValue("@IDNhaCungCap", IDNhaCungCap);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiXuLy", IDTrangThaiXuLy);
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

        public void CapNhatChiTietDonHang(string ID, string IDHangHoa, string IDTemp, int ThucTe, int ChenhLech)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangThuMua_Temp] SET [ThucTe] = @ThucTe,[ChenhLech] = @ChenhLech WHERE [IDHangHoa] = @IDHangHoa AND [ID] = @ID AND [IDTemp] = @IDTemp";
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
      
        public DataTable DanhSachChiTiet_Temp(string IDDonHangThuMua, string IDTemp)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangThuMua_Temp] WHERE IDDonHangThuMua = '" + IDDonHangThuMua + "' AND [IDTemp] = '" + IDTemp + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        ////public DataTable DanhSachChiTiet_LOG_Temp(string SoDonHang)
        ////{
        ////    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
        ////    {
        ////        con.Open();
        ////        string cmdText = "SELECT * FROM [GPM_Log_DuyetHangThuMua] WHERE SoDonHang = '" + SoDonHang + "'";
        ////        using (SqlCommand command = new SqlCommand(cmdText, con))
        ////        using (SqlDataReader reader = command.ExecuteReader())
        ////        {
        ////            DataTable tb = new DataTable();
        ////            tb.Load(reader);
        ////            return tb;
        ////        }
        ////    }
        ////}
        public void ThemChiTietDonHang_Temp(string IDDonHangThuMua, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string GhiChu, string IDTemp)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangThuMua_Temp] ([IDDonHangThuMua],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[GhiChu],[ChenhLech],[IDTemp]) VALUES (@IDDonHangThuMua,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@GhiChu,@ChenhLech,@IDTemp)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHangThuMua", IDDonHangThuMua);
                        myCommand.Parameters.AddWithValue("@IDTemp", IDTemp);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@ChenhLech", SoLuong);
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
        public DataTable DanhSachChiTiet_LOG(string SoDonHang, string IDSoDonHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_Log_DuyetHangThuMua] WHERE SoDonHang =N'" + SoDonHang + "' AND [IDSoDonHang] = '" + IDSoDonHang + "'";
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
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang] WHERE ID = '" + ID + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        //public void Xoa_ALL_Temp()
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string strSQL = " DELETE [GPM_DuyetHangThuMua_Temp]";
        //            using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
        //            {
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
        //        }
        //    }
        //}
    }
}
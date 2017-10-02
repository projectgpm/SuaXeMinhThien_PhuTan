using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtPhieuChuyenKho
    {
        public static string MaPhieuChuyenKho(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select MaPhieuChuyenKho  from GPM_PhieuChuyenKho WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                        return tb.Rows[0]["MaPhieuChuyenKho"].ToString();
                    return null;
                }
            }
        }
        public DataTable DanhSachPhieuChuyenKho_Tong()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND DaXoa = 0 AND IDTrangThai = 1 AND SoMatHang is not null ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Tong_DangChuyen()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND DaXoa = 0 AND (IDTrangThai = 2 OR IDTrangThai = 3) AND SoMatHang is not null ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Tong_HoanThanh()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND DaXoa = 0 AND IDTrangThai = 4 AND SoMatHang is not null ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Tong_Huy()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND DaXoa = 1 AND SoMatHang is not null ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachChiTietPhieuChuyenKho(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.IDDonViTinh, GPM_PhieuChuyenKho_ChiTiet.ID, GPM_PhieuChuyenKho_ChiTiet.IDHangHoa, GPM_PhieuChuyenKho_ChiTiet.SoLuong, GPM_PhieuChuyenKho_ChiTiet.TrongLuong, GPM_PhieuChuyenKho_ChiTiet.GhiChu from GPM_HangHoa, GPM_PhieuChuyenKho_ChiTiet where GPM_HangHoa.ID = GPM_PhieuChuyenKho_ChiTiet.IDHangHoa and GPM_PhieuChuyenKho_ChiTiet.IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public string TongSoHDCuaKhoNhan(string NgayBD, string NgayKT, string IDKhoXuat, string IDKhoNhan)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select IDKhoXuat, count(IDKhoXuat) as Tong from GPM_PhieuChuyenKho where NgayLap >= '" + DateTime.Parse(NgayBD) + "' and NgayLap <= '" + DateTime.Parse(NgayKT) + "' and IDKhoXuat = '" + IDKhoXuat + "' and ('" + IDKhoNhan + "' = -1 or IDKhoNhap = '" + IDKhoNhan + "') group by IDKhoXuat";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                        return tb.Rows[0]["Tong"].ToString();
                    return 0 +"";
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Kho(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM GPM_PhieuChuyenKho WHERE ID = '" + IDPhieuChuyenKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }


        public int TrangThaiPhieuChuyenKho(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDTrangThai FROM GPM_PhieuChuyenKho WHERE ID = '" + IDPhieuChuyenKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return Int32.Parse(tb.Rows[0]["IDTrangThai"].ToString());
                    }
                    return 1;
                }
            }
        }

        public DataTable ChiTietTongSoLuongHangHoa(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select IDPhieuChuyenKho, SUM(SoLuong) as TongSoLuong, SUM(TrongLuong) as TongTrongLuong from GPM_PhieuChuyenKho_ChiTiet where IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "' group by IDPhieuChuyenKho";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }


        public DataTable DanhSachPhieuChuyenKho_Client(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND SoMatHang is not null AND DaXoa = 0 AND IDTrangThai = 1 AND (IDKhoXuat = '" + IDKho + "' OR IDKhoNhap = '" + IDKho + "') ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Client_DaChuyen(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND SoMatHang is not null AND DaXoa = 0 AND (IDTrangThai = 2 OR IDTrangThai = 3) AND (IDKhoXuat = '" + IDKho + "' OR IDKhoNhap = '" + IDKho + "') ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Client_HoanThanh(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND SoMatHang is not null AND DaXoa = 0 AND IDTrangThai = 4 AND IDTrangThai = 1 AND (IDKhoXuat = '" + IDKho + "' OR IDKhoNhap = '" + IDKho + "') ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Client_Huy(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND SoMatHang is not null AND DaXoa = 1 AND (IDKhoXuat = '" + IDKho + "' OR IDKhoNhap = '" + IDKho + "') ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachChiTietPhieuChuyenKho_Temp(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GPM_HangHoa.IDDonViTinh,GPM_HangHoa.TenHangHoa,GPM_HangHoa.MaHang,GPM_ChiTietPhieuChuyenKho_Temp.* FROM GPM_ChiTietPhieuChuyenKho_Temp,GPM_HangHoa WHERE GPM_ChiTietPhieuChuyenKho_Temp.IDHangHoa = GPM_HangHoa.ID AND GPM_ChiTietPhieuChuyenKho_Temp.IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable KiemTraHangHoa(string IDPhieuChuyenKho, string IDHH)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM GPM_PhieuChuyenKho_ChiTiet WHERE IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "' AND IDHangHoa = '" + IDHH + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable PhieuChuyenKho_Null(string IDKhoXuat)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM GPM_PhieuChuyenKho WHERE IDKhoXuat = '" + IDKhoXuat + "' and IDKhoNhap is null and SoMatHang is null and TrongLuong is null";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public object ThemPhieuChuyenKho(string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuChuyenKho = null;
                    string cmdText = "INSERT INTO [GPM_PhieuChuyenKho] ([NgayLap],[IDKhoXuat]) OUTPUT INSERTED.ID VALUES (getdate(),'" + IDKho + "')";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        IDPhieuChuyenKho = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuChuyenKho;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public void XoaPhieuChuyenKho(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhieuChuyenKho] WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
                }
            }
        }

        public void XoaPhieuChuyenKho_Update(string ID,string NV)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_PhieuChuyenKho] SET DaXoa = 1, GhiChu = N'Nhân viên: " + NV + " hủy phiếu.' WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
                }
            }
        }

        public void XoaChiTietPhieuChuyenKho_Delete(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhieuChuyenKho_ChiTiet] WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
                }
            }
        }

        public void XoaChiTietPhieuChuyenKho_Delete_Phieu(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhieuChuyenKho_ChiTiet] WHERE [IDPhieuChuyenKho] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
                }
            }
        }

        public void XoaChiTietPhieuChuyenKho_Update(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE GPM_PhieuChuyenKho_ChiTiet SET SoLuong = 0, TrongLuong = 0 WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
                }
            }
        }

        public void CapNhatPhieuChuyenKho(string ID, string IDKhoXuat, string IDKhoNhap, string IDNhanVienLap, string SoMatHang, string TrongLuong, string GhiChu, string NguoiGiao, string MaSoPhieu, string SoPhieuHeThong, string file)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [FileChungTu] = @FileChungTu, [SoPhieuHeThong] = @SoPhieuHeThong, [MaPhieuChuyenKho] = @MaPhieuChuyenKho, [IDKhoXuat] = @IDKhoXuat, [IDKhoNhap] = @IDKhoNhap, [IDNhanVienLap] = @IDNhanVienLap, [SoMatHang] = @SoMatHang, [TrongLuong] = @TrongLuong, [NgayLap] = getDATE(), [NgayCapNhat] = getDATE(), [GhiChu] = @GhiChu, NguoiGiao = @NguoiGiao where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDKhoXuat", IDKhoXuat);
                        myCommand.Parameters.AddWithValue("@IDKhoNhap", IDKhoNhap);
                        myCommand.Parameters.AddWithValue("@IDNhanVienLap", IDNhanVienLap);
                        myCommand.Parameters.AddWithValue("@SoMatHang", SoMatHang);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@NguoiGiao", NguoiGiao);
                        myCommand.Parameters.AddWithValue("@MaPhieuChuyenKho", MaSoPhieu);
                        myCommand.Parameters.AddWithValue("@SoPhieuHeThong", SoPhieuHeThong);
                        myCommand.Parameters.AddWithValue("@FileChungTu", file);
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

        public void CapNhatPhieuChuyenKho_2(string ID, string SoMatHang, string TrongLuong)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [SoMatHang] = @SoMatHang, [TrongLuong] = @TrongLuong where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@SoMatHang", SoMatHang);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
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

        public void DuyetChuyenKho(string IDPhieuChuyenKho, string TrangThai)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDTrangThai] = @IDTrangThai where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDTrangThai", TrangThai);
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

        public void DuyetChuyenKho_Xuat_CH1(string IDPhieuChuyenKho, string IDNhanVien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDCuaHangTruong1] = @IDCuaHangTruong1,[NgayCapNhat] = getDATE(),[NgayXuat] = getDATE() where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDCuaHangTruong1", IDNhanVien);
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

        public void DuyetChuyenKho_Xuat_GS1(string IDPhieuChuyenKho, string IDNhanVien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDGiamSat1] = @IDGiamSat1,[NgayCapNhat] = getDATE(),[NgayXuat] = getDATE(), IDTrangThai = 2 where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDGiamSat1", IDNhanVien);
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

        public void DuyetChuyenKho_Nhap_CH1(string IDPhieuChuyenKho, string IDNhanVien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDCuaHangTruong2] = @IDCuaHangTruong2,[NgayCapNhat] = getDATE(),[NgayNhap] = getDATE() where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDCuaHangTruong2", IDNhanVien);
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

        public void DuyetChuyenKho_Nhap_GS1(string IDPhieuChuyenKho, string IDNhanVien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDGiamSat2] = @IDGiamSat2,[NgayCapNhat] = getDATE(),[NgayNhap] = getDATE(), IDTrangThai = 3 where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDGiamSat2", IDNhanVien);
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

        public void DuyetChuyenKho_HoanThanh(string IDPhieuChuyenKho, string IDNhanVien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDNhanVienKho1] = @IDNhanVienKho1,[NgayCapNhat] = getDATE(), IDTrangThai = 4 where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDNhanVienKho1", IDNhanVien);
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

        public void CapNhatChiTietPhieuChuyenKho_Temp(string IDPhieuChuyenKho, string IDHangHoa, string SoLuong, string TrongLuong, string GiaBan, string TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_ChiTietPhieuChuyenKho_Temp set [SoLuong] = @SoLuong, [TrongLuong] = @TrongLuong, [GiaBan] = @GiaBan, [TongTien] = @TongTien  where IDPhieuChuyenKho = @IDPhieuChuyenKho AND IDHangHoa = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuChuyenKho", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
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

        public void CapNhatChiTietPhieuChuyenKho(string IDPhieuChuyenKho, string IDHangHoa, string SoLuong, string TrongLuong, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho_ChiTiet set [SoLuong] = @SoLuong, [TrongLuong] = @TrongLuong, [GhiChu] = @GhiChu where IDPhieuChuyenKho = @IDPhieuChuyenKho AND IDHangHoa = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuChuyenKho", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
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

        public void ThemChiTietPhieuChuyenKho_Temp(string IDPhieuChuyenKho, string IDHangHoa, string SoLuong, string TrongLuong, string GiaBan, string TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietPhieuChuyenKho_Temp] ([IDPhieuChuyenKho],[IDHangHoa],[SoLuong],[TrongLuong],[GiaBan],[TongTien]) VALUES (@IDPhieuChuyenKho,@IDHangHoa,@SoLuong,@TrongLuong,@GiaBan,@TongTien)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuChuyenKho", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
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

        public void ThemChiTietPhieuChuyenKho(string IDPhieuChuyenKho, string IDHangHoa, string SoLuong, string TrongLuong, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_PhieuChuyenKho_ChiTiet] ([IDPhieuChuyenKho],[IDHangHoa],[SoLuong],[TrongLuong],[GhiChu]) VALUES (@IDPhieuChuyenKho,@IDHangHoa,@SoLuong,@TrongLuong,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuChuyenKho", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
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
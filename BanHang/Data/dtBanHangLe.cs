using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace BanHang.Data
{
    public class dtBanHangLe
    {
        public float LayGiaBanTheoSoLuong(int IDHangHoa, int SLMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan FROM GPM_HangHoa_GiaTheoSL WHERE IDHangHoa = '" + IDHangHoa + "' AND '" + SLMua + "' >= SoLuongBD AND '" + SLMua + "' <= SoLuongKT AND DaXoa = 0";
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
                    return -1;
                }
            }
        }
        public DataTable DanhSachGiaTheoSoLuong(int IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan FROM GPM_HangHoa_GiaTheoSL WHERE IDHangHoa = '" + IDHangHoa + "' AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }
        public void ThemHangQuyDoi(int IDHangHoaQuiDoi, int SoLuong, int SoLuongCon, string IDKho, string IDNguoiDung, int IDHangHoa, int MaHoaDon)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_LOG_QuiDoi] ([IDHangHoaQuiDoi],[IDHangHoa],[SoLuong],[IDKho],[SoLuongCon],[IDNguoiDung],[MaHoaDon]) VALUES (@IDHangHoaQuiDoi,@IDHangHoa,@SoLuong,@IDKho,@SoLuongCon,@IDNguoiDung,@MaHoaDon)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                        myCommand.Parameters.AddWithValue("@IDHangHoaQuiDoi", IDHangHoaQuiDoi);
                        myCommand.Parameters.AddWithValue("@IDNguoiDung", IDNguoiDung);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
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
        public void CapNhatHangQuyDoi(int IDHangHoaQuiDoi, int SoLuong, int SoLuongCon, string IDKho, string IDNguoiDung, int IDHangHoa)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_LOG_QuiDoi] SET [SoLuong] = @SoLuong , [SoLuongCon] = @SoLuongCon WHERE [IDHangHoaQuiDoi] = @IDHangHoaQuiDoi  AND  [IDHangHoa] = @IDHangHoa AND [IDKho] = @IDKho AND [IDNguoiDung] = @IDNguoiDung";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoaQuiDoi", IDHangHoaQuiDoi);
                        myCommand.Parameters.AddWithValue("@IDNguoiDung", IDNguoiDung);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
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
        public DataTable LayThongHoaDon(string TuKhoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select TOP 10 GPM_HoaDon.ID,GPM_HoaDon.[MaHoaDon],GPM_KhachHang.TenKhachHang,GPM_HoaDon.NgayBan from GPM_HoaDon,GPM_KhachHang WHERE GPM_HoaDon.IDKhachHang = GPM_KhachHang.ID AND (GPM_HoaDon.[MaHoaDon] = '" + TuKhoa + "' OR GPM_KhachHang.TenKhachHang = N'" + TuKhoa + "') ORDER BY GPM_HoaDon.ID DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable LayThongHoaDon_BaoCao(string NgayBD, string NgayKT, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_ChiTietHoaDon.GiaMua,GPM_ChiTietHoaDon.GiaBan,GPM_ChiTietHoaDon.SoLuong from GPM_ChiTietHoaDon,GPM_HoaDon where GPM_ChiTietHoaDon.IDHangHoa = GPM_HoaDon.ID and GPM_HoaDon.IDKho = '" + IDKho + "' and GPM_HoaDon.NgayBan >= '" + NgayBD + "' and GPM_HoaDon.NgayBan <= '" + NgayKT + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable LayThongChiTietHoaDon_ID(string IDHoaDon)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_ChiTietHoaDon.GiaBan,GPM_ChiTietHoaDon.SoLuong,GPM_ChiTietHoaDon.ThanhTien,GPM_HangHoa.TenHangHoa,GPM_HangHoa.MaHang,GPM_DonViTinh.TenDonViTinh from GPM_ChiTietHoaDon,GPM_HangHoa,GPM_DonViTinh WHERE GPM_HangHoa.ID = GPM_ChiTietHoaDon.IDHangHoa AND GPM_HangHoa.IDDonViTinh = GPM_DonViTinh.ID AND GPM_ChiTietHoaDon.IDHoaDon = " + IDHoaDon;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public static int KT_GiaApDung(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaApDung FROM [GPM_Kho] WHERE [ID]  =  '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["GiaApDung"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public DataTable LayThongTinHangHoa(string Barcode, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "";
                cmdText = "SELECT HH.ID, HH.TenHangHoa,HH.MaHang, Dvi.TenDonViTinh, HH.GiaBan, HH.GiaMua " +
                                 "FROM GPM_HangHoa AS HH " +
                                 "INNER JOIN GPM_HangHoaTonKho AS HHTK ON HH.ID = HHTK.IDHangHoa " +
                                 "INNER JOIN GPM_DonViTinh as DVi ON HH.IDDonViTinh = DVi.ID " +
                                 "LEFT OUTER JOIN GPM_HangHoa_Barcode AS BC ON HHTK.IDHangHoa = BC.IDHangHoa " +
                                 "WHERE (BC.Barcode = @Barcode OR CONVERT(NVARCHAR(250), HHTK.IDHangHoa) = @Barcode) AND HHTK.IDKho = @IDKho AND HHTK.DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.Parameters.AddWithValue("@Barcode", Barcode);
                    command.Parameters.AddWithValue("@IDKho", IDKho);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
            //using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //{
            //    con.Open();
            //    string cmdText = "";
            //    cmdText = "SELECT HH.ID, HH.TenHangHoa,HH.MaHang, Dvi.TenDonViTinh, HHTK.GiaBan, HH.GiaMuaSauThue " +
            //                     "FROM GPM_HangHoa AS HH " +
            //                     "INNER JOIN GPM_HangHoaTonKho AS HHTK ON HH.ID = HHTK.IDHangHoa " +
            //                     "INNER JOIN GPM_DonViTinh as DVi ON HH.IDDonViTinh = DVi.ID " +
            //                     "LEFT OUTER JOIN GPM_HangHoa_Barcode AS BC ON HHTK.IDHangHoa = BC.IDHangHoa " +
            //                     "WHERE (BC.Barcode = @Barcode OR CONVERT(NVARCHAR(250), HHTK.IDHangHoa) = @Barcode) AND HHTK.IDKho = @IDKho AND HHTK.DaXoa = 0";
            //    using (SqlCommand command = new SqlCommand(cmdText, con))
            //    {
            //        command.Parameters.AddWithValue("@Barcode", Barcode);
            //        command.Parameters.AddWithValue("@IDKho", IDKho);
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}
        }

        public object InsertHoaDon(string IDKho, string IDNhanVien, string IDKhachHang, HoaDon hoaDon, string IDKyThuat, string TienChietKhauKyThuat, string TienChietKhauKhachHang, string TienCongNoKhachHang, string TyLeChietKhauKhachHang, string TyLeChietKhauKyThuat)
        {
            object IDHoaDon = null;
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                SqlTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    string CompuMaHoaDon = @"SELECT 
                                          REPLICATE('0', 5 - LEN((count(ID) + 1))) + 
                                          CAST((count(ID) + 1) AS varchar) + '-' + 
                                          CAST(@IDKho AS varchar) + '-' + 
                                          FORMAT(GETDATE() , 'ddMMyy')
                                          as 'Mã Hóa Đơn'  
                                          from GPM_HoaDon 
                                          where (IDKho = @IDKho)
                                          AND DATEDIFF(dd,NgayBan, GetDate()) = 0";
                    object MaHoaDon;
                    using (SqlCommand cmd = new SqlCommand(CompuMaHoaDon, con, trans))
                    {
                        cmd.Parameters.AddWithValue("@IDKho", dtSetting.LayMaKho(IDKho));
                        MaHoaDon = cmd.ExecuteScalar();
                    }
                    if (MaHoaDon != null)
                    {
                        string InsertHoaDon = "INSERT INTO [GPM_HoaDon] ([IDKho], [IDKhachHang],[IDNhanVien],[NgayBan],[SoLuongHang],[TongTien],[GiamGia],[KhachCanTra],[KhachThanhToan], [MaHoaDon],[IDKyThuat],[TienChietKhauKyThuat],[TienChietKhauKhachHang],[TyLeChietKhauKhachHang],[TyLeChietKhauKyThuat],[TienSuaXe]) " +
                                              "OUTPUT INSERTED.ID " +
                                              "VALUES (@IDKho, @IDKhachHang, @IDNhanVien, getdate(), @SoLuongHang, @TongTien, @GiamGia, @KhachCanTra, @KhachThanhToan, @MaHoaDon,@IDKyThuat,@TienChietKhauKyThuat,@TienChietKhauKhachHang,@TyLeChietKhauKhachHang,@TyLeChietKhauKyThuat,@TienSuaXe)";
                        using (SqlCommand cmd = new SqlCommand(InsertHoaDon, con, trans))
                        {
                            cmd.Parameters.AddWithValue("@IDKho", IDKho);
                            cmd.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                            cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                            cmd.Parameters.AddWithValue("@SoLuongHang", hoaDon.SoLuongHang);
                            cmd.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                            cmd.Parameters.AddWithValue("@GiamGia", hoaDon.GiamGia);
                            cmd.Parameters.AddWithValue("@KhachCanTra", hoaDon.KhachCanTra);
                            cmd.Parameters.AddWithValue("@KhachThanhToan", hoaDon.KhachThanhToan);
                            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                            cmd.Parameters.AddWithValue("@IDKyThuat", IDKyThuat);
                            cmd.Parameters.AddWithValue("@TienChietKhauKyThuat", TienChietKhauKyThuat);
                            cmd.Parameters.AddWithValue("@TienChietKhauKhachHang", TienChietKhauKhachHang);
                            cmd.Parameters.AddWithValue("@TyLeChietKhauKyThuat", TyLeChietKhauKyThuat);
                            cmd.Parameters.AddWithValue("@TyLeChietKhauKhachHang", TyLeChietKhauKhachHang);
                            cmd.Parameters.AddWithValue("@TienSuaXe", hoaDon.TienSuaXe);
                            IDHoaDon = cmd.ExecuteScalar();
                        }
                        if (IDHoaDon != null)
                        {
                            string CapNhatTongTienKyThuat = "UPDATE [GPM_KyThuat] SET [TongTien] = [TongTien] + @TongTien WHERE [ID] = @IDKyThuat";
                            using (SqlCommand cmd = new SqlCommand(CapNhatTongTienKyThuat, con, trans))
                            {
                                // cập nhật tổng tiền kỹ thuật
                                cmd.Parameters.AddWithValue("@TongTien", TienChietKhauKyThuat);
                                cmd.Parameters.AddWithValue("@IDKyThuat", IDKyThuat);
                                cmd.ExecuteNonQuery();
                            }
                            string CapNhatCongNoKhachHang = "UPDATE [GPM_KhachHang] SET [CongNo] = [CongNo] + @CongNo WHERE [ID] = @IDKhachHang";
                            using (SqlCommand cmd = new SqlCommand(CapNhatCongNoKhachHang, con, trans))
                            {
                                // cập nhật công nợ khách hàng
                                cmd.Parameters.AddWithValue("@CongNo", TienCongNoKhachHang);
                                cmd.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                                cmd.ExecuteNonQuery();
                            }
                            foreach (ChiTietHoaDon cthd in hoaDon.ListChiTietHoaDon)
                            {

                                dtHangHoa dtHH = new dtHangHoa();
                                string InsertChiTietHoaDon = "INSERT INTO [GPM_ChiTietHoaDon] ([IDHoaDon],[IDHangHoa],[GiaMua],[GiaBan] ,[SoLuong],[ChietKhau],[ThanhTien],[NgayBan],[IDKho],[GiaBanKyThuat],[ChenhLech]) " +
                                                             "VALUES (@IDHoaDon, @IDHangHoa, @GiaMua, @GiaBan, @SoLuong, @ChietKhau, @ThanhTien,getdate(),@IDKho,@GiaBanKyThuat,@ChenhLech)";
                                using (SqlCommand cmd = new SqlCommand(InsertChiTietHoaDon, con, trans))
                                {
                                    //thêm chi tiết hóa đơn
                                    cmd.Parameters.AddWithValue("@IDHoaDon", IDHoaDon);
                                    cmd.Parameters.AddWithValue("@IDHangHoa", cthd.IDHangHoa);
                                    cmd.Parameters.AddWithValue("@GiaMua", cthd.GiaMua);
                                    cmd.Parameters.AddWithValue("@GiaBan", cthd.DonGia);
                                    cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                                    cmd.Parameters.AddWithValue("@ChietKhau", 0.0);
                                    cmd.Parameters.AddWithValue("@ThanhTien", cthd.ThanhTien);
                                    cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                    cmd.Parameters.AddWithValue("@GiaBanKyThuat", cthd.GiaKyThuat);
                                    cmd.Parameters.AddWithValue("@ChenhLech", cthd.GiaKyThuat - cthd.DonGia);
                                    cmd.ExecuteNonQuery();
                                }
                                string UpdateLichSuBanHang = "DECLARE @SoLuongCu INT = 0 " +
                                                             "SELECT @SoLuongCu = SoLuongCon FROM [GPM_HangHoaTonKho] WHERE IDHangHoa = @IDHangHoa  AND IDKho = @IDKho " +
                                                             "DECLARE @SoLuongMoi INT = @SoLuongCu - @SoLuongBan " +
                                                             "INSERT INTO [GPM_LichSuKho] ([IDHangHoa], [IDNhanVien], [SoLuong], [SoLuongMoi], [NoiDung],[NgayCapNhat]) VALUES (@IDHangHoa, @IDNhanVien, @SoLuongCu, @SoLuongMoi, @NoiDung, GetDate()) " +
                                                             "UPDATE [GPM_HangHoaTonKho] SET SoLuongCon = @SoLuongMoi, NgayCapNhat = GetDate() WHERE IDHangHoa = @IDHangHoa AND IDKho = @IDKho";
                                using (SqlCommand cmd = new SqlCommand(UpdateLichSuBanHang, con, trans))
                                {
                                    cmd.Parameters.AddWithValue("@SoLuongBan", cthd.SoLuong);
                                    cmd.Parameters.AddWithValue("@IDHangHoa", cthd.IDHangHoa);
                                    cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                    cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                                    cmd.Parameters.AddWithValue("@NoiDung", "Bán hàng lẻ");
                                    cmd.ExecuteNonQuery();
                                }
                                //ghi thẻ kho
                                //int TonKhoHangHoa = 0;
                                //string cmdText1 = " SELECT SoLuongCon FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + cthd.IDHangHoa + "' AND IDKho =" + IDKho;
                                //using (SqlCommand cmd1 = new SqlCommand(cmdText1, con, trans))
                                //using (SqlDataReader reade1r = cmd1.ExecuteReader())
                                //{
                                //    DataTable tb1 = new DataTable();
                                //    tb1.Load(reade1r);
                                //    if (tb1.Rows.Count != 0)
                                //    {
                                //        DataRow dr1 = tb1.Rows[0];
                                //        TonKhoHangHoa = Int32.Parse(dr1["SoLuongCon"].ToString());
                                //    }
                                //    else TonKhoHangHoa = 0;
                                //}

                                //string cmdText = "INSERT INTO [GPM_TheKho] ([MaDonHang], [NgayLap], [DienGiai], [NhapTrongKy],[XuatTrongKy],[TonCuoiKy], [IDNhanVien],[IDKho],[IDHangHoa],[LoaiPhieu],[XuatKhac],[XuatTra],[KiemKho]) OUTPUT INSERTED.ID  VALUES (@MaDonHang,getdate(),@DienGiai, @NhapTrongKy,@XuatTrongKy,@TonCuoiKy,@IDNhanVien,@IDKho,@IDHangHoa,@LoaiPhieu,@XuatKhac,@XuatTra,@KiemKho)";
                                //using (SqlCommand cmd = new SqlCommand(cmdText, con, trans))
                                //{
                                //    cmd.Parameters.AddWithValue("@XuatKhac", 0);
                                //    cmd.Parameters.AddWithValue("@XuatTra", 0);
                                //    cmd.Parameters.AddWithValue("@KiemKho", 0);
                                //    cmd.Parameters.AddWithValue("@LoaiPhieu", "Xuất");
                                //    cmd.Parameters.AddWithValue("@IDHangHoa", cthd.IDHangHoa);
                                //    cmd.Parameters.AddWithValue("@MaDonHang", MaHoaDon);
                                //    cmd.Parameters.AddWithValue("@DienGiai", "Bán Hàng Lẻ");
                                //    cmd.Parameters.AddWithValue("@NhapTrongKy", 0);
                                //    cmd.Parameters.AddWithValue("@XuatTrongKy", cthd.SoLuong);
                                //    cmd.Parameters.AddWithValue("@TonCuoiKy", (TonKhoHangHoa));
                                //    cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                                //    cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                //    cmd.ExecuteNonQuery();
                                //}
                            }
                        }
                    }
                    trans.Commit();
                    con.Close();
                }
                catch (Exception ex)
                {
                    if (trans != null) trans.Rollback();
                    throw new Exception("Quá trình lưu dữ liệu có lỗi xảy ra, vui lòng tải lại trang và thanh toán lại !!");
                }
            }
            return IDHoaDon;
        }
        
    }
}
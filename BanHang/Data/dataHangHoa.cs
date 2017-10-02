using BanHang.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dataHangHoa
    {
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

        public DataTable getDanhSachHangHoa()
        {
            string cmd = "SELECT GPM_HangHoa.*, GPM_HangHoaTonKho.GiaBan, GPM_HangHoaTonKho.GiaBan1, GPM_HangHoaTonKho.GiaBan2, GPM_HangHoaTonKho.GiaBan3, GPM_HangHoaTonKho.GiaBan4, GPM_HangHoaTonKho.GiaBan5 FROM GPM_HangHoa, GPM_HangHoaTonKho WHERE GPM_HangHoa.DaXoa = 0 AND GPM_HangHoa.ID = GPM_HangHoaTonKho.IDHangHoa AND GPM_HangHoa.IDTrangThaiHang < 5 AND GPM_HangHoaTonKho.IDKho = 1";
            return getData(cmd);
        }

        public DataTable getDanhSachHangHoa_Ten_ID()
        {
            string cmd = "SELECT ID,TenHangHoa FROM GPM_HangHoa WHERE DaXoa = 0";
            return getData(cmd);
        }

        public string getTenHH(string ID)
        {
            string cmd = "SELECT TenHangHoa FROM [GPM_HANGHOA] WHERE ID = '" + ID + "'";
            DataTable da =  getData(cmd);
            if (da.Rows.Count != 0)
            {
                return da.Rows[0]["TenHangHoa"].ToString();
            }
            return "-";
        }

        public DataTable getDanhSachHangHoa_ID_2(string ID)
        {
            string cmd = "SELECT GPM_HangHoa.*, GPM_HangHoaTonKho.GiaBan, GPM_HangHoaTonKho.GiaBan1, GPM_HangHoaTonKho.GiaBan2, GPM_HangHoaTonKho.GiaBan3, GPM_HangHoaTonKho.GiaBan4, GPM_HangHoaTonKho.GiaBan5 FROM GPM_HangHoa, GPM_HangHoaTonKho WHERE GPM_HangHoa.DaXoa = 0 AND GPM_HangHoa.ID = GPM_HangHoaTonKho.IDHangHoa AND GPM_HangHoa.IDTrangThaiHang < 5 AND GPM_HangHoaTonKho.IDKho = 1 AND GPM_HangHoa.ID = '" + ID + "'";
            return getData(cmd);
        }

        public DataTable getDanhSachHangHoa_ID(string ID)
        {
            string cmd = "SELECT * FROM [GPM_HANGHOA] WHERE ID = '" + ID + "' AND TenHangHoa is not null";
            return getData(cmd);
        }

        public DataTable getDanhSachHangHoa_IDNhomHang(string IDNhomHang)
        {
            string cmd = "SELECT ID,TenHangHoa FROM [GPM_HANGHOA] WHERE DaXoa = 0 AND (('" + IDNhomHang + "' = -1) OR (IDNhomHang = '" + IDNhomHang + "'))";
            return getData(cmd);
        }

        public DataTable getDanhSachHangHoa_IDNganhHang(string IDNganhHang)
        {
            string cmd = "SELECT GPM_HANGHOA.ID,GPM_HANGHOA.TenHangHoa FROM [GPM_HANGHOA],[GPM_NHOMHANG] WHERE GPM_HANGHOA.DaXoa = 0 AND GPM_HANGHOA.IDNhomHang = GPM_NHOMHANG.ID AND (('" + IDNganhHang + "' = -1) OR (GPM_NHOMHANG.IDNganhHang = '" + IDNganhHang + "'))";
            return getData(cmd);
        }

        public DataTable getDanhSachHangHoa_MaHang(string MaHang)
        {
            string cmd = "SELECT [GPM_HANGHOA].ID,[GPM_HANGHOA].TrongLuong,[GPM_HangHoaTonKho].GiaBan,[GPM_HangHoaTonKho].SoLuongCon FROM [GPM_HangHoaTonKho],[GPM_HANGHOA] WHERE [GPM_HangHoaTonKho].IDHangHoa = [GPM_HANGHOA].ID AND [GPM_HANGHOA].MaHang = '" + MaHang + "' AND [GPM_HANGHOA].TenHangHoa is not null";
            return getData(cmd);
        }

        public DataTable getHangHoa_MaHang(string MaHang)
        {
            string cmd = "SELECT * FROM [GPM_HANGHOA] WHERE MaHang = '" + MaHang + "'";
            return getData(cmd);
        }

        public DataTable getChietHangHoaTonKho_ID(string ID)
        {
            string cmd = "SELECT * FROM [GPM_ChiTietHangHoaTonKho] WHERE ID = '" + ID + "'";
            return getData(cmd);
        }

        public DataTable getDanhSachHangHoa_TonKho_ID(string ID, string IDKho)
        {
            string cmd = "select GPM_HangHoa.ID, GPM_HangHoa.IDDonViTinh, GPM_HangHoa.TrongLuong, GPM_HangHoaTonKho.SoLuongCon from GPM_HangHoa,GPM_HangHoaTonKho where GPM_HangHoa.ID = '" + ID + "' and GPM_HangHoa.ID = GPM_HangHoaTonKho.IDHangHoa and GPM_HangHoaTonKho.IDKho = '" + IDKho + "'";
            return getData(cmd);
        }

        public DataTable KiemTraHangHoa_MaHang(string MaHang)
        {
            string cmd = "SELECT * FROM [GPM_HANGHOA] WHERE MaHang = '" + MaHang + "'";
            return getData(cmd);
        }

        public DataTable KiemTraBarcode(string Barcode)
        {
            string cmd = "SELECT * FROM [GPM_HangHoa_Barcode] WHERE Barcode = '" + Barcode + "'";
            return getData(cmd);
        }

        public int KiemTraHangHoa_Null(string ID)
        {
            string cmd = "SELECT * FROM [GPM_HANGHOA] WHERE ID = '" + ID + "' AND TenHangHoa is null";
            DataTable data = getData(cmd);
            if (data.Rows.Count != 0)
            {
                return 0;
            }
            return -1;
        }

        public static string LayIDTrangThaiHang(string Ten)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_TRANGTHAIHANG] WHERE [TenTrangThai] = '" + Ten + "'";
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

        public static string LayIDTrangThaiBarcode(string Ten)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_TrangThai_Barcode] WHERE [TenTrangThai] = '" + Ten + "'";
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

        public DataTable getDanhSachHangHoa_Full_Barcode()
        {
            dataHangHoa d = new dataHangHoa();
            string cmd = "select GPM_NhomHang.TenNhomHang, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_HangHoa.HeSo, GPM_DonViTinh.TenDonViTinh, GPM_HangSanXuat.TenHangSanXuat, GPM_Thue.TenThue, GPM_HangHoa.IDHangQuyDoi, GPM_NhomDatHang.TenNhom, GPM_HangHoa.GiaMuaTruocThue, GPM_HangHoa.GiaBanTruocThue, GPM_HangHoa.GiaMuaSauThue, GPM_HangHoa.GiaBanSauThue, GPM_HangHoa.GiaBan1, GPM_HangHoa.GiaBan2, GPM_HangHoa.GiaBan3, GPM_HangHoa.GiaBan4, GPM_HangHoa.GiaBan5, GPM_HangHoa.TrongLuong, GPM_HangHoa.HanSuDung, GPM_TrangThaiHang.TenTrangThai as TrangThaiHang, GPM_HangHoa.GhiChu, GPM_TrangThai_Barcode.TenTrangThai as TrangThaiBarcode, GPM_HangHoa_Barcode.Barcode from GPM_NhomHang, GPM_HangHoa, GPM_DonViTinh, GPM_HangSanXuat, GPM_Thue, GPM_NhomDatHang, GPM_TrangThaiHang, GPM_HangHoa_Barcode, GPM_TrangThai_Barcode where GPM_HangHoa.IDNhomHang = GPM_NhomHang.ID and GPM_HangHoa.IDDonViTinh = GPM_DonViTinh.ID and GPM_HangHoa.IDHangSanXuat = GPM_HangSanXuat.ID and GPM_HangHoa.IDThue = GPM_Thue.ID and GPM_HangHoa.IDNhomDatHang = GPM_NhomDatHang.ID and GPM_HangHoa.IDTrangThaiHang = GPM_TrangThaiHang.ID and GPM_HangHoa_Barcode.IDHangHoa = GPM_HangHoa.ID and GPM_HangHoa_Barcode.IDTrangThaiBarcode = GPM_TrangThai_Barcode.ID and GPM_HangHoa.DaXoa = 0 and GPM_HangHoa.IDNhomHang < 5";
            DataTable data = getData(cmd);
            DataTable dataNews = new DataTable();
            dataNews.Columns.Add("NhomHang", typeof(string));
            dataNews.Columns.Add("MaHang", typeof(string));
            dataNews.Columns.Add("TenHangHoa", typeof(string));
            dataNews.Columns.Add("DonViTinh", typeof(string));
            dataNews.Columns.Add("HeSo", typeof(string));
            dataNews.Columns.Add("HangSanXuat", typeof(string));
            dataNews.Columns.Add("Thue", typeof(string));
            dataNews.Columns.Add("HangQuyDoi", typeof(string));
            dataNews.Columns.Add("NhomDatHang", typeof(string));
            dataNews.Columns.Add("GiaMuaTruocThue", typeof(string));
            dataNews.Columns.Add("GiaBanTruocThue", typeof(string));
            dataNews.Columns.Add("GiaMuaSauThue", typeof(string));
            dataNews.Columns.Add("GiaBanSauThue", typeof(string));
            dataNews.Columns.Add("GiaBan1", typeof(string));
            dataNews.Columns.Add("GiaBan2", typeof(string));
            dataNews.Columns.Add("GiaBan3", typeof(string));
            dataNews.Columns.Add("GiaBan4", typeof(string));
            dataNews.Columns.Add("GiaBan5", typeof(string));
            dataNews.Columns.Add("TrongLuong", typeof(string));
            dataNews.Columns.Add("HanSuDung", typeof(string));
            dataNews.Columns.Add("TrangThaiHang", typeof(string));
            dataNews.Columns.Add("GhiChu", typeof(string));
            dataNews.Columns.Add("TrangThaiBarcode", typeof(string));
            dataNews.Columns.Add("Barcode", typeof(string));

            if (data.Rows.Count != 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DataRow dr = data.Rows[i];
                    string TenQuyDoi = d.getTenHH(dr["IDHangQuyDoi"].ToString());
                    string NhomHang = dr["TenNhomHang"].ToString();
                    string MaHang = dr["MaHang"].ToString();
                    string TenHangHoa = dr["TenHangHoa"].ToString();
                    string DonViTinh = dr["TenDonViTinh"].ToString();
                    string HeSo = dr["HeSo"].ToString();
                    string HangSanXuat = dr["TenHangSanXuat"].ToString();
                    string Thue = dr["TenThue"].ToString();
                    string HangQuyDoi = TenQuyDoi;
                    string NhomDatHang = dr["TenNhom"].ToString();
                    string GiaMuaTruocThue = dr["GiaMuaTruocThue"].ToString();
                    string GiaBanTruocThue = dr["GiaBanTruocThue"].ToString();
                    string GiaMuaSauThue = dr["GiaMuaSauThue"].ToString();
                    string GiaBanSauThue = dr["GiaBanSauThue"].ToString();
                    string GiaBan1 = dr["GiaBan1"].ToString();
                    string GiaBan2 = dr["GiaBan2"].ToString();
                    string GiaBan3 = dr["GiaBan3"].ToString();
                    string GiaBan4 = dr["GiaBan4"].ToString();
                    string GiaBan5 = dr["GiaBan5"].ToString();
                    string TrongLuong = dr["TrongLuong"].ToString();
                    string HanSuDung = dr["HanSuDung"].ToString();
                    string TrangThaiHang = dr["TrangThaiHang"].ToString();
                    string GhiChu = dr["GhiChu"].ToString();
                    string TrangThaiBarcode = dr["TrangThaiBarcode"].ToString();
                    string Barcode = dr["Barcode"].ToString();

                    dataNews.Rows.Add(NhomHang, MaHang, TenHangHoa, DonViTinh, HeSo, HangSanXuat, Thue, HangQuyDoi, NhomDatHang,
                        GiaMuaTruocThue, GiaBanTruocThue, GiaMuaSauThue, GiaBanSauThue, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5,
                        TrongLuong, HanSuDung, TrangThaiHang, GhiChu, TrangThaiBarcode, Barcode);
                }
            }
            return dataNews;
        }

        public void XoaHangHoa(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HANGHOA] SET [DAXOA] = 1 WHERE [ID] = @ID " +
                        " UPDATE [GPM_HangHoaTonKho] SET [DAXOA] = 1 WHERE [IDHangHoa] = @ID " +
                        " UPDATE [GPM_HangHoa_Barcode] SET [DAXOA] = 1 WHERE [IDHangHoa] = @ID " +
                        " UPDATE [GPM_HangHoa_GiaTheoSL] SET [DAXOA] = 1 WHERE [IDHangHoa] = @ID " +
                        " UPDATE [GPM_HangHoa_QuyDoi] SET [DAXOA] = 1 WHERE [IDHangHoa] = @ID ";
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

        public void XoaHangHoa_Delete(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "Delete from [GPM_HANGHOA] WHERE [ID] = @ID and TenHangHoa is null";
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

        public void XoaHangHoaQuyDoi_Delete(string IDHangHoa)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "Delete from [GPM_HangHoa_QuyDoi] WHERE [IDHangHoa] = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void XoaHangHoaBarCode_Delete(string IDHangHoa)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "Delete from [GPM_HangHoa_Barcode] WHERE [IDHangHoa] = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }


        public void XoaHangHoaGiaTheoSL_Delete(string IDHangHoa)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "Delete from [GPM_HangHoa_GiaTheoSL] WHERE [IDHangHoa] = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void updateHangHoa(string ID, O_HangHoa hh)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE GPM_HangHoa SET IDNhomHang = @IDNhomHang,MaHang = @MaHang, TenHangHoa = @TenHangHoa, IDDonViTinh = @IDDonViTinh, HeSo = @HeSo, IDHangSanXuat = @IDHangSanXuat, IDThue = @IDThue, IDNhomDatHang = @IDNhomDatHang, GiaMuaTruocThue = @GiaMuaTruocThue, GiaBanTruocThue = @GiaBanTruocThue, GiaMuaSauThue = @GiaMuaSauThue, TrongLuong = @TrongLuong, HanSuDung = @HanSuDung, IDTrangThaiHang = @IDTrangThaiHang, GhiChu = @GhiChu, NgayCapNhat = getDATE() WHERE [ID] = @ID ";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDNhomHang", hh.IDNhomHang);
                        myCommand.Parameters.AddWithValue("@MaHang", hh.MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", hh.TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", hh.IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@HeSo", hh.HeSo);
                        myCommand.Parameters.AddWithValue("@IDHangSanXuat", hh.IDHangSanXuat);
                        myCommand.Parameters.AddWithValue("@IDThue", hh.IDThue);
                        myCommand.Parameters.AddWithValue("@IDNhomDatHang", hh.IDNhomDatHang);
                        myCommand.Parameters.AddWithValue("@GiaMuaTruocThue", hh.GiaMuaTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", hh.GiaBanTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaSauThue", hh.GiaMuaSauThue);
                        myCommand.Parameters.AddWithValue("@TrongLuong", hh.TrongLuong);
                        myCommand.Parameters.AddWithValue("@HanSuDung", hh.HanSuDung);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiHang", hh.IDTrangThaiHang);
                        myCommand.Parameters.AddWithValue("@GhiChu", hh.GhiChu);

                        myCommand.Parameters.AddWithValue("@GiaBan", hh.GiaBanSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBan1", hh.GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", hh.GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", hh.GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", hh.GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", hh.GiaBan5);

                        myCommand.Parameters.AddWithValue("@SoLuongCon", 0);
                        myCommand.Parameters.AddWithValue("@IDKho", 1);

                        myCommand.ExecuteNonQuery();
                    }

                    dtKho dt = new dtKho();
                    DataTable da = dt.LayDanhSachKho_2();
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        strSQL = "INSERT GPM_HangHoaTonKho (IDHangHoa, SoLuongCon, GiaBan, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5, IDKho, NgayCapNhat) VALUES (@ID, @SoLuongCon, @GiaBan, @GiaBan1, @GiaBan2, @GiaBan3, @GiaBan4, @GiaBan5, @IDKho, getDATE())";
                        using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                        {
                            myCommand.Parameters.AddWithValue("@ID", ID);
                            myCommand.Parameters.AddWithValue("@GiaBan", hh.GiaBanSauThue);
                            myCommand.Parameters.AddWithValue("@GiaBan1", hh.GiaBan1);
                            myCommand.Parameters.AddWithValue("@GiaBan2", hh.GiaBan2);
                            myCommand.Parameters.AddWithValue("@GiaBan3", hh.GiaBan3);
                            myCommand.Parameters.AddWithValue("@GiaBan4", hh.GiaBan4);
                            myCommand.Parameters.AddWithValue("@GiaBan5", hh.GiaBan5);

                            myCommand.Parameters.AddWithValue("@SoLuongCon", 0);
                            myCommand.Parameters.AddWithValue("@IDKho", da.Rows[i]["ID"].ToString());
                            myCommand.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void updateHangHoa_update(string ID, O_HangHoa hh)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE GPM_HangHoa SET IDNhomHang = @IDNhomHang,MaHang = @MaHang, TenHangHoa = @TenHangHoa, IDDonViTinh = @IDDonViTinh, HeSo = @HeSo, IDHangSanXuat = @IDHangSanXuat, IDThue = @IDThue, IDNhomDatHang = @IDNhomDatHang, GiaMuaTruocThue = @GiaMuaTruocThue, GiaBanTruocThue = @GiaBanTruocThue, GiaMuaSauThue = @GiaMuaSauThue, TrongLuong = @TrongLuong, HanSuDung = @HanSuDung, IDTrangThaiHang = @IDTrangThaiHang, GhiChu = @GhiChu, NgayCapNhat = getDATE() WHERE [ID] = @ID ";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDNhomHang", hh.IDNhomHang);
                        myCommand.Parameters.AddWithValue("@MaHang", hh.MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", hh.TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", hh.IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@HeSo", hh.HeSo);
                        myCommand.Parameters.AddWithValue("@IDHangSanXuat", hh.IDHangSanXuat);
                        myCommand.Parameters.AddWithValue("@IDThue", hh.IDThue);
                        myCommand.Parameters.AddWithValue("@IDNhomDatHang", hh.IDNhomDatHang);
                        myCommand.Parameters.AddWithValue("@GiaMuaTruocThue", hh.GiaMuaTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", hh.GiaBanTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaSauThue", hh.GiaMuaSauThue);
                        myCommand.Parameters.AddWithValue("@TrongLuong", hh.TrongLuong);
                        myCommand.Parameters.AddWithValue("@HanSuDung", hh.HanSuDung);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiHang", hh.IDTrangThaiHang);
                        myCommand.Parameters.AddWithValue("@GhiChu", hh.GhiChu);

                        myCommand.Parameters.AddWithValue("@GiaBan", hh.GiaBanSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBan1", hh.GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", hh.GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", hh.GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", hh.GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", hh.GiaBan5);

                        myCommand.Parameters.AddWithValue("@SoLuongCon", 0);
                        myCommand.Parameters.AddWithValue("@IDKho", 1);

                        myCommand.ExecuteNonQuery();
                    }

                    dtKho dt = new dtKho();
                    DataTable da = dt.LayDanhSachKho_2();
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        strSQL = "update GPM_HangHoaTonKho set GiaBan = @GiaBan, GiaBan1 = @GiaBan1, GiaBan2 = @GiaBan2, GiaBan3 = @GiaBan3, GiaBan4 = @GiaBan4, GiaBan5 = @GiaBan5 where IDHangHoa = @ID and IDKho = @IDKho";
                        using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                        {
                            myCommand.Parameters.AddWithValue("@ID", ID);
                            myCommand.Parameters.AddWithValue("@GiaBan", hh.GiaBanSauThue);
                            myCommand.Parameters.AddWithValue("@GiaBan1", hh.GiaBan1);
                            myCommand.Parameters.AddWithValue("@GiaBan2", hh.GiaBan2);
                            myCommand.Parameters.AddWithValue("@GiaBan3", hh.GiaBan3);
                            myCommand.Parameters.AddWithValue("@GiaBan4", hh.GiaBan4);
                            myCommand.Parameters.AddWithValue("@GiaBan5", hh.GiaBan5);

                            myCommand.Parameters.AddWithValue("@IDKho", da.Rows[i]["ID"].ToString());
                            myCommand.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void SuaThongTinHangHoa_HangQuyDoi(string MaHang, string IDHangQuyDoi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HANGHOA] SET [IDHangQuyDoi] = @IDHangQuyDoi WHERE [MaHang] = @MaHang AND IDHangQuyDoi = -1";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangQuyDoi", IDHangQuyDoi);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public DataTable getHangHoa_Null()
        {
            string cmd = "select ID, MaHang from GPM_HangHoa where IDNhomHang is null and TenHangHoa is null and IDDonViTinh is null";
            return getData(cmd);
        }

        public object insertHangHoa_Temp()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa] ([MaHang])" +
                                     " OUTPUT INSERTED.ID" +
                                     " VALUES (@MaHang)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@MaHang", "");
                        IDHH = myCommand.ExecuteScalar();
                    }
                    return IDHH;
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public object insertHangHoa_Barcode(string IDHangHoa, string IDTrangThaiBarcode, string Barcode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa_Barcode] ([IDHangHoa],[IDTrangThaiBarcode],[Barcode],[NgayCapnhat])" +
                                     " OUTPUT INSERTED.ID" +
                                     " VALUES (@IDHangHoa,@IDTrangThaiBarcode,@Barcode,getDATE())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiBarcode", IDTrangThaiBarcode);
                        myCommand.Parameters.AddWithValue("@Barcode", Barcode);
                        IDHH = myCommand.ExecuteScalar();
                    }
                    return IDHH;
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public object insertHangHoa_QuyDoi(string IDHangHoa, string IDHangHoaQuyDoi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa_QuyDoi] ([IDHangHoa],[IDHangQuyDoi],[NgayCapNhat]) OUTPUT INSERTED.ID VALUES (@IDHangHoa,@IDHangQuyDoi,getDATE())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDHangQuyDoi", IDHangHoaQuyDoi);
                        IDHH = myCommand.ExecuteScalar();
                    }
                    return IDHH;
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public object insertHangHoa_GiaTheoSL(string IDHangHoa, string SL1, string SL2, string GiaBan)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa_GiaTheoSL] ([IDHangHoa],[SoLuongBD],[SoLuongKT],[GiaBan],[NgayCapNhat]) OUTPUT INSERTED.ID VALUES (@IDHangHoa,@SoLuongBD,@SoLuongKT,@GiaBan,getDATE())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuongBD", SL1);
                        myCommand.Parameters.AddWithValue("@SoLuongKT", SL2);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        IDHH = myCommand.ExecuteScalar();
                    }
                    return IDHH;
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public void updateHangHoa_Barcode(string ID, string IDTrangThaiBarcode, string Barcode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "update GPM_HangHoa_Barcode set IDTrangThaiBarcode = @IDTrangThaiBarcode, Barcode = @Barcode, NgayCapNhat = getDATE() where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiBarcode", IDTrangThaiBarcode);
                        myCommand.Parameters.AddWithValue("@Barcode", Barcode);
                        myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }

        public void updateHangHoa_QuyDoi(string ID, string IDHangQuyDoi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "update GPM_HangHoa_QuyDoi set IDHangQuyDoi = @IDHangQuyDoi, NgayCapNhat = getDATE() where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDHangQuyDoi", IDHangQuyDoi);
                        myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }

        public void updateHangHoa_GiaTheoSL(string ID, string SoLuongBD, string SoLuongKT, string GiaBan)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "update GPM_HangHoa_GiaTheoSL set SoLuongBD = @SoLuongBD, SoLuongKT = @SoLuongKT, GiaBan = @GiaBan, NgayCapNhat = getDATE() where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@SoLuongBD", SoLuongBD);
                        myCommand.Parameters.AddWithValue("@SoLuongKT", SoLuongKT);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }

        public void deleteHangHoa_Barcode(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "delete GPM_HangHoa_Barcode where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }

        public void deleteHangHoaQuyDoi(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "delete GPM_HangHoa_QuyDoi where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }

        public void deleteHangHoa_TheoSL(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "delete GPM_HangHoa_GiaTheoSL where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {

                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }

        

        public DataTable GetListBarCode(object ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_HangHoa_Barcode] WHERE [IDHangHoa] = @IDHangHoa AND [DaXoa] = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.Parameters.AddWithValue("@IDHangHoa", ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        public DataTable GetListHangHoaQuyDoi(object ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HangHoa_QuyDoi].ID,[GPM_HangHoa].MaHang,[GPM_HangHoa].TenHangHoa,[GPM_HangHoa].IDDonViTinh,[GPM_HangHoa].HeSo FROM [GPM_HangHoa],[GPM_HangHoa_QuyDoi] WHERE [GPM_HangHoa].ID = [GPM_HangHoa_QuyDoi].IDHangQuyDoi AND  [GPM_HangHoa_QuyDoi].IDHangHoa = @IDHangHoa AND [GPM_HangHoa_QuyDoi].DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.Parameters.AddWithValue("@IDHangHoa", ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        public DataTable GetListHangHoa_GiaTheoSL(object ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM GPM_HangHoa_GiaTheoSL WHERE IDHangHoa = @IDHangHoa AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.Parameters.AddWithValue("@IDHangHoa", ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        public string LayTenHangHoa_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenHangHoa FROM [GPM_HangHoa] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["TenHangHoa"].ToString();
                }
            }
        }

        public void CapNhatBarCode(int ID, object IDHangHoa, string IDTrangThaiBarcode, string BarCode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoa_Barcode] SET [IDHangHoa] = @IDHangHoa,[IDTrangThaiBarcode] = @IDTrangThaiBarcode, [BarCode] = @BarCode,[NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@BarCode", BarCode);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiBarcode", IDTrangThaiBarcode);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public bool KiemTraMaHang(string IDHH,string MaHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaHang FROM [GPM_HangHoa] WHERE ID != '" + IDHH + "' AND  [MaHang] = '" + MaHang + "'";
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

        public void ThemBarCode(object IDHangHoa, string IDTrangThaiBarcode, string BarCode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa_Barcode] ([IDHangHoa],[IDTrangThaiBarcode],[Barcode],[NgayCapNhat])" +
                             " VALUES(@IDHangHoa,@IDTrangThaiBarcode, @BarCode, getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiBarcode", IDTrangThaiBarcode);
                        myCommand.Parameters.AddWithValue("@BarCode", BarCode);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public static string LayIDHangHoa(string TenHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_HANGHOA] WHERE [TenHangHoa] = N'" + TenHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    else return -1 + "";
                }
            }
        }

        public static string LayIDHangHoa_MaHang(string MaHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_HANGHOA] WHERE [MaHang] = N'" + MaHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    else return -1 + "";
                }
            }
        }

        public void XoaBarCode(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "DELETE FROM [GPM_HangHoa_Barcode] WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
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

        public static int LayID_Count(string IDDV)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT COUNT(MaHang) as CountMH FROM [GPM_HANGHOA] WHERE MaHang like '" + IDDV + "%'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["CountMH"].ToString());
                    }
                    else return -1;
                }
            }
        }



        public void ThemHangVaoTonKho(string IDKho, string IDHangHoa, string SoLuongCon, string GiaBan, string GiaBan1, string GiaBan2, string GiaBan3, string GiaBan4, string GiaBan5)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoaTonKho] ([IDKho],[IDHangHoa],[SoLuongCon],[GiaBan],[GiaBan1],[GiaBan2],[GiaBan3],[GiaBan4],[GiaBan5],[NgayCapNhat]) VALUES (@IDKho,@IDHangHoa,@SoLuongCon,@GiaBan,@GiaBan1,@GiaBan2,@GiaBan3,@GiaBan4,@GiaBan5,getDATE())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@GiaBan1", GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", GiaBan5);
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
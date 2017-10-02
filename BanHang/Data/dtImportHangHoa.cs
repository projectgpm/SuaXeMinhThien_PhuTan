using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtImportHangHoa
    {
        public void XoaDuLieuTemp()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_HangHoa_Import]";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void XoaDuLieuTemp_ID(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_HangHoa_Import] WHERE [ID] = " + ID;
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public DataTable DanhSachHangHoa_Import_Temp()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HangHoa_Import]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable KiemTraHangHoa_Import_Temp(string MaHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HangHoa_Import] WHERE MaHang ='" + MaHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public void insertHangHoa_temp(string IDNhomHang, string MaHang, string TenHangHoa, string IDDonViTinh, string HeSo, string IDHangSanXuat, string IDThue, string HangQuyDoi, string IDNhomDatHang, string GiaMuaTruocThue, string GiaBanTruocThue, string GiaMuaSauThue, string GiaBanSauThue, string GiaBan1, string GiaBan2, string GiaBan3, string GiaBan4, string GiaBan5, string TrongLuong, string HanSuDung, string IDTrangThaiHang, string GhiChu, string IDTrangThaiBarcode, string Barcode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa_Import] ([IDNhomHang], [MaHang], [TenHangHoa], [IDDonViTinh], [HeSo], [IDHangSanXuat], [IDThue],[HangQuyDoi],[IDNhomDatHang],[GiaMuaTruocThue],[GiaBanTruocThue],[GiaMuaSauThue],[GiaBanSauThue], [GiaBan1], [GiaBan2], [GiaBan3], [GiaBan4], [GiaBan5], [TrongLuong], [HanSuDung], [IDTrangThaiHang], [GhiChu],[IDTrangThaiBarcode],[Barcode])" +
                                     " VALUES (@IDNhomHang,@MaHang,@TenHangHoa,@IDDonViTinh, @HeSo,@IDHangSanXuat,@IDThue,@HangQuyDoi,@IDNhomDatHang,@GiaMuaTruocThue,@GiaBanTruocThue,@GiaMuaSauThue,@GiaBanSauThue, @GiaBan1,@GiaBan2, @GiaBan3,@GiaBan4,@GiaBan5,@TrongLuong,@HanSuDung,@IDTrangThaiHang,@GhiChu,@IDTrangThaiBarcode,@Barcode)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNhomHang", IDNhomHang);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@HeSo", HeSo);
                        myCommand.Parameters.AddWithValue("@IDHangSanXuat", IDHangSanXuat);
                        myCommand.Parameters.AddWithValue("@IDThue", IDThue);
                        myCommand.Parameters.AddWithValue("@HangQuyDoi", HangQuyDoi);
                        myCommand.Parameters.AddWithValue("@IDNhomDatHang", IDNhomDatHang);
                        myCommand.Parameters.AddWithValue("@GiaMuaTruocThue", GiaMuaTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", GiaBanTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaSauThue", GiaMuaSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBanSauThue", GiaBanSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBan1", GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", GiaBan5);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@HanSuDung", HanSuDung);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiHang", IDTrangThaiHang);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiBarcode", IDTrangThaiBarcode);
                        myCommand.Parameters.AddWithValue("@Barcode", Barcode);
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
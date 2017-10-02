using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtGiaTheoVung
    {
        
        public DataTable DanhSachHangHoa()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HangHoa].MaHang,[GPM_HangHoa].IDDonViTinh,[GPM_HangHoa].ID,[GPM_HangHoa].TenHangHoa,[GPM_HangHoa].GiaBan1,[GPM_HangHoaTonKho].GiaBan FROM [GPM_HangHoa],[GPM_HangHoaTonKho]  WHERE [GPM_HangHoa].ID = [GPM_HangHoaTonKho].IDHangHoa AND [GPM_HangHoa].DaXoa = 0 AND IDKho = 1";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static void Update_GiaTheoVung(int IDKho, int IDHangHoa, float GiaBan)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangHoaTonKho] SET [GiaBan] = @GiaBan WHERE [IDHangHoa] = @IDHangHoa AND [IDKho]=@IDKho";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
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
        public static void CapNhat_GiaTheoVung(string ID, string IDKho, string GiaBan, string GiaBan1, string GiaBan2, string GiaBan3, string GiaBan4, string GiaBan5)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangHoaTonKho] SET [GiaBan2] = @GiaBan2,[GiaBan3] = @GiaBan3,[GiaBan4]=@GiaBan4,[GiaBan5] = @GiaBan5,[GiaBan1] = @GiaBan1,[GiaBan] = @GiaBan WHERE [IDKho] = @IDKho AND [ID]= @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@GiaBan1", GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", GiaBan5);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public  void CapNhat_GiaTheoVung_NhiuKho(string IDHangHoa, string IDKho, string GiaBan,string GiaThayDoi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangHoaTonKho] SET [" + GiaThayDoi + "] =  @GiaBan   WHERE [IDKho] = @IDKho AND [IDHangHoa]= @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public DataTable DanhSachHangHoa_IDKho(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HangHoa].IDDonViTinh,[GPM_HangHoa].ID as IDHangHoa,[GPM_HangHoa].MaHang,[GPM_HangHoaTonKho].*,[GPM_HangHoa].TenHangHoa FROM [GPM_HangHoa],[GPM_HangHoaTonKho]  WHERE [GPM_HangHoa].ID = [GPM_HangHoaTonKho].IDHangHoa AND [GPM_HangHoa].DaXoa = 0 AND IDKho  = " + IDKho;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachKho()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_Kho] WHERE [DAXOA] = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachKho_IDVung(int IDVung)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_Kho] WHERE [DAXOA] = 0 AND ('" + IDVung + "' = -1 OR [IDVung] = '" + IDVung + "')";
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
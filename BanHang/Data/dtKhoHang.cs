using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace BanHang.Data
{
    public class dtKhoHang
    {
        public DataTable LayDanhSachHangTrongKho(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HangHoaTonKho].*,[GPM_HangHoa].MaHang,[GPM_HangHoa].IDDonViTinh FROM [GPM_HangHoaTonKho],[GPM_HangHoa] WHERE [GPM_HangHoaTonKho].DaXoa = 0 AND [GPM_HangHoa].ID =  [GPM_HangHoaTonKho].IDHangHoa  AND [GPM_HangHoaTonKho].IDKho = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        //public void ThemHang(int IDHangHoa, int SoLuongCon, DateTime NgayCapNhat)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string cmdText = "INSERT INTO [GPM_HangHoaTonKho] ([IDKho],[IDHangHoa], [SoLuongCon], [NgayCapNhat]) VALUES (@IDKho,@IDHangHoa,@SoLuongCon, @NgayCapNhat)";
        //            using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
        //                myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
        //                myCommand.Parameters.AddWithValue("@NgayCapNhat", NgayCapNhat);
        //                myCommand.Parameters.AddWithValue("@IDKho", IDKho);
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

        //public void ThemHang_Full(int IDHangHoa, int SoLuongCon, float GiaBan,int DaXoa, int IDKho)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string cmdText = "INSERT INTO [GPM_HangHoaTonKho] ([IDHangHoa],[SoLuongCon], [GiaBan], [IDKho],[DaXoa],[NgayCapNhat]) VALUES (@IDHangHoa,@SoLuongCon,@GiaBan, @IDKho,@DaXoa,getDATE())";
        //            using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
        //                myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
        //                myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
        //                myCommand.Parameters.AddWithValue("@IDKho", IDKho);
        //                myCommand.Parameters.AddWithValue("@DaXoa", DaXoa);
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
        //public void XoaHang_IDHangHoa(int IDHangHoa)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string strSQL = "UPDATE [GPM_HangHoaTonKho] SET [DAXOA] = 1 WHERE [IDHangHoa] = @IDHangHoa";
        //            using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
        //        }
        //    }
        //}

        //public void Update_HangHoaTonKho(int ID, int DaXoa, float GiaBan)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string strSQL = "UPDATE [GPM_HangHoaTonKho] SET [DaXoa] = @DaXoa,[GiaBan] = @GiaBan WHERE [IDHangHoa] = @IDHangHoa";
        //            using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@IDHangHoa", ID);
        //                myCommand.Parameters.AddWithValue("@DaXoa", DaXoa);
        //                myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
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
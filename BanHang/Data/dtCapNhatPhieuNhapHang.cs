using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtCapNhatPhieuNhapHang
    {
        public void CapNhatChiTietDonHang(string ID, int ThucTe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangThuMua_ChiTiet] SET [ThucTe] = '" + ThucTe + "',[ChenhLech] = [SoLuong]  - '" + ThucTe + "' WHERE  [ID] = '" + ID + "' ";
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
        public static int SoLuongThucTeCu(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ThucTe FROM [GPM_DuyetHangThuMua_ChiTiet] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["ThucTe"].ToString());
                    }
                    else return 0;
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
        public DataTable LayDanhSachDonHangDuyetTrong2Ngay(DateTime NgayHomNay, int SoNgay)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangThuMua] WHERE [IDDonHang] is not null AND [NgayDuyet] >= '" + NgayHomNay.AddDays(-SoNgay).ToString("yyyy-MM-dd hh:mm:ss tt") + "' AND [NgayDuyet] <= '" + NgayHomNay.ToString("yyyy-MM-dd hh:mm:ss tt") + "'";
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
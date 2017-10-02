using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtThongKeDonHangChiNhanh
    {
        public DataTable DanhSachThongKe(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HangHoaTonKho].SoLuongCon AS TonKho,[GPM_DonHangChiNhanh_ChiTiet].IDDonViTinh,[GPM_DonHangChiNhanh_ChiTiet].MaHang,[GPM_DonHangChiNhanh_ChiTiet].IDHangHoa,SUM([GPM_DonHangChiNhanh_ChiTiet].SoLuong) AS SoLuongDat FROM [GPM_DonHangChiNhanh],[GPM_DonHangChiNhanh_ChiTiet],[GPM_HangHoaTonKho] WHERE [GPM_DonHangChiNhanh].GiamSatDuyet = 1   AND [GPM_DonHangChiNhanh].TrangThai = 0   AND [GPM_DonHangChiNhanh_ChiTiet].IDDonHangChiNhanh = [GPM_DonHangChiNhanh].ID AND [GPM_HangHoaTonKho].IDHangHoa = [GPM_DonHangChiNhanh_ChiTiet].IDHangHoa AND  [GPM_HangHoaTonKho].IDKho = '" + IDKho + "' GROUP BY  [GPM_DonHangChiNhanh_ChiTiet].MaHang , [GPM_DonHangChiNhanh_ChiTiet].IDHangHoa,[GPM_DonHangChiNhanh_ChiTiet].IDDonViTinh,[GPM_HangHoaTonKho].SoLuongCon";
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
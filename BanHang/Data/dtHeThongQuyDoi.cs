using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtHeThongQuyDoi
    {
        public  int LayHeSoHangHoa(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT HeSo FROM [GPM_HangHoa] WHERE [ID] = '" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["HeSo"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public DataTable DanhSachHangHoaQuiDoi(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HangHoa].TenHangHoa,[GPM_HangHoa].MaHang,[GPM_DonViTinh].TenDonViTinh,[GPM_HangHoa].ID FROM [GPM_HangHoa_QuyDoi],[GPM_HangHoa],[GPM_DonViTinh] WHERE [GPM_HangHoa_QuyDoi].IDHangQuyDoi = [GPM_HangHoa].ID AND [GPM_DonViTinh].ID = [GPM_HangHoa].IDDonViTinh AND [GPM_HangHoa_QuyDoi].DaXoa = 0 AND [GPM_HangHoa_QuyDoi].IDHangHoa = '" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachHangCanQuiDoi(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_KiemKho] WHERE IDKho is not null AND ('" + IDKho + "' = 1 OR [IDKho] = '" + IDKho + "') ORDER BY [ID] DESC ";
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using BanHang.Data;
using System.Data;

namespace BanHang
{
    public partial class RootMaster : System.Web.UI.MasterPage
    {
        dtMasterPage data = new dtMasterPage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //XuLyThayDoiGiaTheoGio();
              //  XuLyDonHangChiNhanh();
              //  HuyDonHangThuMua();
                //if (!IsPostBack)
                //{
                //    data = new dtMasterPage();
                //    DataTable dbt = data.DanhSachMemuDuocHienThi(Session["IDNhom"].ToString());
                //    if (dbt.Rows.Count > 0)
                //    {
                //        foreach (DataRow dr in dbt.Rows)
                //        {
                //            string name = dr["Name"].ToString();
                //            RibbonItemBase kt = getbyName(name, ribbonMenu);
                //            kt.Visible = true;
                //        }
                //    }
                lblChao.Text = "Xin Chào: " + Session["TenDangNhap"].ToString();
                ASPxLabel2.Text = Server.HtmlDecode("Copyrights &copy;") + DateTime.Now.Year + Server.HtmlDecode(". All Rights Reserved. Designed by GPM.VN");
                //}
            }
        }
        protected RibbonItemBase getbyName(string name, ASPxRibbon ribbon)
        {
            foreach (RibbonTab tab in ribbon.Tabs)
            {
                foreach (RibbonGroup group in tab.Groups)
                {
                    foreach (RibbonItemBase item in group.Items)
                    {
                        if (item.Name.Trim() == name.Trim())
                            return item;

                        RibbonItemBase subItem = getbyNameSubItem(name, item);
                        if (subItem != null)
                            return subItem;
                    }
                }
            }
            return null;
        }
        private RibbonItemBase getbyNameSubItem(string name, RibbonItemBase item)
        {
            if (item is RibbonDropDownButtonItem)
                foreach (RibbonDropDownButtonItem subItem in ((RibbonDropDownButtonItem)item).Items)
                {
                    if (subItem.Name.Trim() == name.Trim())
                    {
                        return subItem;
                    }
                    var subItemResult = getbyNameSubItem(name, subItem);
                    if (subItemResult != null)
                        return subItemResult;
                }
            return null;
        }
    }
}
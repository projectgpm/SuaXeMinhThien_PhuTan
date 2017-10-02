using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class XoaDuLieu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 45) == 1)
                //{
                    LoadDanhSacDonViTinh();
                    LoadDanhSachHangHoa();
                    LoadDanhSachKhachHang();
                    LoadDanhSachNganhHang();
                    LoadDanhSachNhaCungCap();
                    LoadDanhSachNhomHang();
                    LoadDanhSachVung();
                    LoadDanhSachKho();
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}
               
            }
        }
        dtDuLieu data = new dtDuLieu();
        private int VisibleIndexHere;

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            data = new dtDuLieu();
            data.Xoa_ALL_Temp();
            ProgressBar.Position = 100;
            lblThongBao.Text = "Xóa Dữ Liệu rác thành công";

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Xóa dữ liệu temp:", Session["IDKho"].ToString(), "Hệ Thống", "Xóa"); 
        }
        public void LoadDanhSachHangHoa()
        {
            data = new dtDuLieu();
            gridHangHoa.DataSource = data.LayDanhSachHangHoa();
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "KhoiPhuc")
            {
                int ID = Int32.Parse(gridHangHoa.GetRowValues(VisibleIndexHere, gridHangHoa.KeyFieldName).ToString());
                data = new dtDuLieu();
                data.KhoiHangHoa(ID);
                LoadDanhSachHangHoa();
            }
            if (e.ButtonID == "Xoa")
            {
                int ID = Int32.Parse(gridHangHoa.GetRowValues(VisibleIndexHere, gridHangHoa.KeyFieldName).ToString());
                data = new dtDuLieu();
                
                data.XoaHangHoa(ID);
                LoadDanhSachHangHoa();
            }
        }


       

        protected void gridHangHoa_btnXoaTatCa_Click(object sender, EventArgs e)
        {
            data = new dtDuLieu();
            data.XoaHangHoa_ALL();
            LoadDanhSachHangHoa();
        }

        public void LoadDanhSacDonViTinh()
        {
            data = new dtDuLieu();
            grid_DonViTinh.DataSource = data.LayDanhSachDonViTinh();
            grid_DonViTinh.DataBind();
        }


        protected void grid_DonViTinh_btnXoaTatCa_Click(object sender, EventArgs e)
        {
            data = new dtDuLieu();
            data.XoaDonViTinh_ALL();
            LoadDanhSacDonViTinh();
        }

        protected void grid_DonViTinh_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "grid_DonViTinh_KhoiPhuc")
            {
                int ID = Int32.Parse(grid_DonViTinh.GetRowValues(VisibleIndexHere, grid_DonViTinh.KeyFieldName).ToString());
                data = new dtDuLieu();
                data.KhoiPhucDonViTinh(ID);
                LoadDanhSacDonViTinh();
            }
            if (e.ButtonID == "grid_DonViTinh_Xoa")
            {
                int ID = Int32.Parse(grid_DonViTinh.GetRowValues(VisibleIndexHere, grid_DonViTinh.KeyFieldName).ToString());
                data = new dtDuLieu();
                data.XoaDonViTinh_ID(ID);
                LoadDanhSacDonViTinh();
            }
        }

        public void LoadDanhSachKhachHang()
        {
            data = new dtDuLieu();

            gridKhachHang.DataSource = data.LayDanhSachKhachHang();
            gridKhachHang.DataBind();
        }

        protected void gridKhachHang_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "gridKhachHang_KhoiPhuc")
            {
                int ID = Int32.Parse(gridKhachHang.GetRowValues(VisibleIndexHere, gridKhachHang.KeyFieldName).ToString());
                data = new dtDuLieu();
                data.KhoiPhucKhachHang(ID);
                LoadDanhSachKhachHang();
            }
            if (e.ButtonID == "gridKhachHang_Xoa")
            {
                int ID = Int32.Parse(gridKhachHang.GetRowValues(VisibleIndexHere, gridKhachHang.KeyFieldName).ToString());
                data = new dtDuLieu();
                data.XoaKhachHang(ID);
                LoadDanhSachKhachHang();
            }
        }
       
        protected void gridKhachHang_btnXoaTatCa_Click(object sender, EventArgs e)
        {
            data = new dtDuLieu();
            data.XoaKhachHang_ALL();
            LoadDanhSachKhachHang();
        }
        public void LoadDanhSachNganhHang()
        {
            data = new dtDuLieu();

            gridNganhHang.DataSource = data.LayDanhSachNganhHang();
            gridNganhHang.DataBind();
        }
        protected void gridNganhHang_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "gridNganhHang_KhoiPhuc")
            {
                int ID = Int32.Parse(gridNganhHang.GetRowValues(VisibleIndexHere, gridNganhHang.KeyFieldName).ToString());
                data = new dtDuLieu();
                data.KhoiPhucNganhHang(ID);
                LoadDanhSachNganhHang();
            }
            if (e.ButtonID == "gridNganhHang_Xoa")
            {
                int ID = Int32.Parse(gridNganhHang.GetRowValues(VisibleIndexHere, gridNganhHang.KeyFieldName).ToString());
                data = new dtDuLieu();
                data.XoaNganhHang(ID);
                LoadDanhSachNganhHang();
            }
        }

        protected void gridNganhHang_btnXoaTatCa_Click(object sender, EventArgs e)
        {
            data = new dtDuLieu();
            data.XoaNganhHang_ALL();
            LoadDanhSachNganhHang();
        }
        
       public void LoadDanhSachNhaCungCap()
        {
            data = new dtDuLieu();
            gridNhaCungCap.DataSource = data.LayDanhSachNhaCungCap();
            gridNhaCungCap.DataBind();
        }

       protected void gridNhaCungCap_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
       {
           if (e.ButtonID == "gridNhaCungCap_KhoiPhuc")
           {
               int ID = Int32.Parse(gridNhaCungCap.GetRowValues(VisibleIndexHere, gridNhaCungCap.KeyFieldName).ToString());
               data = new dtDuLieu();
               data.KhoiPhucNhaCungCap(ID);
               LoadDanhSachNhaCungCap();
           }
           if (e.ButtonID == "gridNhaCungCap_Xoa")
           {
               int ID = Int32.Parse(gridNhaCungCap.GetRowValues(VisibleIndexHere, gridNhaCungCap.KeyFieldName).ToString());
               data = new dtDuLieu();
               data.XoaNhaCungCap(ID);
               LoadDanhSachNhaCungCap();
           }
       }

       protected void gridNhaCungCap_btnXoaTatCa_Click(object sender, EventArgs e)
       {
           data = new dtDuLieu();
           data.XoaNhaCungCap_ALL();
           LoadDanhSachNhaCungCap();
       }

       public void LoadDanhSachNhomHang()
       {
           data = new dtDuLieu();
           gridNhomHang.DataSource = data.LayDanhSachNhomHang();
           gridNhomHang.DataBind();
       }
       protected void gridNhomHang_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
       {
           if (e.ButtonID == "gridNhomHang_KhoiPhuc")
           {
               int ID = Int32.Parse(gridNhomHang.GetRowValues(VisibleIndexHere, gridNhomHang.KeyFieldName).ToString());
               data = new dtDuLieu();
               data.KhoiPhucNhomHang(ID);
               LoadDanhSachNhomHang();
           }
           if (e.ButtonID == "gridNhomHang_Xoa")
           {
               int ID = Int32.Parse(gridNhomHang.GetRowValues(VisibleIndexHere, gridNhomHang.KeyFieldName).ToString());
               data = new dtDuLieu();
               data.XoaNhomHang(ID);
               LoadDanhSachNhomHang();
           }
       }
      
       protected void gridNhomHang_btnXoaTatCa_Click(object sender, EventArgs e)
       {
           data = new dtDuLieu();
           data.XoaNhomHang_ALL();
           LoadDanhSachNhomHang();
       }
       protected void btnSaoLuuDuLieu_Click(object sender, EventArgs e)
       {
           dtSetting dt = new dtSetting();
           DataTable db = dt.LayTenDatabase();
           DataRow dr = db.Rows[0];
           string Name = dr["DatabaseName"].ToString();
           string CD = Server.MapPath("~/Uploads/");
           string TenFile = DateTime.Now.ToString("ddMMyyyy") + "_" + Name + ".Bak";
           data = new dtDuLieu();
            //FileInfo newFile = new FileInfo(Server.MapPath("~/Uploads/" + TenFile));
            //newFile.Delete();
            string[] Files = Directory.GetFiles(Server.MapPath("~/Uploads/"));

            foreach (string file in Files)
            {
               File.Delete(file);
            }
            data.SaoLuuCSDL(CD, Name);
  
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + TenFile + ";");
            response.TransmitFile(Server.MapPath("~/Uploads/" + TenFile));
            response.Flush();
            response.End();
       }
       private string strFileExcel = "";
       protected void btnUpload_Click(object sender, EventArgs e)
       {
           if (string.IsNullOrEmpty(UploadRestor.FileName))
           {
               Response.Write("<script language='JavaScript'> alert('Chưa chọn file.'); </script>");
               return;
           }
           UploadFile();

           dtSetting dt = new dtSetting();
           DataTable db = dt.LayTenDatabase();
           DataRow dr = db.Rows[0];
           string Name = dr["DatabaseName"].ToString();
           string File = Server.MapPath("~/Uploads/") + strFileExcel;
          
           try
           {
               data = new dtDuLieu();
               ProgressBarPhucHoi.Position = 100;
               data.PhucHoiCSDL(File, Name);
               lblThongBaoPhucHoi.Text = "Phục hồi cơ sở dữ liệu thành công!";
           }
           catch (Exception ex)
           {
               ProgressBarPhucHoi.Position = 99;
               lblThongBaoPhucHoi.Text = "Lỗi xảy ra Trong quá trình phục hồi dữ liệu !" + ex.ToString();
           }  
       }
       private void UploadFile()
       {
           string folder = null;
           string filein = null;
           string ThangNam = null;

           ThangNam = string.Concat(System.DateTime.Now.Month.ToString(), System.DateTime.Now.Year.ToString());
           if (!Directory.Exists(Server.MapPath("~/Uploads/") + ThangNam))
           {
               Directory.CreateDirectory(Server.MapPath("~/Uploads/") + ThangNam);
           }
           folder = Server.MapPath("~/Uploads/" + ThangNam + "/");

           if ( UploadRestor.HasFile)
           {
               strFileExcel = Guid.NewGuid().ToString();
               string theExtension = Path.GetExtension(UploadRestor.FileName);
               strFileExcel += theExtension;

               filein = folder + strFileExcel;
               UploadRestor.SaveAs(filein);
               strFileExcel = ThangNam + "/" + strFileExcel;
           }
       }



       public void LoadDanhSachVung()
       {
           data = new dtDuLieu();
           gridVung.DataSource = data.LayDanhSachVung();
           gridVung.DataBind();
       }
       protected void gridVung_btnVungXoaTatCa_Click(object sender, EventArgs e)
       {
           data = new dtDuLieu();
           data.XoaVung_ALL();
           LoadDanhSachVung();
       }

       protected void gridVung_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
       {
           if (e.ButtonID == "gridVung_KhoiPhuc")
           {
               int ID = Int32.Parse(gridVung.GetRowValues(VisibleIndexHere, gridVung.KeyFieldName).ToString());
               data = new dtDuLieu();
               data.KhoiPhucVung(ID);
               LoadDanhSachVung();
           }
           if (e.ButtonID == "gridVung_Xoa")
           {
               int ID = Int32.Parse(gridVung.GetRowValues(VisibleIndexHere, gridVung.KeyFieldName).ToString());
               data = new dtDuLieu();
               data.XoaVung(ID);
               LoadDanhSachVung();
           }
       }



       public void LoadDanhSachKho()
       {
           data = new dtDuLieu();
           gridKho.DataSource = data.LayDanhSachKho();
           gridKho.DataBind();
       }
       protected void gridKho_btnXoa_Click(object sender, EventArgs e)
       {
           data = new dtDuLieu();
           data.XoaKho_ALL();
           LoadDanhSachKho();
       }

       protected void gridKho_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
       {
           if (e.ButtonID == "gridKho_KhoiPhuc")
           {
               int ID = Int32.Parse(gridKho.GetRowValues(VisibleIndexHere, gridKho.KeyFieldName).ToString());
               data = new dtDuLieu();
               data.KhoiPhucKho(ID);
               LoadDanhSachKho();
           }
           if (e.ButtonID == "gridKho_Xoa")
           {
               int ID = Int32.Parse(gridKho.GetRowValues(VisibleIndexHere, gridKho.KeyFieldName).ToString());
               data = new dtDuLieu();
               data.XoaKho(ID);
               LoadDanhSachKho();
           }
       }

      
     
    }
}
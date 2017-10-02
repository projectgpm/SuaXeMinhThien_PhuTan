<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DanhSachPhieuChuyenKho.aspx.cs" Inherits="BanHang.DanhSachPhieuChuyenKho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
       <%--popup chi tiet don hang--%>
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("ChiTietPhieuChuyenKho.aspx?IDPhieuChuyenKho=" + key);
             popup.ShowAtElement();
             // alert(key);
         };
         
    </script>
       <dx:ASPxFormLayout ID="form1" runat="server">
           <Items>
               <dx:LayoutGroup Caption="Chức năng" ColCount="4" Width="40%">
                   <Items>
                       <dx:LayoutItem Caption="">
                           <LayoutItemNestedControlCollection>
                               <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                   <dx:ASPxButton ID="btnThemPhieuMoi" runat="server" Text="Thêm phiếu mới" PostBackUrl="PhieuChuyenKho.aspx">
                                       <Image IconID="actions_addfile_16x16">
                                       </Image>
                                    </dx:ASPxButton>
                               </dx:LayoutItemNestedControlContainer>
                           </LayoutItemNestedControlCollection>
                       </dx:LayoutItem>
                       <dx:LayoutItem Caption="">
                           <LayoutItemNestedControlCollection>
                               <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                   <dx:ASPxButton ID="btnDanhSachDangChuyen" runat="server" Text="Đang chuyển" PostBackUrl="DanhSachPhieuChuyenKho_DangChuyen.aspx">
                                       <Image IconID="businessobjects_boorderitem_16x16">
                                       </Image>
                                   </dx:ASPxButton>
                               </dx:LayoutItemNestedControlContainer>
                           </LayoutItemNestedControlCollection>
                       </dx:LayoutItem>
                       <dx:LayoutItem Caption="">
                           <LayoutItemNestedControlCollection>
                               <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                   <dx:ASPxButton ID="btnDanhSachHoanThanh" runat="server" Text="Đã chuyển" PostBackUrl="DanhSachPhieuChuyenKho_HoanThanh.aspx">
                                       <Image IconID="businessobjects_boproductgroup_16x16">
                                       </Image>
                                   </dx:ASPxButton>
                               </dx:LayoutItemNestedControlContainer>
                           </LayoutItemNestedControlCollection>
                       </dx:LayoutItem>
                       <dx:LayoutItem Caption="">
                           <LayoutItemNestedControlCollection>
                               <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                   <dx:ASPxButton ID="btnPhieuHuy" runat="server" Text="Phiếu hủy" PostBackUrl="DanhSachPhieuChuyenKho_Huy.aspx">
                                       <Image IconID="actions_deletelist2_16x16">
                                       </Image>
                                   </dx:ASPxButton>
                               </dx:LayoutItemNestedControlContainer>
                           </LayoutItemNestedControlCollection>
                       </dx:LayoutItem>
                   </Items>
               </dx:LayoutGroup>
           </Items>
       </dx:ASPxFormLayout>
    <br />
       <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
           <Items>
               
               <dx:LayoutGroup Caption="Danh sách">
                   <Items>
                       <dx:LayoutItem Caption="">
                           <LayoutItemNestedControlCollection>
                               <dx:LayoutItemNestedControlContainer runat="server">
                                   <dx:ASPxGridView ID="gridPhieuChuyenKho" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID" OnRowDeleting="gridPhieuChuyenKho_RowDeleting">
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings ShowFilterRow="True" ShowTitlePanel="True" />


                                        <SettingsBehavior ConfirmDelete="True" />
                                        <SettingsCommandButton>
                                            <ShowAdaptiveDetailButton ButtonType="Image">
                                            </ShowAdaptiveDetailButton>
                                            <HideAdaptiveDetailButton ButtonType="Image">
                                            </HideAdaptiveDetailButton>
                                            <DeleteButton ButtonType="Image" RenderMode="Image">
                                                <Image IconID="actions_cancel_16x16" ToolTip="Xóa đơn hàng">
                                                </Image>
                                            </DeleteButton>
                                        </SettingsCommandButton>
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" Title="DANH SÁCH PHIẾU CHUYỂN KHO" ConfirmDelete="Bạn chắc chắn muốn xóa?"/>
                                        <Columns>
                                            <dx:GridViewDataComboBoxColumn Caption="Kho nhập" VisibleIndex="4" FieldName="IDKhoNhap">
                                                <PropertiesComboBox DataSourceID="sqlKho" TextField="TenCuaHang" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataDateColumn Caption="Ngày xuất" VisibleIndex="12" FieldName="NgayXuat">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="0">
                
                                                <EditCellStyle HorizontalAlign="Center">
                                                </EditCellStyle>
                
                                                <DataItemTemplate>
                                                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                                                </DataItemTemplate>
                                                <HeaderStyle Wrap="True" />
                                            </dx:GridViewDataButtonEditColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Kho xuất" FieldName="IDKhoXuat" VisibleIndex="3">
                                                <PropertiesComboBox DataSourceID="sqlKho" TextField="TenCuaHang" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Cửa hàng kho xuất" FieldName="IDCuaHangTruong1" VisibleIndex="7">
                                                <PropertiesComboBox DataSourceID="sqlDNhanVien" TextField="TenNguoiDung" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataSpinEditColumn Caption="Số mặt hàng" FieldName="SoMatHang" VisibleIndex="14">
                                                <PropertiesSpinEdit DisplayFormatString="g">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataSpinEditColumn Caption="Trọng lượng" FieldName="TrongLuong" VisibleIndex="15">
                                                <PropertiesSpinEdit DisplayFormatString="{0:n} KG" NumberFormat="Custom">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng thái chuyển hàng" FieldName="IDTrangThai" VisibleIndex="2">
                                                <PropertiesComboBox DataSourceID="sqlTrangThaiChuyenHang" TextField="TenTrangThai" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataDateColumn Caption="Ngày nhập" FieldName="NgayNhap" VisibleIndex="13">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Người lập phiếu" FieldName="IDNhanVienLap" VisibleIndex="5">
                                                <PropertiesComboBox DataSourceID="sqlDNhanVien" TextField="TenNguoiDung" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataDateColumn Caption="Ngày lập" FieldName="NgayLap" VisibleIndex="6">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Giám sát kho xuất" FieldName="IDGiamSat1" VisibleIndex="8">
                                                <PropertiesComboBox DataSourceID="sqlDNhanVien" TextField="TenNguoiDung" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Cửu hàng kho nhận" FieldName="IDCuaHangTruong2" VisibleIndex="9">
                                                <PropertiesComboBox DataSourceID="sqlDNhanVien" TextField="TenNguoiDung" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Giám sát kho nhận" FieldName="IDGiamSat2" VisibleIndex="10">
                                                <PropertiesComboBox DataSourceID="sqlDNhanVien" TextField="TenNguoiDung" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Nhân viên kho tổng" FieldName="IDNhanVienKho1" VisibleIndex="11">
                                                <PropertiesComboBox DataSourceID="sqlDNhanVien" TextField="TenNguoiDung" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="18">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="1" Name="chucnang">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Người giao" FieldName="NguoiGiao" ShowInCustomizationForm="True" VisibleIndex="17">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataHyperLinkColumn Caption="Chứng từ" FieldName="FileChungTu" ShowInCustomizationForm="True" VisibleIndex="16">
                                                <PropertiesHyperLinkEdit ImageHeight="30px" ImageUrl="~/image/iconchungtu.png" ImageWidth="30px">
                                                </PropertiesHyperLinkEdit>
                                            </dx:GridViewDataHyperLinkColumn>
                                        </Columns>
                                        <Styles>
                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                            </Header>
                                            <AlternatingRow Enabled="True">
                                            </AlternatingRow>
                                            <TitlePanel Font-Bold="True" HorizontalAlign="Left">
                                            </TitlePanel>
                                        </Styles>
                                    </dx:ASPxGridView>
                               </dx:LayoutItemNestedControlContainer>
                           </LayoutItemNestedControlCollection>
                       </dx:LayoutItem>
                   </Items>
               </dx:LayoutGroup>
           </Items>
       </dx:ASPxFormLayout>
    
        <asp:SqlDataSource ID="sqlTrangThaiChuyenHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiChuyenHang]"></asp:SqlDataSource>
       <asp:SqlDataSource ID="sqlDNhanVien" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
           <SelectParameters>
               <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
           </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="sqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
           <SelectParameters>
               <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
           </SelectParameters>
       </asp:SqlDataSource>

    <%--popup chi tiet don hang--%>
     <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1100px"
         Height="600px" FooterText="Thông tin chi tiết"
        HeaderText="Thông tin chi tiết phiếu chuyển kho" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
</asp:Content>

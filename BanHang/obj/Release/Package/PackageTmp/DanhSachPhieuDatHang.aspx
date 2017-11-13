<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DanhSachPhieuDatHang.aspx.cs" Inherits="BanHang.DanhSachPhieuDatHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("ChiTietDonHang.aspx?IDDonHang=" + key);
             popup.ShowAtElement();
             // alert(key);
         }

    </script>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutItem Caption="" HorizontalAlign="Left">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                        <dx:ASPxButton ID="btnThemDonHang" runat="server" Text="Thêm Đơn Hàng" PostBackUrl="ThemDonHang.aspx">
                            <Image IconID="actions_add_32x32">
                            </Image>
                            <Paddings Padding="4px" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           
            <dx:LayoutGroup Caption="Danh sách phiếu nhập hàng">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxGridView ID="gridDonDatHang" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
                                    <SettingsEditing Mode="PopupEditForm">
                                    </SettingsEditing>
                                    <Settings AutoFilterCondition="Contains" ShowFilterRow="True" />
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <SettingsCommandButton RenderMode="Image">
                                        <ShowAdaptiveDetailButton ButtonType="Image">
                                        </ShowAdaptiveDetailButton>
                                        <HideAdaptiveDetailButton ButtonType="Image">
                                        </HideAdaptiveDetailButton>
                                        <NewButton>
                                            <Image IconID="actions_add_16x16" ToolTip="Thêm">
                                            </Image>
                                        </NewButton>
                                        <UpdateButton ButtonType="Image" RenderMode="Image">
                                            <Image IconID="save_save_32x32office2013" ToolTip="Lưu">
                                            </Image>
                                        </UpdateButton>
                                        <CancelButton ButtonType="Image" RenderMode="Image">
                                            <Image IconID="actions_close_32x32" ToolTip="Hủy thao tác">
                                            </Image>
                                        </CancelButton>
                                        <EditButton ButtonType="Image" RenderMode="Image">
                                            <Image IconID="actions_edit_16x16devav" ToolTip="Sửa">
                                            </Image>
                                        </EditButton>
                                        <DeleteButton ButtonType="Image" RenderMode="Image">
                                            <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                                            </Image>
                                        </DeleteButton>
                                    </SettingsCommandButton>
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                                    </SettingsPopup>
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin đơn vị tính" Title="DANH SÁCH ĐƠN ĐẶT HÀNG" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
                                     <Columns>
                                         <dx:GridViewDataTextColumn Caption="Số Đơn Hàng" FieldName="SoDonHang" VisibleIndex="0">
                                         </dx:GridViewDataTextColumn>
                                         <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="4">
                                         </dx:GridViewDataTextColumn>
                                         <dx:GridViewDataComboBoxColumn Caption="Người Lập" FieldName="IDNguoiLap" VisibleIndex="1">
                                             <PropertiesComboBox DataSourceID="SqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                                             </PropertiesComboBox>
                                         </dx:GridViewDataComboBoxColumn>
                                         <dx:GridViewDataDateColumn Caption="Ngày Lập" FieldName="NgayLap" VisibleIndex="2">
                                             <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy ">
                                             </PropertiesDateEdit>
                                         </dx:GridViewDataDateColumn>
                                         <dx:GridViewDataSpinEditColumn Caption="Tổng Tiền" FieldName="TongTien" VisibleIndex="3">
                                             <PropertiesSpinEdit DisplayFormatString="{0:N0} VNĐ" NumberFormat="Custom">
                                             </PropertiesSpinEdit>
                                         </dx:GridViewDataSpinEditColumn>
                                         <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="7">
                
                                            <DataItemTemplate>
                                                <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                                            </DataItemTemplate>
                                        </dx:GridViewDataButtonEditColumn>
                                         <dx:GridViewDataComboBoxColumn Caption="Nhà Cung Cấp" FieldName="IDNhaCungCap" VisibleIndex="5">
                                             <PropertiesComboBox DataSourceID="SqlNhaCungCap" TextField="TenNhaCungCap" ValueField="ID">
                                             </PropertiesComboBox>
                                         </dx:GridViewDataComboBoxColumn>
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
     
      <asp:SqlDataSource ID="SqlNhaCungCap" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhaCungCap] FROM [GPM_NhaCungCap] WHERE ([DaXoa] = @DaXoa)">
          <SelectParameters>
              <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
          </SelectParameters>
     </asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
          <SelectParameters>
              <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
          </SelectParameters>
      </asp:SqlDataSource>
    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1100px"
         Height="600px" FooterText="Thông tin chi tiết hàng hóa combo"
        HeaderText="Thông tin chi tiết đơn hàng" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
</asp:Content>

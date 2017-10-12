<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="NhomNguoiDung.aspx.cs" Inherits="BanHang.NhomNguoiDung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("PhanQuyen.aspx?IDNhomNguoiDung=" + key);
             popup.ShowAtElement();
             // alert(key);
         }

    </script><%--<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="6">
         <Items>
             <dx:LayoutItem Caption="">
                 <LayoutItemNestedControlCollection>
                     <dx:LayoutItemNestedControlContainer runat="server">
                         <dx:ASPxButton ID="ASPxFormLayout1_E2" runat="server" PostBackUrl="ThemQuyen.aspx" Text="Thêm Quyền">
                             <Image IconID="actions_add_32x32">
                             </Image>
                         </dx:ASPxButton>
                     </dx:LayoutItemNestedControlContainer>
                 </LayoutItemNestedControlCollection>
             </dx:LayoutItem>
         </Items>
     </dx:ASPxFormLayout>--%>
    <dx:ASPxGridView ID="gridNhomNguoiDung" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID" OnRowDeleting="gridNhomNguoiDung_RowDeleting" OnRowInserting="gridNhomNguoiDung_RowInserting" OnRowUpdating="gridNhomNguoiDung_RowUpdating">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowTitlePanel="True" ShowFilterRow="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton>
            <ShowAdaptiveDetailButton ButtonType="Image">
            </ShowAdaptiveDetailButton>
            <HideAdaptiveDetailButton ButtonType="Image">
            </HideAdaptiveDetailButton>
            <NewButton>
                <Image IconID="actions_add_16x16" ToolTip="Thêm mới">
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
            <EditButton>
                <Image IconID="actions_edit_16x16devav" ToolTip="Sửa">
                </Image>
            </EditButton>
            <DeleteButton>
                <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                </Image>
            </DeleteButton>
        </SettingsCommandButton>
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
        </SettingsPopup>
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin nhóm người dùng" Title="DANH SÁCH NHÓM NGƯỜI DÙNG" EmptyDataRow="Danh sách nhóm trống" SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewColumnLayoutItem ColumnName="Tên nhóm" Name="TenNhom">
                </dx:GridViewColumnLayoutItem>
                <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="6" Name="chucnang" ShowClearFilterButton="True">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Mã Nhóm" FieldName="ID" ReadOnly="True" VisibleIndex="0" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên nhóm" FieldName="TenNhom" VisibleIndex="1">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày cập nhật" FieldName="NgayCapNhat" VisibleIndex="5">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataButtonEditColumn Caption="Phân quyền" VisibleIndex="4" Name="phanquyen">
                
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                </DataItemTemplate>
            </dx:GridViewDataButtonEditColumn>
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
    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="600px"
         Height="600px" FooterText="Thông tin chi tiết đơn đặt hàng"
        HeaderText="Phân quyền nhóm người dùng" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
</asp:Content>

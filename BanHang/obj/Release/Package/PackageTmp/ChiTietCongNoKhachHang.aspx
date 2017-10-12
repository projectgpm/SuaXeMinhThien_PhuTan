<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ChiTietCongNoKhachHang.aspx.cs" Inherits="BanHang.ChiTietCongNoKhachHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript">
        function OnMoreInfoClick(element, key) {
            popup.SetContentUrl("InPhieuThanhToan1.aspx?ID=" + key);
            popup.ShowAtElement();
            // alert(key);
        }

    </script>
    <dx:ASPxGridView ID="gridDanhSach" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings AutoFilterCondition="Contains" ShowFilterRow="True" ShowTitlePanel="True" />
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
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin đơn vị tính" Title="DANH SÁCH CHI TIẾT KHÁCH HÀNG THANH TOÁN CÔNG NỢ" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
         <Columns>
             <dx:GridViewDataTextColumn Caption="Số Đơn Hàng" FieldName="SoHoaDon" VisibleIndex="0">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="Hình Thức Thanh Toán" FieldName="HinhThucThanhToan" VisibleIndex="2">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="Nội Dung Thanh Toán" FieldName="NoiDung" VisibleIndex="4">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Thanh Toán" FieldName="NgayThanhToan" VisibleIndex="5">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                 </PropertiesDateEdit>
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataSpinEditColumn Caption="Số Tiền Thanh Toán" FieldName="SoTienThanhToan" VisibleIndex="3">
                 <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                 </PropertiesSpinEdit>
             </dx:GridViewDataSpinEditColumn>
             <dx:GridViewDataComboBoxColumn Caption="Tên Khách Hàng" FieldName="IDKhachHang" VisibleIndex="1">
                 <PropertiesComboBox DataSourceID="SqlKhachHang" TextField="TenKhachHang" ValueField="ID">
                 </PropertiesComboBox>
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="6">
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">In phiếu </a>
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
    <asp:SqlDataSource ID="SqlKhachHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenKhachHang] FROM [GPM_KhachHang] WHERE (([DaXoa] = @DaXoa) AND ([ID] &gt; @ID))">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="ID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1100px"
         Height="600px" FooterText="Thông tin chi tiết"
        HeaderText="Thông tin chi tiết thanh toán" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
</asp:Content>

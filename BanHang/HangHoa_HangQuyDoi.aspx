<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangHoa_HangQuyDoi.aspx.cs" Inherits="BanHang.HangHoa_HangQuyDoi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridHangHoaQuyDoi" KeyFieldName="ID" OnRowDeleting="gridHangHoaQuyDoi_RowDeleting" OnRowInserting="gridHangHoaQuyDoi_RowInserting" OnRowUpdating="gridHangHoaQuyDoi_RowUpdating">
            <SettingsEditing Mode="PopupEditForm">
            </SettingsEditing>
            <Settings ShowTitlePanel="True"></Settings>

            <SettingsBehavior ConfirmDelete="True" />

            <SettingsCommandButton>
                <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

                <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
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

            <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" CommandNew="Thêm" EmptyDataRow="Danh sách hàng hóa trống" PopupEditFormCaption="Thông tin"></SettingsText>
            <EditFormLayoutProperties>
                <Items>
                    <dx:GridViewColumnLayoutItem ColumnName="Mã Hàng" Name="MaHang">
                    </dx:GridViewColumnLayoutItem>
                    <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                    </dx:EditModeCommandLayoutItem>
                </Items>
            </EditFormLayoutProperties>
            <Columns>

                <dx:GridViewCommandColumn Name="chucnang" ShowEditButton="True" VisibleIndex="7" ShowDeleteButton="True" ShowNewButtonInHeader="True">
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn Caption="Hệ Số" FieldName="HeSo" VisibleIndex="3" ReadOnly="True">
                    <PropertiesSpinEdit DisplayFormatString="g">
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataComboBoxColumn Caption="Đơn vị tính" FieldName="IDDonViTinh" VisibleIndex="2" ReadOnly="True">
                    <PropertiesComboBox DataSourceID="sqlDonVitinh" TextField="TenDonViTinh" ValueField="ID" DisplayFormatString="g">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="TenHangHoa" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
            </Columns>

            <Styles>
                <Header HorizontalAlign="Center" Font-Bold="True"></Header>

                <AlternatingRow Enabled="True"></AlternatingRow>

                <TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
            </Styles>

        </dx:ASPxGridView>
        <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_HangHoa] WHERE (([DaXoa] = @DaXoa) AND (([IDTrangThaiHang] = @IDTrangThaiHang) OR ([IDTrangThaiHang] = @IDTrangThaiHang2)))">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                <asp:Parameter DefaultValue="1" Name="IDTrangThaiHang" />
                <asp:Parameter DefaultValue="3" Name="IDTrangThaiHang2" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietPhieuChuyenKho.aspx.cs" Inherits="BanHang.ChiTietPhieuChuyenKho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
            <Items>
                <dx:LayoutGroup Caption="Danh sách hàng hóa">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridHangHoaChiTiet" KeyFieldName="ID" OnRowDeleting="gridHangHoaChiTiet_RowDeleting" OnRowInserting="gridHangHoaChiTiet_RowInserting">
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
                                            <EditButton ButtonType="Image" RenderMode="Image">
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
                                                <dx:GridViewColumnLayoutItem ColumnName="Số Lượng" Name="SoLuong">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="Ghi Chú" Name="GhiChu">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                                                </dx:EditModeCommandLayoutItem>
                                            </Items>
                                        </EditFormLayoutProperties>
                                        <Columns>

                                            <dx:GridViewCommandColumn Name="chucnang" VisibleIndex="7" ShowDeleteButton="True" ShowNewButtonInHeader="True">
                                            </dx:GridViewCommandColumn>

                                            <dx:GridViewDataTextColumn FieldName="MaHang" ShowInCustomizationForm="True" Caption="Mã Hàng" VisibleIndex="0"></dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn Caption="Tên Hàng" FieldName="TenHangHoa" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Đơn Vị Tính" FieldName="IDDonViTinh" VisibleIndex="2">
                                                <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" ShowInCustomizationForm="True" VisibleIndex="6">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="SoLuong" ShowInCustomizationForm="True" VisibleIndex="3">
                                                <PropertiesSpinEdit DisplayFormatString="" NumberFormat="Custom">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataTextColumn Caption="Trọng Lượng" FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="5">
                                                <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:n} KG">
                                                </PropertiesTextEdit>
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
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="Duyệt phiếu chuyển kho">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnXacNhanChuyenKho" runat="server" HorizontalAlign="Center" OnClick="btnXacNhanChuyenKho_Click" Text="Xác nhận" Width="100%">
                                        <Image IconID="data_createmodeldifferences_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    
    </div>
    </form>
</body>
</html>

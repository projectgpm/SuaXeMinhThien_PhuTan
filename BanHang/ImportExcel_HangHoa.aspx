<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportExcel_HangHoa.aspx.cs" Inherits="BanHang.ImportExcel_HangHoa" %>

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
                <dx:LayoutGroup Caption="Nhập file Excel" ColCount="4" Width="300px">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                    <dx:ASPxUploadControl ID="UploadFileExcel" runat="server" AllowedFileExtensions=".xls" >
                                    </dx:ASPxUploadControl>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                    <dx:ASPxButton ID="btnNhap" runat="server" OnClick="btnNhap_Click" Text="Upload">
                                        <Image IconID="miscellaneous_publish_16x16office2013">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                    <dx:ASPxButton ID="btnThem" runat="server" OnClick="btnThem_Click" Text="Lưu tất cả dữ liệu">
                                        <Image IconID="save_saveto_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                    <dx:ASPxButton ID="btnHuy" runat="server" OnClick="btnHuy_Click" Text="Hủy">
                                        <Image IconID="actions_cancel_16x16office2013">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="Thông tin dữ liệu nhập vào">
                      <Items>
                                
                                <dx:LayoutItem Caption="" ColSpan="1">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                                
                                                <dx:ASPxGridView ID="gridHangHoa_Temp" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowDeleting="gridHangHoa_Temp_RowDeleting">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowTitlePanel="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton RenderMode="Image">
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
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin khách hàng" Title="DANH SÁCH HÀNG HÓA" />
        <EditFormLayoutProperties ColCount="2">
        </EditFormLayoutProperties>
        
                <Columns>
                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="0">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="1" Visible="False">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Mã hàng" FieldName="MaHang" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tên hàng" FieldName="TenHangHoa" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="20" Caption="Trọng lượng">
                        <PropertiesTextEdit DisplayFormatString="{0:n} KG">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="HanSuDung" ShowInCustomizationForm="True" VisibleIndex="21" Caption="Hạn sử dụng">
                        <PropertiesTextEdit DisplayFormatString="{0:#,#} tháng">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="GhiChu" ShowInCustomizationForm="True" VisibleIndex="23" Caption="Ghi Chú">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Barcode" ShowInCustomizationForm="True" VisibleIndex="25">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Nhóm hàng" FieldName="IDNhomHang" ShowInCustomizationForm="True" VisibleIndex="2">
                        <PropertiesComboBox DataSourceID="sqlNhomHang" TextField="TenNhomHang" ValueField="ID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Đơn vị tính" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="5">
                        <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Hãng sản xuất" FieldName="IDHangSanXuat" ShowInCustomizationForm="True" VisibleIndex="7">
                        <PropertiesComboBox DataSourceID="sqlHangSanXuat" TextField="TenHangSanXuat" ValueField="ID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Thuế" FieldName="IDThue" ShowInCustomizationForm="True" VisibleIndex="8">
                        <PropertiesComboBox DataSourceID="sqlThue" TextField="TenThue" ValueField="ID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Nhóm đặt hàng" FieldName="IDNhomDatHang" ShowInCustomizationForm="True" VisibleIndex="10">
                        <PropertiesComboBox DataSourceID="sqlNhomDatHang" TextField="TenNhom" ValueField="ID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Trạng thái hàng" FieldName="IDTrangThaiHang" ShowInCustomizationForm="True" VisibleIndex="22">
                        <PropertiesComboBox DataSourceID="sqlTrangThaiHang" TextField="TenTrangThai" ValueField="ID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Trạng thái barcode" FieldName="IDTrangThaiBarcode" ShowInCustomizationForm="True" VisibleIndex="24">
                        <PropertiesComboBox DataSourceID="sqlTrangThaiBarcode" TextField="TenTrangThai" ValueField="ID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn Caption="Hàng quy đổi" FieldName="HangQuyDoi" ShowInCustomizationForm="True" VisibleIndex="9">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataSpinEditColumn Caption="Hệ số" FieldName="HeSo" ShowInCustomizationForm="True" VisibleIndex="6">
                        <PropertiesSpinEdit DisplayFormatString="g">
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataTextColumn Caption="Giá mua trước thuế" FieldName="GiaMuaTruocThue" ShowInCustomizationForm="True" VisibleIndex="11">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Giá bán trước thuế" FieldName="GiaBanTruocThue" ShowInCustomizationForm="True" VisibleIndex="12">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Giá mua sau thuế" FieldName="GiaMuaSauThue" ShowInCustomizationForm="True" VisibleIndex="13">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Giá bán sau thuế" FieldName="GiaBanSauThue" ShowInCustomizationForm="True" VisibleIndex="14">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Giá bán 1" FieldName="GiaBan1" ShowInCustomizationForm="True" VisibleIndex="15">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Giá bán 2" FieldName="GiaBan2" ShowInCustomizationForm="True" VisibleIndex="16">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Giá bán 3" FieldName="GiaBan3" ShowInCustomizationForm="True" VisibleIndex="17">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Giá bán 4" FieldName="GiaBan4" ShowInCustomizationForm="True" VisibleIndex="18">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Giá bán 5" FieldName="GiaBan5" ShowInCustomizationForm="True" VisibleIndex="19">
                        <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
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
                                                
                                                <asp:SqlDataSource ID="sqlTrangThaiBarcode" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThai_Barcode]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiHang]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlNhomDatHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhom] FROM [GPM_NhomDatHang]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlThue" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenThue] FROM [GPM_Thue] WHERE ([DaXoa] = @DaXoa)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlHangSanXuat" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangSanXuat] FROM [GPM_HangSanXuat] WHERE ([DaXoa] = @DaXoa)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlNhomHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhomHang] FROM [GPM_NhomHang] WHERE ([DaXoa] = @DaXoa)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                
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

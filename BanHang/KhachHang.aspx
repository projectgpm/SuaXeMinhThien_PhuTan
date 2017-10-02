<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="KhachHang.aspx.cs" Inherits="BanHang.KhachHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <%-- <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="8" Width="10%">
        <Items>
            <dx:LayoutItem Caption="" HorizontalAlign="Left">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                        <dx:ASPxButton ID="btnXuatPDF" runat="server" OnClick="btnXuatPDF_Click" Text="Xuất PDF">
                            <Image IconID="export_exporttopdf_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HorizontalAlign="Left">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                        <dx:ASPxButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" Text="Xuất Excel">
                            <Image IconID="export_exporttoxls_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HorizontalAlign="Left">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                        <dx:ASPxButton ID="btnNhapExcel" runat="server" OnClick="btnNhapExcel_Click" Text="Nhập Excel">
                            <Image IconID="export_exporttoxlsx_16x16gray">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>--%>
    <dx:ASPxGridView ID="gridKhachHang" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowDeleting="gridKhachHang_RowDeleting" OnRowInserting="gridKhachHang_RowInserting" OnRowUpdating="gridKhachHang_RowUpdating" OnInitNewRow="gridKhachHang_InitNewRow">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowTitlePanel="True" />
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
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin khách hàng" Title="DANH SÁCH KHÁCH HÀNG" EmptyDataRow="Không có dữ liệu hiển thị" SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewLayoutGroup Caption="Thông tin khách hàng">
                    <Items>
                        <dx:GridViewColumnLayoutItem ColumnName="Nhóm KH" Name="IDNhomKhachHang">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Tên KH" Name="TenKhachHang" Width="100%">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Chiết Khấu">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Ngày sinh" Name="NgaySinh">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Địa chỉ" Name="DiaChi" Width="100%">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="CMND" Name="CMND" Width="90%">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Điện thoại" Name="DienThoai" Width="90%">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Email" Name="Email" Width="90%">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Ghi chú" Name="GhiChu" Width="90%">
                        </dx:GridViewColumnLayoutItem>
                    </Items>
                </dx:GridViewLayoutGroup>
                <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="20" Name="chucnang">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Tên KH" FieldName="TenKhachHang" VisibleIndex="2">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="CMND" FieldName="CMND" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Địa chỉ" FieldName="DiaChi" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Điện thoại" FieldName="DienThoai" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="8">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Barcode" FieldName="Barcode" VisibleIndex="9" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="GhiChu" VisibleIndex="11">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày sinh" FieldName="NgaySinh" VisibleIndex="4">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Nhóm KH" FieldName="IDNhomKhachHang" VisibleIndex="0">
                <PropertiesComboBox DataSourceID="SqlNhomKH" TextField="TenNhomKhachHang" ValueField="ID">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Tích lũy" FieldName="TichLuy" VisibleIndex="10" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày cập nhật" FieldName="NgayCapNhat" VisibleIndex="16" Visible="False">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Mã KH" FieldName="MaKhachHang" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Chi Nhánh" FieldName="IDKho" VisibleIndex="15" Visible="False">
                <PropertiesComboBox DataSourceID="SqlKho" TextField="TenCuaHang" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Chiết Khấu" FieldName="IDChietKhau" VisibleIndex="3">
                <PropertiesComboBox DisplayFormatString="g" DataSourceID="SqlChietKhau" TextField="TenChietKhau" ValueField="ID">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataSpinEditColumn Caption="Công Nợ" FieldName="CongNo" VisibleIndex="19">
                <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
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
    <asp:SqlDataSource ID="SqlChietKhau" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenChietKhau] FROM [GPM_ChietKhau] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlNhomKH" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhomKhachHang] FROM [GPM_NhomKhachHang] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="XuatDuLieu" runat="server">
    </dx:ASPxGridViewExporter>
    </asp:Content>

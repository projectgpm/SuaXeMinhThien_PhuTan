<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ThemQuyen.aspx.cs" Inherits="BanHang.ThemQuyen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Thông Tin" ColCount="3" ColSpan="3" RowSpan="3">
            <Items>
                <dx:LayoutItem Caption="Nhóm Người Dùng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                            <dx:ASPxComboBox ID="cmbNhomNguoiDung" runat="server" DataSourceID="SqlNhomNguoiDung" TextField="TenNhom" ValueField="ID">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlNhomNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhom] FROM [GPM_NhomNguoiDung] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Quyền Truy Cập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                            <dx:ASPxComboBox ID="cmbQuyenTruyCap" runat="server" DataSourceID="SqlQuyen">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlQuyen" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDanhMuc], [Link] FROM [GPM_Menu]"></asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                            <dx:ASPxButton ID="btnThem_temp" runat="server" OnClick="btnThem_temp_Click" Text="Thêm">
                                <Image IconID="actions_add_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup Caption="Danh Sách Quyền" ColCount="3" ColSpan="3">
            <Items>
                <dx:LayoutItem Caption="" ColSpan="3">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                            <dx:ASPxGridView ID="gridDanhSachHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" OnRowDeleting="gridDanhSachHangHoa_RowDeleting" OnRowUpdating="gridDanhSachHangHoa_RowUpdating" Width="100%">
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <SettingsEditing Mode="Batch">
                                </SettingsEditing>
                                <SettingsBehavior ConfirmDelete="True" />
                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton ButtonType="Image">
                                    </ShowAdaptiveDetailButton>
                                    <HideAdaptiveDetailButton ButtonType="Image">
                                    </HideAdaptiveDetailButton>
                                    <NewButton ButtonType="Image" RenderMode="Image">
                                        <Image IconID="actions_add_16x16" ToolTip="Thêm mới">
                                        </Image>
                                    </NewButton>
                                    <UpdateButton>
                                        <Image ToolTip="Lưu">
                                        </Image>
                                    </UpdateButton>
                                    <CancelButton>
                                        <Image ToolTip="Hủy thao tác">
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
                                <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" EmptyDataRow="Danh sách hàng hóa trống." Title="DANH SÁCH HÀNG HÓA GIÁ THEO CHI NHÁNH" />
                                <Columns>
                                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="5">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataCheckColumn Caption="Thêm/ Sửa/ Xóa" FieldName="ChucNang" ShowInCustomizationForm="True" VisibleIndex="3">
                                    </dx:GridViewDataCheckColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Tên Danh Mục" FieldName="IDMenu" ShowInCustomizationForm="True" VisibleIndex="1">
                                        <PropertiesComboBox DataSourceID="SqlQuyen" TextField="TenDanhMuc" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Tên Nhóm" FieldName="IDNhom" ShowInCustomizationForm="True" VisibleIndex="0">
                                        <PropertiesComboBox DataSourceID="SqlNhomNguoiDung" TextField="TenNhom" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Link" FieldName="IDMenu" ShowInCustomizationForm="True" VisibleIndex="2">
                                        <PropertiesComboBox DataSourceID="SqlQuyen" DisplayFormatString="{0}" TextField="Link" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataCheckColumn Caption="Hiển Thị" FieldName="TrangThai" ShowInCustomizationForm="True" VisibleIndex="4">
                                    </dx:GridViewDataCheckColumn>
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
                <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Right">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                            <dx:ASPxButton ID="btnHuy" runat="server" OnClick="btnHuy_Click" Text="Hủy">
                                <Image IconID="actions_cancel_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                            <dx:ASPxButton ID="btnThem" runat="server" OnClick="btnThem_Click" Text="Lưu">
                                <Image IconID="actions_apply_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>
    <asp:HiddenField ID="IDDonDatHang_Temp" runat="server" />
</asp:Content>

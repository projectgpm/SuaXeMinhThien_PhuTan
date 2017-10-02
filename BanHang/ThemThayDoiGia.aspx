<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ThemThayDoiGia.aspx.cs" Inherits="BanHang.ThemThayDoiGia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <dx:ASPxFormLayout ID="LayoutGiaTheoVung" runat="server" Width="100%" ColCount="2">
        <Items>

            <dx:LayoutGroup Caption="Danh sách các chi nhánh" Width="100%" RowSpan="10" ColSpan="2">

                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxComboBox ID="cmbVung" runat="server" AutoPostBack="True"
                                     OnSelectedIndexChanged="cmbVung_SelectedIndexChanged" Width="50%"
                                     
                                    >
                                     <%--<Columns>
                                        <dx:ListBoxColumn FieldName="MaVung" Width="100px" Caption="Mã Vùng" />
                                        <dx:ListBoxColumn FieldName="TenVung" Width="200px" Caption="Tên Vùng"/>
                                    </Columns>--%>
                                </dx:ASPxComboBox>

                                <br />
                                <dx:ASPxCheckBoxList ID="DanhSachKho" runat="server" RepeatColumns="5" Width="100%">
                                </dx:ASPxCheckBoxList>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" RowSpan="10">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <asp:SqlDataSource ID="sqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang], [MaKho],[DiaChi] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>

            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Thông tin" ColCount="3" Width="100%" ColSpan="2">
                
                <Items>
                    <dx:LayoutItem Caption="Chọn Mã Hàng(*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                               <dx:ASPxComboBox ID="cmbMaHang" runat="server" ValueType="System.String" 
                                        DropDownWidth="600" 
                                        ValueField="ID"
                                        NullText="Nhập mã hàng ......." Width="80%" TextFormatString="{0}"
                                        EnableCallbackMode="true" CallbackPageSize="10" 
                                        OnItemsRequestedByFilterCondition="cmbMaHang_ItemsRequestedByFilterCondition"
                                        OnItemRequestedByValue="cmbMaHang_ItemRequestedByValue" 
                                        >                                    
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="MaHang" Width="80px" Caption="Mã Hàng" />
                                            <dx:ListBoxColumn FieldName="TenHangHoa" Width="250px" Caption="Tên Hàng Hóa"/>
                                            <dx:ListBoxColumn FieldName="TenDonViTinh" Width="100px" Caption="Đơn Vị Tính"/>
                                        </Columns>
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
                                    </dx:ASPxComboBox>    
                                 <asp:SqlDataSource ID="SqlMaHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" >                                       
                                    </asp:SqlDataSource>  
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giờ Thay Đổi(*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxDateEdit ID="txtGioThayDoi" runat="server" Width ="100%" DisplayFormatString="dd/MM/yyyy hh:mm tt" EditFormat="DateTime" EditFormatString="dd/MM/yyyy hh:mm tt" UseMaskBehavior="True">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormat="Custom" EditFormatString="hh:mm tt">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnThem" runat="server" OnClick="btnThem_Click" Text="Thêm">
                                    <Image IconID="actions_add_32x32">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutGroup Caption="Danh sách hàng hóa" ColSpan="3">
                        <Items>
                            <dx:LayoutItem Caption="" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Ghi Chú: Nếu không thay đổi giá vui lòng nhập -1." Font-Bold="True" ForeColor="#FF3300"></dx:ASPxLabel>
                                        <dx:ASPxGridView ID="gridHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" ClientInstanceName="gridHangHoa" OnRowDeleting="gridHangHoa_RowDeleting" OnRowUpdating="gridHangHoa_RowUpdating">
                                            
                                            <SettingsPager NumericButtonCount="20" Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <SettingsEditing Mode="Batch">
                                            </SettingsEditing>
                                            <Settings ShowTitlePanel="True" />
                                            <SettingsCommandButton>
                                                <ShowAdaptiveDetailButton ButtonType="Image">
                                                </ShowAdaptiveDetailButton>
                                                <HideAdaptiveDetailButton ButtonType="Image">
                                                </HideAdaptiveDetailButton>
                                                <DeleteButton>
                                                    <Image IconID="actions_cancel_16x16">
                                                    </Image>
                                                </DeleteButton>
                                            </SettingsCommandButton>
                                            <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" Title="DANH SÁCH HÀNG HÓA" EmptyDataRow="Danh sách hàng hóa rỗng" SearchPanelEditorNullText="Nhập thông tin cần tìm" CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa" />
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="11" Name="chucnang">
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2">
                                                    <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán" FieldName="GiaBan" ShowInCustomizationForm="True" VisibleIndex="3">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:#,# đ}" NumberFormat="Custom">
                                                    </PropertiesSpinEdit>
                                                </dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="GiaBan1" ShowInCustomizationForm="True" Caption="Giá Bán 1" VisibleIndex="4">
<PropertiesSpinEdit DisplayFormatString="{0:#,# đ}" NumberFormat="Custom"></PropertiesSpinEdit>
</dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="0" ReadOnly="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 2" FieldName="GiaBan2" ShowInCustomizationForm="True" VisibleIndex="5">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:#,# đ}" NumberFormat="Custom">
                                                    </PropertiesSpinEdit>
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 3" FieldName="GiaBan3" ShowInCustomizationForm="True" VisibleIndex="6">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:#,# đ}" NumberFormat="Custom">
                                                    </PropertiesSpinEdit>
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 4" FieldName="GiaBan4" ShowInCustomizationForm="True" VisibleIndex="7">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:#,# đ}" NumberFormat="Custom">
                                                    </PropertiesSpinEdit>
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 5" FieldName="GiaBan5" ShowInCustomizationForm="True" VisibleIndex="8">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:#,# đ}" NumberFormat="Custom">
                                                    </PropertiesSpinEdit>
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataDateColumn Caption="Giờ Thay Đổi" FieldName="GioThayDoi" ShowInCustomizationForm="True" VisibleIndex="9" ReadOnly="True">
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm tt" EditFormat="Custom" EditFormatString="dd/MM/yyyy hh:mm tt">
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataComboBoxColumn Caption="Chi Nhánh" FieldName="IDKho" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="10">
                                                    <PropertiesComboBox DataSourceID="SqlDanhSachKho" TextField="TenCuaHang" ValueField="ID">
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataComboBoxColumn Caption="Tên Hàng Hóa" FieldName="IDHangHoa" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
                                                    <PropertiesComboBox DataSourceID="SqlHangHoa" TextField="TenHangHoa" ValueField="ID">
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
                                        <asp:SqlDataSource ID="SqlDanhSachKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE (([DaXoa] = @DaXoa) AND ([DienThoai] IS NOT NULL))">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:SqlDataSource ID="SqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE (([DaXoa] = @DaXoa) AND ([TenHangHoa] IS NOT NULL))">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
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
                
            </dx:LayoutGroup>

            <dx:LayoutItem Caption="" HorizontalAlign="Right">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnLuu" runat="server" Text="Lưu" OnClick="btnLuu_Click">
                            <Image IconID="actions_apply_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnHuy" runat="server" Text="Hủy" OnClick="btnHuy_Click">
                            <Image IconID="actions_cancel_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>

        </Items>
    </dx:ASPxFormLayout>
     <asp:HiddenField ID="ID_temp" runat="server" />
</asp:Content>

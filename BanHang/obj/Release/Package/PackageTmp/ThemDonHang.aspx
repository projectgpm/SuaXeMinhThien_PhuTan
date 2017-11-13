<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ThemDonHang.aspx.cs" Inherits="BanHang.ThemDonHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
       
      <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Thông tin đơn hàng" ColCount="3" ColSpan="3" RowSpan="3">
            <Items>
                <dx:LayoutItem Caption="Số Đơn Hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                            <dx:ASPxTextBox ID="txtSoDonHang" runat="server" Enabled="False" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Người Lập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                            <dx:ASPxTextBox ID="txtNguoiLap" runat="server" Enabled="False" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ngày Lập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                            <dx:ASPxDateEdit ID="txtNgayLap" runat="server" OnInit="txtNgayLap_Init" Width="100%">
                            </dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Nhà Cung Cấp" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                            <dx:ASPxComboBox ID="cmbNhaCungCap" runat="server" AutoPostBack="True" DataSourceID="SqlNhaCungCap" OnSelectedIndexChanged="cmbNhaCungCap_SelectedIndexChanged" TextField="TenNhaCungCap" ValueField="ID" Width="100%">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlNhaCungCap" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhaCungCap] FROM [GPM_NhaCungCap] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Đã Thanh Toán">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                            <dx:ASPxCheckBox ID="ckThanhToan" runat="server" CheckState="Unchecked" Enabled="False">
                            </dx:ASPxCheckBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ghi Chú" ColSpan="3">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                            <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup Caption="Hàng Hóa" ColCount="3" ColSpan="3">
            <Items>
                <dx:LayoutItem Caption="Hàng Hóa" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                            <asp:Button ID="btnInsertHang" runat="server" OnClick="btnInsertHang_Click" Style="display: none"/>
                           <dx:ASPxComboBox ID="txtBarcode" runat="server" ValueType="System.String" 
                                        DropDownWidth="600" DropDownStyle="DropDown" 
                                        ValueField="ID" 
                                        NullText="Nhập Barcode hoặc mã hàng ......." Width="100%" TextFormatString="{0} - {1}"
                                        EnableCallbackMode="true" CallbackPageSize="10" 
                                        OnItemsRequestedByFilterCondition="txtBarcode_ItemsRequestedByFilterCondition"
                                        OnItemRequestedByValue="txtBarcode_ItemRequestedByValue" 
                                        >                                    
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="MaHang" Width="80px" Caption="Mã Hàng" />
                                            <dx:ListBoxColumn FieldName="TenHangHoa" Width="250px" Caption="Tên Hàng Hóa"/>
                                            <dx:ListBoxColumn FieldName="TenDonViTinh" Width="100px" Caption="Đơn Vị Tính"/>
                                        </Columns>
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
                                    </dx:ASPxComboBox>
                                    
                                    <asp:SqlDataSource ID="dsHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" >                                       
                                    </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Số Lượng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                            <dx:ASPxSpinEdit ID="txtSoLuong" runat="server" Width="100%" DisplayFormatString="N0" Number="1">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>

        <dx:LayoutGroup ColSpan="3" Caption="Danh Sách Hàng Hóa" ColCount="3">
            <Items>
                <dx:LayoutItem Caption="" ColSpan="3">

                    <LayoutItemNestedControlCollection>

                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">                      
                            <dx:ASPxGridView ID="gridDanhSachHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowUpdating="gridDanhSachHangHoa_RowUpdating">
                                 <SettingsPager Mode="ShowAllRecords">
                                 </SettingsPager>
                                 <SettingsEditing Mode="Batch">
                                 </SettingsEditing>
                                 <Settings ShowFooter="True" />
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
                                    <EditButton>
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
                                <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" Title="DANH SÁCH HÀNG HÓA GIÁ THEO CHI NHÁNH" ConfirmDelete="Bạn chắc chắn muốn xóa?" EmptyDataRow="Danh sách trống." />
                                <Columns>

                                    <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="MaHangHoa" ShowInCustomizationForm="True" VisibleIndex="0" ReadOnly="True">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataSpinEditColumn FieldName="DonGia" ShowInCustomizationForm="True" Caption="Giá Mua" VisibleIndex="5">
                                    <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom"></PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Số Lượng Đặt" FieldName="SoLuong" ShowInCustomizationForm="True" VisibleIndex="4">
                                    <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Tên Hàng Hóa" FieldName="IDHangHoa" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True">
                                        <PropertiesComboBox DataSourceID="SqlNguyenLieu" TextField="TenHangHoa" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                        <PropertiesComboBox DataSourceID="SqlDVT" TextField="TenDonViTinh" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>

                                    <dx:GridViewDataButtonEditColumn Caption="Xóa" ShowInCustomizationForm="True" Width="50px" 
                                        VisibleIndex="8">
                                        <DataItemTemplate>
                                            <dx:ASPxButton ID="BtnXoaHang" runat="server" CommandName="XoaHang"
                                                CommandArgument='<%# Eval("ID") %>' 
                                                onclick="BtnXoaHang_Click" RenderMode="Link">
                                                <Image IconID="actions_cancel_32x32">
                                                </Image>
                                            </dx:ASPxButton>
                                        </DataItemTemplate>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dx:GridViewDataButtonEditColumn>

                                    <dx:GridViewDataSpinEditColumn Caption="Thành Tiền" FieldName="ThanhTien" ShowInCustomizationForm="True" VisibleIndex="7" ReadOnly="True">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>

                                    <dx:GridViewDataImageColumn Caption="Hình Ảnh" FieldName="HinhAnh" ShowInCustomizationForm="True" VisibleIndex="3" Width="90px" ReadOnly="True">
                                        <PropertiesImage ImageUrlFormatString="~/UploadImages/{0}" ImageWidth="90px">
                                        </PropertiesImage>
                                    </dx:GridViewDataImageColumn>

                                </Columns>
                                 <TotalSummary>
                                     <dx:ASPxSummaryItem DisplayFormat="Tổng tiền: {0:N0}" FieldName="ThanhTien" ShowInColumn="Thành Tiền" SummaryType="Sum" />
                                 </TotalSummary>
                                 <Styles>
                                    <Header Font-Bold="True" HorizontalAlign="Center">
                                    </Header>
                                    <AlternatingRow Enabled="True">
                                    </AlternatingRow>
                                    <TitlePanel Font-Bold="True" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                            </dx:ASPxGridView>
                                                
                            <asp:SqlDataSource ID="SqlDVT" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    
                                </SelectParameters>
                            </asp:SqlDataSource>
                                                
                            <asp:SqlDataSource ID="SqlNguyenLieu" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                                                
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="" HorizontalAlign="Right" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                            <dx:ASPxButton ID="btnThem" runat="server" Text="Lưu" OnClick="btnThem_Click">
                                <Image IconID="save_saveto_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                            <dx:ASPxButton ID="btnHuy" runat="server" Text="Hủy" OnClick="btnHuy_Click">
                                <Image IconID="save_saveandclose_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>
    <asp:HiddenField ID="IDThuMuaDatHang_Temp" runat="server" />
</asp:Content>

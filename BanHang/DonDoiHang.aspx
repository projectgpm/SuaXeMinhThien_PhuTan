<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DonDoiHang.aspx.cs" Inherits="BanHang.DonDoiHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
            <Items>
                <dx:LayoutGroup Caption="Thông tin phiếu xuất trả" ColCount="3" ColSpan="3" RowSpan="3">
                    <Items>
                        <dx:LayoutItem Caption="Nhà Cung Cấp">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                    <dx:ASPxComboBox ID="cmbNhaCungCap" runat="server" DataSourceID="sqlNhaCungCap" TextField="TenNhaCungCap" ValueField="ID" Width="100%">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlNhaCungCap" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhaCungCap] FROM [GPM_NhaCungCap] WHERE (([DaXoa] = @DaXoa) AND ([ID] &gt; @ID))">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                            <asp:Parameter DefaultValue="1" Name="ID" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Người Lập Phiếu">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                    <dx:ASPxTextBox ID="txtNguoiLapPhieu" runat="server" Enabled="False" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Kho Lập">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                    <dx:ASPxComboBox ID="cmbKho" runat="server" DataSourceID="sqlKhoHang" TextField="TenCuaHang" ValueField="ID" ReadOnly="true" Enabled="False" Width="100%">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlKhoHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Ngày Lập Phiếu">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                    <dx:ASPxDateEdit ID="cmbNgayLapPhieu" runat="server" DateOnError="Today" DisplayFormatString="dd/MM/yyyy" OnInit="cmbNgayLapPhieu_Init" Width="100%">
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Ghi Chú" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                    <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="Thông tin hàng hóa" ColCount="3" ColSpan="3" RowSpan="3">
                    <Items>
                        <dx:LayoutItem Caption="Hàng Hóa">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                
                                    <dx:ASPxComboBox ID="cmbHangHoa" runat="server" ValueType="System.String" 
                            DropDownWidth="600" DropDownStyle="DropDownList"   AutoPostBack="True"
                            ValueField="ID"
                            NullText="Nhập mã hàng.." Width="100%" TextFormatString="{0} - {1}"
                            EnableCallbackMode="true" CallbackPageSize="10"  OnSelectedIndexChanged="cmbHangHoa_SelectedIndexChanged" OnItemRequestedByValue="cmbHangHoa_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbHangHoa_ItemsRequestedByFilterCondition" 
                                    
                                        
                            >                                    
                            <Columns>
                                <dx:ListBoxColumn FieldName="MaHang" Width="80px" Caption="Mã Hàng" />
                                <dx:ListBoxColumn FieldName="TenHangHoa" Width="200px" Caption="Tên Hàng Hóa"/>
                                <dx:ListBoxColumn FieldName="TenDonViTinh" Width="100px" Caption="Đơn Vị Tính"/>
                                <dx:ListBoxColumn FieldName="GiaMuaSauThue" Width="120px" Caption="Giá Mua Sau Thuế" />
                            </Columns>
                            <DropDownButton Visible="False">
                            </DropDownButton>
                        </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>">
                                                  
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tồn Kho">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                    <dx:ASPxSpinEdit ID="txtTonKho" runat="server" Enabled="False" Width="100%">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="ĐVT">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                    <dx:ASPxComboBox ID="cmbDonViTinh" runat="server" DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID" Enabled="False" Width="100%">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Số Lượng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                    <dx:ASPxSpinEdit ID="txtSoLuong" runat="server" Width="100%">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Đơn Giá">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                    <dx:ASPxSpinEdit ID="txtDonGia" runat="server" AutoPostBack="True" DisplayFormatString="N0" Width="100%">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="" HorizontalAlign="Left">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                                    <dx:ASPxButton ID="btnThem" runat="server" OnClick="btnThem_Click" Text="Thêm">
                                        <Image IconID="chart_addchartpane_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup ColSpan="3" Caption="Danh sách hàng hóa" ColCount="3">
                    <Items>
                        <dx:LayoutItem Caption="" ColSpan="3">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                
                                    <dx:ASPxGridView ID="gridDanhSachHangHoa_Temp" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" OnRowDeleting="gridDanhSachHangHoa_Temp_RowDeleting" Width="100%">
                                        <SettingsBehavior ProcessSelectionChangedOnServer="True" ConfirmDelete="True" />
                                        <SettingsCommandButton>
                                            <ShowAdaptiveDetailButton ButtonType="Image">
                                            </ShowAdaptiveDetailButton>
                                            <HideAdaptiveDetailButton ButtonType="Image">
                                            </HideAdaptiveDetailButton>
                                            <DeleteButton ButtonType="Image" RenderMode="Image">
                                                <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                                                </Image>
                                            </DeleteButton>
                                        </SettingsCommandButton>
                                        <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" />
                                        <Columns>
                                            <dx:GridViewDataComboBoxColumn Caption="Tên Hàng" FieldName="IDHangHoa" ShowInCustomizationForm="True" VisibleIndex="1">
                                                <PropertiesComboBox DataSourceID="sqlDanhSachHangHoa" TextField="TenHangHoa" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="6">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataSpinEditColumn Caption="Giá Mua" FieldName="GiaMua" ShowInCustomizationForm="True" VisibleIndex="4">
                                                <PropertiesSpinEdit DisplayFormatString="N0" >
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataSpinEditColumn Caption="Thành Tiền" FieldName="ThanhTien" ShowInCustomizationForm="True" VisibleIndex="5">
                                                <PropertiesSpinEdit DisplayFormatString="N0">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="SoLuong" ShowInCustomizationForm="True" VisibleIndex="3">
                                                <PropertiesSpinEdit DisplayFormatString="g">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="2">
                                                <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="0">
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
                                                
                                    <asp:SqlDataSource ID="sqlDanhSachHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE ([DaXoa] = @DaXoa)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                                
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="" HorizontalAlign="Right" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                    <dx:ASPxButton ID="btnThemPhieuXuat" runat="server" Text="Lưu" OnClick="btnThemPhieuXuat_Click">
                                        <Image IconID="save_saveto_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                    <dx:ASPxButton ID="btnHuyPhieuXuat" runat="server" Text="Hủy" OnClick="btnHuyPhieuXuat_Click">
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
    <asp:HiddenField ID="IDPhieuXuatTra_Temp" runat="server" />
</asp:Content>

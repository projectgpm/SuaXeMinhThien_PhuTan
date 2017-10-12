<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="PhieuKhachHangTraHang.aspx.cs" Inherits="BanHang.PhieuKhachHangTraHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Thông tin phiếu trả hàng" ColCount="4" ColSpan="3" RowSpan="3">
                <Items>
                    <dx:LayoutItem Caption="Khách hàng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                <dx:ASPxComboBox ID="cmbKhachHang" runat="server" DataSourceID="sqlKhachHang" TextField="TenKhachHang" ValueField="ID"  Width="100%" AutoPostBack="True" OnSelectedIndexChanged="cmbKhachHang_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlKhachHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenKhachHang] FROM [GPM_KhachHang] WHERE (([DaXoa] = @DaXoa))">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                       
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Hóa đơn bán hàng" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                
                                <dx:ASPxComboBox ID="cmbHoaDon" runat="server" ValueType="System.String" 
                                    DropDownWidth="600" DropDownStyle="DropDownList"   AutoPostBack="True"
                                    NullText="Nhập mã hóa đơn..." Width="100%"  TextFormatString="{0}"
                                    EnableCallbackMode="true" CallbackPageSize="10" OnSelectedIndexChanged="cmbHoaDon_SelectedIndexChanged" Enabled="False"               
                                    >                                    
                                    <Columns>
                                        <dx:ListBoxColumn FieldName="MaHoaDon" Width="80px" Caption="Hóa đơn" />
                                        <dx:ListBoxColumn FieldName="TongTien" Width="100px" Caption="Tổng tiền"/>
                                         <dx:ListBoxColumn FieldName="NgayBan" Width="150px" Caption="Ngày Lập"/>
                                    </Columns>
                                 <%--   <DropDownButton Visible="False">
                                    </DropDownButton>--%>

                                </dx:ASPxComboBox>
                                <dx:ASPxCheckBox ID="ckHoaDon" runat="server" OnCheckedChanged="ckHoaDon_CheckedChanged" Width="10%" AutoPostBack="True"></dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Người Lập Phiếu">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxComboBox ID="cmbNguoiLapPhieu" runat="server" DataSourceID="sqlNguoiLap" TextField="TenNguoiDung" ValueField="ID" ReadOnly="True" Width="100%" Enabled="False">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlNguoiLap" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung]">
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ngày Lập Phiếu">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxDateEdit ID="cmbNgayLapPhieu" runat="server" DateOnError="Today" DisplayFormatString="dd/MM/yyyy" OnInit="cmbNgayLapPhieu_Init" Width="100%" Enabled="False">
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Nhân viên bán">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <dx:ASPxComboBox ID="cmbNhanVienBanHang" runat="server" DataSourceID="sqlNguoiLap" TextField="TenNguoiDung" ValueField="ID" ReadOnly="True" Width="100%" Enabled="False">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ghi Chú" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Thông tin hàng hóa" ColCount="3" ColSpan="3" RowSpan="3">
                <Items>
                    <dx:LayoutItem Caption="Hàng Hóa(*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                <dx:ASPxComboBox ID="cmbHangHoa" runat="server" ValueType="System.String" 
                                    DropDownWidth="600" DropDownStyle="DropDownList"   AutoPostBack="True"
                                    ValueField="ID"
                                    NullText="Nhập mã hàng.." Width="100%" TextFormatString="{0} - {1}"
                                    EnableCallbackMode="true" CallbackPageSize="10"  OnSelectedIndexChanged="cmbHangHoa_SelectedIndexChanged" OnItemRequestedByValue="cmbHangHoa_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbHangHoa_ItemsRequestedByFilterCondition" Enabled="False"               
                                    >                                    
                                    <Columns>
                                        <dx:ListBoxColumn FieldName="MaHang" Width="80px" Caption="Mã Hàng" />
                                        <dx:ListBoxColumn FieldName="TenHangHoa" Width="200px" Caption="Tên Hàng Hóa"/>
                                        <dx:ListBoxColumn FieldName="TenDonViTinh" Width="200px" Caption="Đơn Vị Tính"/>
                                        <%--<dx:ListBoxColumn FieldName="SoLuong" Width="100px" Caption="SoLuong"/>--%>
                                    </Columns>
                                    <DropDownButton Visible="False">
                                    </DropDownButton>
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="dsHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="ĐVT">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                <dx:ASPxComboBox ID="cmbDonViTinh" runat="server" Width="100%" DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID" ReadOnly="True" Enabled="False">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán(*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan" runat="server" DisplayFormatString="{0:#,# đ}" Width="100%">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Số Lượng(*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                <dx:ASPxSpinEdit ID="txtSoLuong" runat="server" NullText="0" OnNumberChanged="txtSoLuong_NumberChanged" AutoPostBack="True" HorizontalAlign="Right" Width="100%">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Lý do trả(*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                                <dx:ASPxComboBox ID="cmbLyDoTra" runat="server" DataSourceID="sqlLyDoTra" TextField="LyDoTra" ValueField="ID" Width="100%">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlLyDoTra" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_TrangThaiHangKhachTra]"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" HorizontalAlign="Left">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                                <dx:ASPxButton ID="btnThem" runat="server" Text="Thêm" OnClick="btnThem_Click">
                                    <Image IconID="actions_add_32x32">
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
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                                
                                <dx:ASPxGridView ID="gridDanhSachHangHoa_Temp" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings ShowFooter="True" />
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
                                    <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?"  EmptyDataRow="Danh sách hàng hóa trống" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" Caption="Mã Hàng" FieldName="MaHang">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="3" Caption="Giá bán" FieldName="GiaBan">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="4" Caption="Số lượng" FieldName="SoLuong">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="5" Caption="Thành tiền" FieldName="ThanhTien">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,# đ}">
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Lý do đổi" FieldName="LyDoDoi" ShowInCustomizationForm="True" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Hàng hóa" FieldName="IDHangHoa" ShowInCustomizationForm="True" VisibleIndex="1">
                                            <PropertiesComboBox DataSourceID="sqlHangHoa" TextField="TenHangHoa" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Đơn vị tính" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="2">
                                            <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
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
                                    </Columns>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem DisplayFormat="Tổng tiền: {0:N0}" FieldName="ThanhTien" ShowInColumn="Thành tiền" SummaryType="Sum" />
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
                                                
                                <asp:SqlDataSource ID="sqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                                
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" HorizontalAlign="Right" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                <dx:ASPxButton ID="btnThemPhieuKhachHangTraHang" runat="server" Text="Lưu" OnClick="btnThemPhieuKhachHangTraHang_Click">
                                    <Image IconID="save_saveto_32x32">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                                <dx:ASPxButton ID="btnHuyPhieuKhachHangTraHang" runat="server" Text="Hủy" OnClick="btnHuyPhieuKhachHangTraHang_Click">
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
    <asp:HiddenField ID="IDPhieuKhachHangTraHangTem_Temp" runat="server" />
</asp:Content>

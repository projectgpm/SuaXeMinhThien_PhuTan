<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ThemHangHoaCombo.aspx.cs" Inherits="BanHang.ThemHangHoaCombo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     
<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Thông tin hàng hóa combo" ColCount="3" ColSpan="3" RowSpan="3">
            <Items>
                <dx:LayoutItem Caption="Mã Hàng(*)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                            <dx:ASPxTextBox ID="txtMaHang" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tên Hàng Hóa(*)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                            <dx:ASPxTextBox ID="txtTenHangHoa" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Đơn Vị Tính(*)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                            <dx:ASPxComboBox ID="cmbDonViTinh" runat="server" DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" 
                                ValueField="ID" Width="100%"
                                 ValueType="System.String" 
                                 DropDownWidth="300" DropDownStyle="DropDown" 
                                >
                                <Columns>
                                    <dx:ListBoxColumn FieldName="MaDonVi" Width="100px" Caption="Mã ĐVT" />
                                    <dx:ListBoxColumn FieldName="TenDonViTinh" Width="200px" Caption="Tên ĐVT"/> 
                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID],[MaDonVi], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Nhóm Hàng(*)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox ID="cmbNhomHang" runat="server" Width =" 100%" DataSourceID="SqlNhomHang"
                                 TextField="TenNhomHang" ValueField="ID"
                                 ValueType="System.String" 
                                 DropDownWidth="300" DropDownStyle="DropDown" 
                                >
                                <Columns>
                                    <dx:ListBoxColumn FieldName="MaNhom" Width="100px" Caption="Mã Nhóm" />
                                    <dx:ListBoxColumn FieldName="TenNhomHang" Width="200px" Caption="Tên Nhóm"/> 
                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlNhomHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [MaNhom], [TenNhomHang] FROM [GPM_NhomHang] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tổng Giá Mua Trước Thuế">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxSpinEdit ID="txtGiaMuaTruocThue" runat="server" Enabled="False" Width ="100%" DisplayFormatString="{0:#,#} đ">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tổng Giá Mua Sau Thuế">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxSpinEdit ID="txtGiaMuaSauThue" runat="server" Enabled="False" Width ="100%" DisplayFormatString="{0:#,#} đ">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tổng Giá Bán Trước Thuế">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxSpinEdit ID="txtGiaBanTruocThue" runat="server" Enabled="False" Width ="100%" DisplayFormatString="{0:#,#} đ">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
<dx:LayoutItem Caption="Tổng Giá Bán Sau Thuế"><LayoutItemNestedControlCollection>
<dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxSpinEdit ID="txtGiaBanSauThue" runat="server" Enabled="False" Width ="100%" DisplayFormatString="{0:#,#} đ">
                            </dx:ASPxSpinEdit>

                        </dx:LayoutItemNestedControlContainer>
</LayoutItemNestedControlCollection>
</dx:LayoutItem>
                <dx:LayoutItem Caption="Giá Bán Tổng(*)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                            <dx:ASPxSpinEdit ID="txtGiaBanTong" runat="server" DisplayFormatString="{0:#,#} đ" Width="100%">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tổng Trọng Lượng (kg)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxSpinEdit ID="txtTrongLuong" runat="server" Width="100%" DisplayFormatString="{0} KG">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Barcode(*)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxTextBox ID="txtBarcode" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Hạng sủ dụng(*)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxSpinEdit ID="txtHanSuDung" runat="server" Width="100%" DisplayFormatString="{0}/thang">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ghi Chú" ColSpan="3">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup Caption="Hàng Hóa kèm theo" ColCount="3" ColSpan="3" RowSpan="3">
            <Items>
                <dx:LayoutItem Caption="Hàng Hóa" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                            <dx:ASPxComboBox ID="cmbHangHoa" runat="server" 
                                AutoPostBack="True" OnSelectedIndexChanged="cmbHangHoa_SelectedIndexChanged" 
                                DropDownWidth="750px" DropDownStyle="DropDownList"   TextFormatString="{0}"
                                EnableCallbackMode="true" Width="100%" 
                                OnItemRequestedByValue="cmbHangHoa_ItemRequestedByValue" 
                                OnItemsRequestedByFilterCondition="cmbHangHoa_ItemsRequestedByFilterCondition"
                                ValueField="ID"
                                NullText="Vui lòng chọn hàng hóa.........."
                                >
                                <Columns>
                                    <dx:ListBoxColumn Caption="Mã Hàng" FieldName="MaHang" Width="70px" />
                                    <dx:ListBoxColumn Caption="Tên Hàng Hóa" FieldName="TenHangHoa" Width="100%" />
                                    <dx:ListBoxColumn Caption="Giá Bán Sau Thuế" FieldName="GiaBan" Width="120px" />   
                                    <dx:ListBoxColumn Caption="ĐVT" FieldName="TenDonViTinh" Width="100px" />          
                                </Columns>

                                 <DropDownButton Visible="False">
                                        </DropDownButton>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="dsHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" ></asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tồn Kho">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox ID="txtTonKho" runat="server" Enabled="False" Width="100%">
                            </dx:ASPxComboBox>
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
                <dx:LayoutItem Caption="Trọng Lượng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxSpinEdit ID="txtTL" runat="server" Enabled="False" Width="100%">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Giá Bán Sau Thuế">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxSpinEdit ID="txtGiaBanST" runat="server" Enabled="False" Width="100%" DisplayFormatString="{0:#,#} đ">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ghi Chú" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxTextBox ID="txtGhiChuHangHoa" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="" HorizontalAlign="Left">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                            <dx:ASPxButton ID="btnThem_Temp" runat="server" Text="Thêm" OnClick="btnThem_Temp_Click">
                                <Image IconID="actions_add_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup ColSpan="3" Caption="Danh sách hàng hóa combo" ColCount="3">
            <Items>
                <dx:LayoutItem Caption="" ColSpan="3">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                
                            <dx:ASPxGridView ID="gridDanhSachHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
                                 <SettingsPager Mode="ShowAllRecords">
                                 </SettingsPager>
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
                                <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" Title="DANH SÁCH HÀNG HÓA GIÁ THEO CHI NHÁNH" ConfirmDelete="Bạn chắc chắn muốn xóa?" EmptyDataRow="Danh sách hàng hóa rỗng" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="TenHangHoa" ShowInCustomizationForm="True" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="TenDonViTinh" ShowInCustomizationForm="True" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Thành Tiền" FieldName="ThanhTien" ShowInCustomizationForm="True" VisibleIndex="9">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Giá Bán Sau Thuế" FieldName="GiaBanSauThue" ShowInCustomizationForm="True" VisibleIndex="8">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="SoLuong" ShowInCustomizationForm="True" VisibleIndex="4">
                                        <PropertiesSpinEdit DisplayFormatString="0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataButtonEditColumn Caption="Xóa" ShowInCustomizationForm="True" Width="50px" 
                                        VisibleIndex="11">
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
                                    <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="10" FieldName="GhiChu">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Giá Mua Sau Thuế" ShowInCustomizationForm="True" VisibleIndex="6" FieldName="GiaMuaSauThue" Visible="False">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Giá Bán Trước Thuế" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="GiaBanTruocThue" Visible="False">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Giá Mua Trước Thuế" ShowInCustomizationForm="True" VisibleIndex="5" FieldName="GiaMuaTruocThue" Visible="False">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataTextColumn Caption="Trọng Lượng" FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                 <TotalSummary>
                                     <dx:ASPxSummaryItem DisplayFormat="Tổng = {0:N0}" FieldName="GiaBanSauThue" ShowInColumn="Giá Bán Sau Thuế" SummaryType="Sum" />
                                     <dx:ASPxSummaryItem DisplayFormat="Tổng = {0:N0}" FieldName="SoLuong" ShowInColumn="Số Lượng" SummaryType="Sum" />
                                     <dx:ASPxSummaryItem DisplayFormat="Tổng = {0:N0}" FieldName="ThanhTien" ShowInColumn="Thành Tiền" SummaryType="Sum" />
                                     <dx:ASPxSummaryItem DisplayFormat="Tổng = {0}" FieldName="TrongLuong" ShowInColumn="Trọng Lượng" SummaryType="Sum" />
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
                                                
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="" HorizontalAlign="Right" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                            <dx:ASPxButton ID="btnThem" runat="server" Text="Lưu" OnClick="btnThem_Click">
                                <Image IconID="save_saveto_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
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
             
    <asp:HiddenField ID="IDHangHoaComBo_Temp" runat="server" />
</asp:Content>

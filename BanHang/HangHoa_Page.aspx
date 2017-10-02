<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="HangHoa_Page.aspx.cs" Inherits="BanHang.HangHoa_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <br/>
    <dx:ASPxLabel ID="IDHangHoa" runat="server" Text="IDHangHoa" Visible="False">
    </dx:ASPxLabel>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Thông tin hàng hóa" ColCount="4">
                <Items>
                    <dx:LayoutItem Caption="Nhóm hàng (*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cmbNhomHang" runat="server" Width="100%" DataSourceID="sqlNhomHang"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Mã Nhóm" FieldName="MaNhom" Width="100px" />
                                        <dx:ListBoxColumn Caption="Tên nhóm hàng" FieldName="TenNhomHang" Width="100%" />
                                    </Columns>
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlNhomHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [MaNhom], [TenNhomHang] FROM [GPM_NhomHang] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Mã hàng (*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txtMaHang" runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Tên hàng (*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txtTenHang" runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Đơn vị tính (*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cmbDonViTinh" runat="server" Width="100%" DataSourceID="sqlDonVitinh"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="cmbDonViTinh_SelectedIndexChanged">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Mã ĐVT" FieldName="MaDonVi" Width="100%" />
                                        <dx:ListBoxColumn Caption="Tên đơn vị tính" FieldName="TenDonViTinh" Width="100px" />
                                        <dx:ListBoxColumn Caption="Mô tả" FieldName="MoTa" Width="100%" />
                                    </Columns>
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlDonVitinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [MaDonVi], [TenDonViTinh], [MoTa] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Hệ số (*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtHeSo" runat="server" NullText="1" Width="100%">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Hãng SX (*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cmbHangSX" runat="server" Width="100%" DataSourceID="sqlHangSX"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Mã hãng SX" FieldName="MaNSX" Width="100px" />
                                        <dx:ListBoxColumn Caption="Tên hãng SX" FieldName="TenNSX" Width="100px" />
                                        <dx:ListBoxColumn Caption="SĐT" FieldName="DienThoai" Width="100px" />
                                    </Columns>
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlHangSX" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNSX], [DienThoai], [MaNSX] FROM [GPM_HangSanXuat] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Thuế (*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cmbThue" runat="server" Width="100%" DataSourceID="sqlThue"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" OnSelectedIndexChanged="cmbThue_SelectedIndexChanged" AutoPostBack="True">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Thuế" FieldName="TenThue" Width="100%" />
                                        <dx:ListBoxColumn Caption="Tỉ lệ" FieldName="TiLe" Width="100px" />
                                    </Columns>
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlThue" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenThue], [TiLe] FROM [GPM_Thue] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Nhóm đặt hàng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                 <dx:ASPxComboBox ID="cmbNhomDatHang" runat="server" Width="100%" DataSourceID="sqlNhomDatHang"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID">
                                    <Columns>
                                        <%--<dx:ListBoxColumn Caption="ID" FieldName="ID" Width="100px" />--%>
                                        <dx:ListBoxColumn Caption="Người đặt hàng" FieldName="TenNhom" Width="100%" />
                                    </Columns>
                                </dx:ASPxComboBox>
                                 <asp:SqlDataSource ID="sqlNhomDatHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_NhomDatHang]"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá mua trước thuế">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaMuaTruocThue" runat="server" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaMuaTruocThue_ValueChanged" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá mua sau thuế">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaMuaSauThue" runat="server" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaMuaSauThue_ValueChanged" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán trước thuế">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBanTruocThue" runat="server" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaBanTruocThue_ValueChanged" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán sau thuế">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBanSauThue" runat="server" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaBanSauThue_ValueChanged" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Trọng lượng (Kg)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtTrongLuong" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:n} KG">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Hạn sử dụng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txtHangSuDung" runat="server" Width="100%" NullText="0">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Trạng thái hàng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cmbTrangThaiHang" runat="server" Width="100%" DataSourceID="sqlTrangThaiHang"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID">
                                    <Columns>
                                        <%--<dx:ListBoxColumn Caption="ID" FieldName="ID" Width="100px" />--%>
                                        <dx:ListBoxColumn Caption="Trạng thái" FieldName="TenTrangThai" Width="100%" />
                                    </Columns>
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_TrangThaiHang] WHERE ([ID] &lt; @ID)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="5" Name="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ghi chú">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Thông tin thêm" ColCount="5">
                <Items>
                    <dx:LayoutItem Caption="Giá bán 1">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan1" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán 2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan2" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán 3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan3" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán 4">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan4" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán 5">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan5" runat="server" Width="100%" NullText="0" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Barcode Hàng Hóa">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridHangHoaBarcode" KeyFieldName="ID" OnRowDeleting="gridHangHoaBarcode_RowDeleting" OnRowInserting="gridHangHoaBarcode_RowInserting" OnRowUpdating="gridHangHoaBarcode_RowUpdating">
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

                                    <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" CommandNew="Thêm" EmptyDataRow="Danh sách barcode trống" PopupEditFormCaption="Thông tin barcode"></SettingsText>
                                    <EditFormLayoutProperties>
                                        <Items>
                                            <dx:GridViewColumnLayoutItem ColumnName="Trạng thái barcode" Name="TenTrangThai">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Barcode" Name="Barcode">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                                            </dx:EditModeCommandLayoutItem>
                                        </Items>
                                    </EditFormLayoutProperties>
                                    <Columns>

                                        <dx:GridViewCommandColumn Name="chucnang" ShowEditButton="True" VisibleIndex="7" ShowDeleteButton="True" ShowNewButtonInHeader="True">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataTextColumn Caption="Barcode" FieldName="Barcode" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Trạng thái barcode" FieldName="IDTrangThaiBarcode" VisibleIndex="0">
                                            <PropertiesComboBox DataSourceID="sqlTrangThaiBarcode" TextField="TenTrangThai" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataDateColumn Caption="Ngày cập nhật" FieldName="NgayCapNhat" ShowInCustomizationForm="True" VisibleIndex="6">
                                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                    </Columns>

                                    <Styles>
                                        <Header HorizontalAlign="Center" Font-Bold="True"></Header>

                                        <AlternatingRow Enabled="True"></AlternatingRow>

                                        <TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
                                    </Styles>

                                </dx:ASPxGridView>
                                <asp:SqlDataSource ID="sqlTrangThaiBarcode" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_TrangThai_Barcode]"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Hàng hóa quy đổi">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
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

                                    <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" CommandNew="Thêm" EmptyDataRow="Danh sách hàng hóa quy đổi trống" PopupEditFormCaption="Thông tin hàng hóa quy đổi"></SettingsText>
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
                                <asp:SqlDataSource ID="sqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [MaHang], [TenHangHoa] FROM [GPM_HangHoa] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            
            <dx:LayoutGroup Caption="Bảng giá hàng hóa thay đổi theo số lượng bán">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridHangHoaGiaTheoSL" KeyFieldName="ID" OnRowDeleting="gridHangHoaGiaTheoSL_RowDeleting" OnRowInserting="gridHangHoaGiaTheoSL_RowInserting" OnRowUpdating="gridHangHoaGiaTheoSL_RowUpdating">
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
                                            <dx:GridViewColumnLayoutItem ColumnName="SL 1" Name="SL1">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="SL 2" Name="SL2">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Giá bán" Name="GiaBan">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                                            </dx:EditModeCommandLayoutItem>
                                        </Items>
                                    </EditFormLayoutProperties>
                                    <Columns>

                                        <dx:GridViewCommandColumn Name="chucnang" ShowEditButton="True" VisibleIndex="7" ShowDeleteButton="True" ShowNewButtonInHeader="True">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="SL 1" FieldName="SoLuongBD" VisibleIndex="0">
                                            <PropertiesSpinEdit DisplayFormatString="g">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="SL 2" FieldName="SoLuongKT" ShowInCustomizationForm="True" VisibleIndex="1">
                                            <PropertiesSpinEdit DisplayFormatString="g">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Giá bán" FieldName="GiaBan" ShowInCustomizationForm="True" VisibleIndex="3">
                                            <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" NumberFormat="Custom" DisplayFormatInEditMode="True">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataDateColumn Caption="Ngày cập nhật" FieldName="NgayCapNhat" ShowInCustomizationForm="True" VisibleIndex="6">
                                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                    </Columns>

                                    <Styles>
                                        <Header HorizontalAlign="Center" Font-Bold="True"></Header>

                                        <AlternatingRow Enabled="True"></AlternatingRow>

                                        <TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
                                    </Styles>

                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColCount="2">
                <Items>
                    <dx:LayoutItem Caption="" HorizontalAlign="Right">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnLuuHangHoa" runat="server" HorizontalAlign="Right" Text="Lưu Hàng Hóa" OnClick="btnLuuHangHoa_Click">
                                    <Image IconID="save_saveto_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" HorizontalAlign="Left">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnHuy" runat="server" HorizontalAlign="Left" Text="Hủy Hàng Hóa" OnClick="btnHuy_Click">
                                    <Image IconID="actions_cancel_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>

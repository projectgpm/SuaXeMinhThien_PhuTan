<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangHoa_ChiTiet.aspx.cs" Inherits="BanHang.HangHoa_ChiTiet" %>

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
                <dx:LayoutGroup Caption="Thông tin hàng hóa" ColCount="3">
                    <Items>
                        <dx:LayoutItem Caption="Nhóm hàng (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
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
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                    <dx:ASPxTextBox ID="txtMaHang" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tên hàng (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                    <dx:ASPxTextBox ID="txtTenHang" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Đơn vị tính (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                    <dx:ASPxComboBox ID="cmbDonViTinh" runat="server" Width="100%" DataSourceID="sqlDonVitinh"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="cmbDonViTinh_SelectedIndexChanged">
                                        <Columns>
                                            <%--<dx:ListBoxColumn Caption="ID" FieldName="ID" Width="100px" />--%>
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
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                    <dx:ASPxSpinEdit ID="txtHeSo" runat="server" NullText="1" Width="100%">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Hãng SX (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                    <dx:ASPxComboBox ID="cmbHangSX" runat="server" Width="100%" DataSourceID="sqlHangSX"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID">
                                        <Columns>
                                           <%-- <dx:ListBoxColumn Caption="ID" FieldName="ID" Width="100px" />--%>
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
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                    <dx:ASPxComboBox ID="cmbThue" runat="server" Width="100%" DataSourceID="sqlThue"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" OnSelectedIndexChanged="cmbThue_SelectedIndexChanged">
                                        <Columns>
                                            <%--<dx:ListBoxColumn Caption="ID" FieldName="ID" Width="100px" />--%>
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
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                     <dx:ASPxComboBox ID="cmbNhomDatHang" runat="server" Width="100%" DataSourceID="sqlNhomDatHang"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="STT" FieldName="ID" Width="100px" />
                                            <dx:ListBoxColumn Caption="Người đặt hàng" FieldName="TenNhom" Width="100%" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                     <asp:SqlDataSource ID="sqlNhomDatHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_NhomDatHang]"></asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giá mua trước thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                    <dx:ASPxSpinEdit ID="txtGiaMuaTruocThue" runat="server" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaMuaTruocThue_ValueChanged" DisplayFormatString="{0:#,#} đ">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giá mua sau thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                    <dx:ASPxSpinEdit ID="txtGiaMuaSauThue" runat="server" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaMuaSauThue_ValueChanged" DisplayFormatString="{0:#,#} đ">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giá bán trước thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                                    <dx:ASPxSpinEdit ID="txtGiaBanTruocThue" runat="server" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaBanTruocThue_ValueChanged" DisplayFormatString="{0:#,#} đ">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giá bán sau thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                                    <dx:ASPxSpinEdit ID="txtGiaBanSauThue" runat="server" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaBanSauThue_ValueChanged" DisplayFormatString="{0:#,#} đ">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Trọng lượng (Kg)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                    <dx:ASPxSpinEdit ID="txtTrongLuong" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:n} KG">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Hạn sử dụng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                    <dx:ASPxTextBox ID="txtHangSuDung" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Trạng thái hàng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                    <dx:ASPxComboBox ID="cmbTrangThaiHang" runat="server" Width="100%" DataSourceID="sqlTrangThaiHang"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="STT" FieldName="ID" Width="100px" />
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
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                    <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="Thông tin thêm" ColCount="3">
                <Items>
                    <dx:LayoutItem Caption="Giá bán 1">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan1" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán 2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan2" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán 3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan3" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán 4">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan4" runat="server" NullText="0" Width="100%" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán 5">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan5" runat="server" Width="100%" NullText="0" DisplayFormatString="{0:#,#} đ">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="">
            <Items>
                <dx:LayoutItem Caption="" HorizontalAlign="Center">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">
                            <dx:ASPxButton ID="btnLuuHangHoa" runat="server" HorizontalAlign="Right" Text="Cập nhật" OnClick="btnLuuHangHoa_Click">
                                <Image IconID="save_saveto_16x16">
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

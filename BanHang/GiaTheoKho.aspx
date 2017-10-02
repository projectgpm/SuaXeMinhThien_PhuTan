<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="GiaTheoKho.aspx.cs" Inherits="BanHang.GiaTheoVung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="LayoutGiaTheoVung" runat="server" Width="100%">
        <Items>

            <dx:LayoutGroup Caption="Danh sách các chi nhánh" Width="100%" RowSpan="10">

                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
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
            <dx:LayoutGroup Caption="Thông tin" ColCount="2" Width="100%">
                
                <Items>
                    <dx:LayoutItem Caption="Chọn Chi Nhánh Mẫu Để Áp Dụng Giá" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxComboBox ID="cmbKho" runat="server" TextField="TenCuaHang" ValueField="ID" Width="80%" OnSelectedIndexChanged="cmbKho_SelectedIndexChanged" AutoPostBack="True" DataSourceID="sqlKho" DropDownWidth="400px">
                                    <Columns>
                                        <dx:ListBoxColumn FieldName="MaKho" Width="100px" Caption="Mã Chi Nhánh" />
                                        <dx:ListBoxColumn FieldName="TenCuaHang" Width="200px" Caption="Tên Chi Nhánh"/>
                                         <dx:ListBoxColumn FieldName="DiaChi" Width="200px" Caption="Địa Chỉ"/>
                                    </Columns>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutGroup Caption="Danh sách hàng hóa" ColSpan="2">
                        <Items>
                            <dx:LayoutItem Caption="" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                        <dx:ASPxGridView ID="gridHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowUpdating="gridHangHoa_RowUpdating" ClientInstanceName="gridHangHoa" OnCustomJSProperties="gridHangHoa_CustomJSProperties">
                                            <ClientSideEvents EndCallback="function(s, e) {
                                                console.log(s.cpUpdated);
                                                if(s.cpUpdated == true)
                                                {
                                                    alert('Cập nhật giá thành công!');
                                                }	
                                            }" />
                                            <SettingsPager NumericButtonCount="20">
                                            </SettingsPager>
                                            <SettingsEditing Mode="Batch">
                                            </SettingsEditing>
                                            <Settings ShowFilterRow="True" ShowTitlePanel="True" />
                                            <SettingsCommandButton>
                                                <ShowAdaptiveDetailButton ButtonType="Image">
                                                </ShowAdaptiveDetailButton>
                                                <HideAdaptiveDetailButton ButtonType="Image">
                                                </HideAdaptiveDetailButton>
                                            </SettingsCommandButton>
                                            <SettingsSearchPanel Visible="True" />
                                            <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" Title="DANH SÁCH HÀNG HÓA" EmptyDataRow="Danh sách hàng hóa rỗng" SearchPanelEditorNullText="Nhập thông tin cần tìm" />
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="TenHangHoa" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True">
                                                </dx:GridViewDataTextColumn>
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
                                                <dx:GridViewDataTextColumn FieldName="IDHangHoa" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
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

        </Items>
    </dx:ASPxFormLayout>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ChiTietCongNo.aspx.cs" Inherits="BanHang.ChiTietCongNo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
        <script type="text/javascript">
            function OnMoreInfoClick(element, key) {
                popup.SetContentUrl("InPhieuThanhToan3.aspx?ID=" + key);
                popup.ShowAtElement();
                // alert(key);
            }

    </script>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
        <Items>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="ASPxFormLayout1_E2" runat="server" Text="Cập Nhật Công Nợ" OnClick="ASPxFormLayout1_E2_Click">
                            <Image IconID="filterelements_checkbuttons_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
     <dx:ASPxGridView ID="gridDanhSach" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings AutoFilterCondition="Contains" ShowFilterRow="True" ShowTitlePanel="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton RenderMode="Image">
            <ShowAdaptiveDetailButton ButtonType="Image">
            </ShowAdaptiveDetailButton>
            <HideAdaptiveDetailButton ButtonType="Image">
            </HideAdaptiveDetailButton>
            <NewButton>
                <Image IconID="actions_add_16x16" ToolTip="Thêm">
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
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
        </SettingsPopup>
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin đơn vị tính" Title="DANH SÁCH CHI TIẾT TRẢ NỢ NHÀ CUNG CẤP" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
         <Columns>
             <dx:GridViewDataTextColumn Caption="Số Đơn Hàng" FieldName="SoHoaDon" VisibleIndex="0">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="Hình Thức Thanh Toán" FieldName="HinhThucThanhToan" VisibleIndex="2">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="Nội Dung Thanh Toán" FieldName="NoiDung" VisibleIndex="4">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Thanh Toán" FieldName="NgayThanhToan" VisibleIndex="5">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                 </PropertiesDateEdit>
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataSpinEditColumn Caption="Số Tiền Thanh Toán" FieldName="SoTienThanhToan" VisibleIndex="3">
                 <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                 </PropertiesSpinEdit>
             </dx:GridViewDataSpinEditColumn>
             <dx:GridViewDataComboBoxColumn Caption="Nhà Cung Cấp" FieldName="IDNhaCungCap" VisibleIndex="1">
                 <PropertiesComboBox DataSourceID="SqlNhaCungCap" TextField="TenNhaCungCap" ValueField="ID">
                 </PropertiesComboBox>
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="6">
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">In phiếu </a>
                </DataItemTemplate>
            </dx:GridViewDataButtonEditColumn>
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
     <asp:SqlDataSource ID="SqlNhaCungCap" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhaCungCap] FROM [GPM_NhaCungCap] WHERE ([DaXoa] = @DaXoa)">
         <SelectParameters>
             <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
      <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="700px"
         Height="500px" FooterText="Thông tin chi tiết nhập kho"
        HeaderText="Thông tin cập nhật công nợ" ClientInstanceName="popup" EnableHierarchyRecreation="True">

       <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
      <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2" Width="100%">
        <Items>
            <dx:LayoutItem Caption="Chọn Nhà Cung Cấp" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                        <dx:ASPxComboBox ID="cmbNhaCungCap" runat="server" Width="100%" DataSourceID="SqlNhaCungCap" TextField="TenNhaCungCap" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="cmbNhaCungCap_SelectedIndexChanged">
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Công Nợ Hiện Tại" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                        <dx:ASPxSpinEdit ID="txtNoHienTai" runat="server" Width="100%" DisplayFormatString="N0" Enabled="False">
                        </dx:ASPxSpinEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Hình Thức Thanh Toán" ColSpan="2" RowSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                        <dx:ASPxComboBox ID="cmbHinhThucThanhToan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbHinhThucThanhToan_SelectedIndexChanged" Width="100%" Enabled="False">
                            <Items>
                                <dx:ListEditItem Text="Công Nợ Giảm Dần" Value="0" />
                                <dx:ListEditItem Text="Theo Phiếu Nhập Kho" Value="1" />
                            </Items>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Chọn Mã Phiếu" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                        <dx:ASPxComboBox ID="txtMaPhieu" runat="server" Width="100%" Enabled="False" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="txtMaPhieu_SelectedIndexChanged">
                             <Columns>
                                    <dx:ListBoxColumn Caption="Mã Phiếu" FieldName="SoDonHang" Width="120px" />
                                    <dx:ListBoxColumn Caption="Tổng Tiền " FieldName="TongTien" Width="100px" />
                                    <dx:ListBoxColumn Caption="Ngày lập phiếu" FieldName="NgayLap" Width="80px" />               
                                </Columns>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Số Tiền Thanh Toán" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                        <dx:ASPxSpinEdit ID="txtTienThanhToan" runat="server" Width="100%" DisplayFormatString="N0" Enabled="False">
                        </dx:ASPxSpinEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Nhập Số Hóa Đơn" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                        <dx:ASPxTextBox ID="txtNhapSoHoaDon" runat="server" Width="100%" Enabled="False">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Nội Dung" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                        <dx:ASPxTextBox ID="txtNoiDung" runat="server" Width="100%">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Ngày Thanh Toán" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                        <dx:ASPxDateEdit ID="dateNgayThanhToan" runat="server" Width="100%" DisplayFormatString="dd/MM/yyyy" OnInit="dateNgayThanhToan_Init" Enabled="False">
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HorizontalAlign="Right">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                        <dx:ASPxButton ID="btnCapNhatThanhToan" runat="server" Text="Cập Nhật" OnClick="btnCapNhatThanhToan_Click" >
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                        <dx:ASPxButton ID="btnHuy" runat="server" Text="Hủy" OnClick="btnHuy_Click">
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
           </dx:PopupControlContentControl>
</ContentCollection>

    </dx:ASPxPopupControl>
    <%--popup chi tiet don hang--%>
     <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1100px"
         Height="600px" FooterText="Thông tin chi tiết"
        HeaderText="Thông tin chi tiết thanh toán chiết khấu" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
</asp:Content>

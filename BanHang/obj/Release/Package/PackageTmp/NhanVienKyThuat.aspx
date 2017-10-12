<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="NhanVienKyThuat.aspx.cs" Inherits="BanHang.NhanVienKyThuat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="5">
        <Items>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnCapNhatTien" runat="server" Text="Thanh Toán Tiền" OnClick="btnCapNhatTien_Click">
                            <Image IconID="businessobjects_bosale_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="ASPxFormLayout1_E2" runat="server" Text="Danh Sách Thanh Toán" PostBackUrl="CongNoKyThuat.aspx">
                            <Image IconID="businessobjects_bofileattachment_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxGridView ID="gridDanhSach" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowDeleting="gridDanhSach_RowDeleting" OnRowInserting="gridDanhSach_RowInserting" OnRowUpdating="gridDanhSach_RowUpdating">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowTitlePanel="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton>
            <ShowAdaptiveDetailButton ButtonType="Image">
            </ShowAdaptiveDetailButton>
            <HideAdaptiveDetailButton ButtonType="Image">
            </HideAdaptiveDetailButton>
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
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin" Title="DANH SÁCH KỸ THUẬT" EmptyDataRow="Không có dữ liệu hiển thị" SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewLayoutGroup Caption="Thông tin khách hàng">
                    <Items>
                        <dx:GridViewColumnLayoutItem ColumnName="Tên Kỹ Thuật">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Chiết Khấu">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Địa chỉ">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Điện thoại">
                        </dx:GridViewColumnLayoutItem>
                        <dx:GridViewColumnLayoutItem ColumnName="Ghi chú">
                        </dx:GridViewColumnLayoutItem>
                    </Items>
                </dx:GridViewLayoutGroup>
                <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="20" Name="chucnang">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Tên Kỹ Thuật" FieldName="TenKyThuat" VisibleIndex="2">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Địa chỉ" FieldName="DiaChi" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Điện thoại" FieldName="DienThoai" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="GhiChu" VisibleIndex="11">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Chiết Khấu" FieldName="IDChietKhau" VisibleIndex="3">
                <PropertiesComboBox DisplayFormatString="g" DataSourceID="SqlChietKhau" TextField="TenChietKhau" ValueField="ID">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataSpinEditColumn Caption="Tổng Tiền" FieldName="TongTien" VisibleIndex="19">
                <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
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

     <asp:SqlDataSource ID="SqlChietKhau" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenChietKhau] FROM [GPM_ChietKhau] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>
    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="700px"
         Height="300px" FooterText="Thông tin chi tiết nhập kho"
        HeaderText="Thông tin cập nhật" ClientInstanceName="popup">

       <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
      <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2" Width="100%">
        <Items>
            <dx:LayoutItem Caption="Chọn Kỹ Thuật" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                        <dx:ASPxComboBox ID="cmbKyThuat" runat="server" Width="100%" DataSourceID="SqlNhanVienKyThuat" TextField="TenKyThuat" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="cmbKyThuat_SelectedIndexChanged">
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource ID="SqlNhanVienKyThuat" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenKyThuat] FROM [GPM_KyThuat] WHERE ([DaXoa] = @DaXoa)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Tổng Tiền Hiện Tại" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                        <dx:ASPxSpinEdit ID="txtNoHienTai" runat="server" Width="100%" DisplayFormatString="N0" Enabled="False">
                        </dx:ASPxSpinEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Số Tiền Thanh Toán" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                        <dx:ASPxSpinEdit ID="txtTienThanhToan" runat="server" Width="100%" DisplayFormatString="N0">
                        </dx:ASPxSpinEdit>
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
                        <dx:ASPxDateEdit ID="dateNgayThanhToan" runat="server" Width="100%" DisplayFormatString="dd/MM/yyyy" OnInit="dateNgayThanhToan_Init">
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HorizontalAlign="Right">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                        <dx:ASPxButton ID="btnCapNhatThanhToan" runat="server" Text="Cập Nhật" OnClick="btnCapNhatThanhToan_Click" style="width: 114px" >
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
</asp:Content>

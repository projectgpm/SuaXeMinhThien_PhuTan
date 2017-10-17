<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DanhSachHangHoa.aspx.cs" Inherits="BanHang.DanhSachHangHoa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript">
        preventEndEdit_LostFocus_onBarCode = false;
        var listBarCode = [];
        function onBarCode_KeyDown(s, e) {
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            //alert(keyCode);
            if (keyCode === ASPx.Key.Esc) {
                txtBarCode.SetText('');
                preventEndEdit_LostFocus_onBarCode = true;
            }
            if (keyCode === ASPx.Key.Tab || keyCode === ASPx.Key.Enter) {
                listBarCode.push(txtBarCode.GetText());
                //listBarCode = "";
                //if(tkbListBarCode
                tkbListBarCode.SetText('');
                tkbListBarCode.AddToken(txtBarCode.GetText());
                txtBarCode.SetText('');
                preventEndEdit_LostFocus_onBarCode = true;
            }
        }
        function onBarCode_LostFocus(s, e) {
            if (!preventEndEdit_LostFocus_onBarCode) {
                tkbListBarCode.AddToken(txtBarCode.GetText());
                txtBarCode.SetText('');
            }
            preventEndEdit_LostFocus_onBarCode = false;
        }
        //function OnTextChanged(s, e) {
        //    s.Upload();
        //}
        //function OnFileUploadComplete(s, e) {
        //    alert('FileUploadComplete: ' + e.callbackData);
        //}
</script>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="5">
        <Items>
           
            <dx:LayoutItem Caption="Hiển thị">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                        <dx:ASPxComboBox ID="cmbSoLuongXem" runat="server" AutoPostBack="True" SelectedIndex="0" OnSelectedIndexChanged="cmbSoLuongXem_SelectedIndexChanged">
                            <Items>
                                <dx:ListEditItem Selected="True" Text="50" Value="50" />
                                <dx:ListEditItem Text="100" Value="100" />
                                <dx:ListEditItem Text="200" Value="200" />
                                <dx:ListEditItem Text="500" Value="500" />
                                <dx:ListEditItem Text="1000" Value="1000" />
                                <dx:ListEditItem Text="Tất Cả" Value="50000000000000" />
                            </Items>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" ClientVisible="false"  Text="ASPxButton">
            <ClientSideEvents Click="function(s, e){ txtBarCode.Focus();}" />
        </dx:ASPxButton>
    <%-- <ClientSideEvents FilesUploadComplete="function(s, e) {
	                                if(e.isValid){gridHangHoa.UpdateEdit();}
                                }" />--%>
    <dx:ASPxGridViewExporter ID="export" runat="server" GridViewID="aspGridViewExport">
    </dx:ASPxGridViewExporter>
    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" ClientVisible="false"  Text="ASPxButton">
            <ClientSideEvents Click="function(s, e){ txtBarCode.Focus();}" />
        </dx:ASPxButton>
    <dx:ASPxGridView ID="gridHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowDeleting="gridHangHoa_RowDeleting" OnRowInserting="gridHangHoa_RowInserting" OnRowUpdating="gridHangHoa_RowUpdating" style="margin-top: 0px" OnInitNewRow="gridHangHoa_InitNewRow" OnCustomErrorText="gridHangHoa_CustomErrorText">
        <Templates>
              <EditForm>
                <div style="padding: 4px 3px 4px">
                    <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>                    
                </div>
                <div style="padding: 4px 3px 4px">

                    <dx:ASPxTextBox ID="txtBarCode" ClientInstanceName="txtBarCode" Caption="Barcode" runat="server" Width="100%">
                        <ClientSideEvents KeyDown="onBarCode_KeyDown" LostFocus="onBarCode_LostFocus" />
                    </dx:ASPxTextBox>
                    <br />
                    <dx:ASPxTokenBox ID="tkbListBarCode" ClientInstanceName="tkbListBarCode" runat="server"
                        AllowMouseWheel="True" Tokens='<%# LoadListBarCode(Eval("ID")) %>' Width="100%" NullText="Danh sách BarCode của hàng hóa">
                    </dx:ASPxTokenBox>                    
                </div>
                <div style="text-align: right; padding: 2px">
                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>
                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>
                </div>
            </EditForm>
            <DetailRow>
                <dx:ASPxGridView ID="gridBarCode" runat="server" AutoGenerateColumns="False" 
                    KeyFieldName="ID" oninit="gridBarCode_Init" 
                    Width="100%" onrowdeleting="gridBarCode_RowDeleting" 
                    onrowinserting="gridBarCode_RowInserting" 
                    onrowupdating="gridBarCode_RowUpdating">
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
                    <Columns>
                        <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="3">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" VisibleIndex="0" Visible ="false">
                            <EditFormSettings Visible="False" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="BarCode" FieldName="Barcode" 
                            VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ngày cập nhật" FieldName="NgayCapNhat" 
                            VisibleIndex="2">
                            <EditFormSettings Visible="False" />
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
            </DetailRow>
        </Templates>
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
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin hàng hóa" Title="DANH SÁCH HÀNG HÓA" SearchPanelEditorNullText="Nhập thông tin cần tìm...." />
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewColumnLayoutItem ColumnName="Mã Hàng" Name="TenHangHoa">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Tên Hàng Hóa">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Đơn Vị Tính">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Nhóm Hàng">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Giá Mua" Name="GiaMua">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem Caption="Giá Bán" ColumnName="Giá Bán" Name="GiaBan1">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Ghi Chú" Name="GhiChu">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Hình Ảnh">
                    <Template>
                        <dx:ASPxUploadControl ID="UploadImages" runat="server" AutoStartUpload="True" OnFileUploadComplete="UploadImages_FileUploadComplete" ShowProgressPanel="True" UploadMode="Auto" Width="100%">
                            <ValidationSettings AllowedFileExtensions=".jpge, .jpg, .png">
                            </ValidationSettings>
                            <BrowseButton Text="Chọn file ảnh...">
                            </BrowseButton>
                            <AdvancedModeSettings EnableDragAndDrop="True" EnableMultiSelect="True">
                            </AdvancedModeSettings>
                        </dx:ASPxUploadControl>
                    </Template>
                </dx:GridViewColumnLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="TenHangHoa" VisibleIndex="2">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Đơn Vị Tính" FieldName="IDDonViTinh" VisibleIndex="4">
                <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="13">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="14">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataSpinEditColumn Caption="Giá Mua" FieldName="GiaMua" VisibleIndex="5">
                <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom" DisplayFormatInEditMode="True">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn Caption="Giá Bán" FieldName="GiaBan" VisibleIndex="6">
                <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom" DisplayFormatInEditMode="True">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataComboBoxColumn Caption="Nhóm Hàng" FieldName="IDNhomHang" VisibleIndex="0">
                <PropertiesComboBox DataSourceID="sqlNhomHang" TextField="TenNhomHang" ValueField="ID">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" VisibleIndex="1" ReadOnly="True">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataImageColumn Caption="Hình Ảnh" FieldName="HinhAnh" VisibleIndex="12">
                <PropertiesImage ImageUrlFormatString="~/UploadImages/{0}" ImageAlign="Middle" ImageHeight="100px" ImageWidth="100px">
                </PropertiesImage>
            </dx:GridViewDataImageColumn>
            <dx:GridViewDataTextColumn Caption="BarCode" FieldName="Barcode" VisibleIndex="3">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
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
    <asp:SqlDataSource ID="sqlNhomHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhomHang] FROM [GPM_NhomHang] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DanhMucCombo.aspx.cs" Inherits="BanHang.DanhMucCombo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <%--popup chi tiet don hang--%>
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("ChiTietHangHoaCombo.aspx?IDHangHoaComBo=" + key);
             popup.ShowAtElement();
             // alert(key);
         }

    </script>
     <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2">
        <Items>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
    <dx:ASPxButton ID="btnThemHangHoaComBo" runat="server" Text="Thêm hàng combo" HorizontalAlign="Right" VerticalAlign="Middle" PostBackUrl="ThemHangHoaCombo.aspx">
        <Image IconID="actions_add_32x32">
                            </Image>
        <Paddings Padding="4px" />
    </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           
        </Items>
      </dx:ASPxFormLayout> 
    <dx:ASPxGridView ID="gridDanhMucCombo" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDeleting="gridDanhMucCombo_RowDeleting" OnRowUpdating="gridDanhMucCombo_RowUpdating" KeyFieldName="ID">
          <Settings AutoFilterCondition="Contains" ShowFilterRow="True" ShowTitlePanel="True" />
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
         <Settings ShowFilterRow="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton>
            <ShowAdaptiveDetailButton ButtonType="Image">
            </ShowAdaptiveDetailButton>
            <HideAdaptiveDetailButton ButtonType="Image">
            </HideAdaptiveDetailButton>
            <NewButton ButtonType="Image" RenderMode="Image">
                <Image IconID="comments_editcomment_16x16" ToolTip="Thêm mới">
                </Image>
            </NewButton>
            <UpdateButton ButtonType="Image" RenderMode="Image">
                <Image IconID="save_save_32x32office2013">
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
                <Image IconID="actions_cancel_16x16">
                </Image>
            </DeleteButton>
        </SettingsCommandButton>
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Modal="True" />
        </SettingsPopup>
         <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin danh mục combo" Title="DANH SÁCH DANH MỤC COMBO" EmptyDataRow="Không có dữ liệu hiển thị" SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
          <EditFormLayoutProperties>
              <Items>
                  <dx:GridViewColumnLayoutItem ColumnName="Mã Hàng">
                  </dx:GridViewColumnLayoutItem>
                  <dx:GridViewColumnLayoutItem ColumnName="Tên Hàng Hóa">
                  </dx:GridViewColumnLayoutItem>
                  <dx:GridViewColumnLayoutItem ColumnName="ĐVT">
                  </dx:GridViewColumnLayoutItem>
                  <dx:GridViewColumnLayoutItem ColumnName="Nhóm Hàng">
                  </dx:GridViewColumnLayoutItem>
                  <dx:GridViewColumnLayoutItem ColumnName="Đơn Giá Tổng">
                  </dx:GridViewColumnLayoutItem>
                  <dx:GridViewColumnLayoutItem ColumnName="Tổng Trọng Lượng">
                  </dx:GridViewColumnLayoutItem>
                  <dx:GridViewColumnLayoutItem ColumnName="Trạng Thái">
                  </dx:GridViewColumnLayoutItem>
                  <dx:GridViewColumnLayoutItem ColumnName="Hạn Sử Dụng">
                  </dx:GridViewColumnLayoutItem>
                  <dx:GridViewColumnLayoutItem ColumnName="Ghi Chú">
                  </dx:GridViewColumnLayoutItem>
                  <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                  </dx:EditModeCommandLayoutItem>
              </Items>
          </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowDeleteButton="True" VisibleIndex="16" ShowEditButton="True" Name="chucnang">
                <HeaderStyle Wrap="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="TenHangHoa" VisibleIndex="1">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="ĐVT" VisibleIndex="3" FieldName="IDDonViTinh">
<PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
    <ValidationSettings SetFocusOnError="True">
        <RequiredField IsRequired="True" />
    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataSpinEditColumn Caption="Đơn Giá Tổng" VisibleIndex="9" FieldName="GiaBan" Width="150px">
                <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" NumberFormat="Custom" DisplayFormatInEditMode="True">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="14" Width="150px">
                
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                </DataItemTemplate>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataButtonEditColumn>
            <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" VisibleIndex="0">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataSpinEditColumn Caption="Tổng Trọng Lượng" FieldName="TrongLuong" VisibleIndex="10" Width="150px">
                <PropertiesSpinEdit DisplayFormatString="{0} KG" DisplayFormatInEditMode="True" NumberFormat="Custom">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="IDTrangThaiHang" VisibleIndex="11">
                <PropertiesComboBox DataSourceID="SqlTrangThaiHang" TextField="TenTrangThai" ValueField="ID">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataSpinEditColumn Caption="Hạn Sử Dụng" FieldName="HanSuDung" VisibleIndex="12" Width="150px">
                <PropertiesSpinEdit DisplayFormatString="g">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn Caption="Giá Bán Sau Thuế" FieldName="TongCombo" VisibleIndex="8" Width="150px">
                <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" NumberFormat="Custom" DisplayFormatInEditMode="True">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn Caption="Giá Mua Trước Thuế" FieldName="GiaMuaTruocThue" VisibleIndex="5" Width="150px">
                <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" NumberFormat="Custom" DisplayFormatInEditMode="True">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn Caption="Giá Mua Sau Thuế" FieldName="GiaMuaSauThue" VisibleIndex="6" Width="150px">
                <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" NumberFormat="Custom" DisplayFormatInEditMode="True">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn Caption="Giá Bán Trước Thuế" FieldName="GiaBanTruocThue" VisibleIndex="7" Width="150px">
                <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" NumberFormat="Custom" DisplayFormatInEditMode="True">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataComboBoxColumn Caption="Nhóm Hàng" FieldName="IDNhomHang" VisibleIndex="2">
                <PropertiesComboBox DataSourceID="SqlNhomHang" TextField="TenNhomHang" ValueField="ID">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="13">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataButtonEditColumn Caption=" Cập Nhật SL" Width="150px" 
                                        VisibleIndex="15" Name="capnhatsoluong">
                <DataItemTemplate>
                    <dx:ASPxButton ID="BtnSuaSoLuong" runat="server" CommandName="SuaSoLuongHang"
                        CommandArgument='<%# Eval("ID") %>' 
                        onclick="BtnSuaSoLuong_Click" RenderMode="Link">
                        <Image IconID="comments_editcomment_16x16">
                        </Image>
                    </dx:ASPxButton>
                </DataItemTemplate>
                <HeaderStyle Wrap="True" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataButtonEditColumn>
            <dx:GridViewDataTextColumn Caption="Số Lượng Còn" FieldName="SoLuongCon" VisibleIndex="4">
                <HeaderStyle Wrap="True" />
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
     <asp:SqlDataSource ID="SqlNhomHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhomHang] FROM [GPM_NhomHang] WHERE ([DaXoa] = @DaXoa)">
         <SelectParameters>
             <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiHang] WHERE ([ID] &gt; @ID)">
         <SelectParameters>
             <asp:Parameter DefaultValue="4" Name="ID" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
     <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1200px"
         Height="600px" FooterText="Thông tin chi tiết hàng hóa combo"
        HeaderText="Thông tin chi tiết hàng hóa combo" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>

     <dx:ASPxPopupControl ID="popupSuaSoLuong" runat="server" HeaderText="Sửa số lượng hàng" Width="500px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">

        <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
    <dx:ASPxFormLayout ID="formSuaSoLuong" runat="server" ColCount="2" Width="100%">
        <Items>
            <dx:LayoutItem Caption="Mã hàng" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                        <dx:ASPxTextBox ID="txtMaHangSua" runat="server" Width="100%" Enabled="False">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Tên hàng" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                        <dx:ASPxTextBox ID="txtTenHangSua" runat="server" Width="100%" Enabled="False">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Số lượng" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                        <dx:ASPxSpinEdit ID="txtSoLuongSua" runat="server" DisplayFormatString="g" HorizontalAlign="Center" Number="0" NumberType="Integer">
                        </dx:ASPxSpinEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HorizontalAlign="Right" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                        <dx:ASPxButton ID="btnLuuSuaSL" runat="server" OnClick="btnLuuSuaSL_Click">
                            <Image IconID="save_save_32x32office2013">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                        <dx:ASPxButton ID="btnHuySuaSl" runat="server" OnClick="btnHuySuaSl_Click">
                            <Image IconID="actions_close_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <asp:HiddenField ID="hdfIDSuaSL" runat="server" />
            </dx:PopupControlContentControl>
</ContentCollection>

    </dx:ASPxPopupControl>
</asp:Content>

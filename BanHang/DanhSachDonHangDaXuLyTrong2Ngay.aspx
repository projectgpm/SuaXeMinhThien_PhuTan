<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DanhSachDonHangDaXuLyTrong2Ngay.aspx.cs" Inherits="BanHang.DanhSachDonHangDaXuLyTrong2Ngay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("ChiTietDonHangXuLyTrong2Ngay.aspx?IDDonHang=" + key);
             popup.ShowAtElement();
         }
    </script>


     <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="5">
        <Items>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                        <dx:ASPxButton ID="btnDuyetDonHang" runat="server" Text="Xử Lý Đơn Hàng" HorizontalAlign="Right" VerticalAlign="Middle" PostBackUrl="DuyetDonHangThuMua.aspx">
                            <Image IconID="actions_converttorange_32x32">
                            </Image>
                            <Paddings Padding="4px" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           
              <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                        <dx:ASPxButton ID="btnDonhangXuLy1Phan" runat="server" PostBackUrl="DonHangXuLy1PhanThuMua.aspx" Text="Đơn Hàng Đã Xử Lý 1 Phần">
                            <Image IconID="edit_copy_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                        <dx:ASPxButton ID="ASPxFormLayout1_E3" runat="server" Text="Đơn Hàng Đã Xử Lý" PostBackUrl="DonHangHoanTatThuMua.aspx">
                            <Image IconID="content_checkbox_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                        <dx:ASPxButton ID="ASPxFormLayout1_E2" runat="server" PostBackUrl="DanhSachDonHangDaXuLyTrong2Ngay.aspx" Text="Cập Nhật Đơn Hàng Đã Xử Lý" ToolTip="Đơn hàng đã xử lý trong 2 ngày">
                            <Image IconID="edit_edit_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
      </dx:ASPxFormLayout> 
    <dx:ASPxGridView ID="gridDonDatHang" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
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
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin đơn vị tính" Title="DANH SÁCH ĐƠN HÀNG XỬ LÝ  GẦN ĐÂY" EmptyDataRow="Danh sách đơn hàng trống." SearchPanelEditorNullText=" Nhập thông tin cần tìm.." />
         <Columns>
             <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="14">
                
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                </DataItemTemplate>
                 <HeaderStyle Wrap="True" />
            </dx:GridViewDataButtonEditColumn>
             <dx:GridViewDataTextColumn Caption="Số Đơn Hàng" FieldName="SoDonHang" VisibleIndex="0">
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="9">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataComboBoxColumn Caption="Người Lập Phiếu" FieldName="IDNguoiLap" VisibleIndex="1">
                 <PropertiesComboBox DataSourceID="SqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Đặt Hàng" FieldName="NgayDat" VisibleIndex="2">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                 </PropertiesDateEdit>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Xử Lý" FieldName="NgayDuyet" VisibleIndex="4">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                 </PropertiesDateEdit>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Giao Hàng" FieldName="NgayGiao" VisibleIndex="5">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                 </PropertiesDateEdit>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataComboBoxColumn Caption="Trạng Thái Xử Lý" FieldName="IDTrangThaiXuLy" VisibleIndex="10">
                 <PropertiesComboBox DataSourceID="SqlTrangThaiXuLy" TextField="TenTrangThai" ValueField="ID">
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataSpinEditColumn Caption="Tổng Trọng Lượng" FieldName="TongTrongLuong" VisibleIndex="6" Visible="False">
                 <PropertiesSpinEdit DisplayFormatString="g">
                 </PropertiesSpinEdit>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataSpinEditColumn>
             <dx:GridViewDataComboBoxColumn Caption="Trạng Thái Đơn Hàng" FieldName="TrangThai" VisibleIndex="11">
                 <PropertiesComboBox>
                     <Items>
                         <dx:ListEditItem Text="Chênh Lệch" Value="1" />
                         <dx:ListEditItem Text="Không Chênh Lệch" Value="0" />
                     </Items>
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Cập Nhật" FieldName="NgayCapNhat" VisibleIndex="13">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                 </PropertiesDateEdit>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataComboBoxColumn Caption="Người Xác Nhận" FieldName="IDNguoiDuyet" VisibleIndex="3">
                 <PropertiesComboBox DataSourceID="SqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataHyperLinkColumn Caption="Chứng Từ" FieldName="ChungTu" VisibleIndex="12" UnboundType="String" Name="ChungTu">
                  <PropertiesHyperLinkEdit ImageUrl="image/download.png" ImageWidth="90" ImageHeight="60"></PropertiesHyperLinkEdit>       
             </dx:GridViewDataHyperLinkColumn>
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
     <asp:SqlDataSource ID="SqlTrangThaiXuLy" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiDonHang] WHERE (([ID] &lt;&gt; @ID) AND ([ID] &lt;&gt; @ID2))">
         <SelectParameters>
             <asp:Parameter DefaultValue="2" Name="ID" Type="Int32" />
             <asp:Parameter DefaultValue="3" Name="ID2" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
         <SelectParameters>
             <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
         <SelectParameters>
             <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
     <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1100px"
         Height="600px" FooterText="Thông tin chi tiết hàng hóa combo"
        HeaderText="Thông tin chi tiết đơn hàng" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
</asp:Content>

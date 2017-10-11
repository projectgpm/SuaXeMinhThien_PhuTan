<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="BaoCaoTonKho.aspx.cs" Inherits="BanHang.BaoCaoTonKho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Thông tin báo cáo" ColCount="3" HorizontalAlign="Center">
                <Items>
                    <dx:LayoutItem Caption="Nhóm hàng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxComboBox ID="cmbNhomHang" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="cmbNhomHang_SelectedIndexChanged1">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Hàng Hóa">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <dx:ASPxComboBox ID="cmbHangHoa" runat="server" Width="100%">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                <dx:ASPxButton ID="btnXemBaoCao" runat="server" Text="Xem báo cáo" Width="100%" OnClick="btnXemBaoCao_Click">
                                    <Image IconID="print_printarea_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <SettingsItemCaptions HorizontalAlign="Center" />
                <SettingsItems HorizontalAlign="Center" />
            </dx:LayoutGroup>
        </Items>
        <SettingsItemCaptions HorizontalAlign="Center" />
        <SettingsItemHelpTexts HorizontalAlign="Center" />
        <SettingsItems HorizontalAlign="Center" />
    </dx:ASPxFormLayout>
    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1300px"
         Height="700px" FooterText="Thông tin tồn kho"
        HeaderText="Thông tin chi tiết" ClientInstanceName="popup" EnableHierarchyRecreation="True">
    </dx:ASPxPopupControl>
</asp:Content>

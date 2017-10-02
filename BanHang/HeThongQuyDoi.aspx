<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeThongQuyDoi.aspx.cs" Inherits="BanHang.HeThongQuyDoi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HỆ THỐNG QUY ĐỔI HÀNG HÓA - GPM</title>
    <style>
        .canhgiua {
            text-align:center;
        }
    </style>
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("ChiTietPhieuKiemKho.aspx?IDPhieuKiemKho=" + key);
             popup.ShowAtElement();
             // alert(key);
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br /><br /><br />
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="HỆ THỐNG QUY ĐỔI HÀNG HÓA" Font-Bold="True"  CssClass="canhgiua" Font-Italic="False" Font-Size="X-Large" ForeColor="Blue" Width="100%"></dx:ASPxLabel>
        <br /><br />
        <dx:ASPxFormLayout runat="server" Width="100%">
            <Items>
                <dx:LayoutGroup ColCount="2" Caption="">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="ASPxFormLayout1_E2" runat="server" Text="Hệ Thống Bán Hàng Lẻ" PostBackUrl="BanHangLe.aspx">
                                        <Image IconID="businessobjects_bosale_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="ASPxFormLayout1_E4" runat="server" PostBackUrl="DangXuat.aspx" Text="Đăng Xuất">
                                        <Image IconID="actions_close_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        
                        <dx:LayoutGroup Caption="Danh Sách Hàng Hóa Quy Đổi" ColSpan="2">
                            <Items>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxGridView ID="gridHangHoa" runat="server" AutoGenerateColumns="False" Width="100%">
                                                <SettingsCommandButton>
                                                    <ShowAdaptiveDetailButton ButtonType="Image">
                                                    </ShowAdaptiveDetailButton>
                                                    <HideAdaptiveDetailButton ButtonType="Image">
                                                    </HideAdaptiveDetailButton>
                                                </SettingsCommandButton>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Mã Hàng" ShowInCustomizationForm="True" VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" ShowInCustomizationForm="True" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tồn Kho" ShowInCustomizationForm="True" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                     <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="4">
                                                        <DataItemTemplate>
                                                            <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                                                        </DataItemTemplate>
                                                        <HeaderStyle Wrap="True" />
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
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:LayoutGroup>
               
            </Items>
        </dx:ASPxFormLayout>
    </div>
    </form>
</body>
</html>

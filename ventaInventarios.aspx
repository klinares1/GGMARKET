<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ventaInventarios.aspx.cs" Inherits="NOCHESVI_DEFINITIVO.ventaInventarios" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="DIGITA LA FACTURA"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtnumFactura" runat="server" Height="20px" Width="226px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="&amp;Buscar" />
            <br />
            <br />
            <br />
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="952px">
                <LocalReport ReportPath="ReportVentotas.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSetVentotas" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="GGMARKET.DataSetVentotasTableAdapters.VentasTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_IdCompra" Type="Int64" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="IdCompra" Type="Int64" />
                    <asp:Parameter Name="Nombre" Type="String" />
                    <asp:Parameter Name="Cedula" Type="Int64" />
                    <asp:Parameter Name="Producto" Type="String" />
                    <asp:Parameter Name="valorUnitario" Type="Int64" />
                    <asp:Parameter Name="cantidad" Type="Int64" />
                    <asp:Parameter Name="valorTotal" Type="Int64" />
                    <asp:Parameter Name="numFactura" Type="Int64" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Nombre" Type="String" />
                    <asp:Parameter Name="Cedula" Type="Int64" />
                    <asp:Parameter Name="Producto" Type="String" />
                    <asp:Parameter Name="valorUnitario" Type="Int64" />
                    <asp:Parameter Name="cantidad" Type="Int64" />
                    <asp:Parameter Name="valorTotal" Type="Int64" />
                    <asp:Parameter Name="numFactura" Type="Int64" />
                    <asp:Parameter Name="Original_IdCompra" Type="Int64" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <br />
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server"  style="position: relative; top: 48px; left: 747px" Text="VOLVER" OnClick="Button1_Click1" />
  </div>
            </form>
</body>
</html>

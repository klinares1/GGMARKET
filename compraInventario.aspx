<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compraInventario.aspx.cs" Inherits="NOCHESVI_DEFINITIVO.compraInventario" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 682px; width: 1282px">
            <br />
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1075px" Height="371px">
                <LocalReport ReportPath="ReportCompraaa.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSetCompraa" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="GGMARKET.DataSetCompraaaTableAdapters.ComprasTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_IdCompra" Type="Int64" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="IdCompra" Type="Int64" />
                    <asp:Parameter Name="Nit" Type="Int64" />
                    <asp:Parameter Name="Proveedor" Type="String" />
                    <asp:Parameter Name="Telefono" Type="String" />
                    <asp:Parameter Name="Producto" Type="String" />
                    <asp:Parameter Name="Cantidad" Type="Int64" />
                    <asp:Parameter Name="ValorUnitario" Type="Int64" />
                    <asp:Parameter Name="ValorTotal" Type="Int64" />
                    <asp:Parameter Name="numFactura" Type="Int64" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Nit" Type="Int64" />
                    <asp:Parameter Name="Proveedor" Type="String" />
                    <asp:Parameter Name="Telefono" Type="String" />
                    <asp:Parameter Name="Producto" Type="String" />
                    <asp:Parameter Name="Cantidad" Type="Int64" />
                    <asp:Parameter Name="ValorUnitario" Type="Int64" />
                    <asp:Parameter Name="ValorTotal" Type="Int64" />
                    <asp:Parameter Name="numFactura" Type="Int64" />
                    <asp:Parameter Name="Original_IdCompra" Type="Int64" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="btnInvent" runat="server" OnClick="btnInvent_Click" Text="VOLVER" />
        </div>
    </form>
</body>
</html>

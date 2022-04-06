<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroSesion.aspx.cs" Inherits="NOCHESVI_DEFINITIVO.RegistroSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <link href="Style.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 class="h1">REGISTRO GGMARKET</h1>
            <br />
            <br />
             <br />
            <br />
            <asp:Label ID="lblCorreo" runat="server" Text="Cedula" CssClass="label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtCedula" runat="server" TextMode="Number" Width="150px"></asp:TextBox>
            <br />
            <br />
             <br />
            <br />

            <asp:Label ID="lblNombre" runat="server" Text="Nombre: " CssClass="label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtNombre" runat="server" Width="150px"></asp:TextBox>
            <br />
            <br />
             <br />
            <br />

            <asp:Label ID="lblContraseña" runat="server" Text="Contraseña: " CssClass="label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtContraseniaRegistro" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
            <br />
            <br />
             <br />
            <br />
            <asp:Label ID="lblValidarContraseñaRegistro" runat="server" Text="Confirmar contraseña: " CssClass="label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtConfirmarContraseniaRegistro" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
            <br />
            <br />
             <br />
            <br />
            <asp:Button ID="btnRegistr" runat="server" Text="REGISTRAR USUARIO" OnClick="btnRegistr_Click" Height="30px" Width="150px" />
&nbsp;<br />
             <br />
            <br />
&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/InicioSesion.aspx">volver</asp:HyperLink>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="NOCHESVI_DEFINITIVO.InicioSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>GGMARKET</title>
       <link href="style.css" rel="stylesheet" type="text/css">
 
</head>
<body>
    <form id="form1" runat="server">
        <div id="lblContra">
            <h1 class="h1">INICIO DE SESION DE GGMARKET</h1>
            <br />
            <br />
             <br />
            <br />
            <asp:Label ID="lblNombre" runat="server" Text="Cedula: " CssClass="label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtCedula" runat="server" TextMode="Number" Width="150px"></asp:TextBox>
            <br />
            <br />
             <br />
            <br />
            <asp:Label ID="lblNombre0" runat="server" Text="Contraseña: " CssClass="label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
            <br />
            <br />
             <br />
            <br />
            <asp:Button ID="btnButon" runat="server" Text="INICIAR SESION EN GGMARKET" OnClick="btnButon_Click" Width="250px" />
&nbsp;
            <br />
             <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/RegistroSesion.aspx">¿Aún no tienes una cuenta?, registrate! dando click aqui</asp:HyperLink>
            <br />
            <br />

        </div>
        </form>
</body>
</html>

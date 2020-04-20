 <%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Familias3._1.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="icon" href="Images/CommonHopeIcon.png"/>
    <link rel="stylesheet" type="text/css" href="Css/EstiloLogin.css" runat="server" />
    <link rel="stylesheet" type="text/css" href="Css/EstiloForm.css" runat="server" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Ingresar / Log In</title>
    <script>
        function noatras() {
            window.location.hash = "no-back-button";
            window.location.hash = "Again-No-back-button"
            window.onhashchange = function () {
                window.location.hash = "no-back-button";
            }
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            width: 131px;
        }
    </style>
</head>
<body onload="noatras();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="fondo">
            <div class="loginCont" id="contenedor" runat="server">
                <table class="tablac" id="Formulario" runat="server">
                    <tr>
                        <td>
                            <div class="loginContHeader" style="">
                                <asp:Image ID="Logo" runat="server" ImageUrl="~/Images/FamiliasdeEsperanza_Logo_RGB.png" CssClass="loginLogoCommonHope" />
                            </div>
                            <div class="loginContBody">
                                <div class="groupLogin">
                                    <div class="iconss">
                                        <asp:Image ID="Image2" runat="server" Height="27px" Width="27px" ImageUrl="~/Images/empleado.png" />
                                    </div>
                                    <div class="divTxbLogin">
                                        <asp:TextBox ID="txtuser" runat="server" Height="32px" Width="200px" AutoCompleteType="None" placeholder="Usuario / User"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="revTxtUser" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtuser" ValidationGroup="grpIngresar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                                        <%--<ajaxToolkit:ValidatorCalloutExtender ID="revTxtUser_ValidatorCalloutExtender" runat="server" BehaviorID="revTxtUser_ValidatorCalloutExtender" TargetControlID="revTxtUser"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </div>
                                </div>
                                <div class="groupLogin">
                                    <div class="iconss">
                                        <asp:Image ID="Image1" runat="server" Height="27px" ImageUrl="~/Images/pass.png" Width="27px" CssClass="icons" />
                                    </div>
                                    <div class="divTxbLogin">
                                        <asp:TextBox ID="txtpass" runat="server" Height="32px" Width="200px" placeholder="Contraseña / Password" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="revTxtPass" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtpass" ValidationGroup="grpIngresar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                                        <%--<ajaxToolkit:ValidatorCalloutExtender ID="revTxtPass_ValidatorCalloutExtender" runat="server" BehaviorID="revTxtPass_ValidatorCalloutExtender" TargetControlID="revTxtPass"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </div>
                                </div>
                                <b>
                                    <asp:Button ID="Ingresar" CssClass="buttom" runat="server" Text="Ingresar / Log In" OnClick="Ingresar_Click" Height="35px" ValidationGroup="grpIngresar" />
                                </b>
                            </div>
                        </td>
                    </tr>
                </table>
                <table id="Advertenciatb" class="mensaje" runat="server">
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblad" runat="server" CssClass="labelFormBig" Text="ADVERTENCIA / WARNING:"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblmens" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Aceptarbtn" CssClass="buttom" runat="server" Text="Aceptar / Accept" OnClick="Aceptarbtn_Click" Height="35px" />
                        </td>
                    </tr>
                </table>
                <table id="Cambio_Contraseña" class="mensaje tablac" runat="server">
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblmensn" runat="server" CssClass="labelFormBig" Text="Ingrese su contraseña actual. / Enter your current password."></asp:Label>
                            </b>
                            <asp:TextBox ID="txtactualc" runat="server" Height="32px" Width="200px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblmensnc" runat="server" CssClass="labelFormBig" Text="Ingrese su nueva contraseña. / Enter your new password."></asp:Label><br />
                            </b>
                            <asp:TextBox ID="txtnuevac" runat="server" Height="32px" Width="200px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblmensa" runat="server" CssClass="labelFormBig" Text="Confirme su nueva contraseña. / Confirm your new password."></asp:Label>
                            </b>
                            <asp:TextBox ID="txtconfirmarc" runat="server" Height="32px" Width="200px" ValidationGroup="grpCambiar" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revTxtconfirmarc" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtconfirmarc" ValidationGroup="grpCambiar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>              
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btncambio" CssClass="buttom" runat="server" Text="Aceptar / Accept" Height="35px" OnClick="btncambio_Click" ValidationGroup="grpCambiar" />

                        </td>
                    </tr>
                </table>
                <table runat="server" id="Instrucciones" visible="false">
                    <tr>
                        <td>
                            <b>
                            <asp:Label ID="lbltitul" runat="server" CssClass="labelFormBig" Text=""></asp:Label></b><br>
                            <asp:Label ID="lblmen" runat="server" CssClass="labelFormBig" Text=""></asp:Label><br>
                            <asp:Label ID="lblreg1" runat="server" CssClass="labelFormBig" Text=""></asp:Label><br>
                            <asp:Label ID="lblreg2" runat="server" CssClass="labelFormBig" Text=""></asp:Label><br>
                            <asp:Label ID="lblreg3" runat="server" CssClass="labelFormBig" Text=""></asp:Label><br>
                            <asp:Label ID="lblreg4" runat="server" CssClass="labelFormBig" Text=""></asp:Label><br>
                            <asp:Label ID="lblreg5" runat="server" CssClass="labelFormBig" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnlenguaje" runat="server" Text="" CssClass="buttom Espaciado" OnClick="btnlenguaje_Click" />
                        </td>
                    </tr>
                </table>

            </div>
        </div>

    </form>
    <div class="piePagina" style="height: 15.5px">
        <footer class="" style="background-color: rgb(69, 158, 232); padding-top: 0px">
            <!-- Copyright -->
            <div style="font-size: .7em" class="">
                ©Common Hope 2019 - 02-2019 v. 1
            </div>
            <!-- Copyright -->
        </footer>
    </div>
</body>
</html>

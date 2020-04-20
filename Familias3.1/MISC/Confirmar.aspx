<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmar.aspx.cs" Inherits="Familias3._1.Confirmar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/EstiloLogin.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div id="contenedor" runat="server" class="contenedor">
            <table class="tablac" id="Formulario" runat="server">
                    <tr>
                        <td class="auto-style10">
                            <asp:Image ID="Logo" runat="server" ImageUrl="~/Images/FamiliasdeEsperanza_Logo_RGB.png" Width="274px" Height="50px" /></td>

                    </tr>

                    <tr>

                        <td class="auto-style22">
                            <div class="iconss">
                                <asp:Image ID="Image2" runat="server" Height="27px" Width="27px" ImageUrl="~/Images/empleado.png" />
                            </div>
                            <asp:TextBox ID="txtuser" runat="server" Height="32px" Width="200px" AutoCompleteType="None" placeholder="Usuario/User"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revTxtUser" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtuser" ValidationGroup="grpIngresar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                            <%--<ajaxToolkit:ValidatorCalloutExtender ID="revTxtUser_ValidatorCalloutExtender" runat="server" BehaviorID="revTxtUser_ValidatorCalloutExtender" TargetControlID="revTxtUser"></ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style23"></td>

                    </tr>
                    <tr>
                        <td class="auto-style16">
                            <div class="iconss">
                                <asp:Image ID="Image1" runat="server" Height="27px" ImageUrl="~/Images/pass.png" Width="27px" CssClass="icons" />
                            </div>
                            <asp:TextBox ID="txtpass" runat="server" Height="32px" Width="200px" placeholder="Contraseña/Password" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revTxtPass" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtpass" ValidationGroup="grpIngresar" Display="None" ClientValidationFunction="validate"></asp:RequiredFieldValidator>
                            <%--<ajaxToolkit:ValidatorCalloutExtender ID="revTxtPass_ValidatorCalloutExtender" runat="server" BehaviorID="revTxtPass_ValidatorCalloutExtender" TargetControlID="revTxtPass"></ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                    <tr>
                        <td class="auto-style11">
                            <b>
                                <asp:Button ID="Ingresar" CssClass="buttom" runat="server" Text="Ingresar / Login" OnClick="Ingresar_Click" Height="35px" ValidationGroup="grpIngresar" />
                            </b>
                        </td>
                    </tr>
                </table>
        </div>
    
    </div>
    </form>
</body>
</html>

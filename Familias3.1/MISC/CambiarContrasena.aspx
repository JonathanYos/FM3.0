<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master"  CodeBehind="CambiarContrasena.aspx.cs" Inherits="Familias3._1.MISC.CambiarContraseña" %>

<%@ MasterType virtualPath="~/mast.Master"%> 
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formCont searchFrm">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblActPsw" class="labelBoldForm" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txbActPsw" class="textBoxBlueForm txt" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="refTxbActPsw" runat="server" ErrorMessage="RequiredFieldValidator" Display="None" ValidationGroup="grpGuardar" ControlToValidate="txbActPsw"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="refTxbActPsw_ValidatorCalloutExtender" runat="server" BehaviorID="refTxbActPsw_ValidatorCalloutExtender" TargetControlID="refTxbActPsw">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNuevaPsw"  class="labelBoldForm" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txbNuevaPsw" class="textBoxBlueForm txt" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="refTxbNuevaPsw" runat="server" ErrorMessage="RequiredFieldValidator" Display="None" ValidationGroup="grpGuardar" ControlToValidate="txbNuevaPsw"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="refTxbNuevaPsw_ValidatorCalloutExtender" runat="server" BehaviorID="refTxbNuevaPsw_ValidatorCalloutExtender" TargetControlID="refTxbNuevaPsw">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblConfPsw" class="labelBoldForm" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txbConfPsw" class="textBoxBlueForm txt" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="refTxbConfPsw" runat="server" ErrorMessage="RequiredFieldValidator" Display="None" ValidationGroup="grpGuardar" ControlToValidate="txbConfPsw"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="refTxbConfPsw_ValidatorCalloutExtender" runat="server" BehaviorID="refTxbConfPsw_ValidatorCalloutExtender" TargetControlID="refTxbConfPsw">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td>
                     <asp:Button ID="btnCambiarPsw" class="butonForm" runat="server" ValidationGroup="grpGuardar" Text="Button" OnClick="btnCambiarPsw_Click" />
                    <asp:Button ID="btnVerReglas" class="butonFormSec" runat="server"  Text="Button" OnClick="btnVerReglas_Click" />
                </td>
            </tr>
        </table>
    </div>
   </asp:Content>

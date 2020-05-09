<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="ReporteGeneralActi.aspx.cs" Inherits="Familias3._1.EDUC.ReporteGeneralActi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnltodo" runat="server">
        <table>
            <tr>
                <td><asp:Label ID="lblfechade" CssClass="labelForm" runat="server"></asp:Label></td>
                <td>
                     <asp:RequiredFieldValidator ID="revtxtfecha" runat="server" ControlToValidate="txtdefecha" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="revtxtfecha_ValidatorCalloutExtender" runat="server" BehaviorID="revtxtfecha_ValidatorCalloutExtender" TargetControlID="revtxtfecha">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:TextBox ID="txtdefecha" runat="server" contentEditable="false"></asp:TextBox>
                     <ajaxToolkit:CalendarExtender ID="txtdefecha_CalendarExtender" runat="server" Format="yyyy-MM-dd" BehaviorID="txtdefecha_CalendarExtender" TargetControlID="txtdefecha" />
                </td>
                <td><asp:Label ID="lblfechaa" CssClass="labelForm" runat="server"></asp:Label></td>
                <td>
                     <asp:TextBox ID="txbafecha" runat="server" contentEditable="false"></asp:TextBox>
                     <ajaxToolkit:CalendarExtender ID="txbafecha_CalendarExtender" runat="server" Format="yyyy-MM-dd" BehaviorID="txbafecha_CalendarExtender" TargetControlID="txbafecha" />
                    <asp:RequiredFieldValidator ID="rfvafecha" runat="server" ControlToValidate="txbafecha" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vceafecha" runat="server" BehaviorID="rfvafecha_ValidatorCalloutExtender" TargetControlID="rfvafecha">
                    </ajaxToolkit:ValidatorCalloutExtender>

                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblactividad" CssClass="labelForm" runat="server"></asp:Label></td>
                <td></td>
                <td colspan="2"></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

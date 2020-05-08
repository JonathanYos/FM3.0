<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="ActividadesDuplicadas.aspx.cs" Inherits="Familias3._1.EDUC.ActividadesDuplicadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .w200{
            width:200px;
        }
    </style>
    <asp:Panel ID="pnltodo" CssClass="formContGlobal" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblactividad" CssClass="labelForm" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlactividad" CssClass="comboBoxBlueForm w200" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlactividad_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lbltotal" CssClass="labelBlackForm" runat="server"></asp:Label>
                    <br />
                    <asp:GridView ID="gvhistorial" CssClass="tableCont" runat="server"></asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

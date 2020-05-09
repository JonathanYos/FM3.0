<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="ReporteAnualActividades.aspx.cs" Inherits="Familias3._1.EDUC.ReporteAnualActividades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .w200 {
            width: 200px;
        }

        .w100 {
            width: 100px;
        }
    </style>
    <asp:Panel ID="pnltodo" CssClass="formContGlobal" runat="server">
        <div style="border:1px solid #585858; display:flex; justify-content:center;">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblactividad" CssClass="labelForm" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlactividad" CssClass="comboBoxBlueForm w200" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblanio" CssClass="labelForm" runat="server"></asp:Label><asp:TextBox ID="txtanio" onkeypress="return esDigito(event)" CssClass="textBoxBlueForm w100" runat="server"></asp:TextBox>
                    </td>
                    <td rowspan="2">
                        <asp:Button ID="btngenerar" CssClass="butonForm" runat="server" OnClick="btngenerar_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkinfogen" runat="server" CssClass="labelForm" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="border:1px solid #585858;">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lbltotal" CssClass="labelBlackForm" runat="server"></asp:Label>
                        <br />
                        <asp:GridView ID="gvhistorial" CssClass="tableCont" runat="server"></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

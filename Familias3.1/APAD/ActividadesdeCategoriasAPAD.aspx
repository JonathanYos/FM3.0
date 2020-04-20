<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="ActividadesdeCategoriasAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.ActividadesdeCategoriasAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <style>
            .allwidth {
                width: 100%;
            }

            .clase1 {
                margin: 10px auto;
            }

            .comboBoxForm {
                width: 150px;
            }
        </style>
        <table class="allwidth">
            <tr>
                <td>
                    <table class="clase1 tableCont">
                        <tr>
                            <th>
                                <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lblsitio" runat="server" Text=""></asp:Label></th>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlsitio" runat="server" CssClass="comboBoxForm"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>
                                <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lblcategoria" runat="server" Text=""></asp:Label></th>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlcategoria" runat="server" CssClass="comboBoxForm" AutoPostBack="True" OnSelectedIndexChanged="ddlcategoria_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblcategoriacarta" runat="server" Text=""></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlcategoriacarta" runat="server" CssClass="comboBoxForm"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblanio" runat="server" Text=""></asp:Label></th>
                            <td>
                                <asp:TextBox ID="txbanio" MaxLength="4" onkeypress='return esDigito(event)' runat="server" CssClass="textBoxForm"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnAceptar" runat="server" Text="" CssClass="butonForm" OnClick="btnAceptar_Click" /></td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:GridView ID="gvhistorial" runat="server" CssClass="tableCont clase1 gray">
                        </asp:GridView>
                    </asp:Panel>



                </td>
            </tr>
        </table>
    </div>
</asp:Content>

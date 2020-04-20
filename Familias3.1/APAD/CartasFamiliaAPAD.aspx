<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="CartasFamiliaAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.CartasFamiliaAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <style>
            .con {
                margin: 10px auto;
            }

            .totalt {
                width: 100%;
            }
        </style>
        <table class="gv-padrinos">
            <tr>
                <td>
                    <asp:Panel ID="pnlbusqueda" runat="server" DefaultButton="btnbuscar">
                        <table class="con pink" runat="server" id="ingreso">
                            <tr>
                                <th>
                                    <asp:Label ID="lblsitio" CssClass="labelForm" runat="server"></asp:Label><asp:DropDownList ID="ddlsitio" CssClass="comboBoxForm gv-padrinos" runat="server"></asp:DropDownList></th>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblapad" runat="server" CssClass="labelForm" Text=""></asp:Label><asp:TextBox ID="txtapad" onkeypress="return esDigito(event)" runat="server" CssClass="textBoxForm totalt"></asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lblfamilia" runat="server" CssClass="labelForm" Text=""></asp:Label><asp:TextBox ID="txbfamilia" onkeypress="return esDigito(event)" runat="server" CssClass="textBoxForm totalt"></asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Button ID="btnbuscar" runat="server" Text="" CssClass="butonForm" OnClick="btnbuscar_Click" />
                                </td>
                            </tr>

                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="otro" runat="server" id="historial">
                        <tr>
                            <td>
                                <asp:Button ID="btnregresar" runat="server" Text="" CssClass="butonFormSec" OnClick="btnregresar_Click" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcategoria" runat="server" CssClass="labelForm" Text=""></asp:Label>
                                <asp:DropDownList ID="ddlcategoria" CssClass="comboBoxForm inline" runat="server"></asp:DropDownList>
                                <asp:Button ID="btningresar" runat="server" CssClass="butonForm" Text="" OnClick="btningresar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvhistorial" runat="server" CssClass="tableCont " OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Agregar/Add">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkentregado" runat="server" Checked="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvhistorial2" CssClass="tableCont otro gray" runat="server"></asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlimprimir" runat="server">
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <script>
        $(document).ready(function () {
            $('#ContentPlaceHolder1_btningresar').click(function () {
                $('#ContentPlaceHolder1_btningresar').css('display', 'none');
            });
        });
    </script>

</asp:Content>

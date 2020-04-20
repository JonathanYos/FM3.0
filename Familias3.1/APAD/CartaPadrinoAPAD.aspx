<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="CartaPadrinoAPAD.aspx.cs" Inherits="Familias3._1.San_Pablo.CartaPadrinoAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <table style="width: 100%;">
            <tr>
                <td>
                    <style>
                        .mar10 {
                            margin: 10px;
                        }
                    </style>
                    <asp:Button ID="btnnuevab" runat="server" CssClass="butonForm mar10" OnClick="btnnuevab_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table class="tableCont otro pink">

                            <tr>
                                <th colspan="2">
                                    <asp:Label ID="lblpadrino" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="lblnotas" runat="server"></asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txbnotas" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width: 122px">
                                    <asp:Label ID="lblVfecha" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblnotas1" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width: 122px">
                                    <asp:Label ID="lblVNombre" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblVmiembro" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvapadrinado" CssClass="tableCont" runat="server" OnRowDataBound="gvapadrinado_RowDataBound">
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
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnaceptar" runat="server" CssClass="butonForm" Text="" OnClick="btnaceptar_Click" />
                                    <asp:Button ID="btncancelar" runat="server" Text="" CssClass="butonFormSec" OnClick="btncancelar_Click1" />
                                    <asp:Button ID="btnmodificar" runat="server" CssClass="butonForm" Text="" OnClick="btnmodificar_Click" />
                                    <asp:Button ID="btneliminar" runat="server" CssClass="butonForm" Text="" OnClick="btneliminar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvhistorial" runat="server" CssClass="tableCont gray otro" OnRowCommand="gvhistorial_RowCommand" BorderStyle="None" AutoGenerateColumns="false" OnRowDataBound="gvhistorial_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Project" />
                            <asp:BoundField DataField="Enviada" />
                            <asp:BoundField DataField="Enviada a Guatemala" />
                            <asp:BoundField DataField="Traducida" />
                            <asp:BoundField DataField="Entregado" />
                            <asp:BoundField DataField="Nombre" />
                            <asp:BoundField DataField="Notes" />
                            <asp:ButtonField ButtonType="Button" HeaderText="Accion/Action" Text="Seleccionar/Select" ControlStyle-CssClass="butonForm" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="gvhisapad" runat="server" CssClass="tableCont otro orange" BorderStyle="None"></asp:GridView>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

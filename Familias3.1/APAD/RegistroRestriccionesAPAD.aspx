<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="RegistroRestriccionesAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.RegistroRestriccionesAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .todotam {
            width: 100%;
        }

        .s {
            width: 150px;
        }
    </style>
    <div class="formContGlobal">
        <table class="todotam">
            <tr>
                <td>
                    <table class="tableCont otro pink">
                        <tr>
                            <th>
                                <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lblsitio" runat="server" Text=""></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlsitio" runat="server" CssClass="s comboBoxForm"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>
                                <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lblmiembro" runat="server" Text=""></asp:Label></th>
                            <td>
                                <asp:TextBox ID="txtmiembro" runat="server" CssClass="textBoxForm s"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>
                                <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lbltipo" runat="server" Text=""></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddltipo" runat="server" CssClass="s comboBoxForm"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnguardar" runat="server" Text="" CssClass="butonForm" OnClick="btnguardar_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="otro tableCont gray">
                        <tr>
                            <th>
                                <asp:Label ID="lblsitiob" CssClass="esp" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlsitiob" class="comboBoxForm esp" runat="server"></asp:DropDownList>
                                <asp:Button ID="btnbuscar" CssClass="butonForm esp" runat="server" OnClick="btnbuscar_Click" />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <div class="Contenedor4">
                                    <asp:GridView ID="gvhistorial" runat="server" CssClass="tableCont gray" AutoGenerateColumns="false" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="Sitio" DataField="Project" />
                                            <asp:BoundField HeaderText="Miembro" DataField="MemberId" />
                                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                            <asp:BoundField HeaderText="Restriccion" DataField="Restriccion" />
                                            <asp:BoundField HeaderText="Fecha de Restriccion" DataField="Fecha de Restriccion" />
                                            <asp:BoundField HeaderText="Usuario" DataField="UserId" />
                                            <asp:ButtonField ButtonType="Button" HeaderText="Accion/Action" Text="Seleccionar/Select" ControlStyle-CssClass="butonForm">
                                                <ControlStyle CssClass="butonForm"></ControlStyle>
                                            </asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>


                </td>
            </tr>
        </table>
    </div>
</asp:Content>

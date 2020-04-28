<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="RegistroCarnetsAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.RegistroCarnetsAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .s {
            display: inline-block;
            width: 75px;
        }

        .comboBoxForm {
            width: 100px;
        }

        .ctrar {
            margin: 10px auto;
        }

        .allwidth {
            width: 100%;
        }

        .ddl {
            width: 175px;
            display: inline-block;
        }
    </style><div class="formContGlobal">
    <table class="allwidth">
        <tr>
            <td>
                <table class="tableCont otro pink">
                    <tr>
                        <th>
                            <i style="color:red; font-size:14px; font-weight:bold;">*</i><asp:Label ID="lblsitio" runat="server" Text=""></asp:Label><asp:DropDownList ID="ddlsitio" runat="server" CssClass="comboBoxForm ddl"></asp:DropDownList></th>
                        <th>
                            <i style="color:red; font-size:14px; font-weight:bold;">*</i><asp:Label ID="lblmiembro" runat="server" Text=""></asp:Label><asp:TextBox ID="txbmiembro" runat="server" CssClass="textBoxForm s" onkeypress='return esDigito(event)'></asp:TextBox></th>
                        <th>
                            <asp:Button ID="btningresar" runat="server" Text="" CssClass="butonForm" OnClick="btningresar_Click" /></th>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="tableCont otro">
                    <tr>

                        <th colspan="2">
                            <asp:Label ID="lblanio" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txbanio" runat="server" CssClass="textBoxForm s" onkeypress='return esDigito(event)' MaxLength="4"></asp:TextBox>
                            <asp:Label ID="lblmes" runat="server" Text=""></asp:Label>
                            <asp:DropDownList ID="ddlmes" runat="server" CssClass="comboBoxForm s"></asp:DropDownList>
                            <asp:Label ID="lblsite" runat="server" Text=""></asp:Label>
                            <asp:DropDownList ID="ddlsite" runat="server" CssClass="comboBoxForm s ddl"></asp:DropDownList>

                        </th>
                        <td>
                            <asp:Button ID="btnbuscar" runat="server" Text="" CssClass="butonForm s" OnClick="btnbuscar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="gvhistorial" runat="server" CssClass="tableCont otro gray" AutoGenerateColumns="false" OnRowDataBound="gvhistorial_RowDataBound" OnRowCommand="gvhistorial_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Sitio" />
                                    <asp:BoundField DataField="MemberId" />
                                    <asp:BoundField DataField="Nombre" />
                                    <asp:BoundField DataField="Familia" />
                                    <asp:BoundField DataField="Solicitud" />
                                    <asp:BoundField DataField="Renovacion" />
                                    <asp:BoundField DataField="Usuario" />
                                    <asp:BoundField DataField="Project" Visible="false"/>
                                    <asp:ButtonField ButtonType="Button" Visible="false" HeaderText="Action/Accion" Text="Delete/Eliminar" ControlStyle-CssClass="butonForm"/>
                                 </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
    </table>
    </div>
</asp:Content>

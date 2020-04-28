<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="ApadrinadosPendAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.ApadrinadosPendAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .s {
            margin: 10px auto;
        }

        .ds {
            margin: 20px auto;
        }

        .dls {
            margin-top: -25px;
            position: absolute;
        }

        .conttenedor {
            margin-top: 25px;
        }
    </style>
    <table style="width: 100%;">
        <tr>
            <td>
                <div runat="server" class="formContGlobal" id="Ingreso">
                    <table class="tableCont pink s otro" runat="server">
                        <tr>
                            <th>
                                <asp:Label ID="lblnivel" runat="server" Text=""></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlnivel" runat="server" CssClass="comboBoxForm widthall"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblpueblo" runat="server" Text=""></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlpueblo" runat="server" CssClass="comboBoxForm widthall"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lbledad" runat="server" Text=""></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtedad" runat="server" CssClass="textBoxForm widthall" onkeypress='return esDigito(event)' MaxLength="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblgrado" runat="server" Text=""></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlgrado" runat="server" CssClass="comboBoxForm widthall"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnbuscar" runat="server" CssClass="butonForm" Text="" OnClick="btnbuscar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div runat="server" id="historial" class="formContGlobalReport">
                    <table runat="server" style="width: 100%;">

                        <tr>
                            <td>
                                <div class="conttenedor">
                                    <asp:Button ID="btnotrab" runat="server" CssClass="butonForm dls" OnClick="btnotrab_Click" />
                                    <div id="mdf">
                                        <asp:GridView ID="gvhistorial" runat="server" AutoGenerateColumns="false" CssClass="tableCont ds gray" OnRowDataBound="gvhistorial_RowDataBound" OnRowCommand="gvhistorial_RowCommand">
                                            <Columns>

                                                <asp:BoundField DataField="Nombre" HeaderText="No" />
                                                <asp:BoundField DataField="Edad" HeaderText="" />
                                                <asp:BoundField DataField="Grado" HeaderText="Organizacion" />
                                                <asp:BoundField DataField="Pueblo" HeaderText="Habla Español" />
                                                <asp:BoundField DataField="Tipo" HeaderText="Genero" />
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnSName" runat="server"
                                                            CommandName="cmdSName"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%# Eval("MemberId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <script>
        $(document).ready(function () {
            var alto = $('#pnlMenu').height();
            var ancho = $('#ContentPlaceHolder1_gvhistorial').width();
            $('#mdf').css('width', ancho+10 + 'px');
           // $('#mdf').css('height', (alto - 100) + 'px');
            $('#mdf').css('margin', '0 auto');
            $('#mdf').css('overflow-y', 'auto');
        });
    </script>
</asp:Content>

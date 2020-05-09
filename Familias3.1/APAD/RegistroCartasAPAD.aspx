<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="RegistroCartasAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.RegistroCartasAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .Paraimp {
            width: 100%;
            height: 300px;
        }
    </style>
    <div class="formContGlobal" style="margin-bottom: 10px;">
        <table class="contcart">
            <tr>
                <td style="width: 100%;">
                    <table class="tableCont" style="margin: 0 auto;">
                        <tr>
                            <th>
                                <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lblsitio" runat="server"></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlsitio" runat="server" CssClass="comboBoxForm">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lblmiembro" runat="server"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtmiembro" runat="server" CssClass="textBoxForm" onkeypress="return esDigito(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnbuscar" runat="server" CssClass="butonForm" OnClick="btnbuscar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />

                    <div class="cartcont" style="display:flex; justify-content:center;">

                        <table class="tableCont cart orange">
                            <tr>
                                <th colspan="2">
                                    <b><span class="icon-envelope-o"></span>
                                        <asp:Label ID="lbltitcart" runat="server" Text=""></asp:Label></b>
                                </th>
                            </tr>
                            <tr>
                                <td>

                                    <asp:Label ID="lbltitFecha" runat="server" CssClass="labelForm" Visible="False"></asp:Label>

                                </td>
                                <td>

                                    <asp:Label ID="lblVnota" runat="server" Visible="False" CssClass="labelForm"></asp:Label>
                                    <asp:Label ID="lblVfecha" runat="server" CssClass="labelForm" Visible="False"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <i style="color: red; font-size: 14px; font-weight: bold;">*</i>
                                    <asp:Label ID="lblcat" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcat" CssClass="comboBoxForm1" runat="server" Height="32px" Width="225px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblnotas" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnotas" runat="server" CssClass="textBoxForm1" Height="32px" Width="175px" MaxLength="40"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Agregar/Add">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ReadOnly="true" DataField="SponsorId" Visible="true" HeaderText="No" />
                                            <asp:BoundField ReadOnly="true" DataField="SponsorNames" Visible="true" HeaderText="Nombre/Name" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbltitpadrino" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVpadrino" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                    <asp:Label ID="lblVidpadrino" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button CssClass="butonForm" ID="btncaracep" runat="server" Text="" OnClick="btncaracep_Click" />
                                    <asp:Button ID="btncarcan" runat="server" CssClass="butonFormSec" Text="" OnClick="btncarcan_Click" />
                                    <asp:Button ID="btneliminar" runat="server" Text="" CssClass="butonForm" OnClick="btneliminar_Click" />
                                    <asp:Button ID="btnimprimir" runat="server" Text="" CssClass="butonForm" OnClick="btnimprimir_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvcarta" CssClass="tableCont gray" runat="server" OnSelectedIndexChanged="gvcara_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField HeaderText="Action/Accion" SelectText="Select/Seleccionar" ShowSelectButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:ListBox ID="LtbParaImprimir" CssClass="Paraimp" runat="server"></asp:ListBox>
                    <asp:Button ID="btnimpval" runat="server" Text="" CssClass="butonForm" Visible="false" OnClick="btnimpval_Click" />
                </td>
            </tr>
        </table>
        <div id="divprint" style="visibility:hidden;">
            <asp:Panel ID="pnlimprimir" runat="server"></asp:Panel>

        </div>
    </div>
    <script>
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint =
           window.open('', '', 'letf=0,top=0,width=200,height=200,toolbar=0,scrollbars=0,sta¬tus=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            // prtContent.innerHTML = strOldOne;
        }
    </script>
</asp:Content>

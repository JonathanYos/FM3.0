<%@ Page Title="" Language="C#" MasterPageFile="../mast.Master" AutoEventWireup="true" CodeBehind="ResumenAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.Resumen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="apadall" runat="server" CssClass=" formContGlobal">

        <table class="idtablap">
            <tr>

                <td class="prifila">
                    <table class="tb-Personal tableCont tb-Persona resumen idprin">
                        <tr>
                            <td rowspan="7" class="tdimage">
                                    <img class="imgapadfoto" id="imgApadFoto" runat="server" src="#" />
                                
                            </td>
                            <th colspan="2"><b><span id="Span2" class="icon-user-circle-o" runat="server"></span>
                                <asp:Label ID="lblTitleP" runat="server" Text=" Informacion Personal"></asp:Label></b></th>
                        </tr>
                        <tr>
                            <td colspan="2"><b>
                                <asp:Label ID="lblNombre" CssClass="labelBoldForm" runat="server" Text=""></asp:Label></b></td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>

                            <td>
                                <asp:Label ID="lblRestriccion" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>

                            <td>
                                <asp:Label ID="lblNacimiento" runat="server" Text=""></asp:Label>
                            </td>
                            <tr>
                                <td>


                                    <asp:Label ID="lblGrad" runat="server" Text=""></asp:Label>

                                </td>
                            </tr>
                            <tr>

                                <td>

                                    <asp:Label ID="lbltelefonos" runat="server" Text=""></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnRetomar" runat="server" CssClass="butonForm" Text="Retomar Foto" OnClick="btnRetomar_Click" />
                                    <asp:Label ID="lblretomar" runat="server" CssClass="labelBoldForm" Text=""></asp:Label>
                                </td>
                            </tr>
                    </table>
                    <asp:GridView runat="server" ID="gdvAvisos" CssClass="tb-avisos tableCont resumen red" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Aviso" />
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCondicion" Text='<%# Eval("Aviso").ToString()%>' CssClass="labelFormSec"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
            </tr>

            <tr>

                <td colspan="5" class="c-personal registro">

                    <table class="ingreso " id="ingreso" runat="server">
                        <tr>
                            <td class="separador">
                                <table class="tableCont green resumen">
                                    <tr>
                                        <th>
                                            <b><span class="icon-list-alt"></span>
                                                </b>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td class="indicador">
                                            <div class="ing2">
                                                <asp:GridView ID="gvcara" CssClass="tableCont green " runat="server"></asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tableCont resumen green" runat="server" id="cartareg">
                                    <tr>
                                        <th colspan="4">
                                            <b><span class="icon-envelope-o"></span>
                                                <asp:Label ID="lbltitcart" runat="server" Text=""></asp:Label></b>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <i style="color:red; font-size:14px; font-weight:bold;">*</i> <asp:Label ID="lblcat" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlcat" CssClass="comboBoxForm widthall" runat="server">
                                            </asp:DropDownList>
                                        </td>


                                        <td class="auto-style1">
                                            <asp:Label ID="lblnotas" runat="server" Text="" CssClass="labelForm"></asp:Label>
                                        </td>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="txtnotas" runat="server" CssClass="textBoxForm widthall" MaxLength="40" contentEditable="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="GridView1" runat="server" CssClass="centrar" AutoGenerateColumns="False" GridLines="None">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Agregar/Add">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkagregar" runat="server" Checked="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ReadOnly="true" DataField="SponsorId" Visible="true" HeaderText="No" />
                                                    <asp:BoundField ReadOnly="true" DataField="SponsorNames" Visible="true" HeaderText="Nombre/Name" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button CssClass="butonForm" ID="btncaracep" runat="server" Text="" OnClick="btncaracep_Click" /><asp:Button ID="btncarcan" runat="server" CssClass="butonFormSec" Text="" OnClick="btncarcan_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>



                    </table>
                    <table class="ingreso retornar" id="ingreso2" runat="server">
                        <tr>
                            <td class="separador">
                                <table class="tableCont resumen orange">
                                    <tr>
                                        <th>
                                            <b><span class="icon-list-alt"></span>
                                                </b>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ing2">
                                                <asp:GridView ID="gvregalo" CssClass="tableCont resumen orange" runat="server"></asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tableCont ingfotos tbsize resumen orange" runat="server" id="regalreg">
                                    <tr>
                                        <th colspan="4"><b><span class="icon-gift "></span>
                                            <asp:Label ID="lbltitreg" runat="server" CssClass="" Text=""></asp:Label></b></th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <i style="color:red; font-size:14px; font-weight:bold;">*</i><asp:Label ID="lblcatrel" runat="server" CssClass="labelForm" Text=""></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlcatrel" CssClass="comboBoxForm widthall" runat="server"></asp:DropDownList></td>
                                        <td>
                                            <asp:Label ID="lblrelnotas" CssClass="labelForm" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtnotasrel" runat="server" CssClass="textBoxForm widthall" MaxLength="40"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbltyperel" CssClass="labelForm" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddltyperel" CssClass="comboBoxForm widthall" runat="server"></asp:DropDownList></td>
                                        <td></td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="btnregacep" CssClass="butonForm" runat="server" Text="" OnClick="btnregacep_Click" /><asp:Button ID="btnregcan" runat="server" CssClass="butonFormSec" Text="" OnClick="btnregcan_Click" /></td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="space">
                <td style="display:flex; justify-content:center;">
                    <table class="tableCont resumen purple top10" runat="server" id="pad">
                        <tr>
                            <th>
                                <b><span class="icon-users"></span>
                                    <asp:Label ID="lbltp" runat="server"></asp:Label></b>
                            </th>
                        </tr>
                        <tr>

                            <td>
                                <asp:GridView ID="gvpadrinos" AutoGenerateColumns="false" CssClass="gv-padrinos tableCont resumen " runat="server" OnRowDataBound="gvpadrinos_RowDataBound" OnRowCommand="gvpadrinos_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSName" runat="server"
                                                    CommandName="cmdSName"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    Text='<%# Eval("SponsorId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" />
                                        <asp:BoundField DataField="StartDate" />
                                        <asp:BoundField DataField="EndDate" />
                                        <asp:BoundField DataField="Type Sponsor" />
                                        <asp:BoundField DataField="Gender" />
                                        <asp:BoundField DataField="Speak Spanish" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
        <script>

            $(document).ready(function () {

                $('.scarta').css({
                    "margin": "auto auto",
                    "border": "1px solid gray",
                    "border-bottom-left-radius": "10px",
                    "border-bottom-right-radius": "10px",
                    "border-top-left-radius": "10px",
                    "border-top-right-radius": "10px",
                    "width": "50px",
                    "height": "10px"
                })
                $('.pcarta').css({
                    "margin": "auto auto",
                    "border": "1px solid gray",
                    "border-bottom-left-radius": "10px",
                    "border-bottom-right-radius": "10px",
                    "border-top-left-radius": "10px",
                    "border-top-right-radius": "10px",
                    "width": "50px",
                    "height": "10px"
                })
                var tamanooriginal = $('.idprin').width();
                var anchoavisos = $('#ContentPlaceHolder1_gdvAvisos').width();
                var anchoocarta = $('#ContentPlaceHolder1_ingreso').width();
                var anchoregalo = $('#ContentPlaceHolder1_ingreso2').width();
                var anchototal = $("html").width();
                var anchopadrinos = $('#ContentPlaceHolder1_pad').width();
                var tamanoancho = anchoocarta + 10 + anchoregalo;
                var tamanoavisos = anchoavisos + 25;
                var tamanototaltabla = tamanoancho - tamanoavisos;
                var tamanopad = anchopadrinos - tamanoavisos + 10;
                if (anchoocarta <= 0 && anchoregalo <= 0) {
                    $('.idprin').css('max-width', tamanopad + 'px');
                    $('.idprin').css('width', '80%');
                } else {
                    if (tamanoancho < anchototal) {
                        $('.idprin').css('max-width', tamanototaltabla + 'px');
                        $('.idprin').css('width', '80%');
                        tamanoancho = tamanoancho + 30;
                        $('.idtablap').css('width', tamanoancho + 'px');
                        $('.idtablap').css('margin', '0 auto');
                        $('#ContentPlaceHolder1_imgApadFoto').css({
                            "width": "170px",
                            "height": "200px"
                        });

                    } else {

                    }
                }

            });
        </script>
        <style>
            .top10 {
            margin-top:10px;
            }
        </style>
    </asp:Panel>
</asp:Content>

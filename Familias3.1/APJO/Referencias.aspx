<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="Referencias.aspx.cs" Inherits="Familias3._1.APJO.Referencias1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;<style>
              .ocultar {
                  display: none;
              }

              .modificar {
                  background: #fff;
                  margin: 0 auto;
                  border-radius: 3px;
                  width: 400px;
                  text-align: center;
              }

              .textalignc {
                  text-align: center;
              }

              .most {
                  /*display:block;*/
                  visibility: visible;
                  opacity: 1;
                  transition-delay: 0s;
              }

              .pantalla {
                  background: #fff;
                  box-shadow: 0px 8px 16px 8px rgba(0,0,0,0.2);
              }

              .oizquierda {
                  margin: 10px auto;
                  width: 250px;
                  background: #fff;
                  border-radius: 3px;
              }

              .oderecha {
                  margin: 10px auto;
              }

              .allcenter {
                  margin: 0 auto;
              }

              .tamano200 {
                  width: 175px;
              }

              .borde0 {
                  border: 0;
              }

              .izquierda {
                  border: 1px solid #cbcbcb;
                  margin: 10px auto;
              }

              .derecha {
                  border: 1px solid #cbcbcb;
              }

              .mitad {
                  /*width:50%;*/
                  float: left;
                  display: block;
              }

              .height100 {
                  height: 100px;
              }

              .ocultar {
                  display: none;
              }

              .ordenar {
                  width: 100%;
              }

                  .ordenar tr td input {
                      margin-left: 55px;
                  }

              .refderecha {
                  top: -1px;
                  position: relative;
                  left: 0px;
              }

              .todotamano {
                  width: 100%;
              }

              .descripcion {
                  width: 100%;
                  height: 60px;
                  min-width: 450px;
                  border-radius: 3px;
              }

              .formContBody {
                  box-sizing: border-box;
                  position: relative;
                  padding: 0px 15px;
                  position: static;
              }

              .direccion {
                  font-size: 15px;
                  color: #585858;
              }

              .tamano10 {
                  font-size: 10px;
              }

              .bbff {
                  background: #fff;
              }

              .floatl {
                  float: left;
              }
          </style>
    <div class="formContGlobal">
        <div class="oizquierda">
            <table border="0" style="text-align: center;">
                <tr>
                    <td>
                        <asp:Label ID="lblfamilia" runat="server" CssClass="labelBoldForm"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtmiembro" runat="server" CssClass="textBoxForm widthall textalignc" onkeypress='return esDigito(event)'></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnbuscarfam" CssClass="butonForm" runat="server" OnClick="btnbuscarfam_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:GridView ID="gvhistorial" CssClass="tableCont gray oderecha" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvhistorial_RowDataBound" OnRowCommand="gvhistorial_RowCommand">
            <Columns>
                <asp:BoundField DataField="MemberId" ItemStyle-CssClass="miembro" />
                <asp:BoundField DataField="LastNames" ItemStyle-CssClass="apellido" />
                <asp:BoundField DataField="FirstNames" ItemStyle-CssClass="nombre" />
                <asp:BoundField DataField="Cumpleaños/Edad" />
                <asp:BoundField DataField="Clasificación" />
                <asp:TemplateField HeaderText="Accion/Action" Visible="True">
                    <ItemTemplate>
                        <asp:Button ID="btnreferirt" CommandName="Referir" CssClass="butonForm floatl" Text="Referir/Refer" runat="server" />
                        <asp:Button ID="btnhistorial" runat="server" CommandName="VerHistorial" CssClass="butonFormSec" Text="Historial/History" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Panel ID="pnlfiltros" CssClass="oizquierda" runat="server">
            <table border="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblnombre" CssClass="labelBoldForm" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbljornada" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddljornada" runat="server" CssClass="comboBoxForm tamano200"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblprograma" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlprograma" runat="server" CssClass="comboBoxForm tamano200" AutoPostBack="True" OnSelectedIndexChanged="ddlprograma_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblsubprograma" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlsubprograma" runat="server" CssClass="comboBoxForm tamano200"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="izquierda " border="0">
                            <tr>
                                <td style="font-size: 10px;">Busqueda de Programas por Dia</td>
                            </tr>
                            <tr>
                                <td rowspan="3">
                                    <asp:CheckBoxList ID="cbldias" CssClass="CheckBoxListForm derecha25 ordenar" runat="server"></asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnbuscar" runat="server" CssClass="butonForm solicitud2" OnClick="btnbuscar_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlHistorial" Style="margin: 0 auto;">
            <asp:Label ID="lblmiembroselec" runat="server" CssClass="labelForm bbff"></asp:Label>
            <asp:GridView ID="gvhistorialmiembro" CssClass="tableCont" runat="server" OnRowCommand="gvhistorialmiembro_RowCommand" AutoGenerateColumns="false" OnRowDataBound="gvhistorialmiembro_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="EducationActivityType" ItemStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" />
                    <asp:BoundField DataField="Actividad" />
                    <asp:BoundField DataField="Fecha de Referencia" />
                    <asp:BoundField DataField="SubPrograma" />
                    <asp:BoundField DataField="Programa" />
                    <asp:BoundField DataField="Jornada" />
                    <asp:BoundField DataField="dia" />
                    <asp:BoundField DataField="Comentarios" />
                    <asp:TemplateField HeaderText="Accion/Action" Visible="True">
                        <ItemTemplate>
                            <asp:Button ID="btnmodificarf" CommandName="Modificar" CssClass="butonForm" Text="Modificar/Modify" runat="server" />
                            <asp:Button ID="btneliminarf" CommandName="Eliminar" runat="server" CssClass="butonFormSec" Text="Eliminar/Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="VCodigo" runat="server"></asp:HiddenField>
    <script>
        function val(d) {
            var valor = d;
            $("#ContentPlaceHolder1_VCodigo").val(d);
        }


        $(document).ready(function () {
            try {
                var alto = $('#pnlMenu').height();
                var ancho = $('#ContentPlaceHolder1_gvhistorialmiembro').width();
                $('#ContentPlaceHolder1_pnlHistorial').css('width', ancho + 'px');
                $(".solicitud2").text = "Seleccionar/Select";
                $('.dfd').css('height', alto - 200 + 'px');
                $("#ContentPlaceHolder1_btnReferir").css('display', 'none');
                $(".combos").change(function () {
                    $(".combos ").not(this).prop('selectedIndex', '0');
                    $(".combos ").not(this).siblings().css('display', 'none');
                    //   $(this).siblings().css('display', 'inline-block');
                });

            } catch (e) {
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Panel ID="pnlCalendario" runat="server" CssClass="textalignc" Style="margin: 0 auto; width: 640px;" Visible="false">
        <table class="todotamano">
            <tr>
                <td>
                    <asp:Label ID="lbldescripcion" CssClass="labelForm bbff" runat="server"></asp:Label><span id="cntF" style="color: #000; float: right; font-size: 10px;"></span>
                    <asp:TextBox ID="txtdescripcion" CssClass="descripcion tamano10" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="dfd" style="height: 300px; overflow-y: auto;">
            <asp:Table runat="server" ID="tblEstadisticas" CssClass="tableContInfo refderecha">
                <asp:TableRow>
                    <asp:TableHeaderCell>Día</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Programa</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Subprograma</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Actividad</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <asp:Button ID="btnReferir" runat="server" CssClass="butonForm" Text="Referir" OnClick="btnReferir_Click" />
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlModificar" CssClass="modificar" Visible="false">
        <table class="widthall" style="text-align: center;">
            <tr>
                <td>
                    <asp:Label ID="lblmodificicacion" CssClass="labelForm bbff" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNotasReferenciar" CssClass="labelForm bbff" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtmod" CssClass="textBoxBlueForm widthall height100" runat="server" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnmodicar" runat="server" CssClass="butonForm" OnClick="btnmodicar_Click" />
                    <asp:Button ID="btncancelar" runat="server" CssClass="butonFormSec" OnClick="btncancelar_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

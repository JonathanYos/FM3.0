<%@ Page Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="Seguimiento.aspx.cs" Inherits="Familias3._1.APJO.Seguimiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblPrograma" runat="server" Text="" class="labelBoldForm">Programa:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center" colspan="2">
                    <asp:DropDownList ID="ddlPrograma" runat="server" class="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ddlPrograma_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;<asp:Label ID="lblSubPrograma" runat="server" Text="" class="labelBoldForm">Subprograma:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlSubPrograma" runat="server" class="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ddlSubPrograma_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td></td>
                <td>&nbsp;&nbsp;<asp:Label ID="lblActividad" runat="server" Text="" class="labelBoldForm">Actividad:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center" colspan="6">
                    <asp:DropDownList ID="ddlActividad" runat="server" class="comboBoxBlueForm" AutoPostBack="true" OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td colspan="7"></td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar Referencias" CssClass="butonForm" Visible="false" OnClick="btnBuscar_Click" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align: center"></td>
                <td colspan="15px"></td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align: center"></td>

                <td colspan="15px"></td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlReferencias" Visible ="false" CssClass="formContTransModal">
            <table class="tblCenter">
                <tr>
                    <td style="text-align: center">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="butonForm" Visible="false" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Ver Excluidos" CssClass="butonFormSec" Visible="false" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
            <div style="height: 10px"></div>
            <table class="tableContInfo tblCenter blue">
                <tr>
                    <th>
                        <asp:Label runat="server" ID="lblReferencias">Referencias</asp:Label>
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gdvReferencias" CssClass="tableCont" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="MemberId" />
                                <asp:BoundField DataField="EducationActivityType" />
                                <asp:BoundField DataField="Status" />
                                <asp:BoundField DataField="Miembro" HeaderText="Miembro" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="Edad" HeaderText="Edad" />
                                <asp:BoundField DataField="TipoMiembro" HeaderText="Tipo de Miembro" />
                                <asp:BoundField DataField="Educacion" HeaderText="Info. Educativa" />
                                <asp:TemplateField HeaderText="Semáforo">
                                    <ItemTemplate>
                                        <asp:Panel ID="pnlSemaforo" runat="server" CssClass="pnlVSemaforo" BackColor='<%# (Eval("Semaforo").ToString()=="Rojo") ? colorMalo :  (Eval("Semaforo").ToString()=="Amarillo") ? colorRegular : colorBueno%>'></asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TS" HeaderText="Trabajador Social" />
                                <asp:BoundField DataField="Familia" HeaderText="Familia" />
                                <asp:BoundField DataField="NotasRef" HeaderText="Nota Ref." HtmlEncode="false" />
                                <asp:TemplateField HeaderText="Nota Encargado">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txbNotaEncargado" runat="server" Text='<%# Eval("NotasEnc").ToString() %>' CssClass="textBoxForm" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" Checked="true" onclick="javascript:CopyCheckStateByColumn(this,this.offsetParent.offsetParent.id);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkModificar" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <script>
        $(document).ready(function () {
            $('.secTbl Table').eq(1).css({
                "width": "1000px"
            });
            $('.pnlVSemaforo').css({
                "margin": "auto auto",
                "border": "1px solid gray",
                "border-bottom-left-radius": "10px",
                "border-bottom-right-radius": "10px",
                "border-top-left-radius": "10px",
                "border-top-right-radius": "10px",
                "width": "50px",
                "height": "10px"
            });
        });
    </script>
    <script>
        function CopyCheckStateByColumn(HeaderCheckBox, gridViewName) {
            var columnIndex = HeaderCheckBox.parentElement.cellIndex;
            var newState = HeaderCheckBox.checked;
            ChangeChecksByColumn(gridViewName, newState, columnIndex);
        }
        function ChangeChecksByColumn(gridViewName, newState, columnIndex) {
            var tabla = document.getElementById(gridViewName);
            var nFilas = tabla.rows.length;
            var nColumnas = 5;
            var nCeldas = nColumnas * nFilas;
            for (var i = 1; i < nFilas; i++) {
                if ((tabla.rows[i].cells[columnIndex].firstElementChild.type == "checkbox") &&
                    (tabla.rows[i].cells[columnIndex].firstElementChild.checked != newState)) {
                    tabla.rows[i].cells[columnIndex].firstElementChild.click();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <asp:Panel runat="server" ID="pnlActualizar" CssClass="formContTransModal" Visible="false">
        <table class="tblCenter">
            <tr>
                <td>
                    <asp:Label ID="lblNombre" runat="server" Text="" class="labelBoldForm">Nombre:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblVNombre" runat="server" class="labelForm">Domingo Eliseo</asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblActComentarios" runat="server" Text="Comentarios:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center" colspan="4">
                    <asp:TextBox ID="txbActComentarios" runat="server" Text="" CssClass="textBoxBlueForm"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblActImpresiones" runat="server" Text="Impresiones:" CssClass="labelBoldForm"></asp:Label>
                </td>
                <td style="text-align: center" colspan="4">
                    <asp:TextBox ID="txbActImpresiones" runat="server" Text="" CssClass="textBoxBlueForm num3" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblActHoraSalida" runat="server" Text="" Visible="false" CssClass="labelBoldForm">Hora Salida:</asp:Label>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:TextBox runat="server" ID="txbActHoraSalida" TextMode="Time" Visible="false" CssClass="textBoxBlueForm date noPaste" contentEditable="false"></asp:TextBox>
                    <asp:CheckBox ID="chkSalida" runat="server" />
                    <asp:Label ID="lblSalida" runat="server" Text="Salida" CssClass="labelBoldForm"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="butonForm" OnClick="btnActualizar_Click" />
                    <asp:Button ID="btnCancelarAct" runat="server" Text="Cancelar" CssClass="butonFormSec" OnClick="btnCancelarAct_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

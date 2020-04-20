<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="Visitas.aspx.cs" Inherits="Familias3._1.TS.RegistrarVisitas" %>

<%@ MasterType VirtualPath="~/mast.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal" style="padding:10px 5px;">
        <div>
            <table class="tblCenter">
                <tr>
                    <td>
                        <asp:Label ID="lblTS" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblVTS" runat="server" class="labelForm"></asp:Label>
                    </td>
                    <td></td>
                    <td>&nbsp;&nbsp;<asp:Label ID="lblClasif" runat="server" Text="" class="labelBoldForm">Clasificaciòn:</asp:Label>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblVClasif" runat="server" class="labelForm"></asp:Label>
                    </td>
                    <td></td>
                    <td>&nbsp;&nbsp;<asp:Label ID="lblTelef" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblVTelef" runat="server" class="labelForm"></asp:Label>
                    </td>
                    <td></td>
                    <td>&nbsp;&nbsp;<asp:Label ID="lblDirec" runat="server" Text="" class="labelBoldForm"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblVDirec" runat="server" class="labelForm"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 30px"></div>
        <div>
            <table class="tblCenter">
                <tr>
                    <th style="width: 10px"></th>
                    <th style="width: 10px"></th>
                    <th style="width: 10px"></th>
                    <th style="width: 10px"></th>
                    <th style="width: 10px"></th>
                </tr>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <div style="display: flex">
                                        <asp:Panel ID="pnlRegistro" runat="server">
                                            <table>
                                                <tr>
                                                    <td class="left">
                                                        <asp:Label ID="lblFVisita" runat="server" Text="" class="labelBoldForm"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txbFVisita" runat="server" class="textBoxBlueForm date noPaste" AutoCompleteType="Disabled" contentEditable="false"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txbFVisita_CalendarExtender" runat="server" BehaviorID="txbFVisita_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txbFVisita"></ajaxToolkit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="revTxbFVisita" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txbFVisita" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="revTxbFVisita_ValidatorCalloutExtender" runat="server" BehaviorID="revTxbFVisita_ValidatorCalloutExtender" TargetControlID="revTxbFVisita">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <asp:Label ID="lblVFVisita" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="left">
                                                        <asp:Label ID="lblTipoV" runat="server" Text="" class="labelBoldForm"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlVTipoV" runat="server" class="comboBoxBlueForm"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="revDdlVTipoV" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="ddlVTipoV" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="revDdlVTipoV_ValidatorCalloutExtender" runat="server" BehaviorID="revDdlVTipoV_ValidatorCalloutExtender" TargetControlID="revDdlVTipoV">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <asp:Label ID="lblVTipoV" runat="server" CssClass="labelForm" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnGuardar" runat="server" Text="" class="butonForm" ValidationGroup="grpGuardar" OnClick="btnGuardar_Click" />
                                                        <asp:Button ID="btnEliminar" runat="server" Text="" class="butonFormSec" Visible="false" OnClick="btnEliminar_Click" />
                                                        <asp:Button ID="btnNuevaVst" runat="server" Text="" class="butonFormRet" Visible="false" OnClick="btnNuevaVst_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <div style="width: 10px"></div>
                                        <asp:Panel runat="server" ID="pnlObjetivos" Style="height: 160px">
                                            <table class="tableContInfo blue" style="width: 100%">
                                                <tr>
                                                    <th>
                                                        <asp:Label runat="server" ID="lblObjetivos">Objetivos de Visita</asp:Label>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView runat="server" ID="gdvObjetivos" Style="width: 100%" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="Objetivo" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblAplica" Text='<%# (Eval("Tiene").ToString()=="1") ? dic.Si : dic.No %>' Visible="false"></asp:Label>
                                                                        <asp:CheckBox ID="chkAplica" runat="server"
                                                                            Checked='<%#Convert.ToBoolean(Eval("Tiene")) %>'
                                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                            CommandName="cmdSeleccionar" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Des" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="2">
                        <asp:Panel runat="server" ID="pnlVisitas" CssClass="scroll-Y" Style="height: 160px">
                            <%--<div id="tblVisitasHead"></div>--%>
                            <asp:Table runat="server" ID="tblVisitas" CssClass="tableContInfo gray">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        <asp:Label runat="server" ID="lblVisitas"></asp:Label>
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <%--    <div id="gdvVisitasHead"></div>--%>
                                        <asp:GridView ID="gdvVisitas" runat="server" class="tableContInfo gray" Visible="true" AutoGenerateColumns="false" OnRowCommand="gdvVisitas_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="Des" />
                                                <asp:BoundField DataField="VisitDate" />
                                                <asp:BoundField DataField="VisitDateUser" />
                                                <asp:BoundField DataField="UserId" />
                                                <asp:BoundField DataField="VisitType" />
                                                <asp:BoundField DataField="CreationDateTime" />
                                                <%--<asp:BoundField DataField="Enfoques" />--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSeleccionar" runat="server"
                                                            CommandName="cmdSeleccionar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text='<%#dic.seleccionar%>' CssClass="butonFormTable" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="lblNoTiene" Visible="false" CssClass="labelFormSec"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 30px"></div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 200px">
                        <asp:Panel ID="pnlFam" runat="server">
                            <table class="tableContInsert blue">
                                <tr>
                                    <th class="center" colspan="3">
                                        <asp:Label ID="lblFam" runat="server" Text="Familia:"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th style="width: 15px"></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAlcoh" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblAlcoh" runat="server" Text="Alcoholismo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkDroga" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblDroga" runat="server" Text="Drogadicción"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkEcon" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblEcon" runat="server" Text="Ecomomía"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkVIF" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblVIF" runat="server" Text="VIF"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <%--<br />--%>
                                        <div style="height: 22px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="lblObs1" Text="Observaciones:"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <span id="cntF"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txbFam" class="txbFam txbCat" TextMode="MultiLine" Columns="30" Rows="7" runat="server" BackColor="#F3B2CE" onkeyup='this.value=verificaLongitud(this.value, 750, "cntF")'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="height: 200px">
                        <asp:Panel ID="pnlEduc" runat="server">
                            <table class="tableContInsert blue">
                                <tr>
                                    <th class="center" colspan="3">
                                        <asp:Label ID="lblEduca" runat="server" Text="Educación:"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th style="width: 15px"></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkDeser" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblDeser" runat="server" Text="Deserción"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkProb" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblProb" runat="server" Text="Problemas de Aprendizaje"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkRend" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblRend" runat="server" Text="Rendimiento Académico"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkRepi" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblRepi" runat="server" Text="Repitencia"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <%-- <br />--%>
                                        <div style="height: 22px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="lblObs3" Text="Observaciones:"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <span id="cntE"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txbEduc" class="txbEduc txbCat" TextMode="MultiLine" Columns="30" Rows="7" runat="server" BackColor="#FFDE9D" onkeyup='this.value=verificaLongitud(this.value, 750, "cntE")'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="height: 200px">
                        <asp:Panel ID="pnlSalud" runat="server">
                            <table class="tableContInsert blue" style="width: auto">
                                <tr>
                                    <th class="center" colspan="3">
                                        <asp:Label ID="lblSalud" runat="server" Text="Salud:"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th style="width: 15px"></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkCron" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblCron" runat="server" Text="Crónica"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkEmoc" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblEmoc" runat="server" Text="Emocional"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkEnf" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblEnf" runat="server" Text="Enfermedades Primarias"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <%--<br />--%>
                                        <div style="height: 22px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="lblObs2" Text="Observaciones:"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <span id="cntS"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txbSalud" class="txbSalud txbCat" TextMode="MultiLine" Columns="30" Rows="7" runat="server" BackColor="#D3C5F3" onkeyup='this.value=verificaLongitud(this.value, 750, "cntS")'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="height: 200px">
                        <asp:Panel ID="pnlLeg" runat="server">
                            <table class="tableContInsert blue">
                                <tr>
                                    <th class="center" colspan="3">
                                        <asp:Label ID="lblProbl" runat="server" Text="Problemas Legales:"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th style="width: 10px"></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAnt" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblAnt" runat="server" Text="Antecendentes Legales y Polic."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkDivo" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblDivo" runat="server" Text="Divorcio"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkDPI" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblDPI" runat="server" Text="DPI"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkEsc" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblEsc" runat="server" Text="Escritura"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkPens" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblPens" runat="server" Text="Pensión Alimenticia"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkRect" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblRect" runat="server" Text="Rectificación y Partida de Nacim."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="lblObs4" Text="Observaciones:"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <span id="cntL"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txbLeg" class="txbLeg txbCat" TextMode="MultiLine" Columns="32" Rows="7" runat="server" BackColor="#9FD8F9" onkeyup='this.value=verificaLongitud(this.value, 600, "cntL")'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="pnlVnd" runat="server">
                            <table class="tableContInsert blue" style="right: 0px">
                                <tr>
                                    <th class="center" colspan="3">
                                        <asp:Label ID="lblVnd" runat="server" Text="Vivienda:"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th style="width: 15px"></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <%--<br />--%>
                                        <div style="height: 28px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="lblObs5" Text="Observaciones:"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <span id="cntV"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txbVnd" class="txbVnd txbCat" TextMode="MultiLine" Columns="30" Rows="7" runat="server" BackColor="#FFC7A1" onkeyup='this.value=verificaLongitud(this.value, 600, "cntV")'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            //$('.contenedor').css({
            //    "border": "1px solid black"
            //});
            //$('.contenedor div').css({
            //    "border": "1px solid black",
            //});
            //$('.contenedor div .secTable').css({
            //    "max-height": "20px"
            //});
            //$('.contenedor div table').css({
            //    "border": "1px solid green",
            //});
            //$('.txbCat').css({
            //});
            //$('.contenedor .espacio').css({
            //});
        });
    </script>
    <script type="text/javascript">
        function verificaLongitud(string, int, idSpan) {
            var out = '';
            for (var i = 0; i < int; i++) {
                out += string.charAt(i);
            }
            var span = document.getElementById(idSpan);
            var dif = int - out.length
            span.innerHTML = dif + ' / ' + int;
            return out;
        }

        $(document).ready(function () {
            //var MaxLength = 750;
            //$('#ContentPlaceHolder1_txbFam').keypress(function (e) {
            //    if ($(this).val().length >= MaxLength) {
            //        e.preventDefault();
            //    }
            //});
            var maxLength = 750;
            var maxLength2 = 600;
            try {
                var txb = document.getElementById("ContentPlaceHolder1_txbFam");
                var spn = document.getElementById("cntF");
                spn.innerHTML = (maxLength - txb.value.length) + " / " + maxLength;

                txb = document.getElementById("ContentPlaceHolder1_txbSalud");
                spn = document.getElementById("cntS");
                spn.innerHTML = (maxLength - txb.value.length) + " / " + maxLength;

                txb = document.getElementById("ContentPlaceHolder1_txbLeg");
                spn = document.getElementById("cntL");
                spn.innerHTML = (maxLength2 - txb.value.length) + " / " + maxLength2;

                txb = document.getElementById("ContentPlaceHolder1_txbEduc");
                spn = document.getElementById("cntE");
                spn.innerHTML = (maxLength - txb.value.length) + " / " + maxLength;

                txb = document.getElementById("ContentPlaceHolder1_txbVnd");
                spn = document.getElementById("cntV");
                spn.innerHTML = (maxLength2 - txb.value.length) + " / " + maxLength2;
            } catch (e) {

            }
        });
    </script>
    <%--    <script>
        $(document).ready(function () {
            var visitHeader = $('#<%=tblVisitas.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                      $(visitHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                      $('#<%=tblVisitas.ClientID%> tr th').each(function (i) {
                      // Here Set Width of each th from gridview to new table(clone table) th 
                      $("th:nth-child(" + (i + 1) + ")", visitHeader).css('width', ($(this).width()).toString() + "px");
                  });
                  $("#tblVisitasHead").append(gridHeader);
                  $('#tblVisitasHead').css('position', 'absolute');
                  $('#tblVisitasHead').css('top', $('#<%=tblVisitas.ClientID%>').offset().top);
                  });
    </script>
    <script>
        $(document).ready(function () {
            var gridHeader = $('#<%=gdvVisitas.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
            $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
            $('#<%=gdvVisitas.ClientID%> tr th').each(function (i) {
                          // Here Set Width of each th from gridview to new table(clone table) th 
                          $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
                      });
                      $("#gdvVisitasHead").append(gridHeader);
                      $('#gdvVisitasHead').css('position', 'absolute');
                      $('#gdvVisitasHead').css('top', $('#<%=gdvVisitas.ClientID%>').offset().top);
        });
    </script>--%>
</asp:Content>

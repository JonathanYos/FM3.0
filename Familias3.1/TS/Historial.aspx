<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="Historial.aspx.cs" Inherits="Familias3._1.TS.Historial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .contenedor2 {
            width: 80%;
        }

        .espacio {
            margin-top: 10px;
            padding-left: 11px;
        }

        .quitarpixeles {
            padding: 0;
        }

        .espacio2 {
            margin-top: 10px;
            padding-left: 11px;
        }
    </style>
    <div class="contenedor2 formContGlobal">
        <table>
            <tr>
                <td style="height: 10px"></td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp; &nbsp;
                    <asp:Label runat="server" ID="lblTS" CssClass="labelMediumForm"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 10px"></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label runat="server" ID="lblAntecedentes" CssClass="labelMediumForm">Antecedentes Familiares</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp; &nbsp;
                    <asp:Label runat="server" ID="lblAñoAfil" CssClass="labelMediumForm"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlMiembros" CssClass="espacio2">
                        <table class="tableContInfo blue" style = "width:100%">
                            <tr>
                                <th>
                                    <asp:Label ID="lblInfoGeneral" runat="server">Información General</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvMiembros" runat="server" style = "width:100%" CssClass="tableContInfo blue" AutoGenerateColumns="false" OnRowCommand="gdvMiembros_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnMName" runat="server"
                                                        CommandName="cmdMName"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        Text='<%# Eval("MemberId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Nombre" />
                                            <asp:BoundField DataField="Nacimiento" />
                                            <asp:BoundField DataField="Afiliación" />
                                            <asp:BoundField DataField="Celular" />
                                            <asp:BoundField DataField="Relacion" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlAñoEduc" CssClass="espacio">
                        <table class="tableContInfo yellow"  style = "width:100%">
                            <tr>
                                <th>
                                    <asp:Label ID="lblAñoEduc" runat="server">Educación</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvAñoEduc" style = "width:100%" runat="server" CssClass="tableContInfo yellow" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Nombre" />
                                            <asp:BoundField DataField="Informacion" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlOcupaciones" CssClass="espacio">
                        <table class="tableContInfo pink"  style = "width:100%">
                            <tr>
                                <th>
                                    <asp:Label ID="lblOcupaciones" runat="server">Ocupaciones</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvOcupaciones"  style = "width:100%" runat="server" CssClass="tableContInfo pink" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Nombre" />
                                            <asp:BoundField DataField="Ocupacion" />
                                            <asp:BoundField DataField="FechaInicio" />
                                            <asp:BoundField DataField="IngresosMensuales" />
                                            <asp:BoundField DataField="Jornada" />
                                            <asp:BoundField DataField="HorasSemanales" />
                                            <asp:BoundField DataField="TieneIGGSAfil" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblOcupNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlIngresosExtra" CssClass="espacio">
                        <table class="tableContInfo purple"  style = "width:100%">
                            <tr>
                                <th>
                                    <asp:Label ID="lblIngresosExtra" runat="server">Ingresos Extra</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvIngresosExtra"  style = "width:100%" runat="server" CssClass="tableContInfo purple" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Tipo" />
                                            <asp:BoundField DataField="FechaInicio" />
                                            <asp:BoundField DataField="IngresosMensuales" />
                                            <asp:BoundField DataField="Nota" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblIngExtNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label runat="server" ID="lblMedioAmbiente" CssClass="labelMediumForm">Medio Ambiente</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlMedioAmbiente" CssClass="espacio">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUbicacionTerreno" runat="server" CssClass="labelForm"></asp:Label>
                                    <asp:Label ID="lblVUbicacionTerreno" runat="server" CssClass="labelForm"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="mainTable">
                                        <tr>
                                            <th></th>
                                            <th style="width: 30px"></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="tableContInfo green">
                                                    <tr>
                                                        <td class="center" colspan="4">
                                                            <asp:Label ID="vvnLblCasa" runat="server" Text="Casa:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td class="center"></td>
                                                                    <td class="center">
                                                                        <asp:Label ID="vvnLblMaterialCsa" runat="server" Text="Material"></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="vvnLblCalidadCsa" runat="server" Text="Calidad"></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="vvnLblNotasCsa" runat="server" Text="Notas"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="left">
                                                                        <asp:Label ID="vvnLblParedCsa" runat="server" Text="Pared"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVMatParedCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVCaldParedCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVNotasParedCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="left">
                                                                        <asp:Label ID="vvnLblTechoCsa" runat="server" Text="Techo"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVMatTechoCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVCaldTechoCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVNotasTechoCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="left">
                                                                        <asp:Label ID="vvnLblPisoCsa" runat="server" Text="Piso"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVMatPisoCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVCaldPisoCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVNotasPisoCsa" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td>
                                                <table class="tableContInfo green">
                                                    <tr class="center">
                                                        <td class="center" colspan="4">
                                                            <asp:Label ID="vvnLblCocina" runat="server" Text="Cocina:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td class="center"></td>
                                                                    <td class="center">
                                                                        <asp:Label ID="vvnLblMaterialCcna" runat="server" Text="Material"></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="vvnLblCalidadCcna" runat="server" Text="Calidad"></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="vvnLblNotasCcna" runat="server" Text="Notas"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="left">
                                                                        <asp:Label ID="vvnLblParedCcna" runat="server" Text="Pared"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVMatParedCcna" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVCaldParedCcna" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVNotasParedCcna" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="left">
                                                                        <asp:Label ID="vvnLblTechoCcna" runat="server" Text="Techo"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVMatTechoCcna" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVCaldTechoCcna" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="vvnLblVNotasTechoCcna" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 15px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="tableContInfo green">
                                                    <tr>
                                                        <td class="center" colspan="4">
                                                            <asp:Label ID="vvnLblOtros" runat="server" Text="Otros:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblNoCuartos" runat="server" Text="# Cuartos"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVNoCuartos" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblHigiene" runat="server" Text="Higiene"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVHigiene" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblNotasHigiene" runat="server" Text="Notas Higiene"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVHigieneNotas" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%--                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblSegundoNivel" runat="server" Text="Tiene Segundo Nivel"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVSegundoNivel" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td>
                                                <table class="tableContInfo green">
                                                    <tr>
                                                        <td class="center" colspan="4">
                                                            <asp:Label ID="vvnLblServicios" runat="server" Text="Servicios"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblAgua" runat="server" Text="Agua"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVAgua" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblElectricidad" runat="server" Text="Electricidad"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVElectricidad" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblDrenaje" runat="server" Text="Drenaje"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="vvnlblVDrenaje" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblExcretas" runat="server" Text="Baño"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVExcretas" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                               <td>
                                                <table class="tableContInfo">
                                                    <tr>
                                                        <td class="center" colspan="4">
                                                            <asp:Label ID="vvnLblTerreno" runat="server" Text="Terreno:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblTenencia" runat="server" Text="Tenencia"> </asp:Label></td>
                                                        <td colspan="3">
                                                            <asp:Label ID="vvnLblVTenencia" runat="server" class="labelForm"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblTamaño" runat="server" Text="Tamaño Terreno"> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVTamañoX" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <span class="labelForm">X </span></td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVTamañoY" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblJardín" runat="server" Text="Tamaño Área Verde"> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVTamañoXJardin" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <span class="labelForm">X </span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVTamañoYJardin" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">
                                                            <asp:Label ID="vvnLblTieneEsc" runat="server" Text="Tiene Escritura" > </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vvnLblVTieneEsc" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>--%>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label runat="server" ID="lblSituacionSocial" CssClass="labelMediumForm">Situación Social</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="espacio">
                    <asp:Panel runat="server" ID="pnlFamilia" CssClass="limite">
                        <table class="tableContInfo blue">
                            <tr>
                                <th class="quitarpixeles">
                                    <asp:Label ID="lblFamilia" runat="server">Familia:</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvFamilia" runat="server" CssClass="tableContInfo blue" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" />
                                            <asp:BoundField DataField="Notas" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblFamNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlEducacion" CssClass="block special">
                        <table class="tableContInfo yellow">
                            <tr>
                                <th>
                                    <asp:Label ID="lblEducacion" runat="server">Educación</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvEducacion" runat="server" CssClass="tableContInfo yellow" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" />
                                            <asp:BoundField DataField="Notas" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblEducNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlSalud" CssClass="block  special">
                        <table class="tableContInfo sky">
                            <tr>
                                <th>
                                    <asp:Label ID="lblSalud" runat="server">Salud</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvSalud" runat="server" CssClass="tableContInfo sky" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" />
                                            <asp:BoundField DataField="Notas" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblSaludNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlAdicciones" CssClass="block  special">
                        <table class="tableContInfo pink">
                            <tr>
                                <th>
                                    <asp:Label ID="lblAdicciones" runat="server">Adicciones</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvAdicciones" runat="server" CssClass="tableContInfo pink" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" />
                                            <asp:BoundField DataField="Notas" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblAdicNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlViolencia" CssClass="block  special">
                        <table class="tableContInfo red">
                            <tr>
                                <th>
                                    <asp:Label ID="lblViolencia" runat="server">Violencia</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvViolencia" runat="server" CssClass="tableContInfo red" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" />
                                            <asp:BoundField DataField="Notas" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblVioNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlSociales" CssClass="block  special">
                        <table class="tableContInfo orange">
                            <tr>
                                <th>
                                    <asp:Label ID="lblSociales" runat="server">Sociales</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvSociales" runat="server" CssClass="tableContInfo orange" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" />
                                            <asp:BoundField DataField="Notas" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblSocNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlOtros" CssClass="block special">
                        <table class="tableContInfo gray">
                            <tr>
                                <th>
                                    <asp:Label ID="lblOtros" runat="server">Otros</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvOtros" runat="server" CssClass="tableContInfo gray" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" />
                                            <asp:BoundField DataField="Notas" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblOtrosNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlFocos" CssClass="block special">
                        <table class="tableContInfo purple">
                            <tr>
                                <th>
                                    <asp:Label ID="lblFocos" runat="server">Focos de Atención</asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvFocos" runat="server" CssClass="tableContInfo purple" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" />
                                            <asp:BoundField DataField="Notas" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblFocosNoTiene" runat="server" Visible="false" CssClass="labelFormSec"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlPie" Visible="false">
            <table width="100%" style="border-top-width: 0px" cellspacing="0">
                <tbody>
                    <tr>
                        <td style="border-style: none; border-width: medium">
                            <asp:Label runat="server" CssClass="labelForm">Informe hecho por:</asp:Label></td>
                        <td align="center" style="border-left-style: none; border-left-width: medium; border-right-style: none; border-right-width: medium; border-top-style: none; border-top-width: medium; border-bottom-style: solid; border-bottom-width: 1px">
                            <div style="border-bottom-style: solid; border-bottom-width: 1px">
                                &nbsp;
                            </div>
                        </td>
                        <td style="border-style: none; border-width: medium">&nbsp;</td>
                        <td style="border-style: none; border-width: medium">
                            <p align="right">
                                <asp:Label runat="server" CssClass="labelForm">VoBo</asp:Label>
                            </p>
                        </td>
                        <td align="center" style="border-left-style: none; border-left-width: medium; border-right-style: none; border-right-width: medium; border-top-style: none; border-top-width: medium; border-bottom-style: solid; border-bottom-width: 1px">
                            <div style="border-bottom-style: solid; border-bottom-width: 1px">
                                &nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-style: none; border-width: medium">&nbsp;</td>
                        <td align="center" style="border-left-style: none; border-left-width: medium; border-right-style: none; border-right-width: medium; border-bottom-style: none; border-bottom-width: medium">
                            <b>
                                <asp:Label ID="Label1" runat="server" CssClass="labelForm">AdrianaA</asp:Label></b></td>
                        <td style="border-style: none; border-width: medium">&nbsp;</td>
                        <td style="border-style: none; border-width: medium">&nbsp;</td>
                        <td align="center" style="border-left-style: none; border-left-width: medium; border-right-style: none; border-right-width: medium; border-bottom-style: none; border-bottom-width: medium">
                            <b>
                                <asp:Label runat="server" CssClass="labelForm">Patricia Aurora Ramirez Álvarez</asp:Label></b></td>
                    </tr>
                    <tr>
                        <td style="border-style: none; border-width: medium">&nbsp;</td>
                        <td align="center" style="border-style: none; border-width: medium">
                            <asp:Label runat="server" CssClass="labelForm">Trabajador Social</asp:Label></td>
                        <td style="border-style: none; border-width: medium">&nbsp;</td>
                        <td style="border-style: none; border-width: medium">&nbsp;</td>
                        <td align="center" style="border-style: none; border-width: medium">
                            <asp:Label runat="server" CssClass="labelForm">Director Trabajador Social</asp:Label></td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

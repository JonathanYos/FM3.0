<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="ResumenBecas.aspx.cs" Inherits="Familias3._1.EDUC.ResumenBecas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        * {
            margin: 0;
            padding: 0;
        }

        .contenedortotal {
            width: 100%;
            display: grid;
            height: 200px;
            grid-template-columns: 33% 33% 33%;
            grid-template-rows: 400px 600px;
            grid-gap: 10px;
        }

        .gr {
            background: red;
        }
        .informacionyavisos{
            grid-column:span 3;
            display:grid;
            grid-template-columns:minmax(800px,75%)  25%;
            grid-gap:10px;
        }
        .df{
            background:yellow;
        }
        .tam200{
            width:200px;
        }
    </style>
    <div class="contenedortotal">
        <div class="informacionyavisos gr">
            <div class="informacion df">
                <table style="width:100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblnombre" CssClass="labelBoldForm" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblanioesc" CssClass="labelBoldForm" runat="server" Text="Label"></asp:Label></td>
                        <td><asp:Label ID="lblsemaforo" CssClass="labelBoldForm" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblgrado" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlgrado" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbldesafiliacion" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblescuela" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlescuela" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label2" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblestado" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlestado" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblestadoafil" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblexcestado" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlexestado" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>

                        </td>
                        <td>
                            <asp:Label ID="lbltipoafil" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblcarrera" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlcarrera" CssClass="comboBoxBlueForm tam200" runat="server"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblporcentaje" CssClass="labelForm" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td><asp:Label ID="lblclasificacion" CssClass="labelForm" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="chktienecertificado" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="avisos df">

            </div>
        </div>
        <div class="observaciones gr">
        </div>
        <div class="rembolsos gr">
        </div>
        <div class="calificaciones gr">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

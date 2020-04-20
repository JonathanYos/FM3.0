<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="VisitaPadrinosAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.VisitaPadrinosAPAD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        div.ajax__calendar_container {
            width: 185px;
        }
    </style>
    <style>
        .mar10 {
            margin: 10px;
        }
    </style>
    <div class="formContGlobal">
        <asp:Button ID="btnnuevab" runat="server" CssClass="butonForm mar10" OnClick="btnnuevab_Click" />
        <table class="tableCont otro">
            <tr>
                <td colspan="2">
                    <span class="icon-calendar"></span>
                    <i style="color: red; font-size: 14px; font-weight: bold;">*</i><asp:Label ID="lblfechav" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtfechav" runat="server" CssClass="textBoxForm largo" contentEditable="false"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtfechav_CalendarExtender" runat="server" BehaviorID="txtfechav_CalendarExtender" Format="yyyy-MM-dd" TargetControlID="txtfechav"></ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="revtxtfecha" runat="server" ControlToValidate="txtfechav" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="revtxtfecha_ValidatorCalloutExtender" runat="server" BehaviorID="revtxtfecha_ValidatorCalloutExtender" TargetControlID="revtxtfecha">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>

                <td colspan="4">
                    <asp:Label ID="lblvalornotas" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblVnombre" runat="server" Text="" CssClass="block"></asp:Label>
                    <asp:Label ID="lblnotas" runat="server" Text=""></asp:Label><asp:TextBox ID="txtmnotas" runat="server" CssClass="textBoxForm inline" MaxLength="40"></asp:TextBox>
                    <asp:GridView ID="gvapadrinado" runat="server" AutoGenerateColumns="false" CssClass="tableCont pink">
                        <Columns>
                            <asp:TemplateField HeaderText="Agregar/Add">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbagregar" runat="server" Checked="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agregar/Add" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbagregarv" runat="server" Checked="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField ReadOnly="true" DataField="Sitio" Visible="true" HeaderText="Sitio/Site" />
                            <asp:BoundField ReadOnly="true" DataField="MemberId" Visible="true" HeaderText="No" />
                            <asp:BoundField ReadOnly="true" DataField="Nombre" Visible="true" HeaderText="Nombre/Name" />
                            <asp:TemplateField HeaderText="Notas/Notes">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtnvisita" runat="server" CssClass="textBoxForm"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnmodificar" runat="server" Text="" CssClass="butonForm" OnClick="btnmodificar_Click" /><asp:Button ID="btneliminar" runat="server" Text="" CssClass="butonForm" OnClick="btneliminar_Click" /><asp:Button ID="Button3" runat="server" Text="" CssClass="butonForm" OnClick="Button3_Click" ValidationGroup="grpGuardar" /><asp:Button ID="Button4" runat="server" Text="" CssClass="butonFormSec" OnClick="Button4_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvhistorial" CssClass="tableCont otro gray" runat="server" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Fecha de Visita" />
                <asp:BoundField DataField="Nombres" />
                <asp:BoundField DataField="Notas" />
                <asp:ButtonField ButtonType="Button" HeaderText="Accion/Action" Text="Seleccionar/Select" ControlStyle-CssClass="butonForm" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

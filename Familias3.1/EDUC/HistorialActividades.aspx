<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="HistorialActividades.aspx.cs" Inherits="Familias3._1.EDUC.HistorialActividades" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .w200 {
            width: 200px;
        }

        .ocultar {
            display: none;
        }
    </style>
    <asp:Panel ID="pnltodo" CssClass="formContGlobal" runat="server">
        <table>
            <tr>
                <td>
                    <div style="display: flex; justify-content: center; border:2px solid #585858;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblnombre" CssClass="labelForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblvnombre" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblfamilia" CssClass="labelForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblvfamilia" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblgrado" CssClass="labelForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblvgrado" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblestado" CssClass="labelForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblvestado" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblescuela" CssClass="labelForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblvescuela" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblanio" CssClass="labelForm" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblvanio" CssClass="labelBoldForm" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: flex; justify-content: center; border:1px solid #585858">
                        <div style="border:1px solid #585858">
                            <table runat="server" id="tbregisto">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblactivitit" CssClass="labelForm" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblfecha" CssClass="labelBlackForm" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtfechav" runat="server" CssClass="textBoxBlueForm w200" contentEditable="false"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtfechav_CalendarExtender" runat="server" BehaviorID="txtfechav_CalendarExtender" Format="yyyy-MM-dd" TargetControlID="txtfechav" />
                                        <asp:RequiredFieldValidator ID="revtxtfecha" runat="server" ControlToValidate="txtfechav" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revtxtfecha_ValidatorCalloutExtender" runat="server" BehaviorID="revtxtfecha_ValidatorCalloutExtender" TargetControlID="revtxtfecha"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblactividad" CssClass="labelForm" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlactividad" CssClass="comboBoxForm w200" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblobservaciones" CssClass="labelForm" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtobservaciones" CssClass="textBoxBlueForm w200" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btningresar" runat="server" CssClass="butonForm" OnClick="btningresar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="border:1px solid #585858">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbltithis" CssClass="labelBlackForm" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="max-height:400px; overflow-y:auto;">
                                            <asp:GridView ID="gvhistorial" CssClass="tableCont" runat="server" AutoGenerateColumns="false" OnRowCommand="gvhistorial_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="Crea" ItemStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" />
                                                    <asp:BoundField DataField="Cod" ItemStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" />
                                                    <asp:BoundField DataField="Fecha" />
                                                    <asp:BoundField DataField="Actividad" />
                                                    <asp:BoundField DataField="Observaciones" />
                                                    <asp:BoundField DataField="Usuario" />
                                                    <asp:TemplateField Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btneliminar" runat="server" CommandName="del" CssClass="butonForm" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="RegistroActividadesInd.aspx.cs" Inherits="Familias3._1.EDUC.RegistroActividadesInd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .w200 {
            width: 200px;
        }
        .wcolor td{
            background:#fff;
        }
        .w100 {
            width: 100px;
        }

        .textalignc {
            text-align: center;
        }
        .rcolor td{
         background:   rgb(251, 172, 173);
        }
    </style>
    <asp:Panel ID="pnlcontenedor" CssClass="formContGlobal" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div style="border:1px solid #585858; width: 100%; display: flex; margin: 0 -10px; height: 120px;">
                        <div style="border:1px solid #585858;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkinfogen" CssClass="labelForm" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkduplicados" CssClass="labelForm" runat="server" AutoPostBack="true" OnCheckedChanged="chkduplicados_CheckedChanged" /></td>
                                </tr>
                            </table>
                        </div>
                        <div style="border:1px solid #585858; width: 425px; margin-left: auto;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblfecha" CssClass="labelForm" runat="server" Text="Label"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtfechav" runat="server" CssClass="textBoxBlueForm w200" contentEditable="false" AutoPostBack="True" OnTextChanged="txtfechav_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtfechav_CalendarExtender" runat="server" Format="yyyy-MM-dd" BehaviorID="txtfechav_CalendarExtender" TargetControlID="txtfechav" />
                                        <asp:RequiredFieldValidator ID="revtxtfecha" runat="server" ControlToValidate="txtfechav" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revtxtfecha_ValidatorCalloutExtender" runat="server" BehaviorID="revtxtfecha_ValidatorCalloutExtender" TargetControlID="revtxtfecha"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblmiembro" CssClass="labelForm" runat="server" Text="Label"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtmiembro" CssClass="textBoxBlueForm w100" onkeypress="return esDigito(event)" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbltipo" runat="server" CssClass="labelForm"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddltipo" CssClass="comboBoxForm w200" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltipo_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label CssClass="labelForm" ID="lblfamilia" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtfamilia" CssClass="textBoxBlueForm w100" onkeypress="return esDigito(event)" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblobservaciones" CssClass="labelForm" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtobservaciones" CssClass="textBoxBlueForm w200" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblfaro" runat="server" CssClass="labelForm"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtfaro" runat="server" onkeypress="return esDigito(event)" CssClass="textBoxBlueForm w100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Button ID="btningresar" CssClass="butonForm" runat="server" OnClick="btningresar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="border:1px solid #585858; overflow-y:auto; width: 100%; display: flex; margin: 0 -10px; height: 200px;">
                        <div>
                            <asp:GridView ID="gvhistorial" CssClass="tableCont" runat="server" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Accion/Action" Visible="True">
                                        <ItemTemplate>
                                            <asp:Button ID="btndelim" CssClass="butonForm floatl" Text="Eliminar" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="border:1px solid #585858;  width: 300px; display:flex; align-items:center; margin-left: auto;">
                            <table style="width: 100%;" runat="server" id="tbinfo">
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lblnombre" runat="server" CssClass="labelForm"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lbledad" runat="server" CssClass="labelForm"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lbltipodemiem" runat="server" CssClass="labelForm"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lbleduc" runat="server" CssClass="labelForm"></asp:Label></td>
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
    <asp:Panel ID="pnlfamilia" CssClass="textalignc" Style="margin: 0 auto; max-width: 840px;" runat="server">
        <asp:GridView ID="gvfamilia" CssClass="tableCont" runat="server" OnRowCommand="gvfamilia_RowCommand" OnRowDataBound="gvfamilia_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Accion/Action" Visible="True">
                    <ItemTemplate>
                        <asp:Button ID="btnsel" CssClass="butonForm floatl" Text="Referir/Refer" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>

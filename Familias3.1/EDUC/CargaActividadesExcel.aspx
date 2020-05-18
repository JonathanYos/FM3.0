<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="CargaActividadesExcel.aspx.cs" Inherits="Familias3._1.EDUC.CargaActividadesExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .w200 {
            width: 200px;
        }
        .w100{
            width:100px;
        }
        .imgmuestra {
            width: 400px;
        }

            .imgmuestra img {
                width: 100%;
            }

        .espacio10px {
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .dcolor td {
            background: #F0E68C;
        }
        .ddcolor{
            background: #F0E68C;
        }
        .icolor td {
            background: #F4A460;
        }
    </style>
    <asp:Panel ID="pnltodo" CssClass="formContGlobal" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Panel ID="pnlparte1" runat="server" Style="display: flex; justify-content: center;">
                        <div style="margin-right: 50px;">
                            <table>
                                <tr>
                                    <td colspan="2" style="text-align:right;">
                                        <asp:Button ID="btnregresar" runat="server" CssClass="butonFormRet" OnClick="btnregresar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblactividades" runat="server" CssClass="labelForm"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlactividad" CssClass="comboBoxForm w200" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblfecha" runat="server" CssClass="labelForm"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblhora" runat="server" CssClass="labelForm"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbafecha" CssClass="textBoxBlueForm w200" runat="server" contentEditable="false"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txbafecha_CalendarExtender" runat="server" Format="MM/dd/yyyy" BehaviorID="txbafecha_CalendarExtender" TargetControlID="txbafecha" />
                                        <asp:RequiredFieldValidator ID="rfvafecha" runat="server" ControlToValidate="txbafecha" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceafecha" runat="server" BehaviorID="rfvafecha_ValidatorCalloutExtender" TargetControlID="rfvafecha">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:TextBox ID="txthora" runat="server" CssClass="textBoxBlueForm w100" TextMode="Time"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblnotas" runat="server" CssClass="labelForm"></asp:Label></td>
                                    <td><asp:TextBox ID="txtnotas" runat="server" CssClass="textBoxBlueForm w200"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblregistroca" runat="server" CssClass="labelForm"></asp:Label></td>
                                    <td><asp:Label ID="lblvresgistroca" runat="server" CssClass="labelForm"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td><asp:Label ID="lbldup" runat="server" CssClass="labelForm ddcolor"></asp:Label></td>
                                    <td><asp:TextBox ID="txtdup" runat="server" CssClass="textBoxBlueForm w200"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:FileUpload ID="fluarchivo" runat="server" CssClass="butonFormSec espacio10px" />
                                        <br />
                                        <asp:Button ID="btnguardar" runat="server" CssClass="butonForm" OnClick="btnguardar_Click" />
                                        <asp:Button ID="btnguardarpartedos" runat="server" CssClass="butonForm" OnClick="btnguardarpartedos_Click"/>
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                        <div class="imgmuestra">
                            <asp:Image ID="imgejemplo" runat="server" ImageUrl="~/Images/Listado.jpg" />
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="display: flex; justify-content: center; padding-top:30px;">
                    <asp:GridView ID="GridView1" CssClass="tableCont" runat="server" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkselect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="ReporteGeneralActi.aspx.cs" Inherits="Familias3._1.EDUC.ReporteGeneralActi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .butn {
            font-family: Arial, Helvetica, sans-serif;
            padding: 3px 6px;
            display: inline-block;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.42;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            user-select: none;
            text-decoration: none;
        }

            .butn span p {
                display: flex;
                font-family: Arial, Helvetica, sans-serif;
                text-decoration: none;
                float: right;
                margin: 0;
                padding: 0;
                margin-left: 5px;
            }

        .butondelete {
            background: #d9534f;
            color: #fff;
            border-color: #d43f3a;
        }

            .butondelete:hover {
                color: #fff;
                background-color: #d2322d;
                border-color: #ac2925;
            }

        .butonacept {
            color: #fff;
            background: #5cb85c;
            border-color: #4cae4c;
        }

            .butonacept:hover {
                color: #fff;
                background: #47A447;
                border-color: #398439;
            }

        .butonsave {
            color: #fff;
            background: #428bca;
            border-color: #4180BA;
        }

            .butonsave:hover {
                color: #fff;
                background: #3276B1;
                border-color: #285E8E;
            }

        .butonupdate {
            color: #fff;
            background: #f0ad4e;
            border-color: #eea236;
        }

            .butonupdate:hover {
                color: #fff;
                background: #ED9C28;
                border-color: #eea236;
            }

        .butn {
            margin-left: 10px;
        }
        /*--------------------------------------------------------------------------*/
        .butondelete1 {
            color: #d9534f;
            background: #fff;
            border-color: #d43f3a;
        }

            .butondelete1:hover {
                background: #fff;
                color: #d2322d;
                border-color: #ac2925;
            }

        .butonacept1 {
            background: #fff;
            color: #5cb85c;
            border-color: #4cae4c;
        }

            .butonacept1:hover {
                background: #fff;
                color: #47A447;
                border-color: #398439;
            }

        .butonsave1 {
            background: #fff;
            color: #428bca;
            border-color: #4180BA;
        }

            .butonsave1:hover {
                background-color: #fff;
                color: #3276B1;
                border-color: #285E8E;
            }

        .butonupdate1 {
            background-color: #fff;
            color: #f0ad4e;
            border-color: #eea236;
        }

            .butonupdate1:hover {
                background-color: #fff;
                color: #ED9C28;
                border-color: #eea236;
            }

        .butn {
            margin-left: 10px;
        }
    </style>

    <asp:Panel ID="pnltodo" CssClass="formContGlobalReport" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblfechade" CssClass="labelForm" runat="server"></asp:Label></td>
                <td>
                    <asp:RequiredFieldValidator ID="revtxtfecha" CssClass="textBoxBlueForm" runat="server" ControlToValidate="txtdefecha" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="revtxtfecha_ValidatorCalloutExtender" runat="server" BehaviorID="revtxtfecha_ValidatorCalloutExtender" TargetControlID="revtxtfecha">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:TextBox ID="txtdefecha" CssClass="textBoxBlueForm" runat="server" contentEditable="false"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtdefecha_CalendarExtender" runat="server" Format="yyyy-MM-dd" BehaviorID="txtdefecha_CalendarExtender" TargetControlID="txtdefecha" />
                </td>
                <td>
                    <asp:Label ID="lblfechaa" CssClass="labelForm" runat="server"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txbafecha" CssClass="textBoxBlueForm" runat="server" contentEditable="false"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txbafecha_CalendarExtender" runat="server" Format="yyyy-MM-dd" BehaviorID="txbafecha_CalendarExtender" TargetControlID="txbafecha" />
                    <asp:RequiredFieldValidator ID="rfvafecha" runat="server" ControlToValidate="txbafecha" ValidationGroup="grpGuardar" Display="None"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vceafecha" runat="server" BehaviorID="rfvafecha_ValidatorCalloutExtender" TargetControlID="rfvafecha">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblactividad" CssClass="labelForm" runat="server"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlactividad" CssClass="comboBoxBlueForm w200" runat="server"></asp:DropDownList></td>
                <td colspan="2">
                    <asp:Button ID="btngenerar" CssClass="butonForm" runat="server" OnClick="btngenerar_Click" />
                    <asp:LinkButton ID="lnkb1" runat="server" CssClass="butondelete butn" />
                    <asp:LinkButton ID="lnkb2" runat="server" CssClass="butonacept butn" />
                    <asp:LinkButton ID="lnkb3" runat="server" CssClass="butonsave butn" />
                    <asp:LinkButton ID="lnkb4" runat="server" CssClass="butonupdate butn" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:LinkButton ID="lnkb5" runat="server" CssClass="butondelete1 butn" />
                    <asp:LinkButton ID="lnkb6" runat="server" CssClass="butonacept1 butn" />
                    <asp:LinkButton ID="lnkb7" runat="server" CssClass="butonsave1 butn" />
                    <asp:LinkButton ID="lnkb8" runat="server" CssClass="butonupdate1 butn" />

                </td>
            </tr>
        </table>
        <br />
        <div style="width: 100%;">
            <asp:Label ID="lbltotal" CssClass="labelBlackForm" runat="server"></asp:Label>
            <br />
            <asp:GridView ID="gvhistorial" CssClass="tableCont" runat="server"></asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

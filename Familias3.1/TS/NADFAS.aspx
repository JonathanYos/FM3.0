<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="NADFAS.aspx.cs" Inherits="Familias3._1.TS.RegistrarNADFAS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <div>
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
            <asp:Panel runat="server" CssClass="formContTrans">
                <asp:Panel ID="pnlMiembros" runat="server">
                    <table class="tblCenter">
                        <tr>
                            <td>
                                <table class="tableContInfo tblCenter">
                                    <tr>
                                        <td class="center">
                                            <asp:Label ID="lblMiembros" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gdvMiembros" CssClass="tableContInfo tblCenter" AutoGenerateColumns="false" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="Miembro" />
                                                    <asp:BoundField DataField="CreationDateTime" />
                                                    <asp:BoundField DataField="StartDate" />
                                                    <asp:BoundField DataField="Miembro" />
                                                    <asp:BoundField DataField="Nombre" />
                                                    <asp:BoundField DataField="Edad" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAplica" Text='<%# (Eval("Tiene").ToString() == "True") ? dic.Si : dic.No %>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkAplica" Checked='<%#Eval("Tiene")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="butonForm" Text="Guardar" OnClick="btnGuardar_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

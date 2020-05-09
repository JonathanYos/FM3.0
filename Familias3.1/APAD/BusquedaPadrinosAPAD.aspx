<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="BusquedaPadrinosAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.BusquedaPadrinosAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" class="formContGlobal">
        <table class="tableCont pink cntrar2" runat="server" id="tbregistro">
            <tr>
                <th>
                    <asp:Label ID="lblnombre" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:TextBox ID="txtnombre" runat="server" CssClass="textBoxBlueForm widthall"></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblestado" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:DropDownList ID="ddlestado" runat="server" CssClass="widthall comboBoxBlueForm"></asp:DropDownList></td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblpais" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:DropDownList ID="ddlpais" runat="server" CssClass=" widthall comboBoxBlueForm"></asp:DropDownList></td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblhabla" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:CheckBox ID="cbhabla" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblnumero" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:TextBox ID="txtnumero" runat="server" CssClass="textBoxBlueForm widthall"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnbuscar" runat="server" Text="" CssClass="butonForm" OnClick="btnbuscar_Click" /></td>
            </tr>
        </table>
        <table style="width: 100%" runat="server" id="tbhistorial">
            <tr>
                <td>
                    <div class="borde4">

                        <asp:Button ID="btnOtraB" runat="server" Text="" CssClass="butonForm" OnClick="btnOtraB_Click" />
                        <asp:GridView ID="gvhistorial" CssClass="tableCont cntrar2 gray" runat="server" AutoGenerateColumns="False" OnRowCommand="gvhistorial_RowCommand" OnRowDataBound="gvhistorial_RowDataBound">
                            <Columns>

                                <asp:BoundField DataField="SponsorId" Visible="false" HeaderText="No" />
                                <asp:BoundField DataField="SponsorNames" HeaderText="" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="Pais" HeaderText="" />
                                <asp:BoundField DataField="OrganizationContactNames" HeaderText="Organizacion" />
                                <asp:BoundField DataField="Habla Español" HeaderText="Habla Español" />
                                <asp:BoundField DataField="Genero" HeaderText="Genero" />
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="btnSName" runat="server"
                                            CommandName="cmdSName"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            Text='<%# Eval("SponsorId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

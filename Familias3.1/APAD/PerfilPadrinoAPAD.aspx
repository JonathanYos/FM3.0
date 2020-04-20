<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="PerfilPadrinoAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.PerfilPadrinoAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .mar10 {
            margin: 10px;
        }
    </style>
    <div class="formContGlobal">
        <asp:Button ID="btnnuevab" runat="server" CssClass="butonForm mar10" OnClick="btnnuevab_Click" />
        <table class="cntrar2 tableCont pink">
            <tr>
                <th style="text-align: center;" colspan="2">
                    <span class="icon-user-circle-o"></span>
                    <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                </th>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblnombre" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:Label ID="lblVnombre" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblestado" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:Label ID="lblVestado" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblpais" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:Label ID="lblVpais" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblorganizacion" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:Label ID="lblVorganizacion" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblgenero" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:Label ID="lblVgenero" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblespanol" runat="server" Text=""></asp:Label></th>
                <td>
                    <asp:Label ID="lblVespanol" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
        <asp:GridView ID="gvhistorial" CssClass="gray tableCont cntrar2" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvhistorial_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="No." DataField="MemberId" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Sitio" DataField="Sitio" />
                <asp:BoundField HeaderText="Fecha Inicio" DataField="Fecha de Inicio" />
                <asp:BoundField HeaderText="Fecha Fin" DataField="Fecha Fin" />
                <asp:BoundField HeaderText="Tipo" DataField="Tipo" />
                <asp:BoundField HeaderText="Cumpleaños" DataField="Cumpleaños" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

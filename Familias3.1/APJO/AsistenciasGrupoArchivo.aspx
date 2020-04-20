<%@ Page Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="AsistenciasGrupoArchivo.aspx.cs" Inherits="Familias3._1.APJO.AsistenciasGrupoArchivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tblCenter">
        <tr>
            <td>
                <asp:FileUpload runat="server" ID="FileUpload" />
                <asp:Button ID="btnCargar" runat="server" Text="Cargar" CssClass="butonForm" OnClick="btnCargar_Click" />
                <asp:Button ID="btnNuevaCarga" runat="server" Text="Nueva Carga" CssClass="butonFormRet" OnClick="btnNuevaCarga_Click" Visible="false" />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="butonForm" OnClick="btnGuardar_Click" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gdvAsistencias" runat="server" CssClass="tableContInfo" OnRowDataBound="gdvAsistencias_RowDataBound">
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

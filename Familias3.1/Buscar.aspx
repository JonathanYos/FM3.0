<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="mast.Master" CodeBehind="Buscar.aspx.cs" Inherits="Familias3._1.SEARCH" %>
<%@ MasterType virtualPath="mast.Master"%> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlBuscar" runat="server" class="formCont searchFrm">
        <table>
            <tr>
                <td>
                    <table style="display:inline-block; margin:auto auto">
                        <tr>
                            <td>
                                <asp:Label ID="lblMemberId" class ="labelForm" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txbMemberId" class="textBoxBlueForm num2" MaxLength="6" runat="server" onkeypress='return esDigito(event)'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>   
                                <asp:Label ID="lblFamilyId" class ="labelForm" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txbFamilyId" class="textBoxBlueForm num2" MaxLength="6"  runat="server" onkeypress='return esDigito(event)'></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="display:inline-block">
                        <tr>
                            <td style="height: 60px">
                                <asp:ImageButton ID="ibtnBuscar" runat="server" Height="60px" ImageUrl="~/Images/search.png" Width="60px" OnClick="lblBuscar_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position:absolute;left:5px;bottom:15px">
            <asp:Label runat="server" ID="lblBuscar" CssClass="labelFormBig"></asp:Label>
            <asp:HyperLink ID="lnkBuscarMiembrosOtraInfo"  CssClass="labelFormBig" runat="server" href="../MISC/BusquedaMiembrosOtraInfo.aspx">Buscar Miembro por Otra Info.</asp:HyperLink>
            <span class="labelFormBig">,&nbsp;</span>
            <asp:HyperLink ID="lnkBuscarMiembrosInfoEduc" CssClass="labelFormBig" runat="server" href="../MISC/BusquedaMiembrosInfoEduc.aspx">Buscar Miembro Info. Educativa</asp:HyperLink>
            <asp:Label runat="server" ID="lblO" class="labelFormBig">,&nbsp;</asp:Label>
            <asp:HyperLink ID="lnkBuscarFamilias" CssClass="labelFormBig" runat="server" href="../MISC/BusquedaFamilias.aspx">Buscar Familia</asp:HyperLink>
            <span class="labelFormBig">.</span>
        </div>
    </asp:Panel>
</asp:Content>

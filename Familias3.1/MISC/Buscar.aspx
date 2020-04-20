<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="Buscar.aspx.cs" Inherits="Familias3._1.MISC.Buscar" %>
<%@ MasterType virtualPath="~/mast.Master"%> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlBuscar" runat="server" class="formCont searchFrm">
        <table>
            <tr>
                <td>
                    <table style="display:inline-block; margin:auto auto">
                        <tr>
                            <td>
                                <asp:Label ID="lblMemberId" class ="labelBoldForm" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txbMemberId"  AutoCompleteType="Disabled" class="textBoxBlueForm num2" MaxLength="6" runat="server" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>   
                                <asp:Label ID="lblFamilyId" class ="labelBoldForm" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txbFamilyId"  AutoCompleteType="Disabled" class="textBoxBlueForm num2" MaxLength="6"  runat="server" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>   
                                <asp:Label ID="lblFaroId" class ="labelBoldForm" runat="server" Visible ="false" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txbFaroId"  AutoCompleteType="Disabled" class="textBoxBlueForm num2" Visible="false" MaxLength="6"  runat="server" onkeypress='return esDigito(event)' onkeyup='this.value=retornaSoloDigitos(this.value)'></asp:TextBox>
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
            <asp:Label runat="server" ID="lblBuscar" CssClass="labelFormBig" ForeColor="White"></asp:Label>
            <asp:HyperLink ID="lnkBuscarMiembrosOtraInfo"  CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaMiembrosOtraInfo.aspx">Buscar Miembro por Otra Info.</asp:HyperLink>
            <asp:Label runat="server" ID="lblComa" class="labelFormBig" ForeColor="White">,&nbsp;</asp:Label>
            <asp:HyperLink ID="lnkBuscarMiembrosInfoEduc" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaMiembrosInfoEduc.aspx">Buscar Miembro Info. Educativa</asp:HyperLink>
            <asp:Label runat="server" ID="lblO" class="labelFormBig" ForeColor="White">,&nbsp;</asp:Label>
            <asp:HyperLink ID="lnkBuscarFamilias" CssClass="labelFormBig" ForeColor="White" runat="server" href="../MISC/BusquedaFamilias.aspx">Buscar Familia</asp:HyperLink>
            <span class="labelFormBig" style="color:white">.</span>
        </div>
    </asp:Panel>
</asp:Content>

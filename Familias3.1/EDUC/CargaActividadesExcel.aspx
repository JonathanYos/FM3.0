<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="CargaActividadesExcel.aspx.cs" Inherits="Familias3._1.EDUC.CargaActividadesExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .w200 {
            width: 200px;
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
    </style>
    <asp:Panel ID="pnltodo" CssClass="formContGlobal" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div style="display: flex; justify-content: center;">
                        <div style="margin-right:50px;">
                            <table>
                                <tr>
                                    <td><asp:Label ID="lblactividades" runat="server" CssClass="labelForm"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlactividad" CssClass="comboBoxForm w200" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:FileUpload ID="fluarchivo" runat="server" CssClass="butonFormSec espacio10px" />
                                        <br />
                                        <asp:Button ID="btnguardar" runat="server" CssClass="butonForm" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="imgmuestra">
                            <asp:Image ID="imgejemplo" runat="server" ImageUrl="~/Images/Listado.jpg" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

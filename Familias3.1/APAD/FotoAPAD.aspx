<%@ Page Title="" Language="C#" MasterPageFile="~/mast.Master" AutoEventWireup="true" CodeBehind="FotoAPAD.aspx.cs" Inherits="Familias3._1.Apadrinamiento.FotoAPAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formContGlobal">
        <table class="tableCont" style="margin: 0 auto;" runat="server" id="tbfiltro">
            <tr>
                <th>
                    <asp:Label ID="lblsitio" runat="server"></asp:Label>
                </th>
                <td>
                    <asp:DropDownList ID="ddlsitio" runat="server" CssClass="comboBoxForm">
                    </asp:DropDownList>
                </td>
                <th>
                    <asp:Label ID="lblmiembro" runat="server"></asp:Label>
                </th>
                <td>
                    <asp:TextBox ID="txtmiembro" runat="server" CssClass="textBoxForm" onkeypress="return esDigito(event)"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnbuscar" runat="server" CssClass="butonForm" OnClick="btnbuscar_Click" />
                </td>
            </tr>
        </table>
        <table class="tableCont tablafoto" runat="server" id="ingreso">
            <tr>
                <th colspan="2">
                    <span class="icon-camera"></span>
                    <asp:Label ID="lbltitulo" runat="server" Text=""></asp:Label>
                </th>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image1" CssClass="fotoimg" runat="server" />
                </td>
                <td>
                    <asp:Image ID="Image2" CssClass="fotoimg" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <span class="icon-image"></span>
                    <asp:Label ID="lblimgact" runat="server" Text="Label"></asp:Label>
                </th>
                <th>
                    <span class="icon-image"></span>
                    <asp:Label ID="lblimgnew" runat="server" Text="Label"></asp:Label>
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="butonFormSec" onchange="readURL(this)" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnsubir" runat="server" Text="" CssClass="butonForm" OnClick="btnsubir_Click1" />
                </td>
                <td>
                    <asp:Button ID="btncanc" runat="server" Text="" CssClass="butonForm" OnClick="btncanc_Click" />
                </td>
            </tr>
        </table>
        <table class="tableCont tablafoto gray">
            <tr>
                <th style="height: 20px">
                    <asp:Label ID="lblconttotal" runat="server" Text=""></asp:Label></th>
                <th style="height: 20px">
                    <asp:Label ID="lblcontfoto" runat="server" Text=""></asp:Label></th>
                <th style="height: 20px">
                    <asp:Label ID="lblcontfotono" runat="server" Text=""></asp:Label></th>
                <th style="height: 20px">
                    <asp:Label ID="lblcontretomar" runat="server" Text=""></asp:Label></th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVtotal" runat="server" Text=""></asp:Label></td>
                <td>
                    <asp:Label ID="lblVfoto" runat="server" Text=""></asp:Label></td>
                <td>
                    <asp:Label ID="lblVfotono" runat="server" Text=""></asp:Label></td>
                <td>
                    <asp:Label ID="lblVretomar" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </div>
    <script>
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#<%= Image2.ClientID %>").attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
                }
            }
            $("#<%= FileUpload1.ClientID %>").change(function () {
            readURL(this);
        });
    </script>
</asp:Content>

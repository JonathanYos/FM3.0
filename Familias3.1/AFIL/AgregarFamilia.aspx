<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/mast.Master" CodeBehind="AgregarFamilia.aspx.cs" Inherits="Familias3._1.AFIL.AgregarFamilia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/mast.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel runat="server" CssClass="formCont" ID="pnlRegistro">
            <table class="tblCenter">
                <tr>
                    <td>
                        <table class="tblCenter">
                            <tr>
                                <th></th>
                                <th></th>
                                <th style="width: 40px"></th>
                                <th></th>
                                <th></th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="mdfFlblDireccion" CssClass="labelBoldForm">Dirección:</asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox runat="server" ID="mdfFtxbDireccion" AutoCompleteType="Disabled" CssClass="textBoxBlueForm" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="mdfFRevtxbDireccion" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfFtxbDireccion" ValidationGroup="mdfFGrpGuardar" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="mdfFRevtxbDireccion_ValidatorCalloutExtender" runat="server" BehaviorID="mdfFRevtxbDireccion_ValidatorCalloutExtender" TargetControlID="mdfFRevtxbDireccion">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="mdfFlblArea" CssClass="labelBoldForm">Área:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="mdfFddlArea" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="mdfFRevddlArea" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="mdfFddlArea" ValidationGroup="mdfFGrpGuardar" Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="mdfFRevddlArea_ValidatorCalloutExtender" runat="server" BehaviorID="mdfFRevddlArea_ValidatorCalloutExtender" TargetControlID="mdfFRevddlArea">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label runat="server" ID="mdfFlblPueblo" CssClass="labelBoldForm">Pueblo:</asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="mdfFlblVPueblo" CssClass="labelForm"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="mdfFlblMunicipio" CssClass="labelBoldForm">Municipio:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="mdfFddlMunicipio" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label runat="server" ID="mdfFlblTiempo" CssClass="labelBoldForm">Tiempo de Vivir en el Lugar:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="mdfFtxbTiempo" AutoCompleteType="Disabled" CssClass="textBoxBlueForm"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="mdfFlblNumeroCelular" CssClass="labelBoldForm">Número de Teléfono:</asp:Label>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfFtxbNumeroCelular1" AutoCompleteType="Disabled" CssClass="textBoxBlueForm  num3" MaxLength="4" onkeypress='return esDigito(event)'></asp:TextBox>
                                            </td>
                                            <td>-</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="mdfFtxbNumeroCelular2" AutoCompleteType="Disabled" CssClass="textBoxBlueForm num3" MaxLength="4" onkeypress='return esDigito(event)'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label runat="server" ID="mdfFlblEtnia" CssClass="labelBoldForm">Etnia:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="mdfFddlEtnia" CssClass="comboBoxBlueForm"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px"></td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: center">
                                    <asp:Button ID="mdfFbtnGuardar" runat="server" Text="Guardar" CssClass="butonForm" ValidationGroup="mdfFGrpGuardar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlHistorial">
            <table class="tableContInfo  tblCenter">
                <tr>
                    <th>Miembros de la Familia</th>
                </tr>
                <tr>
                    <td>
                        <table class="tableContInfo">
                            <tr>
                                <th>Nombres</th>
                                <th>Apellidos</th>
                                <th>Género</th>
                                <th>Nacimiento</th>
                                <th>Relación</th>
                            </tr>
                            <tr>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="M">Masculino</option>
                                        <option value="F">Femenino</option>
                                    </select>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                            <td>
                                                <select>
                                                    <option value="1">Ene</option>
                                                    <option value="2">Feb</option>
                                                    <option value="3">Mar</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="">Jefe de Casa</option>
                                        <option value="">Hijo Jefe Casa</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="M">Masculino</option>
                                        <option value="F">Femenino</option>
                                    </select>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                            <td>
                                                <select>
                                                    <option value="1">Ene</option>
                                                    <option value="2">Feb</option>
                                                    <option value="3">Mar</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="">Jefe de Casa</option>
                                        <option value="">Hijo Jefe Casa</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="M">Masculino</option>
                                        <option value="F">Femenino</option>
                                    </select>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                            <td>
                                                <select>
                                                    <option value="1">Ene</option>
                                                    <option value="2">Feb</option>
                                                    <option value="3">Mar</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="">Jefe de Casa</option>
                                        <option value="">Hijo Jefe Casa</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="M">Masculino</option>
                                        <option value="F">Femenino</option>
                                    </select>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                            <td>
                                                <select>
                                                    <option value="1">Ene</option>
                                                    <option value="2">Feb</option>
                                                    <option value="3">Mar</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="">Jefe de Casa</option>
                                        <option value="">Hijo Jefe Casa</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="M">Masculino</option>
                                        <option value="F">Femenino</option>
                                    </select>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                            <td>
                                                <select>
                                                    <option value="1">Ene</option>
                                                    <option value="2">Feb</option>
                                                    <option value="3">Mar</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="">Jefe de Casa</option>
                                        <option value="">Hijo Jefe Casa</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="M">Masculino</option>
                                        <option value="F">Femenino</option>
                                    </select>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                            <td>
                                                <select>
                                                    <option value="1">Ene</option>
                                                    <option value="2">Feb</option>
                                                    <option value="3">Mar</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="">Jefe de Casa</option>
                                        <option value="">Hijo Jefe Casa</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <input class="textBoxForm" /></td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="M">Masculino</option>
                                        <option value="F">Femenino</option>
                                    </select>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                            <td>
                                                <select>
                                                    <option value="1">Ene</option>
                                                    <option value="2">Feb</option>
                                                    <option value="3">Mar</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input class="num3 comboBoxForm" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <select class="comboBoxForm">
                                        <option value="">Jefe de Casa</option>
                                        <option value="">Hijo Jefe Casa</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

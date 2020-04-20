<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListBox.aspx.cs" Inherits="Familias3._1.ListBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MultiSelect DropDownList with CheckBoxes in ASP.Net</title>
</head>
<body>
    <form id="form1" runat="server">
        <link rel="stylesheet" href="css/estiloForm.css" type="text/css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
        <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
        <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
            type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=lstEmployee]').multiselect({
                    includeSelectAllOption: true
                });
            });
        </script>
        Employee :
        <asp:ListBox ID="lstEmployee" runat="server" SelectionMode="Multiple">
            <asp:ListItem Text="Lunes" Value="1" />
            <asp:ListItem Text="Martes" Value="2" />
            <asp:ListItem Text="Miércoles" Value="3" />
            <asp:ListItem Text="Viernes" Value="4" />
            <asp:ListItem Text="Sabado" Value="5" />
            <asp:ListItem Text="Domingo" Value="5" />
        </asp:ListBox>
        <asp:Button ID="Button1" Text="Submit" runat="server" OnClick="Submit" />
    </form>
</body>
</html>

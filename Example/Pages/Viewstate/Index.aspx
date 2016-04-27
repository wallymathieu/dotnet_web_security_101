<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" 
    Inherits="Example.Viewstate.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <asp:Repeater ID="Texts" runat="server">
                <HeaderTemplate>
                    <tr>
                        <th>Text</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Container.DataItem%></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div>
            <asp:TextBox ID="Text" runat="server"></asp:TextBox>
            <asp:Button ID="AddText" runat="server" Text="Add text" OnClick="AddText_Click" />
        </div>
    </form>
</body>
</html>

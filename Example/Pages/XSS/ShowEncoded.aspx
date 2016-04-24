<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowEncoded.aspx.cs" Inherits="Example.XSS.ShowEncoded" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
 <div id="infodiv">
      info
    </div>
    <h2>
      Show product with html-encode</h2>
    <fieldset>
      <legend>Encoded</legend>
      <p>
        ProductID:
        <%= HttpUtility.HtmlEncode(Model.ProductID) %>
      </p>
      <p>
        ProductNumber:
        <%= HttpUtility.HtmlEncode(Model.ProductNumber) %>
      </p>
      <p>
        ProductName:
        <%= HttpUtility.HtmlEncode(Model.ProductName) %>
      </p>
      <p>
        Description:
        <%= HttpUtility.HtmlEncode(Model.Description) %>
      </p>
    </fieldset>
</body>
</html>

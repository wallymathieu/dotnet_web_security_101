<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowUnEncoded.aspx.cs" Inherits="Example.XSS.ShowUnEncoded" %>

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
      Show product without html-encode</h2>
    <fieldset>
      <legend>UnEncoded</legend>
      <p>
        ProductID:
        <%= Model.ProductID %>
      </p>
      <p>
        ProductNumber:
        <%= Model.ProductNumber %>
      </p>
      <p>
        ProductName:
        <%= Model.ProductName %>
      </p>
      <p>
        Description:
        <%= Model.Description %>
      </p>
    </fieldset>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlogin.aspx.cs" Inherits="userlogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>:: WOM - Login ::</title>
<style type="text/css">
<!--
body {
	background-color: #93B47F;
}
-->
</style>
<link href="css/login.css" rel="stylesheet" type="text/css">
</head>

<body>
<p>&nbsp;</p>
    <form id="form1" runat="server">
<table width="816" border="0" align="center" cellpadding="0" cellspacing="0" background="img/login_bg.jpg">
  <!--DWLayoutTable-->
  <tr>
    <td width="565" height="155">&nbsp;</td>
    <td width="193">&nbsp;</td>
    <td width="58">&nbsp;</td>
  </tr>
  <tr>
    <td height="178">&nbsp;</td>
    <td valign="top">

	
      <table width="100%"  border="0" cellspacing="3" cellpadding="3">
        <tr>
          <td width="40%">&nbsp;</td>
          <td width="60%">&nbsp;</td>
        </tr>
        <tr>
          <td class="login_font">Username</td>
          <td>
          <asp:TextBox ID="txtUsername" runat="server" CssClass="login_txt"  MaxLength="50" AutoComplete="off"/>
          </td>
        </tr>
        <tr>
          <td class="login_font">Password</td>
          <td><asp:TextBox ID="txtPassword" runat="server" CssClass="login_txt" TextMode="Password" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>
              &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="img/btn_login.gif" OnClick="btnLogin_Click" /></td>
        </tr>
      </table>
    
	
	<asp:Label ID="lblLoginStatus" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                Font-Strikeout="False" ForeColor="Black"  Width="180px" /></td>
               
    <td></td>
  </tr>
  <tr>
    <td height="168">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
</form>
</body>
</html>



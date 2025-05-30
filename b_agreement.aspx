<%@ Page Language="C#" AutoEventWireup="true" CodeFile="b_agreement.aspx.cs" Inherits="b_agreement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>:: WorkTrax ::</title>
    <style type="text/css">
    <!--
    body {
	    margin-left: 0px;
	    height:100%;
	    width:100%;
	    margin-top: 0px;
	    margin-right: 0px;
	    margin-bottom: 0px;
	    background-color: #F5FAF1;
    }
    td {
	    font-size: 10px;
	    font-family: Verdana;
    }
    A:visited {
    text-decoration: none;
    color: #0000FF;
    }
    A:hover {
        text-decoration: none;
        color: #0000FF;
    }
    A:link {
        text-decoration: none;
        color: #0000FF;
    }
    -->
    </style>
    <link href="css/pageFormat.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
    function overTD(obj)
    {
	    document.getElementById(obj).style.background	=	"#F5FAF1";
    }
    function outTD(obj)
    {
	    document.getElementById(obj).style.background	=	"";
    }
    </script>

</head>
<body onLoad="MM_preloadImages('img/tabs/agree_on.gif','img/tabs/eff_on.gif')">
    <div>
    <form id="form1" runat="server">
    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td height="51" valign="top" style="background-image:url(img/topBar.gif);background-repeat:repeat-x;"><table width="100%"  border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="21%"><img src="img/womLogo.gif" width="174" height="23"></td>
        <td width="79%">&nbsp;</td>
      </tr>
      <tr>
        <td height="20" class="user">&nbsp;&nbsp;User :&nbsp;
            <asp:Label ID="lblUser" runat="server" Style="z-index: 100; left: 49px; position: absolute;
                top: 28px"></asp:Label>
        </td>
        <td valign="bottom">
            <table width="300" border="0" align="right" cellpadding="0" cellspacing="2">
              <tr>
                <td id="td01" align="center" title="Enter a new work order" style="width: 33px"><a href="home.aspx" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;List&nbsp;</a> </td>
                <td id="td2" align="center" title="Enter a new work order"><a href="neworder.aspx" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;New Order&nbsp;</a> </td>
                <td id="td03" align="center"><a href="logoff.aspx" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;Log Off&nbsp;</a> </td>
                <td id="td04" align="center" title="Display a work order by its ID number"><a href="#" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;Show #&nbsp;</a> </td>
                <td ><input name="txtShowOrder" type="text" class="showTxt" id="txtShowOrder"></td>
              </tr>
            </table>
        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top" class="arsLogo">  <br>
      <table width="70%"  border="0" cellspacing="0" cellpadding="0" style="margin-left:11%;">
        <tr>
          <td style="width: 534px"><a href="b_workorder.aspx" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image5','','img/tabs/wo_on.gif',1)"><img src="img/tabs/wo_off.gif" width="140" height="38" border="0"></a><a href="b_effortschedule.aspx" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image4','','img/tabs/eff_on.gif',1)"><img src="img/tabs/eff_off.gif" name="Image5" width="217" height="38" border="0"></a><img src="img/tabs/agree_off.gif" name="Image4" width="141" height="38" border="0"></td>
        </tr>
      </table>
	  
      <table width="75%"  border="0" align="left" cellpadding="5" cellspacing="3" bgcolor="#E0EDD9" style="margin-left:5%;">
        <tr>
          <td><br />
		  <table width="100%" id="tblWODetails" runat="server" border="0" cellpadding="2" cellspacing="2" bgcolor="#CADAC0">
            <tr>
              <td class="blogFont">Order </td>
              <td width="85%" class="blogFont"><strong></strong></td>
              </tr>
            <tr>
              <td class="blogFont">Project </td>
              <td class="blogFont"><strong></strong></td>
              </tr>
            <tr>
              <td width="15%" class="blogFont">Work Type </td>
              <td class="blogFont"><strong></strong></td>
              </tr>
            <tr>
              <td class="blogFont">Assigned To</td>
              <td class="blogFont"><strong></strong></td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td class="blogFont"> Comment:<strong><br>
                <br>
                <asp:TextBox Columns="80" ID="txtBlogComments" runat="Server" Height="122px" Width="653px" TextMode="MultiLine"></asp:TextBox>
            </strong> <br>
            <br>
            Attach a file:
      <asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;&nbsp;<p align="right">
      Assign to:&nbsp; 
        <asp:DropDownList ID="ddlstUsers" runat="server" Width="200px" CssClass="blogsmallFont">
        </asp:DropDownList>
      <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />  
      <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /> 
	  </p>
      <hr />
      <table border="0" cellpadding="0" cellspacing="0" width="100%">
      <tr><td><%GetAgreementBlog();%></td></tr>
      </table>
      </td>
        </tr>
      </table>
    </td>
  </tr>
    </table>    
    </form>
    </div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="b_workorder.aspx.cs" Inherits="b_workorder" %>

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
	font-size: 11px;
	font-family: Verdana;
}
.navLinks {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 11px;
	font-weight: bold;
	color: #F5FAF1;
	text-decoration: none;
}
.mouseOn {
	background-color:#F5FAF1;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 11px;
	font-weight: bold;
	color: #047E5C;
	text-decoration: none;
}
    -->
    </style>
   
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
    <script language="JavaScript" type="text/JavaScript">
    <!--
    function MM_reloadPage(init) {  //reloads the window if Nav4 resized
      if (init==true) with (navigator) {if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
        document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}
      else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();
    }
    MM_reloadPage(true);

    function MM_preloadImages() { //v3.0
      var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
        var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
        if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
    }

    function MM_swapImgRestore() { //v3.0
      var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
    }

    function MM_findObj(n, d) { //v4.01
      var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
        d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
      if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
      for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
      if(!x && d.getElementById) x=d.getElementById(n); return x;
    }

    function MM_swapImage() { //v3.0
      var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
       if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
    }
    //-->
    </script>
    <link href="css/pageFormat.css" rel="stylesheet" type="text/css" />
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
          <td><img src="img/tabs/wo_on.gif" width="140" height="38"><a href="b_effortschedule.aspx?wid=<%=Request.QueryString["wid"] %>" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image5','','img/tabs/eff_on.gif',1)"><img src="img/tabs/eff_off.gif" name="Image5" width="217" height="38" border="0"></a><a href="finalAgreement.aspx?wid=<%=Request.QueryString["wid"] %>" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image4','','img/tabs/agree_on.gif',1)"><img src="img/tabs/agree_off.gif" name="Image4" width="141" height="38" border="0"></a></td>
        </tr>
      </table>
	  
      <table width="75%"  border="0" align="left" cellpadding="5" cellspacing="3" bgcolor="#E0EDD9" style="margin-left:5%;">
        <tr>
          <td style="height: 116px"><br />
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
              <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                  <asp:View ID="View1" runat="server">
            Attach a file: &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Width="346px" /></asp:View>
                  <asp:View ID="View2" runat="server">
              <asp:Panel ID="ErrorPanel" runat="server" BorderColor="Red" BorderStyle="Dotted" BorderWidth="2px"
                  Height="112px" Width="457px" HorizontalAlign="Center" style="font-size:11px;font-weight:bold;color:Red;line-height:15px;" BackColor="#FFC0C0" Visible="False">
                  <br />The uploaded file already exists!<br />
                  Confirm by browsing the same file or choose another to upload.<asp:FileUpload ID="FileUpload2" runat="server" Width="346px" /><br />
                  <br />
                  <asp:Button ID="BtnOverwrite" runat="server" Text="Overwrite file" Width="95px" OnClick="BtnOverwrite_Click" /></asp:Panel>
                  </asp:View>
              </asp:MultiView>
        &nbsp;&nbsp;<p align="right">
      Assign to:&nbsp; 
        <asp:DropDownList ID="ddlstUsers" runat="server" Width="200px" CssClass="blogsmallFont">
        </asp:DropDownList>
      <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />  
      <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /> 
	  </p>
      <hr />
      <table border="0" cellpadding="0" cellspacing="0" width="100%">
      <tr><td><%GetWorkOrderBlog();%></td></tr>
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

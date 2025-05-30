<%@ Page Language="C#" AutoEventWireup="true" CodeFile="finalAgreement.aspx.cs" Inherits="finalAgreement" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
"http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>:: Wom - Home ::</title>
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
.userSignature{
line-height:60px;border-bottom: 1px solid #747272;
}
-->
</style>
<link href="css/pageFormat.css" rel="stylesheet" type="text/css">
<script language="javascript" src="jscripts/datetimepicker.js" type="text/javascript"></script>
<script language="javascript">
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
</head>

<body onLoad="MM_preloadImages('img/tabs/wo_on.gif','img/tabs/eff_on.gif')">
 <form id="form1" runat="server">
<table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td height="51" valign="top" style="background-image:url(img/topBar.gif);background-repeat:repeat-x;"><table width="100%"  border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="21%"><img src="img/womLogo.gif" width="174" height="23"></td>
        <td width="79%">&nbsp;</td>
      </tr>
      <tr>
        <td height="20" class="user">&nbsp;&nbsp;User : <asp:Label ID="lblUser" runat="server" Style="z-index: 100; left: 49px; position: absolute;
                top: 28px"></asp:Label> </td>
        <td valign="bottom">
            <table width="300" border="0" align="right" cellpadding="0" cellspacing="2">
              <tr>
                 <td id="td01" align="center" title="Enter a new work order"><a href="home.aspx" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;List&nbsp;</a> </td>
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
    <td valign="top" class="arsLogo" style="width: 1056px">  <br>
      <table width="80%"  border="0" cellspacing="0" cellpadding="0" style="margin-left:11%;">
        <tr>
          <td style="width: 534px">
          <a href="b_workorder.aspx?wid=<%=Request.QueryString["wid"]%>" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image5','','img/tabs/wo_on.gif',1)">
          <img src="img/tabs/wo_off.gif" name="Image5" width="140" height="38" border="0"></a><a href="b_effortschedule.aspx?wid=<% =Request.QueryString["wid"]%>" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image4','','img/tabs/eff_on.gif',1)"><img src="img/tabs/eff_off.gif" name="Image4" width="217" height="38" border="0"></a><img src="img/tabs/agree_on.gif" width="141" height="38" border="0">
          </td>
        </tr>
      </table>
	  
      <table width="75%"  border="0" align="left" cellpadding="5" cellspacing="3" bgcolor="#E0EDD9" style="margin-left:5%;">
        <tr>
          <td valign="top" style="height: 823px; width: 917px;"><div align="right" style="text-align: left"><a href="#"></a>
              <br />
              <strong><span style="font-size: 10pt">ARS T&amp;TT - ARS SE Work Agreement<br />
                  <br />
                  Work Agreement No : </span></strong>&nbsp;<asp:Label
                  ID="lblWANo" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
                  Text="Label" Width="317px"></asp:Label>
                  <hr />
                  <br />
              <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                  <asp:View ID="View1" runat="server">
            <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
              <tr>
                <td width="928" class="blogFont"><strong class="blogFont">1. Project Information</strong>
                    <table id="tbleProjDetails" runat="server" width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                      <tr bgcolor="#C4DAB6">
                        <td width="25%" height="20">Project Name </td>
                        <td width="25%">Project No. </td>
                        <td width="25%">Start Date of Agreement </td>
                        <td width="25%">End Date of Agreement </td>
                      </tr>
                      <tr bgcolor="#D8E7CF">
                        <td height="25">
                            <asp:TextBox ID="txtProjName" runat="server" Width="216px" Enabled="False"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtProjNo" runat="server" Width="216px" Enabled="False"></asp:TextBox></td>
                        <td> 
                        <asp:TextBox ID="txtSDate" runat="server" Width="196px"></asp:TextBox>
                            <A href="javascript:NewCal('<%=txtSDate.ClientID%>','ddmmyyyy',false,24,'dropdown',true)">
                        <IMG style="Z-INDEX: 105; LEFT: 0px; POSITION: relative; TOP: 1px" height=16 alt="Select Date/Time" src="img/cal.gif" width=16 border=0 /></A>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" Width="196px"></asp:TextBox>
                            <A href="javascript:NewCal('<%=txtEndDate.ClientID%>','ddmmyyyy',false,24,'dropdown',true)">
                        <IMG style="Z-INDEX: 105; LEFT: 0px; POSITION: relative; TOP: 1px" height=16 alt="Select Date/Time" src="img/cal.gif" width=16 border=0 /></A>&nbsp;
                        
                        </td>
                      </tr>
                </table></td>
              </tr>
            </table>
                      <br />
		    <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
              <tr>
                <td width="928"><strong class="blogFont">2. Project Category </strong><em>(Please select the project category and specify number of hours) </em>
                    <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                      <tr bgcolor="#C4DAB6">
                        <td width="71%" bgcolor="#C4DAB6" height="20"><strong>Project Category </strong></td>
                        <td width="25%"><strong>Person Hours </strong></td>
                      </tr>
                      <tr bgcolor="#D8E7CF">
                        <td>
                                <p><strong>Fixed Price Work</strong> ( maximum number of man hours agreed ) </p>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFP" runat="server" Width="166px"></asp:TextBox></td>
                      </tr>
                      <tr bgcolor="#D8E7CF">
                        <td><strong>Time and material based</strong> (number of man hours estimated for work. When nearing the quantum at 80%, T&amp;TT will be notified to change the mandate) </td>
                        <td>
                            <asp:TextBox ID="txtTM" runat="server" Width="166px"></asp:TextBox></td>
                      </tr>
                      <tr bgcolor="#D8E7CF">
                        <td><strong> Services </strong> (maximum number of man hours per month) </td>
                        <td>
                            <asp:TextBox ID="txtServ" runat="server" Width="166px"></asp:TextBox></td>
                      </tr>
                      <tr bgcolor="#D8E7CF">
                        <td><strong> Ad-Hoc Support </strong> (maximum number of man hours)&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdHoc" runat="server" Width="166px"></asp:TextBox></td>
                      </tr>
                </table></td>
              </tr>
            </table>
                      <br />
		    <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
              <tr>
                <td width="928"><strong class="blogFont">3. Summary of Work </strong>
                    <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                      <tr bgcolor="#D8E7CF">
                        <td width="25%" bgcolor="#D8E7CF">
                            <asp:TextBox ID="txtSum" runat="server" TextMode="MultiLine" Width="99%" Height="67px"></asp:TextBox></td>
                      </tr>
                </table></td>
              </tr>
            </table>
                      <br />
		    <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
              <tr>
                <td width="928"><strong class="blogFont">4. Specification of the deliverables</strong>
                    <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                      <tr bgcolor="#D8E7CF">
                        <td width="25%" bgcolor="#D8E7CF" style="height: 69px">
                            <asp:TextBox ID="txtSpec" runat="server" TextMode="MultiLine" Width="99%" Height="67px"></asp:TextBox></td>
                      </tr>
                </table></td>
              </tr>
            </table>
                      <br />
		    <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
              <tr>
                <td width="928"> This agreement has been signed by authorised representative of ARS T&amp;TT and ARS SE
                    <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                      <tr bgcolor="#C4DAB6">
                        <td width="25%" height="20"><strong>For ARS T&amp;TT,</strong></td>
                        <td width="25%"><strong> For ARS Software Engineering,</strong> </td>
                      </tr>
                      <tr bgcolor="#D8E7CF">
                        <td bgcolor="#D8E7CF">
                            <asp:ListBox ID="listTTStakeHolders" runat="server" SelectionMode="Multiple" Width="457px"></asp:ListBox></td>
                        <td bgcolor="#D8E7CF">
                            <asp:ListBox ID="listSEStakeHolders" runat="server" Width="457px" SelectionMode="Multiple"></asp:ListBox></td>
                      </tr>
                    </table>
                    <em> <br>
      The validity of this contract is applicable and binding on all concerned from the project initiation day. A paper and signed copy at SE and T&amp;TT is mandatory. New versions always supersede previous versions in full. The agreement should follow a request within 5 working days, unless agreed with mutual consent in written. Variance work will lead to a new version of this agreement, or new agreement within 5 days.</em> </td>
              </tr>
            </table>
                      <br />
                      <p align="center">
                          &nbsp;<asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview"
                          Width="92px" />
                        </p>  
                      <p>
                          &nbsp;</p>
                          </asp:View>
                          
                 <!-- ###################################### View 2 ####################################################### -->         
                  <asp:View ID="View2" runat="server">
                      <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
                          <tr>
                              <td width="928" class="blogFont">
                                  <strong class="blogFont">1. Project Information</strong>
                                  <table id="Table1" runat="server" width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#C4DAB6">
                                          <td width="25%" height="20">
                                              Project Name
                                          </td>
                                          <td width="25%">
                                              Project No.
                                          </td>
                                          <td width="25%">
                                              Start Date of Agreement
                                          </td>
                                          <td width="25%">
                                              End Date of Agreement
                                          </td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="25">
                                              <asp:Label ID="lbl_pr_ProjName" runat="server" Text="Label" Width="220px"></asp:Label></td>
                                          <td>
                                              <asp:Label ID="lbl_pr_ProjNo" runat="server" Text="Label" Width="219px"></asp:Label></td>
                                          <td>
                                              <asp:Label ID="lbl_pr_SDate" runat="server" Text="Label" Width="210px"></asp:Label></td>
                                          <td>
                                              <asp:Label ID="lbl_pr_EDate" runat="server" Text="Label" Width="201px"></asp:Label></td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                      </table>
                      <br />
                      <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
                          <tr>
                              <td width="928" style="height: 167px">
                                  <strong class="blogFont">2. Project Category </strong><em>(Please select the project
                                      category and specify number of hours) </em>
                                  <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#C4DAB6">
                                          <td width="71%" bgcolor="#C4DAB6" height="20">
                                              <strong>Project Category </strong>
                                          </td>
                                          <td width="25%">
                                              <strong>Person Hours </strong>
                                          </td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="20">
                                              <p>
                                                  <strong>Fixed Price Work</strong> ( maximum number of man hours agreed )</p>
                                          </td>
                                          <td>
                                              <asp:Label ID="lbl_pr_FP" runat="server" Text="Label" Width="176px"></asp:Label></td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="20">
                                              <strong>Time and material based</strong> (number of man hours estimated for work.
                                              When nearing the quantum at 80%, T&amp;TT will be notified to change the mandate)
                                          </td>
                                          <td>
                                              <asp:Label ID="lbl_pr_TM" runat="server" Text="Label" Width="173px"></asp:Label></td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="20">
                                              <strong>Services </strong>(maximum number of man hours per month)
                                          </td>
                                          <td>
                                              <asp:Label ID="lbl_pr_Services" runat="server" Text="Label" Width="174px"></asp:Label></td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="20">
                                              <strong>Ad-Hoc Support </strong>(maximum number of man hours)&nbsp;
                                          </td>
                                          <td>
                                              <asp:Label ID="lbl_pr_Adhoc" runat="server" Text="Label" Width="176px"></asp:Label></td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                      </table>
                      <br />
                      <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1" runat=server>
                          <tr>
                              <td width="928">
                                  <strong class="blogFont">3. Summary of Work </strong>
                                  <table id="td_pr_Summary" runat="server" width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#D8E7CF">
                                          <td width="25%" bgcolor="#D8E7CF" height="80" valign="top" >
                                              <asp:Label ID="lbl_pr_sum" runat="server" Text="Label" Width="915px"></asp:Label></td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                      </table>
                      <br /><table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
                          <tr>
                              <td width="928">
                                  <strong class="blogFont">4. Specification of the deliverables</strong>
                                  <table id="td_pr_Spec" width="100%" runat=server  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#D8E7CF">
                                          <td bgcolor="#D8E7CF" height="50" width="25%" >
                                              <asp:Label ID="lbl_pr_spec" runat="server" Text="Label" Width="919px"></asp:Label></td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                      </table>
                      <br />
                      <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
                          <tr>
                              <td width="928">
                                  This agreement has been signed by authorised representative of ARS T&amp;TT and
                                  ARS SE
                                  <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#C4DAB6">
                                          <td width="25%" height="20">
                                              <strong>For ARS T&amp;TT,</strong></td>
                                          <td width="25%">
                                              <strong>For ARS Software Engineering,</strong>
                                          </td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF"> <!----------- Layers ------------------------------------->
                                          <td bgcolor="#D8E7CF" valign="top">
                                              <div id="layTTUsers" style="width: 350px; padding-left:5px;" runat="server">
                                              </div>   
                                             </td>
                                          <td bgcolor="#D8E7CF" valign="top">
                                              <div id="laySEUsers" style="width: 350px; padding-left:5px;" runat="server">
                                              </div>
                                              
                                              </td>
                                      </tr>
                                  </table>
                                  <em>
                                      <br>
                                      The validity of this contract is applicable and binding on all concerned from the
                                      project initiation day. A paper and signed copy at SE and T&amp;TT is mandatory.
                                      New versions always supersede previous versions in full. The agreement should follow
                                      a request within 5 working days, unless agreed with mutual consent in written. Variance
                                      work will lead to a new version of this agreement, or new agreement within 5 days.</em>
                              </td>
                          </tr>
                      </table>
                      <br />
	      <asp:Button ID="btnAgree" runat="server" Text="Agree" OnClick="btnAgree_Click" Width="76px"  />
              <asp:Button ID="btnDisagree" runat="server" Text="Disagree" OnClick="btnDisagree_Click" /></asp:View>
              
              <!----------- ############################# VIEW 3 ##################################### --------------------------->
                  <asp:View ID="View3" runat="server"><table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
                      <tr>
                          <td width="928" class="blogFont">
                              <strong class="blogFont">1. Project Information</strong>
                              <table id="Table2" runat="server" width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                  <tr bgcolor="#C4DAB6">
                                      <td width="25%" height="20">
                                          Project Name
                                      </td>
                                      <td width="25%">
                                          Project No.
                                      </td>
                                      <td width="25%">
                                          Start Date of Agreement
                                      </td>
                                      <td width="25%">
                                          End Date of Agreement
                                      </td>
                                  </tr>
                                  <tr bgcolor="#D8E7CF">
                                      <td height="25">
                                          <asp:Label ID="lbl_ProjName" runat="server" Text="Label" Width="220px"></asp:Label></td>
                                      <td>
                                          <asp:Label ID="lbl_ProjNo" runat="server" Text="Label" Width="219px"></asp:Label></td>
                                      <td>
                                          <asp:Label ID="lbl_SDate" runat="server" Text="Label" Width="210px"></asp:Label></td>
                                      <td>
                                          <asp:Label ID="lbl_EDate" runat="server" Text="Label" Width="201px"></asp:Label></td>
                                  </tr>
                              </table>
                          </td>
                      </tr>
                  </table>
                      <br /><table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
                          <tr>
                              <td width="928" style="height: 167px">
                                  <strong class="blogFont">2. Project Category </strong><em>(Please select the project
                                      category and specify number of hours) </em>
                                  <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#C4DAB6">
                                          <td width="71%" bgcolor="#C4DAB6" height="20">
                                              <strong>Project Category </strong>
                                          </td>
                                          <td width="25%">
                                              <strong>Person Hours </strong>
                                          </td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="20">
                                              <p>
                                                  <strong>Fixed Price Work</strong> ( maximum number of man hours agreed )</p>
                                          </td>
                                          <td>
                                              <asp:Label ID="lbl_FP" runat="server" Text="Label" Width="176px"></asp:Label></td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="20">
                                              <b>Time and material based</b> (number of
                                              man hours estimated for work. When nearing the quantum at 80%, T&amp;TT will be
                                              notified to change the mandate)
                                          </td>
                                          <td>
                                              <asp:Label ID="lbl_TM" runat="server" Text="Label" Width="173px" Font-Bold="False"></asp:Label></td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="20">
                                              <strong>Services </strong>(maximum number of man hours per month)
                                          </td>
                                          <td>
                                              <asp:Label ID="lbl_Ser" runat="server" Text="Label" Width="174px"></asp:Label></td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <td height="20">
                                              <strong>Ad-Hoc Support </strong>(maximum number of man hours)&nbsp;
                                          </td>
                                          <td>
                                              <asp:Label ID="lbl_Adhoc" runat="server" Text="Label" Width="176px"></asp:Label></td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                      </table>
                      <br /><table width="936"  border="0" align="center" cellpadding="3" cellspacing="1" runat=server id="Table4">
                          <tr>
                              <td width="928">
                                  <strong class="blogFont">3. Summary of Work </strong>
                                  <table id="Table3" runat="server" width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#D8E7CF">
                                          <td width="25%" bgcolor="#D8E7CF" height="50" valign="top" >
                                              <asp:Label ID="lbl_Sum" runat="server" Text="Label" Width="915px"></asp:Label></td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                      </table>
                      <br />
                      <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
                          <tr>
                              <td width="928">
                                  <strong class="blogFont">4. Specification of the deliverables</strong>
                                  <table id="Table5" width="100%" runat=server  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#D8E7CF">
                                          <td bgcolor="#D8E7CF" height="50" width="25%" valign="top" >
                                              <asp:Label ID="lbl_Spec" runat="server" Text="Label" Width="919px"></asp:Label></td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                      </table>
                      <br />
                      <table width="936"  border="0" align="center" cellpadding="3" cellspacing="1">
                          <tr>
                              <td width="928">
                                  This agreement has been signed by authorised representative of ARS T&amp;TT and
                                  ARS SE
                                  <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#459465" style="margin-top:5px;">
                                      <tr bgcolor="#C4DAB6">
                                          <td width="25%" height="20">
                                              <strong>For ARS T&amp;TT,</strong></td>
                                          <td width="25%">
                                              <strong>For ARS Software Engineering,</strong>
                                          </td>
                                      </tr>
                                      <tr bgcolor="#D8E7CF">
                                          <!----------- Layers ------------------------------------->
                                          <td bgcolor="#D8E7CF" valign="top">
                                              <div id="lay_TTUsers" style="width: 350px; padding-left:5px;" runat="server">
                                              </div>
                                          </td>
                                          <td bgcolor="#D8E7CF" valign="top">
                                              <div id="lay_SEUsers" style="width: 350px; padding-left:5px;" runat="server">
                                              </div>
                                          </td>
                                      </tr>
                                  </table>
                                  <em>
                                      <br>
                                      The validity of this contract is applicable and binding on all concerned from the
                                      project initiation day. A paper and signed copy at SE and T&amp;TT is mandatory.
                                      New versions always supersede previous versions in full. The agreement should follow
                                      a request within 5 working days, unless agreed with mutual consent in written. Variance
                                      work will lead to a new version of this agreement, or new agreement within 5 days.</em>
                              </td>
                          </tr>
                      </table>
                      <br />

                      <asp:Panel ID="Pnl_UserButtons" runat="server" Height="32px" Width="100%" HorizontalAlign="Center"> &nbsp;
                      <asp:Button ID="btnStakeholderAgree" runat="server" Text="Agree" Width="113px" OnClick="btnStakeholderAgree_Click" />
                          <asp:Button
                          ID="btnStakeholderDisagree" runat="server" Text="Disagree" Width="113px" OnClick="btnStakeholderDisagree_Click" />
                          </asp:Panel>
                      <br />
                      <hr>
            <div align="center"><strong><br>
                Additional comments or any reasons to disagree if any.<br />
              <br>
                &nbsp;<asp:TextBox ID="txtBlogComments" runat="Server" Columns="80" Height="122px"
                    TextMode="MultiLine" Width="653px"></asp:TextBox></strong><br>
            </div>
            <p align="center">
                Assign to:&nbsp;<asp:DropDownList ID="ddlstUsers" runat="server" CssClass="blogsmallFont" Width="200px">
                </asp:DropDownList>
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                    Text="Save" /></p>
      <hr>
      <p>
      <table border="0" cellpadding="0" cellspacing="0" width="936" align="center">
      <tr><td width="936"><%GetAgreementBlog();%></td></tr>
      </table>      
          
      </p>
                  </asp:View>
              
              </asp:MultiView>
              
              
              &nbsp;
		      </div>
		    </td>
        </tr>
        <tr>
          <td class="blogFont" style="width: 917px"> <p>
              &nbsp;</p>
      </td>
        </tr>
      </table></td>
  </tr>
</table>
</form>
</body>
</html>


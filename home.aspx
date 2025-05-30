<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>:: WorkTrax ::</title>
    <link href="css/pageFormat.css" rel="stylesheet" type="text/css" />
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
   
    -->
    </style>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
    <tr>
    <td height="51" valign="top" style="background-image:url(img/topBar.gif);background-repeat:repeat-x;">
    <table width="100%"  border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="21%"><img src="img/womLogo.gif" width="174" height="23"></td>
        <td width="79%">&nbsp;</td>
      </tr>
      <tr>
        <td height="20" class="user">&nbsp;&nbsp;User :&nbsp;
            <asp:Label ID="lblUser" runat="server" Style="z-index: 100; left: 49px; position: absolute;
                top: 26px"></asp:Label>
        </td>
        <td valign="bottom">
       <form action="" method="post" name="frmShow" id="frmShow">
            <table width="300" border="0" align="right" cellpadding="0" cellspacing="2">
              <tr>
                 <td id="td01" align="center" title="Enter a new work order"><a href="home.aspx" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;List&nbsp;</a> </td>
                <td id="td2" align="center" title="Enter a new work order"><a href="neworder.aspx" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;New Order&nbsp;</a> </td>
                <td id="td03" align="center"><a h
                ef="logoff.aspx" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;Log Off&nbsp;</a> </td>
                <td id="td04" align="center" title="Display a work order by its ID number"><a href="#" class="navLinks" onmouseover="this.className='mouseOn'" onmouseout="this.className='navLinks'">&nbsp;Show #&nbsp;</a> </td>
                <td ><input name="txtShowOrder" type="text" class="showTxt" id="txtShowOrder"></td>
              </tr>
            </table>
        </form>
        </td>
      </tr>
    </table>
	<br />
	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Style="z-index: 100; left: 26px; position: absolute; top: 63px" Font-Names="Verdana" Font-Size="10px" Width="95%" CellPadding="3" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="No Work Orders Found !" AllowSorting="True" OnSorting="GridView1_Sorting" PageSize="15">
            <RowStyle BackColor="Beige" HorizontalAlign="Left" Height="15px" />
            <HeaderStyle BackColor="Black" Font-Names="Verdana" Font-Size="10px" ForeColor="White" HorizontalAlign="Left" Height="20px" />
            <Columns>
            <asp:imagefield dataimageurlfield="StatusImage"></asp:imagefield>
            <asp:HyperLinkField DataNavigateUrlFields="WorkOrderId" DataNavigateUrlFormatString="b_workorder.aspx?wid={0}" DataTextField="WorkOrder" HeaderText="Work Order" SortExpression="WorkOrder"  />
            <asp:BoundField DataField="ProjectName" HeaderText="Project" SortExpression="ProjectName" />
            <asp:BoundField DataField="WorkType" HeaderText="Work Type" SortExpression="WorkType" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"/>
            <asp:BoundField DataField="AssignedTo" HeaderText="Assigned To" SortExpression="AssignedTo" />
            <asp:BoundField DataField="HoursUsed" HeaderText="Total Hours" SortExpression="HoursUsed" />
            <asp:BoundField DataField="FromToDates" HeaderText="Agreement Dates" SortExpression="FromToDates" />
            </Columns>
            <PagerStyle BackColor="Gray" ForeColor="White" Height="10px" HorizontalAlign="Center" />
    </asp:GridView>
    </td>
    </tr>
    </table> 
    </div>
    </form>
</body>
</html>

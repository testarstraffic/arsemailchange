<%@ Page Language="C#" AutoEventWireup="true" CodeFile="neworder.aspx.cs" Inherits="neworder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
    
    .style2 {font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; }
    .style3 {font-size: small}
    -->
    </style>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
    <tr>
    <td height="51" valign="top" style="background-image:url(img/topBar.gif);background-repeat:repeat-x; width: 954px;">
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
    </table>
	<br /><br />
    <p>&nbsp;</p>
    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" BackColor="#E6E2D8" BorderColor="#999999"
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11pt"
            Height="300px" Style="left: 150px; position: relative; top: 32px" Width="774px" OnFinishButtonClick="Wizard1_FinishButtonClick" CellPadding="2" HeaderText="Create Workorder" OnNextButtonClick="Wizard1_NextButtonClick">
            <StepStyle BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="2px" />
            <SideBarStyle BackColor="#1C5E55" Font-Size="8pt" VerticalAlign="Top" Width="180px" />
            <NavigationButtonStyle BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" ForeColor="#1C5E55" />
            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server" Title="1. Project Details" StepType="Start">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <table width="100%"  border="0" cellpadding="10" cellspacing="1"  >
      <tr >
        <td style=" text-align: left;" align="right" class="style2">
            <strong>Select Project </strong>
        </td>
        <td align="center" class="style2">:</td>
        <td style="">
            <asp:DropDownList ID="ddlstProjects" runat="server" BackColor="#F5FAF1" Font-Names="Verdana" Font-Size="9pt" Width="350px" Height="20px">
            </asp:DropDownList>
            </td>
      </tr>
      <tr >
        <td style="">
        </td>
          <td>
          </td>
          <td style="text-align: right; width: 402px;" valign="top">
              <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" style="left: -55px; position: relative; top: -4px">New Project?</asp:LinkButton>
          </td>
      </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <table width="100%"  border="0" cellpadding="10" cellspacing="1" align="center" >
                                <tr >
                                    <td style="width: 118px">
                                        <div align="right" class="style2">
                                            <strong>Enter Project No.&nbsp;</strong></div>
                                    </td>
                                    <td>
                                        <div align="center" class="style2">
                                            :</div>
                                    </td>
                                    <td>
                                        <span class="style3">
                                            <asp:TextBox ID="txtProjNo" runat="server" Width="307px" Font-Names="Verdana" Font-Size="9pt" MaxLength="50"></asp:TextBox>
                                        </span>
                                    </td>
                                </tr>
                                <tr ><td style="width: 118px">
                                    <div align="right" class="style2">
                                        <strong>Project Name</strong></div>
                                </td>
                                    <td>
                                        <div align="center" class="style2">
                                            :</div>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtProjName" runat="server" Width="307px" Font-Names="Verdana" Font-Size="9pt" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 118px">
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Button ID="btnCreateProject" runat="server" Text="create new project" OnClick="btnCreateProject_Click" />
                                    </td>
                                </tr>
                            </table>
                           <div align=center style="text-align: left"> <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                ForeColor="Red" Width="330px" style="left: 186px; position: relative; top: 0px"></asp:Label></div>
                        </asp:View>
                    </asp:MultiView>
                    
                </asp:WizardStep>
                <asp:WizardStep runat="server" StepType="Finish" Title="2. Generate Workorder">
                    <table  border="0" cellpadding="10" cellspacing="1" align="center" style="width: 91%">
                        <tr>
                            <td style="text-align: right; width: 714px;">
                                <strong>WA No.</strong></td>
                            <td>
                                :</td>
        <td><span class="style3">
            <asp:Label ID="lbWANo" runat="server" Font-Names="Verdana" Font-Size="10pt" Text="-"
                Width="341px"></asp:Label>
        </span></td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 714px;">
                                <strong>Project</strong></td>
                            <td>
                                :</td>
                            <td>
                                <asp:Label ID="lblProj" runat="server" Font-Names="Verdana" Font-Size="10pt" Text="-"
                                    Width="341px"></asp:Label>
                            </td>
                        </tr>
                        <tr><td style="width: 714px"><div align="right" class="style2">
                    <strong>Work Type</strong></div></td>
        <td><div align="center" class="style2">:</div></td>
        <td><span class="style3">
            <asp:DropDownList ID="ddlstWorkTypes" runat="server" BackColor="#F5FAF1" Font-Names="Verdana" Font-Size="11px" Width="350px" DataSourceID="SqlDSWorkTypes" DataTextField="WorkType" DataValueField="WorkTypeId">
            </asp:DropDownList>
        </span></td>
                        </tr>
                        <tr>
                            <td style="width: 714px; text-align: right;">
                                <strong>ARSSE PM</strong></td>
                            <td style="width: 100px">
                                :</td>
                            <td style="width: 100px">
                            <asp:ListBox ID="lstStakeholders" runat="server" BackColor="#F5FAF1" Font-Names="Verdana" Font-Size="11px" Width="349px" SelectionMode="Multiple" Height="133px"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                    <asp:SqlDataSource ID="SqlDSWorkTypes" runat="server" ConnectionString="Data Source=DURGA;Initial Catalog=WorkTrax;User ID=sa"
                        ProviderName="System.Data.SqlClient" SelectCommand="SELECT [WorkTypeId], [WorkType] FROM [WorkTypes]">
                    </asp:SqlDataSource>
                    <asp:Label ID="lblError2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                        ForeColor="Red" Style="left: 59px; position: relative; top: 0px" Width="457px"></asp:Label>
                </asp:WizardStep>
            </WizardSteps>
            <SideBarButtonStyle ForeColor="WhiteSmoke" Font-Names="Verdana" Font-Size="8pt" Font-Underline="False" />
            <HeaderStyle BackColor="#1C5E55" BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="2px"
                Font-Bold="True" Font-Size="10pt" ForeColor="White" HorizontalAlign="Center" Height="25px" />
        </asp:Wizard>
    
    </td>
    </tr>
    </table> 
    </div>
        
    </form>
</body>
</html>

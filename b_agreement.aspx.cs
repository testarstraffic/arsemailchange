using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;
using WorkTrax;

public partial class b_agreement : System.Web.UI.Page
{
    // Declare objects
    private WorkTraxDB wtDatabase;
    private Settings wtSettings;
    private Logger wtLogger;
    private ErrorConstants wtError;
    private WorkOrder oWorkOrder;

    protected void Page_Load(object sender, EventArgs e)
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        if (Session["LoggedInUser"] == null)
        {
            Response.Write("<p><b><font color='red'>Page expired.. Please <a href='Default.aspx'>relogin </a></font></b></p>");
            Response.End();
        }

        // Open active connection with database
        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        //----------------------------------------------------------
        // Get the id of the workorder passed from the previous page
        // and Add WorkOrderId to Session
        if (Session["WorkOrderId"].ToString() == "")
            Session.Add("WorkOrderId", Request.QueryString["wid"]);
        //----------------------------------------------------------

        if (!Page.IsPostBack)
        {
            GetWorkOrderDetails();
            PopulateStakeHolders();
        }

        // Destroy objects
        wtDatabase.CloseDatabase();
        wtDatabase = null;
        wtLogger = null;
        wtSettings = null;
        wtError = null;
        oWorkOrder = null;
    }

    /// <summary>
    /// Display details of Work Order
    /// </summary>
    private void GetWorkOrderDetails()
    {
        oWorkOrder = new WorkOrder();
        // Get the id of the workorder passed from the previous page
        oWorkOrder.WorkOrderId = int.Parse(Session["WorkOrderId"].ToString());
        wtDatabase.GetWorkOrderDetails(oWorkOrder);
        
        // Display username on header bar
        lblUser.Text = Session["LoggedInUser"].ToString(); 

        // Fill values in table (WorkOrder details)
        tblWODetails.Rows[0].Cells[1].InnerText = oWorkOrder.WorkOrderName;
        tblWODetails.Rows[1].Cells[1].InnerText = oWorkOrder.Project;
        tblWODetails.Rows[2].Cells[1].InnerText = oWorkOrder.WorkType;
        tblWODetails.Rows[3].Cells[1].InnerText = oWorkOrder.AssignedTo;
    }

    private void PopulateStakeHolders()
    {
        DataTable dtUsers;
        // Populate stakeholders
        dtUsers = wtDatabase.GetStakeHolders("AL");
        ddlstUsers.DataTextField = "EmployeeName";
        ddlstUsers.DataValueField = "UserId";
        ddlstUsers.DataSource = dtUsers;
        ddlstUsers.DataBind();
        dtUsers = null;
    }

    /// <summary>
    /// Print blog contents
    /// </summary>
    public void GetAgreementBlog()
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        // Open active connection with database
        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        oWorkOrder = new WorkOrder();
        // Get the blog contents and print on page
        string blogTable = "";
        blogTable = wtDatabase.GetAgreementBlog(int.Parse(Session["WorkOrderId"].ToString()), Server.MapPath("Uploads") + "\\WorkOrder\\");
        Response.Write(blogTable);

        // Destroy objects
        wtDatabase.CloseDatabase();
        wtDatabase = null;
        wtLogger = null;
        wtSettings = null;
        wtError = null;
        oWorkOrder = null;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        // Get current date/time
        string currentDateTime = DateTime.Now.ToString("dd-MMM-yyyy") + " " + DateTime.Now.ToString("HH:MM");

        // Get attached filename
        string attachedFileName = FileUpload1.FileName;
        if (attachedFileName == "")
            attachedFileName = "";

        // Get details to save
        oWorkOrder = new WorkOrder();
        oWorkOrder.WorkOrderId = int.Parse(Session["WorkOrderId"].ToString());
        if(ddlstUsers.SelectedIndex == 0) // If no stake holder selected
            oWorkOrder.AGAssignedDetails = "<b>Commented by <font color=green>" + Session["LoggedInUser"].ToString() + "</font></b> " + currentDateTime;
        else
            oWorkOrder.AGAssignedDetails = "<b>Assigned to <font color=green>" + ddlstUsers.SelectedItem.Text + "</font> by <font color=green>" + Session["LoggedInUser"].ToString() + "</font></b> " + currentDateTime;
        oWorkOrder.AGBlogComments = txtBlogComments.Text.Trim();
        oWorkOrder.AGFileName = attachedFileName;
        
        // Get attached file
        // If no file attached, attach a dummy file
        string attachedFile = FileUpload1.FileName;
        if (attachedFile == "")
            attachedFile = "_dummy.txt";

        try
        {
            // Upload file to folder
            FileUpload1.SaveAs(Server.MapPath("Uploads") + "\\WorkOrder\\" + oWorkOrder.WorkOrderId + "\\" + attachedFile);
        }
        catch(Exception ex)
        {
            wtLogger.LogMessage(LogPriorityLevel.FatalError, ">> Attachment Folder for EffortSchedule blog NOT Found! " + ex.Message.ToString());
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
            wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        // Open active connection with database
        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        string FromEmailId = "", ToEmailId = "";
        
        try
        {
            // Save to database
            //>>>>>>>>> wtDatabase.InsertAgreementBlog(oWorkOrder);
            FromEmailId = wtDatabase.GetEmailId(int.Parse(Session["LoggedInUserId"].ToString()));
            ToEmailId = wtDatabase.GetEmailId(int.Parse(ddlstUsers.SelectedItem.Value));
        }
        catch (Exception ex)
        {
            wtLogger.LogMessage(LogPriorityLevel.FatalError, ">> Error inserting Agreement blog. " + ex.Message.ToString());
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
            wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        string CurrentDateTime = DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("hh:mm");
        string HyperLinkString = wtSettings.SiteBaseURL + "navigate.aspx?wo=" + Session["WorkOrderName"].ToString() + "&pg=ag";

        // Destroy objects
        wtDatabase.CloseDatabase();
        wtDatabase = null;
        wtLogger = null;
        wtSettings = null;
        wtError = null;
        oWorkOrder = null;

        //----------------------------------
        // Send mail to 'AssignedTo' person
        //----------------------------------
        try
        {
            MailMessage oMailMessage = new MailMessage();
            oMailMessage.From = new MailAddress(FromEmailId, Session["LoggedInUser"].ToString());
            //oMailMessage.To.Add(new MailAddress(ToEmailId));
            oMailMessage.To.Add(new MailAddress("hari@arssoftware.com"));
            oMailMessage.Subject = "WorkTrax : " + Session["WorkOrderName"].ToString();
            oMailMessage.IsBodyHtml = true;

            StringBuilder BodyContents = new StringBuilder();
            BodyContents.Append("<table width='90%'  border='0' cellpadding='3' cellspacing='1' bgcolor='#FFFFFF'>");
            BodyContents.Append("<tr bgcolor='#FFFFFF'>");
            BodyContents.Append("<td width='100%' style='font-size: 11px; font-family: Verdana;'><strong><a href='" + HyperLinkString + "'>WorkTrax</a> : Agreement Blog Notification</strong></td>");
            BodyContents.Append("</tr></table>");
            BodyContents.Append("<br>");

            BodyContents.Append("<table width='90%'  border='0' cellpadding='3' cellspacing='1' bgcolor='#FFFFFF'>");
            BodyContents.Append("<tr bgcolor='#C3DEC3'>");
            BodyContents.Append("<td width='25%' style='font-size: 11px; font-family: Verdana;'><strong>Work Order</strong></td>");
            BodyContents.Append("<td width='25%' style='font-size: 11px; font-family: Verdana;'><strong>From</strong></td>");
            BodyContents.Append("<td width='25%' style='font-size: 11px; font-family: Verdana;'><strong>To</strong></td>");
            BodyContents.Append("<td width='25%' style='font-size: 11px; font-family: Verdana;'><strong>Date</strong></td>");
            BodyContents.Append("</tr><tr bgcolor='#C3DEC3'>");
            BodyContents.Append("<td width='25%' style='font-size: 11px; font-family: Verdana;'>" + Session["WorkOrderName"].ToString() + "</td>");
            BodyContents.Append("<td width='25%' style='font-size: 11px; font-family: Verdana;'>" + Session["LoggedInUser"].ToString() + "</td>");
            BodyContents.Append("<td width='25%' style='font-size: 11px; font-family: Verdana;'>" + ddlstUsers.SelectedItem.Text + "</td>");
            BodyContents.Append("<td width='25%' style='font-size: 11px; font-family: Verdana;'>" + CurrentDateTime + "</td>");
            BodyContents.Append("</tr></table>");

            BodyContents.Append("<br>");
            BodyContents.Append("<table width='90%'  border='0' cellpadding='3' cellspacing='1' bgcolor='#FFFFFF'>");
            BodyContents.Append("<tr bgcolor='#C3DEC3'>");
            BodyContents.Append("<td width='100%' style='font-size: 11px; font-family: Verdana;'><strong>Comments : </strong>" + txtBlogComments.Text.Trim() + "</td>");
            BodyContents.Append("</tr></table>");


            oMailMessage.Body = BodyContents.ToString();
            SmtpClient oSmtpclient = new SmtpClient();
            oSmtpclient.Send(oMailMessage);
        }
        catch (Exception ex)
        {
            wtLogger.LogMessage(LogPriorityLevel.FatalError, ">> Error sending Email. " + ex.Message.ToString());
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
            wtError.ERR_SEND_MAIL + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
}

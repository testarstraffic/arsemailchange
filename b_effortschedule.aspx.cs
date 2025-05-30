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
using System.IO;
using WorkTrax;

public partial class b_effortschedule : System.Web.UI.Page
{
    // Declare objects
    private WorkTraxDB wtDatabase;
    private Settings wtSettings;
    private Logger wtLogger;
    private ErrorConstants wtError;
    private WorkOrder oWorkOrder;
    int showErrPanel;
    string fileSavePath;

    protected void Page_Load(object sender, EventArgs e)
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        MultiView1.ActiveViewIndex = 0;

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
       // if (Session["WorkOrderId"].ToString() == "")
            Session.Add("WorkOrderId", Request.QueryString["wid"]);

        //----------------------------------------------------------

        if (!Page.IsPostBack)
        {
            GetWorkOrderDetails();
            PopulateStakeHolders();
            showErrPanel = 0;
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
    public void GetEffortScheduleBlog()
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
        blogTable = wtDatabase.GetEffortScheduleBlog(int.Parse(Session["WorkOrderId"].ToString()), "Uploads\\WorkOrder\\");
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
        //string currentDateTime = DateTime.Now.ToString("dd-MMM-yyyy") + " " + DateTime.Now.ToString("HH:MM");

        // Get attached filename
        string attachedFileName = FileUpload1.FileName;
        if (attachedFileName == "")
            attachedFileName = "";
                
        // Get attached file
        string attachedFile = FileUpload1.FileName.Trim();

        if (attachedFile.Length > 0)
        {
            try
            {
                string workOrderId = Request.QueryString["wid"];
                string sFilePath = Server.MapPath("Uploads") + "\\WorkOrder\\" + workOrderId;

                if (!Directory.Exists(sFilePath)) // If folder doesn't exists, create one..
                {
                    // Specify a "currently active folder"
                    string activeDir = Server.MapPath("Uploads") + "\\WorkOrder\\";

                    //Create a new subfolder under the current active folder
                    string newPath = System.IO.Path.Combine(activeDir, workOrderId);

                    // Create the subfolder
                    System.IO.Directory.CreateDirectory(newPath);

                }
                 fileSavePath = sFilePath + "\\" + attachedFile;
                 Session.Add("attachedFile", attachedFile);// add to session 

                if (File.Exists(fileSavePath))
                {
                    showErrPanel = 1;
                    MultiView1.ActiveViewIndex = 1;
                }
                else
                {
                    // Upload file to folder
                    FileUpload1.SaveAs(fileSavePath);
                }
            }
            catch (Exception ex)
            {
                wtLogger.LogMessage(LogPriorityLevel.FatalError, ">> Attachment Folder for EffortSchedule blog NOT Found! " + ex.Message.ToString());
                string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                wtError.ERR_DB_CONNECTION + "</b></font></div>";
                Response.Write(responseError);
                Response.End();
            }
        }
        

        if (showErrPanel == 0)
        {
            ErrorPanel.Visible = false;
            InsertBlogInfo(attachedFile);
        }
        else
        {
            ErrorPanel.Visible = true;
        }

        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    /*********************************************
 * Insert Blog details with attachment info.
 * Mail to corresponding assigned person
 ********************************************/
    protected void InsertBlogInfo(string attachedFile)
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        // Get current date/time
        string currentDateTime = DateTime.Now.ToString("dd-MMM-yyyy") + " " + DateTime.Now.ToString("HH:MM");

        // Get details to save
        oWorkOrder = new WorkOrder();
        oWorkOrder.WorkOrderId = int.Parse(Session["WorkOrderId"].ToString());

        //if (ddlstUsers.SelectedValueSelectedIndex == 0) // If no stake holder selected
        //    oWorkOrder.ESAssignedDetails = "<b>Commented by <font color=green>" + Session["LoggedInUser"].ToString() + "</font></b> " + currentDateTime;
        //else
            oWorkOrder.ESAssignedDetails = "<b>Assigned to <font color=green>" + ddlstUsers.SelectedItem.Text + "</font> by <font color=green>" + Session["LoggedInUser"].ToString() + "</font></b> " + currentDateTime;

        oWorkOrder.ESBlogComments = txtBlogComments.Text.Trim();
        oWorkOrder.ESFileName = attachedFile;


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
            wtDatabase.InsertEffortScheduleBlog(oWorkOrder);
            // Get the Email IDs
            FromEmailId = wtDatabase.GetEmailId(int.Parse(Session["LoggedInUserId"].ToString()));
            ToEmailId = wtDatabase.GetEmailId(int.Parse(ddlstUsers.SelectedItem.Value));
        }
        catch (Exception ex)
        {
            wtLogger.LogMessage(LogPriorityLevel.FatalError, ">> Error inserting WorkOrder blog. " + ex.Message.ToString());
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
            wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        string CurrentDateTime = DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("hh:mm");
        string HyperLinkString = wtSettings.SiteBaseURL + "navigate.aspx?wo=" + Session["WorkOrderName"].ToString() + "&pg=wo";

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
            oMailMessage.To.Add(new MailAddress("demis@arssoftware.com"));
            oMailMessage.Subject = "WorkTrax : " + Session["WorkOrderName"].ToString();
            oMailMessage.IsBodyHtml = true;

            StringBuilder BodyContents = new StringBuilder();
            BodyContents.Append("<table width='90%'  border='0' cellpadding='3' cellspacing='1' bgcolor='#FFFFFF'>");
            BodyContents.Append("<tr bgcolor='#FFFFFF'>");
            BodyContents.Append("<td width='100%' style='font-size: 11px; font-family: Verdana;'><strong><a href='" + HyperLinkString + "'>WorkTrax</a> : Work Order Blog Notification</strong></td>");
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

            txtBlogComments.Text = String.Empty;
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
    protected void BtnOverwrite_Click(object sender, EventArgs e)
    {
        string workOrderId = Request.QueryString["wid"];
        string sFilePath = Server.MapPath("Uploads") + "\\WorkOrder\\" + workOrderId;
        sFilePath = sFilePath + "\\" + FileUpload2.FileName.Trim();
        FileUpload2.SaveAs(sFilePath);
        ErrorPanel.Visible = false;
        InsertBlogInfo(Session["attachedFile"].ToString());
        MultiView1.ActiveViewIndex = 0;
    }
}

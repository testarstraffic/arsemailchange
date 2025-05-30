using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security;
using WorkTrax;

public partial class _Default : System.Web.UI.Page 
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

        // Get windows domain user
        string windowsDomainUser = User.Identity.Name;
        wtLogger.LogMessage(LogPriorityLevel.Informational, ">> User windows domain - " + windowsDomainUser.ToString());
        // Continue if valid domain user
        if (User.Identity.IsAuthenticated)
        {
            // Split to retrieve username only
            char[] slash_splitter = { '\\' };
            string[] arr_windowsDomainUser = windowsDomainUser.Split(slash_splitter);
            string username = arr_windowsDomainUser[1];

            // Call database function
            if (!wtDatabase.OpenDatabase())
            {
                string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                       wtError.ERR_DB_CONNECTION + "</b></font></div>";
                Response.Write(responseError);
                Response.End();
            }

            oWorkOrder = new WorkOrder();
            int iRetVal = wtDatabase.ValidateWorkTraxUser(username, oWorkOrder);
            //int iRetVal = wtDatabase.ValidateWorkTraxUser("Demis", oWorkOrder);
            string LoggedInUser = oWorkOrder.LoggedInUser;
            // Add EmployeeName and Id (Logged in user) to Session
            Session.Add("LoggedInUser", LoggedInUser);
            Session.Add("LoggedInUserId", oWorkOrder.LoggedInUserId);

            // Add null value to session variable
            Session.Add("WorkOrderId", "");

            // Invalid user
            if (iRetVal == 0)
            {
                Response.Write("<br><br><div align='center'><font face='verdana' size='2px' color='red'>" +
                       "<b>Access denied, Invalid user.</b></font></div>");
                wtLogger.LogMessage(LogPriorityLevel.FatalError, ">> User could not login - " + username);
            }
            // Valid user
            else
                Response.Redirect("home.aspx");
        }
        // If not found in domain, display login page
        else
        {
            Response.Redirect("userlogin.aspx", true);
        }
        // Destroy objects
        wtDatabase.CloseDatabase();
        wtDatabase = null;
        wtLogger = null;
        wtSettings = null;
        wtError = null;
        oWorkOrder = null;
    }
}

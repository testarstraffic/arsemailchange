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
using WorkTrax;

public partial class navigate : System.Web.UI.Page
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
        // Continue only if valid domain user
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
                Response.End();
            }
        }


        // Open active connection with database
        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        String WorkOder = Request.QueryString["wo"];
        String RedirectPage = Request.QueryString["pg"];

        if (!Page.IsPostBack)
        {
            if (RedirectPage == "wo")
                RedirectPage = "b_workorder";
            else if (RedirectPage == "es")
                RedirectPage = "b_effortschedule";
            else if (RedirectPage == "ag")
                RedirectPage = "b_agreement";
                            
            oWorkOrder = new WorkOrder();
            int WorkOderId = wtDatabase.GetWorkOrderId(WorkOder);

            Session.Remove("WorkOrderId");
            Session.Add("WorkOrderId", "");

            Session.Remove("LoggedInUser");
            Session.Add("LoggedInUser", "");

            Response.Redirect(RedirectPage + ".aspx?wid=" + WorkOderId);
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

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

public partial class userlogin : System.Web.UI.Page
{
    // Declare objects
    private WorkTraxDB wtDatabase;
    private Settings wtSettings;
    private Logger wtLogger;
    private ErrorConstants wtError;
    private WorkOrder oWorkOrder;

    protected void Page_Load(object sender, EventArgs e)
    {
        //txtUsername.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text.Trim().Equals(string.Empty) || (txtPassword.Text.Trim().Equals(string.Empty)))
            return;
        else
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
            int iRetVal = wtDatabase.ValidateWebUser(txtUsername.Text.Trim(), txtPassword.Text.Trim(), oWorkOrder);
            string LoggedInUser = oWorkOrder.LoggedInUser;
            // Add EmployeeName and Id (Logged in user) to Session
            Session.Add("LoggedInUser", LoggedInUser);
            Session.Add("LoggedInUserId", oWorkOrder.LoggedInUserId);

            // Add null value to session variable
            Session.Add("WorkOrderId", "");

            // Invalid user
            if (iRetVal == 0)
                lblLoginStatus.Text = "Invalid User !";            
            // Valid user
            else
                Response.Redirect("home.aspx");

            // Destroy objects
            wtDatabase.CloseDatabase();
            wtDatabase = null;
            wtLogger = null;
            wtSettings = null;
            wtError = null;
            oWorkOrder = null;
        }
    }

}

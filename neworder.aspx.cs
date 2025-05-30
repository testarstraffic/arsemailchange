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
using System.Data.SqlClient;
using WorkTrax;

public partial class neworder : System.Web.UI.Page
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

        MultiView1.ActiveViewIndex = 0;
        lblError.Text = "";
        lblError2.Text = "";

        if (Session["LoggedInUser"] == null)
        {
            Response.Write("<p><b><font color='red'>Page expired.. Please <a href='Default.aspx'>relogin </a></font></b></p>");
            Response.End();
        }

        //----------------------------------------------------------
        // Get the id of the workorder passed from the previous page
        // and Add WorkOrderId to Session
        //if (Session["WorkOrderId"].ToString() == "")
        //    Session.Add("WorkOrderId", Request.QueryString["wid"]);
        //----------------------------------------------------------

        if (!Page.IsPostBack)
        {
            // Open active connection with database
            if (!wtDatabase.OpenDatabase())
            {
                string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                       wtError.ERR_DB_CONNECTION + "</b></font></div>";
                Response.Write(responseError);
                Response.End();
            }

            FillLookUpData();
        }
    }

    private void FillLookUpData()
    {

        // Create datatable
        DataTable m_DataTable;

        // Populate Projects
        m_DataTable = wtDatabase.GetProjects();
        ddlstProjects.DataTextField = "Project";
        ddlstProjects.DataValueField = "ProjectId";
        ddlstProjects.DataSource = m_DataTable;
        ddlstProjects.DataBind();
        
        // Populate Stakeholders
        m_DataTable = wtDatabase.GetStakeHolders("SEPM");
        lstStakeholders.DataTextField = "EmployeeName";
        lstStakeholders.DataValueField = "UserId";
        lstStakeholders.DataSource = m_DataTable;
        lstStakeholders.DataBind();
        
        // Destroy datatable
        m_DataTable = null;

        // Destroy objects
        //wtDatabase.CloseDatabase();
        //wtDatabase = null;
        //wtLogger = null;
        //wtSettings = null;
        //wtError = null;
        //oWorkOrder = null;
    }
    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (lbWANo.Text == "-")
        {
            lblError2.Text = "Invalid project selection. Click 'Previous' button to select.";
        }
        else
        {
            if (!wtDatabase.OpenDatabase())
            {
                string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                       wtError.ERR_DB_CONNECTION + "</b></font></div>";
                lblError2.Text = responseError;
                Response.End();
            }

            wtDatabase.InsertWorkOrder(
                                    lbWANo.Text,
                                    Convert.ToInt32(ddlstWorkTypes.SelectedValue.ToString()),
                                    Convert.ToInt32(ddlstProjects.SelectedValue.ToString()),
                                    Convert.ToInt32(Session["LoggedInUserId"].ToString()),
                                    Convert.ToInt32(lstStakeholders.SelectedValue)
                                    );
            Response.Redirect("home.aspx");
        }

    }
    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        int projId;
        string workAgreeNo;
        string[] getddlstVals = ddlstProjects.SelectedItem.Text.Split(':');
        workAgreeNo = getddlstVals[0];
        string projectName = getddlstVals[1];
        

        projId = Convert.ToInt32(ddlstProjects.SelectedValue);

        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            lblError2.Text = responseError;
            Response.End();
        }
        projId = wtDatabase.GetProjectCount(projId);
        projId++; // increment workorder count as per project

        if (projId.ToString().Length < 2)
        {
            workAgreeNo += " - 0" + projId.ToString();
        }
        else
        {
            workAgreeNo += " - "+ projId.ToString();
        }

        lbWANo.Text = workAgreeNo;
        lblProj.Text = projectName;                            
        
    }
    protected void btnCreateProject_Click(object sender, EventArgs e)
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            lblError2.Text = responseError;
            Response.End();
        }

        string sprojNo, sprojName;
        sprojNo   = txtProjNo.Text.ToString().Trim();
        sprojName = txtProjName.Text.ToString().Trim();

        if ((sprojNo.Length < 1) || (sprojName.Length < 1))
        {
            lblError2.Text = "Project details required.";
            MultiView1.ActiveViewIndex = 1;
        }
        else
        {
            int dbResult = wtDatabase.InsertProject(sprojNo, sprojName);

            if (dbResult == 1)
            {
                MultiView1.ActiveViewIndex = 0;
                FillLookUpData();

            }
            else if (dbResult == 0)
            {
                lblError2.Text = "Project number already exists!";
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                lblError2.Text = "Unexpected error in DB.";
                MultiView1.ActiveViewIndex = 1;
            }
        }

        // Destroy objects
        wtDatabase.CloseDatabase();
        wtDatabase = null;
        wtLogger = null;
        wtSettings = null;
        wtError = null;
        oWorkOrder = null;         
       
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        txtProjNo.Text = "";
        txtProjName.Text = "";
         
        MultiView1.ActiveViewIndex = 1;

    }
}

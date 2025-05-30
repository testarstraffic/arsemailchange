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

public partial class home : System.Web.UI.Page
{
    // Declare objects
    private WorkTraxDB wtDatabase;
    private Settings wtSettings;
    private Logger wtLogger;
    private ErrorConstants wtError;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];

        if (Session["LoggedInUser"] == null)
        {
            Response.Write("<p><b><font color='red'>Page expired.. Please <a href='Default.aspx'>relogin </a></font></b></p>");
            Response.End();
        }

        if(!Page.IsPostBack)
        {
            PopulateGrid();
            Session.Remove("WorkOrderId");
            Session.Add("WorkOrderId", "");

            try
            {
                // Display username on header bar
                lblUser.Text = Session["LoggedInUser"].ToString();
            }
            catch (Exception ex)
                {
                    //Response.Redirect("home.aspx");
                    wtLogger.LogMessage(LogPriorityLevel.FatalError, "Session null - home.aspx.cs " + ex.Message);

                }
        }
    }


    /// <summary>
    /// Populate GridView
    /// </summary>
    private void PopulateGrid()
    {
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        // Call database function
        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        // Bind GridView to datatable
        DataTable dtWorkOrders = wtDatabase.GetWorkOdersListing();
        GridView1.DataSource = dtWorkOrders;
        GridView1.DataBind();
        
        // Destroy objects
        wtDatabase.CloseDatabase();
        //wtDatabase = null;
        //wtLogger = null;
        //wtSettings = null;
        //wtError = null;
    }

    /// <summary>
    /// Change Row background color only for "Agreed" items
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Proceed only if its a data row and not a header/footer
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Retrieve the dataitem, and cast it to the appropriate type 
            // GridView is bound to an SqlDataSource and so dataitem is of type DataRowView
            DataRowView drv = e.Row.DataItem as DataRowView;
            // Proceed only if the Value of the column 'Status' is 'Agreed'
            if (drv["Status"].ToString().Equals("Agreed"))
            {
                e.Row.BackColor = System.Drawing.Color.WhiteSmoke;
                // Change Cell color
                // e.Row.Cells[1].BackColor = System.Drawing.Color.Gray;
            }
        }
    }

    /// <summary>
    /// Enable paging of GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        PopulateGrid();

        // SORTING EACH PAGE
        //DataTable dt = GridView1.DataSource as DataTable;
        //DataView dv = new DataView(dt);
        //dv.Sort = Session["currentSortExpression"].ToString() + " " + ((SortDirection)Session["sortingState"]);
        //GridView1.DataSource = dv;
        //GridView1.DataBind();

        //foreach (GridViewRow row in GridView1.Rows)
        //{
        //    TableCell cell = row.Cells[2];
        //    DateTime time = Convert.ToDateTime(cell.Text);
        //    cell.Text = time.ToLongDateString();
        //    cell.Wrap = false;
        //}
    }

    /// <summary>
    /// Enable Sorting of columns in GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        // Call database function
        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        DataTable dataTable = wtDatabase.GetWorkOdersListing();
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            // Sort ascending/descending
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                dataView.Sort = e.SortExpression + " " + "DESC";
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                dataView.Sort = e.SortExpression + " " + "ASC";
            } 
            // Bind to DataTable
            GridView1.DataSource = dataView;
            GridView1.DataBind();
        }
            
        // Destroy objects
        wtDatabase.CloseDatabase();
        wtDatabase = null;
        wtLogger = null;
        wtSettings = null;
        wtError = null;
    }

    /// <summary>
    /// Determine if sort in Ascending/Descending order
    /// Set the default sorting to Descending
    /// </summary>
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Descending;
            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }


}

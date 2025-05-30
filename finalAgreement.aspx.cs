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

public partial class finalAgreement : System.Web.UI.Page
{
    // Declare objects
    private WorkTraxDB wtDatabase;
    private Settings wtSettings;
    private Logger wtLogger;
    private ErrorConstants wtError;
    private WorkOrder oWorkOrder;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        /***********************************************************************
         * Attention Developer!
         * 
         * This page has 3 views :-
         * 1) Agreement Form view 
         * 2) Agreement Preview
         * 3) Agreement final view
         * 
         * These cases are handled by the Multiview.ActiveViewIndex method :)
         * ********************************************************************/
        
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
        //if (Session["WorkOrderId"].ToString() == "")
            Session.Add("WorkOrderId", Request.QueryString["wid"]);
        //----------------------------------------------------------

            GetWorkOrderDetails();
            CheckAgreementExists(); // If agreement exists then redirect 

        if (!Page.IsPostBack)
        {
            txtSDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            txtEndDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");           
            PopulateStakeHolders();
        }
        

        // Destroy objects
        //wtDatabase.CloseDatabase();
        //wtDatabase = null;
        //wtLogger = null;
        //wtSettings = null;
       // wtError = null;
        //oWorkOrder = null;
    }

    /// <summary>
    /// Display details of Work Order
    /// </summary>
    private void GetWorkOrderDetails()
    {
        oWorkOrder = new WorkOrder();
        // Get the id of the workorder passed from the previous page
        oWorkOrder.WorkOrderId = int.Parse(Session["WorkOrderId"].ToString());
        oWorkOrder.LoggedInUserId = int.Parse(Session["LoggedInUserId"].ToString());
        wtDatabase.GetWorkOrderDetails(oWorkOrder);

        // Fill values in table (WorkOrder details) 
        txtProjName.Text        = oWorkOrder.Project;
        lbl_pr_ProjName.Text    = oWorkOrder.Project;
        txtProjNo.Text          = oWorkOrder.ProjectNo;
        lbl_pr_ProjNo.Text      = oWorkOrder.ProjectNo;
        lblWANo.Text            = oWorkOrder.WorkOrderName;
        
    }

    private void CheckAgreementExists()
    {
        int flagAgreement;
        flagAgreement = wtDatabase.AgreementCheck(int.Parse(Request.QueryString["wid"]));

        if (flagAgreement != 0)
        {
            MultiView1.ActiveViewIndex = 2;
            ShowAgreement();
        }
    }


    private void PopulateStakeHolders()
    {
        // Display username on header bar
        lblUser.Text = Session["LoggedInUser"].ToString(); 

        DataTable dtUsers;
        // Populate stakeholders
        dtUsers = wtDatabase.GetStakeHolders("AL");
        ddlstUsers.DataTextField = "EmployeeName";
        ddlstUsers.DataValueField = "UserId";
        ddlstUsers.DataSource = dtUsers;
        ddlstUsers.DataBind();

        dtUsers = wtDatabase.GetStakeHolders("TT");
        listTTStakeHolders.DataTextField = "EmployeeName";
        listTTStakeHolders.DataValueField = "UserId";
        listTTStakeHolders.DataSource = dtUsers;
        listTTStakeHolders.DataBind();

        dtUsers = wtDatabase.GetStakeHolders("SE");
        listSEStakeHolders.DataTextField = "EmployeeName";
        listSEStakeHolders.DataValueField = "UserId";
        listSEStakeHolders.DataSource = dtUsers;
        listSEStakeHolders.DataBind();
        
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

       
        // Get details to save
        oWorkOrder = new WorkOrder();
        oWorkOrder.WorkOrderId = int.Parse(Session["WorkOrderId"].ToString());
        if(ddlstUsers.SelectedIndex == 0) // If no stake holder selected
            oWorkOrder.AGAssignedDetails = "<b>Commented by <font color=green>" + Session["LoggedInUser"].ToString() + "</font></b> " + currentDateTime;
        else
            oWorkOrder.AGAssignedDetails = "<b>Assigned to <font color=green>" + ddlstUsers.SelectedItem.Text + "</font> by <font color=green>" + Session["LoggedInUser"].ToString() + "</font></b> " + currentDateTime;
        oWorkOrder.AGBlogComments = txtBlogComments.Text.Trim();
        
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
           wtDatabase.InsertAgreementBlog(oWorkOrder);
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

        
        //----------------------------------
        // Send mail to 'AssignedTo' person
        //----------------------------------
        try
        {
            MailMessage oMailMessage = new MailMessage();
            oMailMessage.From = new MailAddress(FromEmailId, Session["LoggedInUser"].ToString());
            oMailMessage.To.Add(new MailAddress(ToEmailId));
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
        finally
        {
            // Destroy objects
            wtDatabase.CloseDatabase();
            wtDatabase = null;
            wtLogger = null;
            wtSettings = null;
            wtError = null;
            oWorkOrder = null;
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    // When the Assignee creates the agreement and agrees to send it to Stakeholders
    protected void btnAgree_Click(object sender, EventArgs e)
    {
        wtSettings = (Settings)Application["Settings"];
        wtLogger = (Logger)Application["Logger"];
        wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        wtError = new ErrorConstants();

        int iWorkOrder,iFixedPrice,iTimeAndMaterial,iServices,iAdHoc,iTotalHoursRequired;    

        DateTime dStart = DateTime.MinValue;
        DateTime dEnd   = DateTime.MinValue;
        string sSummary, sSpec;

        System.Globalization.CultureInfo MyCultureInfo = new System.Globalization.CultureInfo("en-US");
        MyCultureInfo.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";

        //********** Get form values ********************************************/
        iWorkOrder = Convert.ToInt32(Request.QueryString["wid"]);
        Int32.TryParse(txtFP.Text, out iFixedPrice);
        Int32.TryParse(txtTM.Text, out iTimeAndMaterial);
        Int32.TryParse(txtServ.Text, out iServices);
        Int32.TryParse(txtAdHoc.Text, out iAdHoc);
        iTotalHoursRequired = iFixedPrice + iTimeAndMaterial + iServices + iAdHoc;
        dStart              = DateTime.ParseExact(txtSDate.Text, "dd-MM-yyyy", MyCultureInfo);
        dEnd                = DateTime.ParseExact(txtEndDate.Text, "dd-MM-yyyy", MyCultureInfo);
        sSummary            = txtSum.Text;
        sSpec               = txtSpec.Text;

        string selectedusers = String.Empty;

        //get ARS TT selected Users
        for (int i = 0; i < listTTStakeHolders.Items.Count; i++)
        {
            if (listTTStakeHolders.Items[i].Selected == true)
            {
                selectedusers +=  listTTStakeHolders.Items[i].Value + ",";
            }
        }

        //get selected users from the listbox SE ----------------------
        for (int i = 0; i < listSEStakeHolders.Items.Count; i++)
        {
            if (listSEStakeHolders.Items[i].Selected == true)
            {
                selectedusers += listSEStakeHolders.Items[i].Value + ",";
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

        try
        {
            // Save to database ***********************************************************************
            wtDatabase.InsertAgreement(iWorkOrder, dStart, dEnd, iFixedPrice, iTimeAndMaterial, iServices, iAdHoc, sSummary, sSpec, iTotalHoursRequired);
            wtDatabase.InsertAgreementStatus(selectedusers, iWorkOrder);
            wtDatabase.UpdateAgreementStatus(oWorkOrder.LoggedInUserId, true, iWorkOrder);
            Response.Redirect("home.aspx", false);
         }
        catch (Exception ex)
        {
            wtLogger.LogMessage(LogPriorityLevel.FatalError, ">> Error inserting Agreement " + ex.Message.ToString());
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
            wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        // Destroy objects
        //wtDatabase.CloseDatabase();
        //wtDatabase = null;
        //wtLogger = null;
        //wtSettings = null;
        //wtError = null;
        //oWorkOrder = null;
    }

    protected void btnDisagree_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

        lbl_pr_SDate.Text = txtSDate.Text;
        lbl_pr_EDate.Text = txtEndDate.Text;
        
        lbl_pr_FP.Text = txtFP.Text;
        lbl_pr_TM.Text = txtTM.Text;
        lbl_pr_Services.Text = txtServ.Text;
        lbl_pr_Adhoc.Text = txtAdHoc.Text;

        string summary = txtSum.Text.Replace("\r\n", "<br>");
        string spec = txtSpec.Text.Replace("\r\n", "<br>");
        lbl_pr_sum.Text = summary;
        lbl_pr_spec.Text = spec;

        ArrayList arrTTusers = new ArrayList();
        ArrayList arrSEusers = new ArrayList();

        int ttODuser = 0, seODuser = 0;
        
        //get selected users from the listbox TT -------------------------
        for (int i = 0; i < listTTStakeHolders.Items.Count; i++)
			{
                if (listTTStakeHolders.Items[i].Selected == true)
                {
                    string ttusername = listTTStakeHolders.Items[i].Text;
                    string ttuservalue = listTTStakeHolders.Items[i].Value;
                    arrTTusers.Add(ttusername + "," + ttuservalue);
                    
                }
			}

            //get selected users from the listbox SE ----------------------
            for (int i = 0; i < listSEStakeHolders.Items.Count; i++)
            {
                if (listSEStakeHolders.Items[i].Selected == true)
                {
                    string seusername = listSEStakeHolders.Items[i].Text;
                    string seuservalue = listSEStakeHolders.Items[i].Value;
                    arrSEusers.Add(seusername + "," + seuservalue);
                    
                }
            }


            //Build TT user Table -------------------------------------------------------------------------------
            //****************************************************************************************************
            //string ttuserTable = "<div class='userSignature'>&nbsp;</div><div>Jan Linssen, Managing Director</div>";

            //if (SearchArrayList(arrTTusers, "Andre Lensink") > -1)
            //{
            //    ttuserTable += "<div class='userSignature'>&nbsp;</div><div>Andre Lensink, Operational Director</div>";
            //    ttODuser = 1;
            //}

            //if (SearchArrayList(arrTTusers, "Andre Lensink") > -1)
            //{
            //    ttuserTable += "<div class='userSignature'>&nbsp;</div><div>Andre Lensink, Operational Director</div>";
            //    ttODuser = 1;
            //}

            //for (int n = 0; n < arrTTusers.Count; n++ )
            //{
            //    string username = arrTTusers[n].ToString();
            //    string[] arrusername = username.Split(',');

            //    if (ttODuser == 1)
            //    {
            //        if ((arrusername[0] != "Jan Linssen") && (arrusername[0] != "Andre Lensink"))
            //            ttuserTable += "<div class='userSignature'>&nbsp;</div><div>" + arrusername[0] + ", " + GetUserDesignation(Convert.ToInt32(arrusername[1].ToString())) +"</div>";
            //    }
            //    else
            //    {
            //        if ((arrusername[0] != "Jan Linssen"))
            //            ttuserTable += "<div class='userSignature'>&nbsp;</div><div>" + arrusername[0] +", " + GetUserDesignation(Convert.ToInt32(arrusername[1].ToString()))+ "</div>";
            //    }
            //}

            string ttuserTable = "";
            for (int n = 0; n < arrTTusers.Count; n++)
            {
                string username = arrTTusers[n].ToString();
                string[] arrusername = username.Split(',');

                ttuserTable += "<div class='userSignature'>&nbsp;</div><div>" + arrusername[0] + ", " + GetUserDesignation(Convert.ToInt32(arrusername[1].ToString())) + "</div>";

            }
          
            layTTUsers.InnerHtml = ttuserTable;


            //Build SE user Table -------------------------------------------------------------------------------
            //****************************************************************************************************
            //string seuserTable = "<div class='userSignature'>&nbsp;</div><div>Martijn van der Spek, Managing Director</div>";

            //if (SearchArrayList(arrSEusers, "Sunil Kumar") > -1)
            //{
            //    seuserTable += "<div class='userSignature'>&nbsp;</div><div>Sunil Kumar, Operational Director</div>";
            //    seODuser = 1;
            //}

            //for (int n = 0; n < arrSEusers.Count; n++)
            //{
            //    string username = arrSEusers[n].ToString();
            //    string[] arrusername = username.Split(',');

            //    if ((arrusername[0] != "Martijn van der Spek") && (arrusername[0] != "Sunil Kumar"))
            //            seuserTable += "<div class='userSignature'>&nbsp;</div><div>" + arrusername[0] +", " + GetUserDesignation(Convert.ToInt32(arrusername[1].ToString()))+ "</div>";

            //}

            string seuserTable = "";
            for (int n = 0; n < arrSEusers.Count; n++)
            {
                string username = arrSEusers[n].ToString();
                string[] arrusername = username.Split(',');
                
                seuserTable += "<div class='userSignature'>&nbsp;</div><div>" + arrusername[0] + ", " + GetUserDesignation(Convert.ToInt32(arrusername[1].ToString())) + "</div>";

            }

            laySEUsers.InnerHtml = seuserTable; 
    }

    protected void ShowAgreement()
    {
        Pnl_UserButtons.Visible = false;

        DataTable dtAgreement;
        dtAgreement = wtDatabase.ShowAgreement(oWorkOrder.WorkOrderId);

        if (dtAgreement != null)
        {
            lbl_ProjName.Text = oWorkOrder.Project;
            lbl_ProjNo.Text = oWorkOrder.ProjectNo;
            lbl_SDate.Text = dtAgreement.Rows[0]["StartDate"].ToString();
            lbl_EDate.Text = dtAgreement.Rows[0]["EndDate"].ToString();
            lbl_FP.Text = dtAgreement.Rows[0]["FixedPrice"].ToString();
            lbl_TM.Text = dtAgreement.Rows[0]["TimeAndMaterial"].ToString();
            lbl_Ser.Text = dtAgreement.Rows[0]["Services"].ToString();
            lbl_Adhoc.Text = dtAgreement.Rows[0]["AdHoc"].ToString();

            string summary = dtAgreement.Rows[0]["Summary"].ToString().Replace("\r\n", "<br>");
            string spec = dtAgreement.Rows[0]["SpecificationDoc"].ToString().Replace("\r\n", "<br>");

            lbl_Sum.Text = summary;
            lbl_Spec.Text = spec;
          }

          DataTable dtStakeHolders;
          dtStakeHolders = wtDatabase.ShowAgreementStakeHolders(oWorkOrder.WorkOrderId);

          string ttuserTable = "";
          string seuserTable = "";
          int flagChkUser = 0;

          for (int i = 0; i < dtStakeHolders.Rows.Count; i++)
          {
              DataRow row = dtStakeHolders.Rows[i];
              string organisation = row["UserOffice"].ToString();
              string username = row["EmployeeName"].ToString();
              string designation = row["Designation"].ToString();
              string imageStatus = row["AgreeStatus"].ToString();
              string wtimage ="";
              string stakeholder = row["StakeHolder"].ToString();

              // set image path
              if (imageStatus == "False")
                  wtimage = "<img src='img/sign/signed_deny.gif'>";
              else if (imageStatus == "True")
                  wtimage = "<img src='img/sign/signed_preview.gif'>";
              else
                  wtimage = "";

              //Show-hide buttons
              if(flagChkUser == 0)
              {
                  if (stakeholder == oWorkOrder.LoggedInUserId.ToString())
                  {
                      flagChkUser = 1;// quit..no more checking

                      if (imageStatus == "")// if user haven't agreed or disagreed
                      {
                          Pnl_UserButtons.Visible = true;

                      }
                  }
              }

              //build user signatures
              switch (organisation)
              {
                  case "TT":
                      ttuserTable += "<div class='userSignature' style='height:50px'>"+wtimage+"</div><div>"+username+", "+  designation +"</div>" ;
                      break;
                  case "SE":
                      seuserTable += "<div class='userSignature' style='height:50px'>" + wtimage + "</div><div>" + username + ", " + designation + "</div>";
                      break;

              }
          }

          lay_TTUsers.InnerHtml = ttuserTable;
          lay_SEUsers.InnerHtml = seuserTable;
                    
    }

    /*********************************************************
     * Search an Array item with a desired item value
     *  @param ArrayList
     *  @param keyword to be searched
     * ********************************************************/
    protected int SearchArrayList(ArrayList arr, string keyword)
    {
        int flag = -1;

        for (int i = 0; i < arr.Count; i++)
        {
            string[] arrValue = arr[i].ToString().Split(',');

            if (arrValue[0].ToString() == keyword)
            {
                flag = 1;
                break;
            }
        }

        return flag;
    }

    /*********************************************************
     * Search an Array item with a desired item value
     *  @param ArrayList
     *  @param keyword to be searched
     * ********************************************************/
    protected string GetUserDesignation(int userID)
    {   wtDatabase = new WorkTrax.WorkTraxDB(ref wtLogger, ref wtSettings);
        string userdesig = String.Empty;
        if (!wtDatabase.OpenDatabase())
        {
            string responseError = "<br><br><div align='center'><font face='verdana' size='2px' color='red'><b>" +
                   wtError.ERR_DB_CONNECTION + "</b></font></div>";
            Response.Write(responseError);
            Response.End();
        }

        userdesig = wtDatabase.GetUserDesignation(userID);
        return userdesig;

    }
    protected void btnStakeholderAgree_Click(object sender, EventArgs e)
    {
        wtDatabase.UpdateAgreementStatus(oWorkOrder.LoggedInUserId, true, oWorkOrder.WorkOrderId);
        Response.Redirect("home.aspx", false);
    }
    protected void btnStakeholderDisagree_Click(object sender, EventArgs e)
    {
        wtDatabase.UpdateAgreementStatus(oWorkOrder.LoggedInUserId, false, oWorkOrder.WorkOrderId);
        Response.Redirect("home.aspx", false);
    }
}

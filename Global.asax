<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.IO.Compression" %>

<script runat="server">
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Create the settings object and read the settings values from Web.Config file
        WorkTrax.Settings AppSettings = new WorkTrax.Settings();
        // Create the logger object
        WorkTrax.Logger AppLogger = new WorkTrax.Logger();     
        
        if (!AppSettings.ReadSettings())
        {
            AppLogger.SetLogFilePath(AppSettings.LogFileLocation);
            AppLogger.StartLog(AppSettings.LogFileName, AppSettings.LogFileSize, AppSettings.LogLevel);
            AppLogger.LogMessage(WorkTrax.LogPriorityLevel.FatalError, "Invalid setting values in Web.config");
            AppLogger.LogMessage(WorkTrax.LogPriorityLevel.FatalError, "Closing...");
            return;
        }
        AppLogger.SetLogFilePath(AppSettings.LogFileLocation);
        AppLogger.StartLog(AppSettings.LogFileName, AppSettings.LogFileSize, AppSettings.LogLevel);
     
        // Add the settings and logger  objects to the application
        Application["Settings"] = AppSettings;
        Application["Logger"] = AppLogger;

        AppLogger.LogMessage(WorkTrax.LogPriorityLevel.Functional, "Application started.");
    }
    void Application_End(object sender, EventArgs e)
    {
        WorkTrax.Logger AppLogger = Application["Logger"] as WorkTrax.Logger;
    
        //  Code that runs on application shutdown 
        if (AppLogger != null)
        {
            AppLogger.Dispose();
        }
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session.Clear(); 
    }
    
    void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpApplication app = (HttpApplication)sender;
        WorkTrax.Logger AppLogger = HttpContext.Current.Application["Logger"] as WorkTrax.Logger;
        AppLogger.LogMessage(WorkTrax.LogPriorityLevel.Functional, "*************************************************************************************");
        string acceptEncoding = app.Request.Headers["Accept-Encoding"];
        Stream prevUncompressedStream = app.Response.Filter;

        if (acceptEncoding == null || acceptEncoding.Length == 0)
            return;

        acceptEncoding = acceptEncoding.ToLower();

        if (acceptEncoding.Contains("gzip"))
        {
            // gzip
            app.Response.Filter = new GZipStream(prevUncompressedStream,
                CompressionMode.Compress);
            app.Response.AppendHeader("Content-Encoding",
                "gzip");
            AppLogger.LogMessage(WorkTrax.LogPriorityLevel.Functional, "Content-Encoding set to gzip.");
        }
        else if (acceptEncoding.Contains("deflate"))
        {
            // defalte
            app.Response.Filter = new DeflateStream(prevUncompressedStream,
                CompressionMode.Compress);
            app.Response.AppendHeader("Content-Encoding",
                "deflate");
            AppLogger.LogMessage(WorkTrax.LogPriorityLevel.Functional, "Content-Encoding set to deflate.");
        }
    }    
</script>

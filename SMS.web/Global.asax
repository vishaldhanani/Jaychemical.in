<%@ Application Language="C#" %>
<%@ Import Namespace="SMS.web" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="Qtm.Lib" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        HttpConfiguration config = new HttpConfiguration();
        config.Formatters.Remove(config.Formatters.XmlFormatter);


        var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
        json.UseDataContractJsonSerializer = true;

        GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));                

    }

    void Session_Start(object sender, EventArgs e)
    {
        SessionManager.CartTable(HttpContext.Current);
    }

    void Session_End(object sender, EventArgs e)
    {
        //Response.Redirect("Login.aspx");
    }

</script>

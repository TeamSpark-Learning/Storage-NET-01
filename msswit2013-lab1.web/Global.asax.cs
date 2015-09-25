using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using msswit2013_lab1.core.Blob;
using msswit2013_lab1.core.Queue;
using msswit2013_lab1.core.Table.Service;

namespace msswit2013_lab1.web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

            new CategoryService().Initialize();
            new GoodService().Initialize();
            new BlobService().Initialize();
            new QueueService().Initialize();
        }
	}
}
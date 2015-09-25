using System.Web;
using System.Web.Mvc;

namespace msswit2013_lab1.web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
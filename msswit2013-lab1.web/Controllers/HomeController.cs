using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using msswit2013_lab1.core.Blob;
using msswit2013_lab1.core.Table.Entry;
using msswit2013_lab1.core.Table.Service;
using msswit2013_lab1.web.Models;

namespace msswit2013_lab1.web.Controllers
{
    public class HomeController : Controller
    {
		private readonly CategoryService categoryService;
		private readonly GoodService goodService;
	    private readonly BlobService blobService;

		public HomeController()
		{
			categoryService = new CategoryService();
			goodService = new GoodService();
			blobService = new BlobService();
		}

        public ActionResult Index()
        {
			return View();
        }

		public ActionResult Category(object id)
		{
			return View("Index", id);
		}

	    public ActionResult Good()
	    {
		    return View();
	    }

	    [ChildActionOnly]
	    public ActionResult CategoriesList()
	    {
		    var list = categoryService.GetAll();

			return View(list);
		}

		[ChildActionOnly]
	    public ActionResult GoodsList(string id)
	    {
			var list = string.IsNullOrEmpty(id) ?
				new List<GoodModel>() :
				goodService.GetByCategoryId(id, true)
					.Select(i => new GoodModel
					{
						Title = i.Title,
						Price = i.Price,
						Image = blobService.GetBlobUrl(i.RowKey)
					})
					.ToList();

			return View(list);
	    }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using msswit2013_lab1.core.Blob;
using msswit2013_lab1.core.Queue;
using msswit2013_lab1.core.Table.Entry;
using msswit2013_lab1.core.Table.Service;

namespace msswit2013_lab1.web.Controllers
{
    public class AdminController : Controller
    {
	    private readonly CategoryService categoryService;
		private readonly GoodService goodService;
	    private readonly BlobService blobService;
		private readonly QueueService queueService;

		public AdminController()
		{
			categoryService = new CategoryService();
			goodService = new GoodService();
			blobService = new BlobService();
			queueService = new QueueService();
		}

        public ActionResult Index()
        {
            return View();
        }

	    public ActionResult CategoriesList()
	    {
		    var list = categoryService.GetAll();

			return View(list);
	    }

		public ActionResult CategoryEntry(string id)
		{
			return View(
				string.IsNullOrEmpty(id) ?
					new Category() :
					categoryService.GetById(id));
	    }

		[HttpPost]
		public ActionResult CategoryEntry(Category entity)
		{
			categoryService.InsertOrReplace(entity);

			return RedirectToAction("CategoriesList");
		}

		public ActionResult CategoryDelete(string id)
		{
			categoryService.DeleteById("", id);

			return RedirectToAction("CategoriesList");
		}

	    public ActionResult GoodsList(string id)
	    {
			var list = string.IsNullOrEmpty(id) ?
				new List<Good>() :
				goodService.GetByCategoryId(id);

		    ViewData.Add("categoryId", id);

			return View(list);
	    }

		public ActionResult GoodEntry(string categoryId, string goodId)
	    {
			return View(
				string.IsNullOrEmpty(goodId) ?
					new Good { PartitionKey = categoryId } :
					goodService.GetById(goodId));
	    }

		[HttpPost]
		public ActionResult GoodEntry(Good entry, HttpPostedFileBase cover)
		{
			if (cover != null)
			{
				blobService.UploadByStream(entry.RowKey, cover.InputStream);
			}

			goodService.InsertOrReplace(entry);

			queueService.PushMessage(entry.RowKey);

			return RedirectToAction("GoodsList", new { id = entry.PartitionKey });
		}

		public ActionResult GoodDelete(string id)
		{
			goodService.DeleteById(id);

			return RedirectToAction("CategoriesList");
		}
    }
}

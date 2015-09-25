using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using msswit2013_lab1.core.Table.Entry;

namespace msswit2013_lab1.core.Table.Service
{
	public class CategoryService : TableServiceBase
	{
		protected override string TableName
		{
			get { return "Categories"; }
		}

		public void Initialize()
		{
			InitializeBase(new Category());
		}

		public List<Category> GetAll()
		{
			var query = new TableQuery<Category>();
			return Table.ExecuteQuery(query).ToList()
				.OrderBy(i => i.Title).ToList();
		}

		public Category GetById(string id)
		{
			return (Category)GetByIdBase<Category>("", id);
		}
	}
}

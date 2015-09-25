using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using msswit2013_lab1.core.Table.Entry;

namespace msswit2013_lab1.core.Table.Service
{
	public class GoodService : TableServiceBase
	{
		protected override string TableName
		{
			get { return "Goods"; }
		}

		public void Initialize()
		{
			InitializeBase(new Good());
		}

		public List<Good> GetAll()
		{
			var query = new TableQuery<Good>();
			return Table.ExecuteQuery(query).ToList()
				.OrderBy(i => i.Title).ToList();
		}

		public List<Good> GetByCategoryId(string categoryId,
			bool? isApprover = null)
		{
			string filter;
			if (isApprover.HasValue)
			{
				filter = TableQuery.CombineFilters(
					TableQuery.GenerateFilterCondition(
						"PartitionKey",
						QueryComparisons.Equal,
						categoryId),
					TableOperators.And,
					TableQuery.GenerateFilterConditionForBool(
						"IsApproved",
						QueryComparisons.Equal,
						isApprover.Value)
					);
			}
			else
			{
				filter = TableQuery.GenerateFilterCondition(
					"PartitionKey",
					QueryComparisons.Equal,
					categoryId);
			}

			var query = new TableQuery<Good>()
				.Where(filter);
			return Table.ExecuteQuery(query).ToList()
				.OrderBy(i => i.Title).ToList();
		}

		public Good GetById(string categoryId, string goodId)
		{
			return (Good)GetByIdBase<Good>(categoryId, goodId);
		}

		public Good GetById(string goodId)
		{
			var query = new TableQuery<Good>()
				.Where(
					TableQuery.GenerateFilterCondition(
						"RowKey",
						QueryComparisons.Equal,
						goodId));
			return Table.ExecuteQuery(query).FirstOrDefault();
		}

		public void DeleteById(string goodId)
		{
			var good = GetById(goodId);
			Table.Execute(TableOperation.Delete(good));
		}
	}
}

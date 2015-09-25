using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace msswit2013_lab1.core.Table.Entry
{
	public class Good : TableEntity
	{
		public Good() : this("", Guid.NewGuid().ToString())
		{
		}

		public Good(string categoryId, string goodId)
		{
			PartitionKey = categoryId;
			RowKey = goodId;
			IsApproved = false;
		}

		public string Title { get; set; }
		public int Price { get; set; }
		public bool IsApproved { get; set; }
	}
}

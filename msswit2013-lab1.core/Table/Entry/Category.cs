using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace msswit2013_lab1.core.Table.Entry
{
	public class Category : TableEntity
	{
		public Category() : this(Guid.NewGuid().ToString())
		{
		}

		public Category(string id)
		{
			PartitionKey = "";
			RowKey = id;
		}

		public string Title { get; set; }
	}
}

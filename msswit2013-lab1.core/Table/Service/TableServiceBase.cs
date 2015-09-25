using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using msswit2013_lab1.core.Table.Entry;

namespace msswit2013_lab1.core.Table.Service
{
	public abstract class TableServiceBase : StorageServiceBase
	{
		protected abstract string TableName { get; }
		
		private CloudTableClient _tableClient;
		protected CloudTableClient TableClient
		{ get { return _tableClient ??
			(_tableClient = StorageAccount.CreateCloudTableClient()); } }

		private CloudTable _table;
		protected CloudTable Table
		{ get { return _table ?? (_table =
			TableClient.GetTableReference(TableName)); } }

		protected void InitializeBase(TableEntity entity)
		{
			Table.CreateIfNotExists();
			Table.Execute(TableOperation.Insert(entity));
			Table.Execute(TableOperation.Delete(entity));
		}

		protected TableEntity GetByIdBase<T>(string partitionKey, string rowKey)
			where T : TableEntity
		{
			return (TableEntity)Table.Execute(
				TableOperation.Retrieve<T>(partitionKey, rowKey)).Result;
		}

		public void DeleteById(string partitionKey, string rowKey)
		{
			var entity = GetByIdBase<TableEntity>(partitionKey, rowKey);
			if (entity != null)
			{
				Table.Execute(TableOperation.Delete(entity));
			}
		}

		public void InsertOrReplace(TableEntity entity)
		{
			Table.Execute(TableOperation.InsertOrReplace(entity));
		}
	}
}

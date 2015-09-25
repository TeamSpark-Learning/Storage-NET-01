using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;

namespace msswit2013_lab1.core.Queue
{
	public class QueueService : StorageServiceBase
	{
		public const string QUEUE = "goods";

		private CloudQueueClient _queueClient;
		protected CloudQueueClient QueueClient
		{ get { return _queueClient ?? (_queueClient = StorageAccount.CreateCloudQueueClient()); } }

		private CloudQueue _queue;
		protected CloudQueue Queue
		{ get { return _queue ?? (_queue = QueueClient.GetQueueReference(QUEUE)); } }

		public void Initialize()
		{
			Queue.CreateIfNotExists();
		}

		public void PushMessage(string text)
		{
			var message = new CloudQueueMessage(text);
			Queue.AddMessage(message);
		}

		public CloudQueueMessage GetMessage()
		{
			return Queue.GetMessage(new TimeSpan(0, 1, 0));
		}

		public void DeleteMessage(string messageId, string popReceipt)
		{
			Queue.DeleteMessage(messageId, popReceipt);
		}
	}
}

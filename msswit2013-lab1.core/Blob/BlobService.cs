using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace msswit2013_lab1.core.Blob
{
	public class BlobService : StorageServiceBase
	{
		public const string CONTAINER = "images";

		private CloudBlobClient _blobClient;
		protected CloudBlobClient BlobClient
		{ get { return _blobClient ?? (_blobClient = StorageAccount.CreateCloudBlobClient()); } }

		private CloudBlobContainer _blobContainer;
		protected CloudBlobContainer BlobContainer
		{ get { return _blobContainer ?? (_blobContainer = BlobClient.GetContainerReference(CONTAINER)); } }

		public void Initialize()
		{
			BlobContainer.CreateIfNotExists();

			BlobContainer.SetPermissions(new BlobContainerPermissions
			{
				PublicAccess = BlobContainerPublicAccessType.Blob
			});
		}

		public void UploadByStream(string name, Stream content)
		{
			var blob = BlobContainer.GetBlockBlobReference(name);
			blob.UploadFromStream(content);
		}

		public string GetBlobUrl(string name)
		{
			var blob = BlobContainer.GetBlockBlobReference(name);
			return blob.Uri.AbsoluteUri;
		}
	}
}

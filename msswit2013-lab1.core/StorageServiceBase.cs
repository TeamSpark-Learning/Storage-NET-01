using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace msswit2013_lab1.core
{
	public abstract class StorageServiceBase
	{
		private CloudStorageAccount _storageAccount;

	    protected StorageServiceBase()
	    {
            var credentials = new StorageCredentials("account name", "account key");

            _storageAccount = new CloudStorageAccount(credentials, true);
	    }

	    protected CloudStorageAccount StorageAccount
		{
			get { return _storageAccount; }
		}
	}
}

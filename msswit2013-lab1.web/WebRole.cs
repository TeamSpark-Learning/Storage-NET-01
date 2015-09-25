using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using msswit2013_lab1.core.Blob;
using msswit2013_lab1.core.Queue;
using msswit2013_lab1.core.Table.Service;

namespace msswit2013_lab1.web
{
	public class WebRole : RoleEntryPoint
	{
		public override bool OnStart()
		{
			new CategoryService().Initialize();
			new GoodService().Initialize();
			new BlobService().Initialize();
			new QueueService().Initialize();

			return base.OnStart();
		}
	}
}

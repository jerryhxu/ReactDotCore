using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CoxAuto.InventoryManagementService.Tools.ICAGDataMigrator
{
	public class MyServiceFactory : IMyServiceFactory
	{
		private readonly ILogger<MyService> _logger;

		public MyServiceFactory(ILogger<MyService> logger)
		{
			_logger = logger;
		}

		public IMyService GetMyService(string tableName)
		{
			return new MyService(_logger, tableName);
		}
	}
}

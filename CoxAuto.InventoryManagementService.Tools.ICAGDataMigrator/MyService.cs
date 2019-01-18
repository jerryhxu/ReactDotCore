using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CoxAuto.InventoryManagementService.Tools.ICAGDataMigrator
{
	public class MyService : IMyService
	{
		private readonly ILogger<MyService> _logger;
		private string _tableName;

		public MyService(ILogger<MyService> logger, string tableName)
		{
			_logger = logger;
			_tableName = tableName;

		}
		public void DoThing(int number)
		{
			_logger.LogInformation(string.Format("My Service {0}", _tableName));
		}
	}
}

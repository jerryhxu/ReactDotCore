using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;

namespace CoxAuto.InventoryManagementService.Tools.ICAGDataMigrator
{
	public class App
	{
		private readonly IMyServiceFactory _myServiceFactory;
		private readonly IDynamoDBRepoFactory _dynamoDBFactory;
		private readonly ILogger<App> _logger;

		public App(IMyServiceFactory myServiceFactory, IDynamoDBRepoFactory dynamoDBFactory, ILogger<App> logger)
		{
			_myServiceFactory = myServiceFactory;
			_dynamoDBFactory = dynamoDBFactory;
			_logger = logger;
		}

		public async Task RunAsync()
		{
			_logger.LogInformation($"This is a console application ");
			var myService = _myServiceFactory.GetMyService("jerry");
			myService.DoThing(2);
			var dynamodbFactory = _dynamoDBFactory.GetDynamoDbUniqueConstraintRepository("Uniqueness");
			System.Console.ReadKey();
			var results = await dynamodbFactory.RetrieveAsync(hash: "abc").ConfigureAwait(false);
		}

	}
}

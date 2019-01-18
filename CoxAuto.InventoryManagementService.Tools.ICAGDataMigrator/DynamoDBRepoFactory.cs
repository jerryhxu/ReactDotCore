using System;
using System.Collections.Generic;
using System.Text;
using CoxAuto.Vince.InventoryManagementService.Data.DynamoDB;
using CoxAuto.Vince.InventoryManagementService.Repositories.DynamoDB;
using Microsoft.Extensions.Logging;

namespace CoxAuto.InventoryManagementService.Tools.ICAGDataMigrator
{
	public class DynamoDBRepoFactory : IDynamoDBRepoFactory
	{
		private readonly ITableWrapper _tableWrapper;
		private readonly ILogger<DynamoDbUniqueConstraintRepository> _logger;
		private readonly IDynamoDBClientProvider _dynamoDBClientProvider;

		public DynamoDBRepoFactory(
			ITableWrapper tableWrapper,
			ILogger<DynamoDbUniqueConstraintRepository> logger,
			IDynamoDBClientProvider dynamoDBClientProvider
			)
		{
			_tableWrapper = tableWrapper;
			_logger = logger;
			_dynamoDBClientProvider = dynamoDBClientProvider;

		}

		public DynamoDbUniqueConstraintRepository GetDynamoDbUniqueConstraintRepository(string table)
		{
			return new DynamoDbUniqueConstraintRepository(
				_dynamoDBClientProvider,
				_tableWrapper,
				_logger,
				table);
		}
	}
}

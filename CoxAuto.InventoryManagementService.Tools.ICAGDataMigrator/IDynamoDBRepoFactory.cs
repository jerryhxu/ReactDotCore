using System;
using System.Collections.Generic;
using System.Text;
using CoxAuto.Vince.InventoryManagementService.Repositories.DynamoDB;

namespace CoxAuto.InventoryManagementService.Tools.ICAGDataMigrator
{
	public interface IDynamoDBRepoFactory
	{
		DynamoDbUniqueConstraintRepository GetDynamoDbUniqueConstraintRepository(string table);
	}
}

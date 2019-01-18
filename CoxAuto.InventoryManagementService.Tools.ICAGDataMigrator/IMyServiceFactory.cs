using System;
using System.Collections.Generic;
using System.Text;

namespace CoxAuto.InventoryManagementService.Tools.ICAGDataMigrator
{
	public interface IMyServiceFactory
	{
		IMyService GetMyService(string tableName);
	}
}

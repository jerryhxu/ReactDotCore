using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using CoxAuto.Vince.InventoryManagementService.Data.DynamoDB;
using CoxAuto.Vince.InventoryManagementService.Repositories.DynamoDB;
using Microsoft.Extensions.Logging;

namespace CoxAuto.InventoryManagementService.Tools.ICAGDataMigrator
{
	public class App
	{
		private readonly IMyServiceFactory _myServiceFactory;
		private readonly ILogger<App> _logger;
		private readonly IDynamoDbUniqueConstraintRepository _repo;

		public App(
			IMyServiceFactory myServiceFactory,
			ILogger<App> logger,
			IDynamoDbUniqueConstraintRepository repo)
		{
			_myServiceFactory = myServiceFactory;
			_logger = logger;
			_repo = repo;
		}

		public async Task RunAsync()
		{
			_logger.LogInformation($"This is a console application ");
			string hash = "160B2087-9AD2-45B9-8471-33F460823280_STOCK_0PHZEY299HRYLKCMQ";
			List<string> hashes = new List<string> {
				hash
			};
			var hashComponents = hash.Split('_');
			string newHash = string.Format("{0}_{1}_{2}", Guid.NewGuid(), hashComponents[1], hashComponents[2]);

			// var result = await _repo.RetrieveAsync(hashes).ConfigureAwait(false);
			var result = await _repo.RetrieveAsync(hash: hash).ConfigureAwait(false);



			var document = new UniqueConstraintDocument {
				Hash = new Guid().ToString(),
				CreationTime = DateTime.Now,
				InventoryId = result.Value?.InventoryId
			};
			var documents = new List<UniqueConstraintDocument>();
			documents.Add(document);
			hash = Guid.NewGuid().ToString();
			var createResults = await _repo.CreateAsync(documents).ConfigureAwait(false);
			//var deleteResults = await _repo.DeleteAsync(hash);
			return;
		}

	}
}

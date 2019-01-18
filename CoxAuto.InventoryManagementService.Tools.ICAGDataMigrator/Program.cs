using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using CoxAuto.Vince.InventoryManagementService.Data.DynamoDB;
using CoxAuto.Vince.InventoryManagementService.Repositories.DynamoDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CoxAuto.InventoryManagementService.Tools.ICAGDataMigrator

{
	class Program
	{
		public static async Task MainAsync(string[] args)
		{
			IDynamoDBClientProvider clientProvider = new DynamoDBClientProvider("us-east-1", "ddb-ue1-ims-develop-");
			IDynamoRetryPolicy dynamoRetryPolicy = new DynamoRetryPolicy(null);
			ITableWrapper tableWrapper = new TableWrapper(dynamoRetryPolicy);
			ILogger<DynamoDbUniqueConstraintRepository> logger;
			DynamoDbUniqueConstraintRepository dynamoDbUniqueness = new DynamoDbUniqueConstraintRepository
				(
					clientProvider,
					tableWrapper,
					null,
					"uniqueness"
				);
			var result = await dynamoDbUniqueness.RetrieveAsync("160B2087-9AD2-45B9-8471-33F460823280_0L5FZ52115QVN0Z4N").ConfigureAwait(false);
			//var serviceCollection = new ServiceCollection()
			//	.AddSingleton<IMyService>(s => new MyService(4));
			//	.AddSingleton<IDynamoDbUniqueConstraintRepository>(x => new DynamoDbUniqueConstraintRepository());

			//var serviceProvider = serviceCollection.BuildServiceProvider();
			//var myService = serviceProvider.GetService<IMyService>();
			//myService.DoThing(10);

			AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
			// This client will access the US East 1 region.
			clientConfig.RegionEndpoint = RegionEndpoint.USEast1;
			AmazonDynamoDBClient client = new AmazonDynamoDBClient(clientConfig);
			Table table = Table.LoadTable(client, "ddb-ue1-ims-develop-uniqueness");

			string tableName = "ddb-ue1-ims-develop-uniqueness";

			var request = new GetItemRequest {
				TableName = tableName,
				Key = new Dictionary<string, AttributeValue>() { { "Hash", new AttributeValue { S = "160B2087-9AD2-45B9-8471-33F460823280_0L5FZ52115QVN0Z4N" } } },
			};
			// ar response = client.GetItemAsync(request).Result;
			var response = await client.GetItemAsync(request).ConfigureAwait(false);
			AmazonSQSConfig amazonSQSConfig = new AmazonSQSConfig();
			amazonSQSConfig.ServiceURL = "http://sqs.us-east-1.amazonaws.com";
			using (var sqs = new AmazonSQSClient(amazonSQSConfig))
			{
				ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();
				var myQueueURL = @"https://sqs.us-east-1.amazonaws.com/002067833750/Jerry-Test";
				receiveMessageRequest.QueueUrl = myQueueURL;
				ReceiveMessageResponse receiveMessageResponse = await 
					sqs.ReceiveMessageAsync(receiveMessageRequest).ConfigureAwait((false));
				
			}

			// Get message from SQS queue to get the following: Entity ID, X_Correlation_ID, ICAG_ID
			// Call IQS to get inventory
			// Access DynamoDB to migrate hash
			// AddSplunkLogging(loggerConfiguration, inventoryQueryLoggingConfiguration);
		}

		public static void Main(string[] args)
		{
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);

			// create service provider
			var serviceProvider = serviceCollection.BuildServiceProvider();

			// entry to run app
			serviceProvider.GetService<App>().RunAsync();
		}

		private static void ConfigureServices(IServiceCollection serviceCollection)
		{
			//serviceCollection.AddSingleton(new LoggerFactory()
			//	.AddConsole());
			serviceCollection.AddLogging(configure => configure.AddConsole());
			serviceCollection.AddTransient<IMyServiceFactory, MyServiceFactory>();
			serviceCollection.AddTransient<IDynamoRetryPolicy, DynamoRetryPolicy>();
			serviceCollection.AddSingleton<IDynamoDBClientProvider>(s =>
				new DynamoDBClientProvider("us-east-1", "ddb-ue1-ims-develop-"));
			serviceCollection.AddTransient<ITableWrapper, TableWrapper>();
			serviceCollection.AddSingleton<IDynamoDBRepoFactory, DynamoDBRepoFactory>();

			// add app
			serviceCollection.AddTransient<App>();
		}

		private static void CallDynamoDB()
		{
			//IDynamoDBClientProvider clientProvider = new DynamoDBClientProvider("us-east-1", "ddb-ue1-ims-develop-");
			//IDynamoRetryPolicy dynamoRetryPolicy = new DynamoRetryPolicy(null);
			//ITableWrapper tableWrapper = new TableWrapper(dynamoRetryPolicy);
			//ILogger<DynamoDbUniqueConstraintRepository> logger;

			//ILoggerFactory loggerFactory = new LoggerFactory()
			//ILogger logger = loggerFactory.CreateLogger<Program>();
			//DynamoDbUniqueConstraintRepository dynamoDbUniqueness = new DynamoDbUniqueConstraintRepository
			//(
			//	clientProvider,
			//	tableWrapper,
			//	null,
			//	"uniqueness"
			//);
			//var result = dynamoDbUniqueness.RetrieveAsync("160B2087-9AD2-45B9-8471-33F460823280_0L5FZ52115QVN0Z4N").Result;


			//var serviceCollection = new ServiceCollection()
			//	.AddSingleton<IMyService, MyService>();

			//var serviceProvider = serviceCollection.BuildServiceProvider();
			//var myService = serviceProvider.GetService<IMyService>();
			//myService.DoThing(10);

			//AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
			//// This client will access the US East 1 region.
			//clientConfig.RegionEndpoint = RegionEndpoint.USEast1;
			//AmazonDynamoDBClient client = new AmazonDynamoDBClient(clientConfig);
			//Table table = Table.LoadTable(client, "ddb-ue1-ims-develop-uniqueness");

			//string tableName = "ddb-ue1-ims-develop-uniqueness";

			//var request = new GetItemRequest {
			//	TableName = tableName,
			//	Key = new Dictionary<string, AttributeValue>() { { "Hash", new AttributeValue { S = "160B2087-9AD2-45B9-8471-33F460823280_0L5FZ52115QVN0Z4N" } } },
			//};
			//var response = client.GetItemAsync(request).Result;
			//MainAsync(args).Wait();
		}
	}
}

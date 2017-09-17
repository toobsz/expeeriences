using System;
using System.Collections.Generic;
using System.Diagnostics;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace DynamoDB
{
    public class DynamoConnection
    {
        private IAmazonDynamoDB _dynamoDb;
        private const string TableName = "TestTable";
        private const string AccessKey = "AKIAIIUVKT6TXYWRUEXA";
        private const string SecretKey = "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";

        public DynamoConnection(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;

            _dynamoDb.CreateTableAsync(new CreateTableRequest(TableName, new List<KeySchemaElement>
            {
                new KeySchemaElement { AttributeName = "Row1" },
                new KeySchemaElement { AttributeName = "Row2" }
            }));
        }

        public void StartDynamo()
        {
            var dynamoCredentials = new BasicAWSCredentials(AccessKey, SecretKey);
            var dynamoConfig = new AmazonDynamoDBConfig { RegionEndpoint = RegionEndpoint.USEast1};

            AmazonDynamoDBClient dynamoClient;
            try
            {
                dynamoClient = new AmazonDynamoDBClient(dynamoCredentials, dynamoConfig);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
                PauseForDebugWindow();
            }

        }

        public static void PauseForDebugWindow()
        {
            // Keep the console open if in Debug mode...
            Console.Write("\n\n ...Press any key to continue");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}

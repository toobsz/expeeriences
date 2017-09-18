using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB
{
    public class DynamoConnection
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private const string TableName = "TestTable";

        public DynamoConnection(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }

        public void CreateTable()
        {
            try
            {
                _dynamoDb.CreateTableAsync(new CreateTableRequest(TableName, new List<KeySchemaElement>
                {
                    new KeySchemaElement {AttributeName = "Row1"},
                    new KeySchemaElement {AttributeName = "Row2"}
                }));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: ", e);
            }
        }

        public List<string> GetTableNames()
        {
            try
            {
                var test = _dynamoDb.ListTablesAsync();

                return test.Result.TableNames;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: ", e);
                return new List<string>();
            }
        }

        public void AddItem(Dictionary<string, AttributeValue> items)
        {
            try
            {
                //var testMany = _dynamoDb.BatchWriteItemAsync("test", )
                var test = _dynamoDb.PutItemAsync("test", items);

                var test1 = test.Result.ItemCollectionMetrics;
                var test2 = test.Result.Attributes;
                var test3 = test.Result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: ", e);
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

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
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
                return _dynamoDb.ListTablesAsync().Result.TableNames;
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
                _dynamoDb.PutItemAsync(TableName, items);
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

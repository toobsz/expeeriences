using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using Amazon;
using Amazon.DynamoDBv2;

namespace DynamoDB
{
    public class DynamoConnection
    {
        public void StartDynamo()
        {
            var dynamoConfig = new AmazonDynamoDBConfig();
            dynamoConfig.RegionEndpoint = RegionEndpoint.USEast1;
            dynamoConfig.UseHttp = true;

            AmazonDynamoDBClient dynamoClient;

            try
            {
                dynamoClient = new AmazonDynamoDBClient();
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

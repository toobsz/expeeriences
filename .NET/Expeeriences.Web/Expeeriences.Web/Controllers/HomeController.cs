using System;
using System.Collections.Generic;
using System.Diagnostics;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal;
using DynamoDB;
using Microsoft.AspNetCore.Mvc;

namespace Expeeriences_Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IAmazonDynamoDB dynamoDb)
        {
            var dynamoConnection = new DynamoConnection(dynamoDb);
            //dynamoConnection.CreateTable();
            //var test = dynamoConnection.GetTableNames();

            Dictionary<string, AttributeValue> items = new Dictionary<string, AttributeValue>();
            //items["row1"] = new AttributeValue(ss: new List<string> { "item4", "item2", "item3" });
            items["row1"] = new AttributeValue { S = "item4" };
            dynamoConnection.AddItem(items);
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}

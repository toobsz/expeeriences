using System.Collections.Generic;
using System.Diagnostics;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
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
            //dynamoConnection.GetTableNames();

            //Dictionary<string, AttributeValue> items = new Dictionary<string, AttributeValue>();
            //items.Add("Row1", new AttributeValue(new List<string>{ "item1", "item2", "item3" }));
            //dynamoConnection.AddItem(items);
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

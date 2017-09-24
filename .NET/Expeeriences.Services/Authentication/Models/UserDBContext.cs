using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Models
{
    public sealed class UserDbContext : DynamoDBContext
    {
        public UserDbContext(DynamoDBContextConfig options) : base(new AmazonDynamoDBClient())
        {
           DbLoggerCategory.Database.EnsureCreated();
        }
    }
}

using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Services.RowService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNU.IT.DBMSTests.ServiceTests
{
    [TestClass]
    public class RowServiceTests
    {
        private readonly string tableName = "testTableName";
        private readonly string StringType = nameof(String).ToLower();

        private readonly string row1Name = "Balenciaga";
        private readonly string row1Country = "Spain";
        private readonly string row2Name = "Supreme";
        private readonly string row2Country = "USA";

        private readonly string searchKeyword = "sup";
        private readonly string searchColumn = "name";

        private class BrandTableSchema
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("country")]
            public string Country { get; set; }
        }

        [TestMethod]
        public async Task SearchAsync()
        {
            // Arrange
            var table = new Table
            {
                Id = Guid.NewGuid(),
                DatabaseId = Guid.NewGuid(),
                Name = tableName,
                Schema = JsonConvert.SerializeObject(new BrandTableSchema { Name = StringType, Country = StringType })
            };

            var row1 = new Row
            {
                Id = Guid.NewGuid(),
                TableId = table.Id,
                Content = JsonConvert.SerializeObject(new BrandTableSchema { Name = row1Name, Country = row1Country })
            };

            var row2 = new Row
            {
                Id = Guid.NewGuid(),
                TableId = table.Id,
                Content = JsonConvert.SerializeObject(new BrandTableSchema { Name = row2Name, Country = row2Country })
            };

            using var context = new AzureSqlDbContext(DbContextUtilities.GetContextOptions());

            await context.Rows.AddAsync(row1);
            await context.Rows.AddAsync(row2);
            await context.Tables.AddAsync(table);
            await context.SaveChangesAsync();

            var rowService = new RowService(context);

            // Act
            var result = await rowService.SearchByKeywordAsync(table.Id, searchKeyword, searchColumn);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(row2Name, result.FirstOrDefault().Content["name"]);
        }
    }
}

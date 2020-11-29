using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Converters;
using KNU.IT.DbServices.Services.RowService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
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

            var rowService = new SqlRowService(context);

            // Act
            var result = await rowService.SearchByKeywordAsync(table.Id, searchKeyword, searchColumn);
            var resultResponse = result.Select(r => RowConverter.GetRowResponse(r)).ToList();

            // Assert
            Assert.IsNotNull(resultResponse);
            Assert.AreEqual(1, resultResponse.Count);
            Assert.AreEqual(row2Name, resultResponse.FirstOrDefault().Content["name"]);
        }
    }
}

using KNU.IT.DbManagementSystem.Pages;
using KNU.IT.DbManagementSystem.Services.DatabaseService;
using KNU.IT.DbManagementSystem.Services.TableService;
using KNU.IT.DbManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace KNU.IT.DbManagementSystemTests.PageModelTests
{
    [TestClass]
    public class ExploreTablesPageModelTests
    {
        private readonly string databaseName = "testDatabaseName";
        private readonly Guid databaseId = Guid.NewGuid();
        private readonly string tableName = "testTableName";
        private readonly string tableSchema = "testTableSchema";

        [TestMethod]
        public async Task OnGetAsync()
        {
            // Arrange
            var database = new Database
            {
                Id = databaseId,
                Name = databaseName
            };
            var table = new Table
            {
                Id = Guid.NewGuid(),
                Name = tableName,
                Schema = tableSchema,
                DatabaseId = databaseId
            };
            var tableList = new List<Table> { table };

            var mockTableService = new Mock<ITableService>();
            mockTableService.Setup(m => m.GetAllByDatabaseAsync(databaseId)).ReturnsAsync(tableList);

            var mockDatabaseService = new Mock<IDatabaseService>();
            mockDatabaseService.Setup(m => m.GetAsync(It.IsAny<Guid>())).ReturnsAsync(database);

            var pageModel = new ExploreTablesModel(mockTableService.Object, mockDatabaseService.Object);

            // Act
            var response = await pageModel.OnGetAsync(databaseId);


            // Assert
            Assert.IsNotNull(response);
            mockDatabaseService.Verify(m => m.GetAsync(It.IsAny<Guid>()), Times.Once());
            mockTableService.Verify(m => m.GetAllByDatabaseAsync(It.IsAny<Guid>()), Times.Once());
            Assert.IsNotNull(pageModel.Database);
            Assert.IsNotNull(pageModel.Tables);
            Assert.AreEqual(database, pageModel.Database);
            Assert.AreEqual(tableList, pageModel.Tables);
        }
    }
}

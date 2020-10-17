using KNU.IT.DbManagementSystem.Pages;
using KNU.IT.DbManagementSystem.Services.DatabaseService;
using KNU.IT.DbManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbManagementSystemTests.PageModelTests
{
    [TestClass]
    public class ExploreDatabasesPageModelTests
    {
        private readonly string databaseName = "testDatabaseName";

        [TestMethod]
        public async Task OnGetAsync()
        {
            // Arrange
            var database = new Database
            {
                Id = Guid.NewGuid(),
                Name = databaseName
            };
            var databaseList = new List<Database> { database };

            var mockDatabaseService = new Mock<IDatabaseService>();
            mockDatabaseService.Setup(m => m.GetAllAsync()).ReturnsAsync(databaseList);
            var pageModel = new ExploreDatabasesModel(mockDatabaseService.Object);

            // Act
            var response = await pageModel.OnGetAsync();

            // Assert
            Assert.IsNotNull(response);
            mockDatabaseService.Verify(m => m.GetAllAsync(), Times.Once());
            Assert.IsNotNull(pageModel.Databases);
            Assert.AreEqual(databaseList, pageModel.Databases);
        }
    }
}

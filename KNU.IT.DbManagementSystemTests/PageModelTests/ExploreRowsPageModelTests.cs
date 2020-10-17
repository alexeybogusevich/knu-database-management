using KNU.IT.DbManagementSystem.Models;
using KNU.IT.DbManagementSystem.Pages;
using KNU.IT.DbManagementSystem.Services.DatabaseService;
using KNU.IT.DbManagementSystem.Services.RowService;
using KNU.IT.DbManagementSystem.Services.TableService;
using KNU.IT.DbManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNU.IT.DbManagementSystemTests.PageModelTests
{
    [TestClass]
    public class ExploreRowsPageModelTests
    {
        private readonly Guid databaseId = Guid.NewGuid();
        private readonly Guid tableId = Guid.NewGuid();
        private readonly string tableName = "testTableName";
        private readonly string tableSchema = "testTableSchema";

        [TestMethod]
        public async Task OnGetAsync()
        {
            // Arrange
            var table = new Table
            {
                Id = Guid.NewGuid(),
                Name = tableName,
                Schema = tableSchema,
                DatabaseId = databaseId
            };
            var row = new RowViewModel
            {
                Id = Guid.NewGuid(),
                TableId = table.Id
            };
            var rowList = new List<RowViewModel> { row };

            var mockRowService = new Mock<IRowService>();
            mockRowService.Setup(m => m.GetAllViewModelsByTableAsync(databaseId)).ReturnsAsync(rowList);

            var mockTableService = new Mock<ITableService>();
            mockTableService.Setup(m => m.GetAsync(tableId)).ReturnsAsync(table);

            var pageModel = new ExploreRowsModel(mockTableService.Object, mockRowService.Object);

            // Act
            var response = await pageModel.OnGetAsync(databaseId);


            // Assert
            Assert.IsNotNull(response);
            mockTableService.Verify(m => m.GetAsync(It.IsAny<Guid>()), Times.Once());
            mockRowService.Verify(m => m.GetAllViewModelsByTableAsync(It.IsAny<Guid>()), Times.Once());
            Assert.IsNotNull(pageModel.Table);
            Assert.IsNotNull(pageModel.Rows);
            Assert.AreEqual(table, pageModel.Table);
            Assert.AreEqual(rowList, pageModel.Rows);
        }
    }
}

using KNU.IT.DbManagementSystem.Services.DatabaseService;
using KNU.IT.DbManagementSystemTests;
using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.ServiceTests.DbManagementSystemTests
{
    [TestClass]
    public class DatabaseServiceTests
    {
        private readonly string databaseName = "testDatabaseName";

        [TestMethod]
        public async Task GetAsync()
        {
            // Arrange
            var database = new Database
            {
                Id = Guid.NewGuid(),
                Name = databaseName
            };
            using var context = new AzureSqlDbContext(DbContextUtilities.GetContextOptions());
            await context.Databases.AddAsync(database);
            await context.SaveChangesAsync();

            var databaseService = new DatabaseService(context);

            // Act
            var result = await databaseService.GetAsync(database.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(database, result);
        }

        [TestMethod]
        public async Task CreateAsync()
        {
            // Arrange
            var database = new Database
            {
                Id = Guid.NewGuid(),
                Name = databaseName
            };
            using var context = new AzureSqlDbContext(DbContextUtilities.GetContextOptions());

            var databaseService = new DatabaseService(context);

            // Act
            await databaseService.CreateAsync(database);

            // Assert
            Assert.AreNotEqual(context.Databases.Count(), 0);
            Assert.AreEqual(context.Databases.FirstOrDefault(), database);
        }

        [TestMethod]
        public async Task UpdateAsync()
        {
            // Arrange
            var database = new Database
            {
                Id = Guid.NewGuid(),
                Name = databaseName
            };
            using var context = new AzureSqlDbContext(DbContextUtilities.GetContextOptions());
            await context.Databases.AddAsync(database);
            await context.SaveChangesAsync();

            var updatedName = "updatedTestDatabaseName";
            database.Name = updatedName;

            var databaseService = new DatabaseService(context);

            // Act
            await databaseService.UpdateAsync(database);

            // Assert
            Assert.AreEqual(context.Databases.FirstOrDefault().Name, updatedName);
            Assert.AreEqual(context.Databases.FirstOrDefault(), database);
        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            // Arrange
            var database = new Database
            {
                Id = Guid.NewGuid(),
                Name = databaseName
            };
            using var context = new AzureSqlDbContext(DbContextUtilities.GetContextOptions());
            await context.Databases.AddAsync(database);
            await context.SaveChangesAsync();

            var databaseService = new DatabaseService(context);

            // Act
            await databaseService.DeleteAsync(database.Id);

            // Assert
            Assert.AreEqual(context.Databases.Count(), 0);
        }
    }
}

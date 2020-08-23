using System;
using System.IO;
using CloudShop.DAL;
using CloudShop.DAL.Implementations;
using CloudShop.DAL.Implementations.Factories;
using CloudShop.DAL.Structures;
using Shouldly;
using Xunit;

namespace CloudShop.LearningTests
{
    public class UnitTest1
    {

        [Fact]
        public void CreateSampleDb() {
            var sqliteDbPath = GetSampleDbFile();
            if (File.Exists(sqliteDbPath)) File.Delete(sqliteDbPath);
            File.Create(sqliteDbPath).Close();
            
            var factory = new CloudShopDalFactory();
            var dal = factory.Create($"Data Source={sqliteDbPath}", DatabaseType.Sqlite);
            var migrationResult = dal.Migrate();
            migrationResult.IsSuccess.ShouldBeTrue();
        }
        
        private static string GetSampleDbFile()
        {
            var currentPath = Environment.CurrentDirectory;
            while (!Directory.Exists(Path.Join(currentPath, "CloudShop.DAL"))) {
                currentPath = Directory.GetParent(currentPath).FullName;
            }

            return Path.Join(new[] {currentPath, "CloudShop.DAL", "Migrations", "sampledb.sqlite"});
        }
    }
}
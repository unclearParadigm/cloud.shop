// ReSharper disable UseDeconstruction
using System;
using System.IO;
using CloudShop.DAL.Implementations;
using CloudShop.DAL.Implementations.Factories;
using CloudShop.DAL.Migrations;
using CloudShop.DAL.Structures;
using Shouldly;
using Xunit;

namespace CloudShop.Tests.Dal.MigrationTests
{
    public class SqliteDalMigrationTests
    {
        [Fact]
        public void TestMigrationLocatorFindsMigrations() {
            MigrationLocator.GetSchemaMigrations().ShouldNotBeEmpty();
        }
        
        [Fact]
        public void TestMigrateSqlite() {
            var sqliteDbPath = Path.Join(Environment.CurrentDirectory, $"{Guid.NewGuid()}.sqlite");
            File.Exists(sqliteDbPath).ShouldBeFalse();
            File.Create(sqliteDbPath).Close();
            File.Exists(sqliteDbPath).ShouldBeTrue();

            var factory = new CloudShopDalFactory();
            var dal = factory.Create($"Data Source={sqliteDbPath}", DatabaseType.Sqlite);
            var migrationResult = dal.Migrate();
            migrationResult.IsSuccess.ShouldBeTrue();

            File.Delete(sqliteDbPath);
            File.Exists(sqliteDbPath).ShouldBeFalse();
        }
        
        [Fact]
        public void TestEmptyMigration() 
        {
            var sqliteDbPath = Path.Join(Environment.CurrentDirectory, $"{Guid.NewGuid()}.sqlite");
            var factory = new CloudShopDalFactory();
            var dal = factory.Create($"Data Source={sqliteDbPath}", DatabaseType.Sqlite);
            var migrationResult = dal.Migrate();
            migrationResult.IsSuccess.ShouldBeTrue();

            var users = dal.GetUsers();
            users.IsSuccess.ShouldBeTrue();
            users.Value.ShouldBeEmpty();
            var articles = dal.GetArticles();
            articles.IsSuccess.ShouldBeTrue();
            articles.Value.ShouldBeEmpty();
            var transactions = dal.GetTransactions();
            transactions.IsSuccess.ShouldBeTrue();
            transactions.Value.ShouldBeEmpty();
            
            File.Delete(sqliteDbPath);
            File.Exists(sqliteDbPath).ShouldBeFalse();
        }
    }
}
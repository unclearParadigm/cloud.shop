using System;
using System.Data;
using CloudShop.DAL.Migrations;
using CloudShop.DAL.Structures;
using CSharpFunctionalExtensions;
using DbUp;
using DbUp.Builder;
using DbUp.Engine;
using Microsoft.Data.Sqlite;

namespace CloudShop.DAL.Implementations.Infrastructure
{
    internal class DatabaseConnectionOpener {
        private readonly string _connectionString;
        private readonly DatabaseType _databaseType;

        internal DatabaseConnectionOpener(string connectionString) {
            if(string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("The provided Connection-String is not valid", nameof(connectionString));
            
            _connectionString = connectionString;
            var guessedDatabaseType = GuessDatabaseType(connectionString);
            if(guessedDatabaseType.HasNoValue) 
                throw new ArgumentException("Could not find a matching Database Driver-Type for the specified ConnectionString. Auto-Detection of DatabaseType failed.", nameof(connectionString));
        }

        internal DatabaseConnectionOpener(string connectionString, DatabaseType databaseType)
        {
            if(string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("The provided Connection-String is not valid", nameof(connectionString));
            
            _connectionString = connectionString;
            _databaseType = databaseType;
        }

        public UpgradeEngine GetMigrationUpdateEngine()
        {
            UpgradeEngineBuilder builder;
            
            switch(_databaseType) {
                case DatabaseType.Sqlite:
                    builder = DeployChanges.To.SQLiteDatabase(_connectionString);
                    break;
                case DatabaseType.Oracle:
                    throw new NotImplementedException("Oracle Implementation does not yet exist");
                case DatabaseType.MariaDb:
                    throw new NotImplementedException("MariaDb Implementation does not yet exist");
                case DatabaseType.Postgresql:
                    throw new NotImplementedException("Postgresql Implementation does not yet exist");
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return builder
                .WithScripts(MigrationLocator.GetSchemaMigrations())
                .Build();
        }

        public IDbConnection OpenConnection()
        {
            var connection = GetConnection();
            connection.Open();
            return connection;
        }
        
        private IDbConnection GetConnection()
        {
            switch (_databaseType)
            {
                case DatabaseType.Sqlite:
                    return new SqliteConnection(_connectionString);
                    break;
                case DatabaseType.Oracle:
                    break;
                case DatabaseType.MariaDb:
                    break;
                case DatabaseType.Postgresql:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            throw new ArgumentOutOfRangeException();
        }

        private static Maybe<DatabaseType> GuessDatabaseType(string connectionString)
        {
            if(connectionString.StartsWith("Data Source") && connectionString.ToLower().EndsWith(".sqlite"))
                return Maybe<DatabaseType>.From(DatabaseType.Sqlite);
            

            return Maybe<DatabaseType>.None;
        }
    }
}
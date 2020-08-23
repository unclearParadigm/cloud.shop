using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DbUp.Engine;

namespace CloudShop.DAL.Migrations
{
    public static class MigrationLocator {
        public static IEnumerable<SqlScript> GetSchemaMigrations() {
            return Directory
                .GetFiles(Environment.CurrentDirectory, "*.sql", SearchOption.AllDirectories)
                .Where(filePath => filePath != null && Path.GetFileName(filePath).StartsWith("M"))
                .Where(filePath => filePath.ToLower().EndsWith(".sql"))
                .Where(filePath => Path.GetFileName(filePath).Substring(1, 2).All(char.IsDigit))
                .OrderBy(filePath => Convert.ToInt32(Path.GetFileName(filePath).Substring(1, 2)))
                .Select(filePath => new SqlScript(Path.GetFileName(filePath), File.ReadAllText(filePath, Encoding.UTF8)))
                .ToList();
        }
    }
}
using App.Metrics;
using CloudShop.DAL.Implementations.Infrastructure;
using CloudShop.DAL.Implementations.Proxies;
using CloudShop.DAL.Interfaces;
using CloudShop.DAL.Structures;
using Microsoft.Extensions.Logging;

namespace CloudShop.DAL.Implementations.Factories
{
    public class CloudShopDalFactory : ICloudShopDalFactory
    {
        public ICloudShopDal Create(string connectionString)
        {
            var connectionOpener = new DatabaseConnectionOpener(connectionString);
            var cloudShopDal = new DbAgnosticDal(connectionOpener);
            return cloudShopDal;
        }

        public ICloudShopDal Create(string connectionString, DatabaseType databaseType)
        {
            var connectionOpener = new DatabaseConnectionOpener(connectionString, databaseType);
            var cloudShopDal = new DbAgnosticDal(connectionOpener);
            return cloudShopDal;
        }

        public ICloudShopDal Create(string connectionString, DatabaseType databaseType, IMetrics metrics)
        {
            var connectionOpener = new DatabaseConnectionOpener(connectionString, databaseType);
            var cloudShopDal = new DalMetricsProxy(metrics, new DbAgnosticDal(connectionOpener));
            return cloudShopDal;
        }

        public ICloudShopDal Create(string connectionString, DatabaseType databaseType, IMetrics metrics, ILogger logger)
        {
            var connectionOpener = new DatabaseConnectionOpener(connectionString, databaseType);
            var cloudShopDal = new DalLoggingProxy(logger, new DalMetricsProxy(metrics, new DbAgnosticDal(connectionOpener)));
            return cloudShopDal;
        }
    }
}
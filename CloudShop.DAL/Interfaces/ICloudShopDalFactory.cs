using App.Metrics;
using CloudShop.DAL.Structures;
using Microsoft.Extensions.Logging;

namespace CloudShop.DAL.Interfaces {
    public interface ICloudShopDalFactory {
        ICloudShopDal Create(string connectionString);
        ICloudShopDal Create(string connectionString, DatabaseType databaseType);
        ICloudShopDal Create(string connectionString, DatabaseType databaseType, IMetrics metrics);
        ICloudShopDal Create(string connectionString, DatabaseType databaseType, IMetrics metrics, ILogger logger);
    }
}
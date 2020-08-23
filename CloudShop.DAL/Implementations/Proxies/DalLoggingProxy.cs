using System.Collections.Generic;
using CloudShop.DAL.Interfaces;
using Cloudshop.Domain;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CloudShop.DAL.Implementations.Proxies
{
    internal class DalLoggingProxy : ICloudShopDal
    {
        private readonly ILogger _logger;
        private readonly ICloudShopDal _cloudShopDal;

        internal DalLoggingProxy(ILogger logger, ICloudShopDal cloudShopDal)
        {
            _logger = logger;
            _cloudShopDal = cloudShopDal;
        }
        
        public Result Migrate()
        {
            return _cloudShopDal.Migrate();
        }

        public Result<Maybe<UserDto>> GetUser(long userId)
        {
            return _cloudShopDal.GetUser(userId);
        }

        public Result<IList<UserDto>> GetUsers()
        {
            return _cloudShopDal.GetUsers();
        }

        public Result<Maybe<ArticleDto>> GetArticle(long articleId)
        {
            return _cloudShopDal.GetArticle(articleId);
        }

        public Result<IList<ArticleDto>> GetArticles()
        {
            return _cloudShopDal.GetArticles();
        }

        public Result<IList<ArticleDto>> GetActiveArticles()
        {
            return _cloudShopDal.GetActiveArticles();
        }

        public Result<IList<TransactionDto>> GetTransactions()
        {
            return _cloudShopDal.GetTransactions();
        }

        public Result AddTransaction(TransactionDto transactionDto)
        {
            return _cloudShopDal.AddTransaction(transactionDto);
        }

        public Result DecreaseStockCount(long articleId, long quantity)
        {
            return _cloudShopDal.DecreaseStockCount(articleId, quantity);
        }
    }
}
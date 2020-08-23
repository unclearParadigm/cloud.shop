using System.Collections.Generic;
using Cloudshop.Domain;
using CSharpFunctionalExtensions;

namespace CloudShop.DAL.Interfaces
{
    public interface ICloudShopDal
    {
        Result Migrate();

        Result<Maybe<UserDto>> GetUser(long userId);
        Result<IList<UserDto>> GetUsers();

        Result<Maybe<ArticleDto>> GetArticle(long articleId);
        Result<IList<ArticleDto>> GetArticles();
        Result<IList<ArticleDto>> GetActiveArticles();
        
        Result<IList<TransactionDto>> GetTransactions();
        Result AddTransaction(TransactionDto transactionDto);
        Result DecreaseStockCount(long articleId, long quantity);
    }
}
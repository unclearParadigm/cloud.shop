using System;
using System.Linq;
using System.Collections.Generic;

using CSharpFunctionalExtensions;

using CloudShop.DAL;
using CloudShop.DAL.Interfaces;
using Cloudshop.Domain;
using CloudShop.Mailer;

namespace CloudShop.BL
{
    public class CloudShopBl
    {
        private readonly ICloudShopDal _cloudShopDal;
        private readonly ICloudShopMailer _cloudShopMailer;

        public CloudShopBl(ICloudShopDal cloudShopDal, ICloudShopMailer cloudShopMailer) {
            _cloudShopDal = cloudShopDal;
            _cloudShopMailer = cloudShopMailer;
        }

        public Result Purchase(UserDto userDto, IList<ArticleDto> articles) {
            if (!userDto.HasPurchasePermissions) 
                return Result.Failure($"Insufficient Permissions for User: '{userDto}'");
            if (articles.All(a => a.IsArticleInValidRange))
                return Result.Failure($"{articles.First(a => !a.IsArticleInValidRange)} is not (yet) purchaseable");
            
            var transactionDate = DateTime.UtcNow;
            var distinctArticles = articles.Select(a => a.Id).Distinct();
            
            var transactions = distinctArticles
                .Select(articleId => new TransactionDto
                {
                    UserId = userDto.Id,
                    ArticleId = articleId, 
                    Quantity = articles.Count(a => a.Id == articleId),
                    TransactionDate = transactionDate
                })
                .ToList();

            var transactionResults = transactions
                .Select(_cloudShopDal.AddTransaction);

            var stockOperationResult = transactions
                .Select(t => _cloudShopDal.DecreaseStockCount(t.ArticleId, t.Quantity));
            
            return Result
                .Combine(transactionResults.Concat(stockOperationResult))
                .TapIf(userDto.WantsToReceiveBillingMails, () => _cloudShopMailer.SendBill(userDto.Email, articles));
        }
    }
}
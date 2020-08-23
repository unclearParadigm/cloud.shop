using System;
using System.Collections.Generic;
using Cloudshop.Domain;
using CSharpFunctionalExtensions;

namespace CloudShop.Mailer
{
    public interface ICloudShopMailer
    {
        Result SendBill(string mail, IList<ArticleDto> purchasedArticles);
    }
}
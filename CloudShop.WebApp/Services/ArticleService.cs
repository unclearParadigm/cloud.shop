using System.Collections.Generic;
using System.Linq;
using CloudShop.DAL.Interfaces;
using CloudShop.WebApp.Models.ArticleService;
using CSharpFunctionalExtensions;

namespace CloudShop.WebApp.Services
{
    public class ArticleService
    {
        private readonly ICloudShopDal _cloudShopDal;
        
        public ArticleService()
        {
        }
        
        public ArticleService(ICloudShopDal cloudShopDal)
        {
            _cloudShopDal = cloudShopDal;
        }
        


        public IList<ArticleViewModel> GetArticles()
        {
            return new List<ArticleViewModel>()
            {
                new ArticleViewModel()
                {
                    Manufacturer = "Milka", 
                    ArticleId = 0, 
                    Description = "Weiche Bisquit-Roulade mit Nussfüllung und Schokoladeüberzug",
                    ArticleImage = "https://prod-cd-origin.milka.de/~/media/Project/Brands/Milka/de/All-Products/milka-tender-milch/Product-Details/pdp-hd--1.png",
                    Price = 0.69m,
                    ArticleName = "Tender Nuss"
                },
                new ArticleViewModel()
                {
                    Manufacturer = "Milka", 
                    Description = "Weiche Bisquit-Roulade mit Milch-Creme-Füllung und Schokoladeüberzug",
                    ArticleId = 0, 
                    ArticleImage = "https://prod-cd-origin.milka.de/~/media/Project/Brands/Milka/de/All-Products/milka-tender-milch/Product-Details/pdp-hd--1.png",
                    Price = 0.50m,
                    ArticleName = "Tender Milch"
                },
                new ArticleViewModel()
                {
                    Manufacturer = "UTZ", 
                    ArticleId = 0, 
                    Description = "Knuspriger Riegel für Zwischendurch mit viel Ballaststoffen und Geschmack. Schokoladenkeks-Geschmack",
                    ArticleImage = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.worldofsweets.de%2Fout%2Fpictures%2Fmaster%2Fproduct%2F2%2Fcorny-schoko-banane-6-riegel.jpg&f=1&nofb=1",
                    Price = 0.40m,
                    ArticleName = "Corny Schoko Crunch"
                },
                new ArticleViewModel()
                {
                    Manufacturer = "UTZ", 
                    ArticleId = 0, 
                    Description = "Knuspriger Riegel für Zwischendurch mit viel Ballaststoffen und Geschmack. Bananen-Geschmack",
                    ArticleImage = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.worldofsweets.de%2Fout%2Fpictures%2Fmaster%2Fproduct%2F2%2Fcorny-schoko-banane-6-riegel.jpg&f=1&nofb=1",
                    Price = 0.60m,
                    ArticleName = "Corny Banane"
                },
                new ArticleViewModel()
                {
                    Manufacturer = "Kinder", 
                    ArticleId = 0, 
                    Description = "2 Riegel, mit Haselnuss-Creme-Füllung, ummantelt mit herzhafter Schokolade",
                    ArticleImage = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2F2.bp.blogspot.com%2F-X1DdyIhoJRc%2FUPskqaAoDqI%2FAAAAAAAAAqc%2F7evxqz9EIbE%2Fs1600%2Fkinder_bueno.jpg&f=1&nofb=1",
                    Price = 0.80m,
                    ArticleName = "Kinder Bueno (Milch-Haselnuss)"
                }
            };
            
            // ReSharper disable once UseDeconstruction
            var articles = _cloudShopDal.GetArticles();
            if (articles.IsFailure) return new List<ArticleViewModel>();
            
            return articles
                .Value
                .Select(a => new ArticleViewModel {
                    ArticleImage = a.ArticleImage,
                    ArticleName = a.ArticleName,
                    Manufacturer = a.Manufacturer,
                    Price = a.ArticlePrice
                })
                .ToList();
        }
    }
}
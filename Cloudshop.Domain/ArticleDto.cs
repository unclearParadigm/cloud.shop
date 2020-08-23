using System;

namespace Cloudshop.Domain
{
    public class ArticleDto
    {
        public long Id { get; set; }
        public string ArticleName { get; set; }
        public string Manufacturer { get; set; }

        public decimal ArticleWeight { get; set; }
        public decimal ArticlePrice { get; set; }
        public string ArticleImage { get; set; }
        
        public decimal KiloJoule { get; set; }
        public decimal KiloCalories { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        public bool IsArticleInValidRange =>
            DateTime.UtcNow >= ValidFrom
            && DateTime.UtcNow <= ValidUntil;
    }
}
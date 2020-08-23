// ReSharper disable ClassNeverInstantiated.Global
namespace CloudShop.DAL.Entities
{
    internal sealed class ArticleEntity : IdentifiedEntity
    {
        public string ArticleName { get; set; }
        public string Manufacturer { get; set; }

        public decimal ArticleWeight { get; set; }
        public decimal ArticlePrice { get; set; }
        public string ArticleImage { get; set; }
        
        public decimal KiloJoule { get; set; }
        public decimal KiloCalories { get; set; }
    }
}
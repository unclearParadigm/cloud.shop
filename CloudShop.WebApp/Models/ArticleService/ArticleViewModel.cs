namespace CloudShop.WebApp.Models.ArticleService
{
    public class ArticleViewModel
    {
        public long ArticleId { get; set; }
        public string Manufacturer { get; set; }
        public string ArticleName { get; set; }
        public string ArticleImage { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
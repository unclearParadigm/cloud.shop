// ReSharper disable ClassNeverInstantiated.Global
namespace CloudShop.DAL.Entities
{
    internal sealed class StockEntryEntity : IdentifiedEntity
    {
        public long ArticleId { get; set; }
        public long AvailableQuantity { get; set; }
    }
}
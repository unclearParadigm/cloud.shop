// ReSharper disable ClassNeverInstantiated.Global
using System;

namespace CloudShop.DAL.Entities
{
    internal sealed class TransactionEntity : IdentifiedEntity
    {
        public long UserId { get; set; }
        public long ArticleId { get; set; }
        public long Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
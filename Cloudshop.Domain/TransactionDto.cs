using System;

namespace Cloudshop.Domain
{
    public class TransactionDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ArticleId { get; set; }
        public long Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
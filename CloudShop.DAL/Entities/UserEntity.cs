// ReSharper disable ClassNeverInstantiated.Global
namespace CloudShop.DAL.Entities
{
    internal sealed class UserEntity : IdentifiedEntity
    {
        public string CardNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        
        public bool HasPurchasePermissions { get; set; }
        public bool WantsToReceiveBillingMails { get; set; }
    }
}
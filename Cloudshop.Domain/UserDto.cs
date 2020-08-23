using System;

namespace Cloudshop.Domain
{
    public class UserDto {
        public long Id { get; set; }
        
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        
        public string CardNumber { get; set; }
        
        public bool HasPurchasePermissions { get; set; }
        public bool WantsToReceiveBillingMails { get; set; }
    }
}
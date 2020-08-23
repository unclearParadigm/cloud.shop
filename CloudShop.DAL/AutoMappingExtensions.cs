using AutoMapper;
using Cloudshop.Domain;
using CloudShop.DAL.Entities;

namespace CloudShop.DAL
{
    public static class AutoMappingExtensions
    {
        private static readonly MapperConfiguration Configuration = new MapperConfiguration(cfg =>
        {
            /* Mapping Configuration */
            cfg.AllowNullCollections = false;
            cfg.AllowNullDestinationValues = false;

            /* Actual Mappings */
            cfg.CreateMap<UserEntity, UserDto>();
            cfg.CreateMap<ArticleEntity, ArticleDto>();
            cfg.CreateMap<StockEntryEntity, StockEntryDto>();
            cfg.CreateMap<TransactionEntity, TransactionDto>();
        });
        
        private static readonly IMapper Mapper = Configuration.CreateMapper();
        
        public static TTarget Map<TTarget>(this object source) {
            return Mapper.Map<TTarget>(source);
        }
    }
}
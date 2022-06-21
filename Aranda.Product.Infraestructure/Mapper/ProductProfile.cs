using Aranda.Product.Infraestructure.DTO;
using AutoMapper;

namespace Aranda.Product.Infraestructure.Mapper
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDTO, Models.Product>().ForPath(x=> x.Category.Id, y=> y.MapFrom(p=> p.IdCategory));
            CreateMap<Models.Product, ProductDTO>().ForMember(x=> x.Category, y=> y.MapFrom(p=> p.Category != null ? p.Category.Name : string.Empty) );
        }
    }
}

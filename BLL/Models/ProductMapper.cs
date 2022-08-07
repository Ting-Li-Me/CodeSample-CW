using AutoMapper;
using BLL.Interfaces;
using Domain.Models;

namespace BLL.Models
{
    public  class ProductMapper: IMapping<Product,ProductBO>
    {
        private IMapper mapperToBO;
        private IMapper mapperToEntity;
        public ProductMapper()
        {
            mapperToEntity = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ProductBO, Product>()));
            mapperToBO = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductBO>()));
        }

        public ProductBO MapToBO(Product product)
        {
            var productBO = mapperToBO.Map<ProductBO>(product);
            return productBO;
        }

        public Product MapToEntity(ProductBO productBO)
        {
            var product = mapperToEntity.Map<Product>(productBO);
            return product;
        }

    }
}

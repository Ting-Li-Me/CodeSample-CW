using BLL.Interfaces;
using BLL.Models;
using Domain.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private  IUnitOfWork unitOfWork;
        private IMapping<Product, ProductBO> mapping;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            mapping = new ProductMapper();
        }

        public async Task<IEnumerable<ProductBO>> GetAllProductsAsync()
        {
            IEnumerable<Product> productEntitys = null;
            IList<ProductBO> productBOs = new List<ProductBO>();
            try
            {
                productEntitys = await unitOfWork.Product.
                    GetAllAsync();

                if (productEntitys != null && productEntitys.Count() > 0)
                {
                    foreach (var entity in productEntitys)
                    {
                        productBOs.Add(mapping.MapToBO(entity));
                    }

                }
            }
            catch
            {
                throw new Exception();
            }

            return productBOs;

        }


        public async Task<ProductBO> GetProdByidAsync(int id)
        {
            Product productEntity = null;
            ProductBO productBO = null;
            try
            {
                productEntity = await unitOfWork.Product.GetByIdAsync(id);

                if (productEntity != null )
                {
                    productBO = mapping.MapToBO(productEntity);

                }
            }
            catch
            {
                throw new Exception();
            }

            return productBO;

        }

        public async Task AddProdAsync(ProductBO productBO)
        {
            Product productEntity = mapping.MapToEntity(productBO);

            try
            {
                await unitOfWork.Product.AddAsync(productEntity);
                await unitOfWork.SaveAsync();

            }
            catch
            {
                throw new Exception();
            }
        }


        public async Task UpdateProdAsync(ProductBO productBO)
        {
            Product productEntity = mapping.MapToEntity(productBO);
            try
            {
                unitOfWork.Product.Update(productEntity);

                await unitOfWork.SaveAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task DeleteProdAsync(int id)
        {
            try
            {
               var productEntity =  await unitOfWork.Product.GetByIdAsync(id);

                if (productEntity != null)
                {
                    unitOfWork.Product.Delete(productEntity);
                    await unitOfWork.SaveAsync();

                }
            }
            catch
            {
                throw new Exception();
            }
        }




    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyWashApi.Data.Models;
using MyWashApi.Data.Repositories;
using ImageUploader;

namespace MyWashApi.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public async Task<Product> GetProduct(Guid id)
        {
            return await _productRepository.GetProduct(id);
        }

        public async Task<Product> CreateProduct(Product newProduct)
        {
            // Use transaction to be safe...
            if (!UploadImage(newProduct.ImageArray))
            {
                throw new Exception($"Could not upload product image.");
            }

            return await _productRepository.AddAsync(newProduct);
        }

        public async Task UpdateProduct(Product product)
        {
            // Use transaction to be safe...
            if (!UploadImage(product.ImageArray))
            {
                throw new Exception($"Could not upload product image.");
            }

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProduct(Guid productId)
        {
            var product = await _productRepository.GetProduct(productId);
            await _productRepository.Delete(product);
        }

        private bool UploadImage(byte[] data)
        {
            var stream = new MemoryStream(data);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";

            return FilesHelper.UploadImage(stream, folder, file);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepehr.Application.DTOs.Product;

namespace Sepehr.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts();
    }
}

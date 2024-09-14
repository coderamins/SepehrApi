
namespace Sepehr.Domain.ViewModels
{
    public class ProductBrandByProdTypeViewModel
    {
        public int Id { get; set; }
        public string Desc { get; set; } = string.Empty;
        
        public List<ProductBrandViewModel> ProductBrands { get; set; }  

    }
}

using Infrastructure.DataModels;

namespace SampleProduct.ViewModels.Products
{
    public class ProductModel
    {
        public IEnumerable<view_product> Products { get; set; }
        public string ErrorMessage { get; set; }
    }
}

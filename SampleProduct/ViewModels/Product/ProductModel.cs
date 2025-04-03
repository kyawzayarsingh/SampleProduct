using Infrastructure.DataModels;

namespace SampleProduct.ViewModels.Product
{
    public class ProductModel
    {
        public IEnumerable<view_product>? Products { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

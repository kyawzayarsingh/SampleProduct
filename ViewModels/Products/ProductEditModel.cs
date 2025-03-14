using Infrastructure.DataModels;

namespace SampleProduct.ViewModels.Products
{
    public class ProductEditModel
    {
        public view_product Product { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

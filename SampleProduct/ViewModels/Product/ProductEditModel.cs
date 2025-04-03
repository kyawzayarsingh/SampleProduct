using Infrastructure.DataModels;

namespace SampleProduct.ViewModels.Product
{
    public class ProductEditModel
    {
        public view_product? Product { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

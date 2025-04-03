using Infrastructure.DataModels;

namespace SampleProduct.ViewModels.Product
{
    public class ProductEntryModel
    {
        public product? Product { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

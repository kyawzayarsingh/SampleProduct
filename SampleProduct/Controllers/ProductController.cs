using Infrastructure.DataModels;
using Infrastructure.Respositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleProduct.ViewModels.Product;

namespace SampleProduct.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly IConfiguration configuration;
        public ProductController()
        {
            this._productRepository = new ProductRepository();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProducts();
            ProductModel model = new ProductModel();
            model.Products = products;
            return View(model);
        }

        public IActionResult Create()
        {
            ProductEntryModel model = new ProductEntryModel();
            model.Product = new product();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductEntryModel model)
        {
            if (string.IsNullOrEmpty(model.Product.product_name))
            {
                model.ErrorMessage = "*Please enter product name!";
            }
            else
            {
                product product = new product();
                product.product_name = model.Product.product_name;
                product.description = model.Product.description;
                await _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var product = await _productRepository.FindProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ProductEditModel editModel = new ProductEditModel();
            editModel.Product = await _productRepository.FindProductById(id);

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string productEditModel)
        {
            ProductEditModel editModel = new ProductEditModel();
            editModel.Product = JsonConvert.DeserializeObject<view_product>(productEditModel);

            if (editModel.Product is not null)
            {
                product product = new product();
                product.product_id = editModel.Product.product_id;
                product.price = editModel.Product.price;
                product.product_name = editModel.Product.product_name;
                product.description = editModel.Product.description;

                await _productRepository.Update(product);
                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var product = await _productRepository.FindProductById(id);

            if (product == null)
            {
                return BadRequest();
            }
            else
            {
                await _productRepository.Remove(product);
                return RedirectToAction("Index");
            }
        }
    }
}

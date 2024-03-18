using Management_System.Models.Dtos;
using Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_System.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        #region MyRegion

        private readonly ILogger<ProductController> logger;
        private IProductService productService;

        public ProductController(ILogger<ProductController> Logger, IProductService ProductService)
        {
            logger = Logger;
            productService = ProductService;
        }

        #endregion

        #region Get
        public async Task<IActionResult> Index()
        {
            var result = await productService.GetAllProductsAsync(new SearchFilterProductDto());
            List<ProductDto> products = new();
            if (result != null)
                products = result.ToList();

            return View(products);
        }

        public async Task<IActionResult> Detail(Guid Id)
        {
            var result = await productService.GetProductByIdAsync(Id);
            if (result == null)
                return NotFound();
            ProductDto product = result;
            return View(product);
        }

        #endregion

        #region Create

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddProductDto addProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                await productService.AddProductAsync(addProductDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            try
            {
                await productService.DeleteProductAsync(Id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Index");
        }


        #endregion

        #region Edit
        public async Task<IActionResult> Edit(Guid Id)
        {
            var result = await productService.GetProductByIdAsync(Id);
            if (result == null)
                return NotFound();
            EditProductDto product = new EditProductDto()
            {
                Id = result.Id,
                Description = result.Description,
                Name = result.Name
            }
            ;
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Guid Id, EditProductDto editProductDto)
        {
            try
            {
                await productService.EditProductAsync(Id, editProductDto);
                logger.LogInformation($"Product With Id {Id} Updated At {DateTime.Now}");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}

namespace Management_System.Services
{
    public interface IProductService
    {
        #region Get
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(SearchFilterProductDto searchFilterProductDto = null);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        #endregion

        #region Add , Update , Delete
        Task<ProductDto> AddProductAsync(AddProductDto addProductDto);
        Task<ProductDto> EditProductAsync(Guid Id, EditProductDto editProductDto);
        Task DeleteProductAsync(Guid id);
        #endregion
    }

    [EasyDi(ServiceLifetime.Scoped)]
    [Display(Name = "محصولات", Description = "این قسمت مربوط به محصولات می باشد")]
    public class ProductService : IProductService
    {
        #region Constructor
        private readonly IMapper mapper;
        private readonly MainContext context;
        private readonly IPuterContext puterContext;
        public ProductService(IMapper Mapper, MainContext Context, IPuterContext PuterContext)
        {
            mapper = Mapper;
            context = Context;
            puterContext = PuterContext;
        }
        #endregion

        #region Get
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(SearchFilterProductDto searchFilterProductDto)
        {
            var query = context.Products.Where(p => !p.IsDeleted).AsNoTracking().AsQueryable();

            if (searchFilterProductDto != null)
            {
                if (searchFilterProductDto.Id != null) query = query.Where(p => searchFilterProductDto.Id.Contains(p.Id));
                if (searchFilterProductDto.Name != null) query = query.Where(p => searchFilterProductDto.Name.Contains(p.Name));
                if (searchFilterProductDto.Description != null) query = query.Where(p => searchFilterProductDto.Description.Contains(p.Description));
            }

            var Products = await query.ToListAsync();
            if (Products == null || Products.Count == 0) return new List<ProductDto>();
            return mapper.Map<IEnumerable<ProductDto>>(Products);

        }
        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return new ProductDto();

            return mapper.Map<ProductDto>(product);
        }
        #endregion

        #region Add , Update , Delete
        public async Task<ProductDto> AddProductAsync(AddProductDto addProductDto)
        {
            var product = mapper.Map<Product>(addProductDto);
            if (addProductDto.ImageFile is not null)
            {
                product.ImageAddress = addProductDto.ImageFile.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductImg",
                    addProductDto.ImageFile.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await addProductDto.ImageFile.CopyToAsync(stream);
            }

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return mapper.Map<ProductDto>(product);
        }
        public async Task<ProductDto> EditProductAsync(Guid Id, EditProductDto editProductDto)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == Id);
            if (product == null) return new ProductDto();

            if (editProductDto.ImageFile is not null)
            {
                if (product.ImageAddress is not null)
                {
                    var path_Last_img = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductImg", product.ImageAddress);
                    if (File.Exists(path_Last_img))
                    {
                        File.Delete(path_Last_img);
                    }
                }

                product.ImageAddress = editProductDto.ImageFile.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductImg",
                    editProductDto.ImageFile.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await editProductDto.ImageFile.CopyToAsync(stream);
            }

            puterContext.Put(product, editProductDto);
             context.Products.Update(product);
            await context.SaveChangesAsync();

            return mapper.Map<ProductDto>(product);
        }
        public async Task DeleteProductAsync(Guid id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            product!.IsDeleted = true;

            #region delete_img
            //if (product.ImageAddress is not null)
            //{
            //    var path_Last_img = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductImg", product.ImageAddress);
            //    if (System.IO.File.Exists(path_Last_img))
            //    {
            //        System.IO.File.Delete(path_Last_img);
            //    }
            //}
            #endregion
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
        #endregion
    }
}
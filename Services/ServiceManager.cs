using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    private readonly IOrderService _orderService;
    public ServiceManager(IProductService productService, ICategoryService categoryService, IOrderService orderService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _orderService = orderService;
    }

    public ICategoryService CategoryService => _categoryService;
    public IProductService ProductService => _productService;
    public IOrderService OrderService => _orderService;
}
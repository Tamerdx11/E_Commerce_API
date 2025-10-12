namespace E_Commerce.Service.Services;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    : IProductService
{
    public async Task<IEnumerable<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken = default)
    {
        var brands = await unitOfWork.GetRepository<ProductBrand>().GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<BrandResponse>>(brands);
    }

    public async Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.GetRepository<Product>().GetByIdAsync(id, cancellationToken);
        return mapper.Map<ProductResponse>(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await unitOfWork.GetRepository<Product>().GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken = default)
    {
        var types = await unitOfWork.GetRepository<ProductType>().GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<TypeResponse>>(types);
    }
}

using E_Commerce.ServiceAbstraction.Common;
using E_Commerce.Shared.DataTransferObjects.Orders;

namespace E_Commerce.ServiceAbstraction;

public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email);
}

using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controller;

public class OrdersController(IOrderService service)
    : APIBaseController
{
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await service.CreateAsync(request, email!);
        return HandleResult(result);

    }
}

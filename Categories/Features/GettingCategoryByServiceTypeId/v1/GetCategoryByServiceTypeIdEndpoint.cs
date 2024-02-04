using Microsoft.AspNetCore.Mvc;
using QuickFix.Categories.Features.GettingCategoryById.v1;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryByServiceTypeId.v1
{
    public class GetCategoryByServiceTypeIdController : Controller
    {
        private readonly ILogger<GetCategoryByServiceTypeIdController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetCategoryByServiceTypeIdController(ILogger<GetCategoryByServiceTypeIdController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/category_serviceType/get_by_id/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpGet]
        public async Task<ActionResult<GetCategoryByServiceTypeIdRespons>> GetCategoryByServiceTypeIdMeth(Guid ServiceTypeId)
        {
            var result = await _sender.SendAsync(new GetCategoryByServiceTypeId(ServiceTypeId));
            return Ok(result);
        }
    }
}

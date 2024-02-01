using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Abstractions.Queries;
using StackExchange.Redis;

namespace QuickFix.ServicesType.Features.GettingServicesTypeByPage.v1
{
    public class GetServicesTypeController : Controller
    {
        private readonly ILogger<GetServicesTypeController> _logger;
        private readonly IQueryProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetServicesTypeController(ILogger<GetServicesTypeController> logger, IQueryProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/serviceType/get_by_page/v1")]
        [ApiExplorerSettings(GroupName = "serviceType")]
        [HttpGet]
        public async Task<ActionResult<GetServicesTypeByPageRespons>> GetUnitByPageMeth(GetServiceTypeByPageRequest request)
        {
            var result = await _sender.SendAsync(
                new GetServicesTypeByPage
                {
                    Filters = request.Filters,
                    Includes = request.Includes,
                    Page = request.Page,
                    Sorts = request.Sorts,
                    PageSize = request.PageSize
                },
                _cancellationToken
            );
            return Ok(result);
        }

        [Route("api/serviceType/get_by_page/v1")]
        [ApiExplorerSettings(GroupName = "serviceType")]
        [HttpPost]
        public async Task<ActionResult<GetServicesTypeByPageRespons>> ByPostGetUnitByPageMeth([FromBody] GetServiceTypeByPageRequest request)
        {
            var result = await _sender.SendAsync(
                new GetServicesTypeByPage
                {
                    Filters = request.Filters,
                    Includes = request.Includes,
                    Page = request.Page,
                    Sorts = request.Sorts,
                    PageSize = request.PageSize
                },
                _cancellationToken
            );
            return Ok(result);
        }
    }
}

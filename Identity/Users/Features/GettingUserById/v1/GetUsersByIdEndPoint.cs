using System;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Identity.Users.Features.GettingUserById.v1;



    public class GetUsersByIdController:Controller
    {
        private readonly ILogger<GetUsersByIdController> _logger;

        private CancellationToken _cancellationToken;

        private readonly IQueryProcessor _sender;
        
        public GetUsersByIdController(IQueryProcessor sender, ILogger<GetUsersByIdController> logger)
        {
            _sender = sender;
            _logger = logger;
        }
[Route("api/user/get_by_Id/v1")]
[ApiExplorerSettings(GroupName = "user")]
        [HttpGet]
        public async Task<ActionResult> GetUsersByIdMeth(Guid Id)
        {
            var result = await _sender.SendAsync(new GetUsersById(Id), _cancellationToken);
            return Ok(result);
        }
    }

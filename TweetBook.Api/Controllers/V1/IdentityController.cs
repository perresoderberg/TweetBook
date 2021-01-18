using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Api.Contract.V1;
using TweetBook.Api.Contract.V1.Requests;
using TweetBook.Api.Contract.V1.Responses;
using TweetBook.Core.Business.Services;

namespace TweetBook.Api.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationRequest request)
        {
            var auth = _identityService.RegisterAsync(request.UserName, request.Password);

            if (!auth.IsCompletedSuccessfully)
            { 
                return BadRequest(new AuthFailedResponse()
                    {
                        Errors = auth.Result.Errors
                    }
                );
            }


            return Ok(new AuthSuccessResponse()
                {
                    Token = auth.Result.Token
                }
            );
        }
    }
}

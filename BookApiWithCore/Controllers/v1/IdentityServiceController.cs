using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiWithCore.Contracts;
using BookApiWithCore.Contracts.v1.Requests;
using BookApiWithCore.Contracts.v1.Response;
using BookApiWithCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApiWithCore.Controllers.v1
{
    //[Route("api/[controller]")]
    //[ApiController]
    
    public class IdentityServiceController : Controller
    {
        private readonly IIdentityService _identityService;
        public IdentityServiceController(IIdentityService identityservice)
        {
            _identityService = identityservice;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistartionRequest userObj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var AuthResponse = await _identityService.RegisterAsync(userObj.Email, userObj.Password);
            if (!AuthResponse.Success)
            {
                return BadRequest( new AuthFailedResponse
                {
                    Errors = AuthResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse { Token=AuthResponse.Token});
        }


        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginRequest userObj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var AuthResponse = await _identityService.LoginAsync(userObj.Email, userObj.Password);
            if (!AuthResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = AuthResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse { Token = AuthResponse.Token });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Geo_Back.Controllers
{

    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IdentityService _identityManager;

        public IdentityController(IdentityService userService) 
        {
            _identityManager = userService;
        }

        [HttpPost("api/v1/login")]
        public async Task<IdentityService.LoginResultModelView> POSTLogin(IdentityService.LoginModelView login)
        {
            return await _identityManager.Login(login);
        }



    }
}

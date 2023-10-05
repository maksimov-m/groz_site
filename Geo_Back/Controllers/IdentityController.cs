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
        public IdentityService.LoginResultModelView POSTLogin(IdentityService.LoginModelView login)
        {
            
        }



    }
}

using Sonaar.Interface;
using Microsoft.AspNetCore.Mvc;
using Sonaar.Domain.DataContexts;

namespace Sonaar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public readonly DataContext _context;
        public readonly ITokenService _tokenService;

        public BaseApiController(DataContext context, ITokenService tokenService)
        {
            this._context = context;
            this._tokenService = tokenService;
        }
    }
}

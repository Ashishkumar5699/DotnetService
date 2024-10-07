using Sonaar.Domain.DataContexts;
using Sonaar.Interface;

namespace Sonaar.Controllers
{
    public class AdminController : BaseApiController
    {
        public AdminController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }

        #region APIs
        
        #endregion
    }
}

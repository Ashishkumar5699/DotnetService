using APIServices.Data;
using APIServices.Interface;

namespace APIServices.Controllers
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

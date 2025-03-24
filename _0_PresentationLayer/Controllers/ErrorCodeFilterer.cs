using Microsoft.AspNetCore.Identity;

namespace _0_PresentationLayer.Controllers
{
    public static class ErrorCodeFilterer
    {
        public static Microsoft.AspNetCore.Mvc.ObjectResult ResultWrapErrorCode(this IdentityResult identityResult)
        {
            throw new NotImplementedException();
        }
        public static Microsoft.AspNetCore.Mvc.ObjectResult ExceptionWrapErrorCode(this Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}

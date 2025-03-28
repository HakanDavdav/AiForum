using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace _0_PresentationLayer.Controllers
{
    public static class ErrorCodeFilterer
    {
        public static ObjectResult ResultWrapErrorCode(this IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return new OkObjectResult(new { Message = "Operation succeeded" });
            }

            var firstError = identityResult.Errors.FirstOrDefault();
            if (firstError == null)
            {
                return new ObjectResult(new { Error = "Unknown error occurred" }) { StatusCode = 500 };
            }

            return firstError switch
            {
                _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors.ConflictError =>
                    new ConflictObjectResult(firstError),

                _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors.ForbiddenError =>
                    new ObjectResult(firstError) { StatusCode = 403 },

                _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors.InternalServerError =>
                    new ObjectResult(firstError) { StatusCode = 500 },

                _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors.NotFoundError =>
                    new NotFoundObjectResult(firstError),

                _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors.UnauthorizedError =>
                    new UnauthorizedObjectResult(firstError),

                _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors.UnexpectedError =>
                    new ObjectResult(firstError) { StatusCode = 500 },

                _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors.ValidationError =>
                    new BadRequestObjectResult(firstError),

                _ => new ObjectResult(new { Error = firstError.Description }) { StatusCode = 500 }
            };
        }

        public static ObjectResult ExceptionWrapErrorCode(this Exception ex)
        {
            // Eğer bir özel exception türü varsa ona göre işlem yapalım
            switch (ex)
            {
                case ArgumentException argEx:
                    // ArgumentException örneği
                    return new BadRequestObjectResult(new { Error = argEx.Message }) { StatusCode = 400 };

                case UnauthorizedAccessException unAuthEx:
                    // UnauthorizedAccessException örneği
                    return new UnauthorizedObjectResult(new { Error = unAuthEx.Message }) { StatusCode = 401 };

                case NotImplementedException notImplEx:
                    // NotImplementedException örneği
                    return new ObjectResult(new { Error = notImplEx.Message }) { StatusCode = 501 };

                case InvalidOperationException invalidOpEx:
                    // InvalidOperationException örneği
                    return new ConflictObjectResult(new { Error = invalidOpEx.Message }) { StatusCode = 409 };

                // Diğer özel exception'lar burada eklenebilir.

                default:
                    // Genel hata durumu
                    return new ObjectResult(new { Error = ex.Message }) { StatusCode = 500 };
            }
        }

    }
}

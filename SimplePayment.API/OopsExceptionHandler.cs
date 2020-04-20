using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace SimplePayment.API
{
    public class OopsExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var errorDataModel = new ErrorDataModel
            {
                Message = "Internal server error occurred, error has been reported!",
                Details = context.Exception.Message,
                ErrorReference = context.Exception.Data["ErrorReference"] != null ? context.Exception.Data["ErrorReference"].ToString() : string.Empty,
                DateTime = DateTime.UtcNow
            };

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, errorDataModel);
            context.Result = new ResponseMessageResult(response);
        }
    }
}
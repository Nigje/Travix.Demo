using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travix.Common.Exceptions;

namespace Travix.Demo.Controllers
{
    [Route("api/v1/exception")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        [HttpGet]
        [Route("/not-found")]
        public void ThrowNotFound() { throw new TravixNotFoundException("Not Found."); }

        [HttpGet]
        [Route("/bad-request")]
        public void ThrowBadRequest() { throw new TravixArgumentException(message: "Bad Reqeust."); }

        [HttpGet]
        [Route("/internal-server-error")]
        public void ThrowInternalServerError() { throw new TravixException(message: "Internal Server Error."); }

        [HttpGet]
        [Route("/exception")]
        public void ThrowException() { throw new Exception("Exception"); }
    }
}

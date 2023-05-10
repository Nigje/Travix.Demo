using Microsoft.AspNetCore.Http;

namespace Travix.Common.Models
{
    public class RequestContext
    {
        private readonly IHttpContextAccessor _context;
        public RequestContext(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string Username => _context.HttpContext?.Request?.Headers["Username"].FirstOrDefault();
        public int? UserId
        {
            get
            {
                int.TryParse(
                    _context.HttpContext?.Request?.Headers.FirstOrDefault(c => c.Key == "UserId").Value,
                    out var uun);
                return uun;
            }
        }
    }
}

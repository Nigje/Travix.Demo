using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Travix.DB;
using Travix.Demo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Travix.Demo.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await userService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> Get([FromRoute]int id)
        {
            return await userService.GetAsync(id);
        }

        [HttpPost]
        public async Task<long> Post([FromBody] User user)
        {
            return await userService.CreateAsync(user);
        }

        [HttpPut("{id}")]
        public async Task Put([FromRoute] int id, [FromBody] User user)
        {
            await userService.UpdatAsync(id, user);
        }

        [HttpDelete("{id}"),]
        public async Task Delete([FromRoute] int id)
        {
           await userService.DeleteAsync(id);
        }
    }
}

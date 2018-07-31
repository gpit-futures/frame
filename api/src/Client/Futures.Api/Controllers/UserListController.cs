using System.Threading.Tasks;
using Futures.Lists.Domain.Services.UserLists.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futures.Api.Controllers
{
    [Produces("application/json")]
    [AllowAnonymous]
    [Route("api/user-lists")]
    public class UserListController : Controller
    {
        public UserListController(IFuturesUserRepository futuresUser)
        {
            this.FuturesUser = futuresUser;
        }

        private IFuturesUserRepository FuturesUser { get; }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this.FuturesUser.GetAll();

            return this.Ok(users);
        }
    }
}
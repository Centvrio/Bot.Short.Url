using System;
using System.Threading.Tasks;
using Centvrio.Bot.Short.Url.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Centvrio.Bot.Short.Url.Controllers
{
    [Produces("application/json")]
    [Route("bots")]
    [ApiController]
    public class BotsController : Controller
    {
        private readonly Bot bot;
        private readonly CommandExecutor executor;
        private readonly ILogger<BotsController> logger;

        public BotsController(Bot bot, CommandExecutor executor, ILogger<BotsController> logger)
        {
            this.bot = bot;
            this.executor = executor;
            this.logger = logger;
        }


        [HttpPost("shortener/update")]
        public async Task<IActionResult> Update([FromBody]Update update)
        {
            try
            {
                await executor.Execute(bot.Client, update);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Internal exception", null);
            }
            return Ok();
        }
    }
}
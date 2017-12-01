using Centvrio.Bot.Short.Url.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Centvrio.Bot.Short.Url.Extensions
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseTelegramBot(this IApplicationBuilder app)
        {
            Bot bot = app.ApplicationServices.GetService<Bot>();
            bot.Init();
            return app;
        }
    }
}

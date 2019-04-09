using Microsoft.Extensions.DependencyInjection;
using SimpleCalendar.Abstracts.IBootstrap;
using SimpleCalendar.Abstracts.Interface;
using SimpleCalendar.Core.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCalendar.Core.Bootstrap
{
    public class CorePartialStartup : IPartialStartup
    {
        /// <summary>
        /// Core services registration
        /// </summary>
        /// <param name="services"></param>
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICalendarService, CalendarService>();
            services.AddSingleton<IOpenWeatherService, OpenWeatherService>();
        }
    }
}

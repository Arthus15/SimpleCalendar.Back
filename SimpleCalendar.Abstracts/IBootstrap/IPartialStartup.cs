using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCalendar.Abstracts.IBootstrap
{
    /// <summary>
    /// Partial Startup to be registered with the service bootstrapping.
    /// </summary>
    public interface IPartialStartup
    {
        /// Registers the services of the library.
        /// </summary>
        /// <param name="services">Service collection to register the services into.</param>
        void RegisterServices(IServiceCollection services);
    }
}

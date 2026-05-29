using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using UserCom.Authentication;

namespace UserCom.NetCore.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUserCom(this IServiceCollection services, Action<UserComConfigurator> configuration = null)
        {
            services.AddSingleton<IUserComClient, UserComClient>();

            var cfg = new UserComConfigurator();

            configuration?.Invoke(cfg);

            cfg.ApplyConfiguration(services);
        }
    }

    public class UserComConfigurator
    {
        private readonly List<Action<IServiceCollection>> _configurations = new List<Action<IServiceCollection>>();

        public void UseToken(string appName, string token)
        {
            AddConfiguration(services =>
            {
                services.AddSingleton<UserComAuthenticator, TokenUserComAuthenticator>(x => new TokenUserComAuthenticator(appName, token));
            });
        }

        public void AddConfiguration(Action<IServiceCollection> configurationAction)
        {
            _configurations.Add(configurationAction);
        }

        public void ApplyConfiguration(IServiceCollection services)
        {
            foreach (var action in _configurations)
            {
                action(services);
            }
        }
    }
}

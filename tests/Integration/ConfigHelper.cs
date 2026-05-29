using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using UserCom;
using UserCom.Authentication;

namespace Integration
{
    public static class ConfigHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static UserComAuthenticator GetAuthenticator()
        {
            var config = GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);

            return new TokenUserComAuthenticator(config["account"], config["token"]);
        }
    }
}

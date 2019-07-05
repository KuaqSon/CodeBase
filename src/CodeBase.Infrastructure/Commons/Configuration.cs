using CodeBase.Core.Commons;
using System.Configuration;

namespace CodeBase.Infrastructure.Commons
{
    public class Configuration : IConfiguration
    {
        private string ValueOf(string key) => ConfigurationManager.AppSettings["key"];

        public string ApiUrl => ValueOf("ApiUrl");
    }
}

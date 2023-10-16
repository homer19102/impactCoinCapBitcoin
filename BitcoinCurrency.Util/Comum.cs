using Microsoft.Extensions.Configuration;

namespace BitcoinCurrency.Util
{
    public static class Comum
    {
        private static ConfigurationBuilder _builder = new ConfigurationBuilder();

        public static string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";

        private static readonly Dictionary<string, IConfigurationRoot> _configurationBuilder = new Dictionary<string, IConfigurationRoot>();

        public static string GetParameterByKey(string parameter)
        {
            try
            {
                return GetConfiguration("Parameters:" + parameter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetConfiguration(string key)
        {
            GerarConfiguration();
            return _configurationBuilder[_environment][key];
        }


        private static void GerarConfiguration()
        {
            try
            {
                if (_builder == null)
                {
                    _builder = new ConfigurationBuilder();
                }

                if (!_configurationBuilder.ContainsKey(_environment))
                {
                    _builder.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddJsonFile("appsettings." + _environment + ".json", optional: true);
                    _configurationBuilder.Add(_environment, _builder.Build());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using BitcoinCurrency.Data.IRepository;
using BitcoinCurrency.Data.Repository;
using BitcoinCurrency.Domain;
using BitcoinCurrency.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BitCoinCurrency
{
    class Program
    {
        static async Task Main(string[] args) 
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var bitCoinService = serviceProvider.GetService<IBitcoinService>();

            Console.WriteLine("Serviço iniciado com sucesso !\n");

            var serviceReturn = await bitCoinService.ExecuteBitcoinCurrency();

            if (serviceReturn != null)
            {
                Console.WriteLine($"Valor do bitcoin atualizado com sucesso na base de dados cotação atual em real: R${serviceReturn.currencyReal}\n");
                Console.WriteLine($"Valor em USD retornado pela coincap: US${serviceReturn.currencyUsd}\n");
                Console.WriteLine($"Valor utilizado para conversão do dólar em real: R${serviceReturn.valueUseToConvertUsdToReal}\n");
                Console.ReadLine();
            }
            else 
            {
                Console.WriteLine($"Erro ao executar rotina de bitcoin");
                Console.ReadLine();
            }

           

        }

        public static void ConfigureServices(IServiceCollection services) 
        {
            services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient("mongodb+srv://coincapCapProjectUser:q3RYXE3V8jW8kjYp@coincapproject.ndcqyfy.mongodb.net/ImpactaEngenhariaDados");
            });

            services.AddScoped<IBitcoinService, BitcoinService>()
                .AddScoped<IBitcoinCurrencyRepository, BitcoinCurrencyRepository>()
                .AddScoped(c => c.GetService<IMongoClient>().StartSession());

        }
    }

}

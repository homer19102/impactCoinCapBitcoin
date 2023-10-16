using BitcoinCurrency.Data.IRepository;
using BitcoinCurrency.Domain.Models;
using BitcoinCurrency.Util;
using MailKit.Security;
using MimeKit;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace BitcoinCurrency.Domain.Services
{
    public class BitcoinService : IBitcoinService
    {
        private readonly IBitcoinCurrencyRepository _bitcoinCurrencyRepository;

        public BitcoinService(IBitcoinCurrencyRepository bitcoinCurrencyRepository)
        {
            _bitcoinCurrencyRepository = bitcoinCurrencyRepository;
        }

        public async Task<CurrencyModelView> ExecuteBitcoinCurrency()
        {
            var response = await HttpRequest.HttpGet(Comum.GetParameterByKey("coinCap"), "bitcoin");

            var cotacaoDolar = await HttpRequest.HttpGet(Comum.GetParameterByKey("economia"), "USD-BRL");

            if (response.Item2 == HttpStatusCode.OK && cotacaoDolar.Item2 == HttpStatusCode.OK)
            {

                var coincap = JsonSerializer.Deserialize<CoincapRateModelView>(Utils.GetElementInStringJson(response.Item1, "data"));

                var real = JsonSerializer.Deserialize<CoinDolarRealModelView>(Utils.GetElementInStringJson(cotacaoDolar.Item1, "USDBRL"));

                var coincapUsdBitcoin = Math.Round(float.Parse(coincap.rateUsd, CultureInfo.InvariantCulture.NumberFormat), 2);
                var realValueToDolar = Math.Round(float.Parse(real.ask, CultureInfo.InvariantCulture.NumberFormat), 2);

                var valorConvertido = ConvertUsdToBrl(coincapUsdBitcoin, realValueToDolar);

                await _bitcoinCurrencyRepository.InsertAsync(Map(valorConvertido, coincapUsdBitcoin.ToString(), realValueToDolar.ToString()));

                if(float.Parse(valorConvertido) < float.Parse(Comum.GetParameterByKey("valorMinimoBitcoin")))
                    await EmailSender(valorConvertido, coincapUsdBitcoin.ToString(), realValueToDolar.ToString());

                return MapCurrency(valorConvertido, coincapUsdBitcoin.ToString(), realValueToDolar.ToString());
            }

            return null;

        }

        private async Task EmailSender(string valorConvertido, string coincapUsdBitcoin, string realValueToDolar) 
        {
            
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(Comum.GetParameterByKey("emailCoincap")));
            emailMessage.To.Add(MailboxAddress.Parse(Comum.GetParameterByKey("emaildestinatario")));
            emailMessage.Subject = $"Bitcoin abaixo do valor {SystemTime.Now.ToString("dd/MM/yyyy")}";

            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.TextBody = $"Bitcoin abaixo do valor de R$ 300.000,00 \n\nValor atual da cotação por conversão R$ {valorConvertido}\n\nValor coincap US$ {coincapUsdBitcoin}\n\nBase de cálculo R$ {realValueToDolar}";
            emailMessage.Body = emailBodyBuilder.ToMessageBody();
            
            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(Comum.GetParameterByKey("smtp"), 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(Comum.GetParameterByKey("emailCoincap"), Comum.GetParameterByKey("passwordGmail"));
            await smtp.SendAsync(emailMessage);
            smtp.Disconnect(true);
        }

        public string ConvertUsdToBrl(double coincapUsdBitcoin, double realValueToDolar)
        {
            var result = coincapUsdBitcoin * realValueToDolar;

            return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:#,###.##}", result);
        }

        private BitcoinCurrency.Data.Models.BitcoinCurrency Map(string valorReal, string valorUsd, string real)
        {
            return new BitcoinCurrency.Data.Models.BitcoinCurrency
            {
                currencyReal = valorReal,
                currencyUsd = valorUsd,
                realValueToUsd = real,
                updateDate = SystemTime.Now,
            };
        }

        private CurrencyModelView MapCurrency(string valorReal, string valorUsd, string real)
        {
            return new CurrencyModelView
            {
                currencyReal = valorReal,
                currencyUsd = valorUsd,
                valueUseToConvertUsdToReal = real
            };
        }
    }
}

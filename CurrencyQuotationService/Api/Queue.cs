using CurrencyQuotationService.Api.Model;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Configuration;

namespace CurrencyQuotationService.Api
{
    public class Queue
    {
        private readonly string API_HOST = ConfigurationManager.AppSettings.Get("API_HOST");
        private readonly RestClient client;
        private readonly ILogger logger;

        public Queue()
        {
            client = new RestClient($"{API_HOST}/api");
            logger = new Logger<Queue>().Log;
        }

        public async Task<EntryModel> GetEntry()
        {
            try
            {
                var request = new RestRequest("GetItemFila", RestSharp.Method.Get);
                var response = await client.ExecuteAsync<EntryModel>(request);

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogWarning($"{response.ResponseUri} = {response.StatusCode}", response.ErrorMessage);
                    return null;
                }
                logger.LogInformation($"{response.ResponseUri} = {response.StatusCode}", response.ErrorMessage);
                return response.Data;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);

                return null;
                throw;
            }
        }
    }
}

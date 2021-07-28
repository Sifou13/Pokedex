using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


//Infrastructure Code should in theory be moved to a different library project and tested in isolation, and often, when a  team grows
// it end up owned by the Leads/Seniors and/or an Architecture team since such a project is where most of the plumming and infrastructure 
// abstraction resides
namespace Pokedex.Services.Infrastructure
{
    public static class WebRequestHelper
    {
        private static HttpClient httpClient;

        public static async Task<T> GetResponse<T>(string url, JsonSerializerOptions jsonSerializerOptions = null) where T : class
        {
            HttpResponseMessage httpResponse = await GetHttpResponse(url);

            Stream result;

            try
            {
                result = await httpResponse.Content.ReadAsStreamAsync();

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return (T)null;
                                
                return await JsonSerializer.DeserializeAsync<T>(result, jsonSerializerOptions);
            }
            catch (Exception exception)
            {   
                //Rethrowing exception up the stack for better error handling and user experience on the front end and friendly responses on Apis
                throw new Exception($"There was an issue while getting a response from {url}: {exception.Message}");
            }
            finally
            {
                httpResponse.Dispose();
            }
        }

        private static async Task<HttpResponseMessage> GetHttpResponse(string url)
        {
            httpClient = httpClient ?? new HttpClient();
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            
            return httpResponse;
        }
    }
}

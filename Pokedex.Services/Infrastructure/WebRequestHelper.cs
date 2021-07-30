using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
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

        public static async Task<T> Get<T>(string url, Dictionary<string,string> queryStrings = null, JsonSerializerOptions jsonSerializerOptions = null) where T : class
        {
            HttpResponseMessage httpResponse = await GetHttpResponse(url, queryStrings);

            Stream result;

            try
            {
                result = await httpResponse.Content.ReadAsStreamAsync();

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return (T)null;
                                
                return await JsonSerializer.DeserializeAsync<T>(result, jsonSerializerOptions);
            }
            catch
            {
                //Rethrowing exception up the stack for without better error handling, troubleshooting (stack is preserved when using throw alone) and user experience on the front end and friendly responses on Apis
                throw;
            }
            finally
            {
                httpResponse.Dispose();
            }
        }

        private static async Task<HttpResponseMessage> GetHttpResponse(string url, Dictionary<string, string> queryStrings)
        {
            httpClient ??= new HttpClient();

            string sanitizedQueryString = GenerateQueryString(queryStrings);

            string requestUrl = $"{url}{sanitizedQueryString}";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUrl);

            return httpResponse;
        }

        private static string GenerateQueryString(Dictionary<string, string> queryStrings)
        {
            if (queryStrings == null || !queryStrings.Any())
                return string.Empty;
            
            StringBuilder queryStringBuilder = new StringBuilder("?");

            foreach(KeyValuePair<string, string> queryString in queryStrings)
            {
                queryStringBuilder.Append(queryString.Key);
                queryStringBuilder.Append("=");
                queryStringBuilder.Append(SanitizeQueryString(queryString.Value));
                queryStringBuilder.Append("&");
            }

            return queryStringBuilder.ToString();
        }

        private static string SanitizeQueryString(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                return string.Empty;

            string flattenedQueryString = queryString.Replace("\n", " ").Replace("\f", " ").Replace("\r", " ");

            return $"{Uri.EscapeUriString(flattenedQueryString)}";
        }
    }
}

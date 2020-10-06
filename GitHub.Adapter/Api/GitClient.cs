using Core.Logger;
using Core.Models;
using GH_Adapter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace GitHub.Adapter.Api
{
    public class GitClient
    {
        public async Task<GitHubResponse> GetData(string query)
        {
            try
            { 
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.github.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("product", "1"));

                    var result = await GetDataResponse(client, query);

                    return result;
                }
            } 
            catch (Exception ex)
            {
                Log.Write(ex.Message, "", "", "GetData");
                return null;
            }
        }

        private async Task<GitHubResponse> GetDataResponse(HttpClient client, string query)
        {
            try
            {
                var response = await client.GetAsync($"search/repositories?q={query}");

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<GitHubResponse>(data);

                    if (result != null && result.items != null && result.items.Length > 0)
                        result.ResponseType = Enums.ResponseType.Success;
                    else
                        result.ResponseType = Enums.ResponseType.NoResult;

                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, "", "", "GetDataResponse");

                return new GitHubResponse()
                {
                    ResponseType = Enums.ResponseType.ServerError
                };
            }
        }
    }
}
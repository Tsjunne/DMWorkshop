using DMWorkshop.DTO.Login;
using DMWorkshop.Model.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Login
{
    public class FacebookService
    {
        private readonly HttpClient _httpClient;

        public FacebookService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://graph.facebook.com/v2.8/")
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<ExternalUser> GetUserFromFacebook(string facebookToken, CancellationToken cancellationToken)
        {
            var result = await Get<dynamic>(facebookToken, "me", "fields=first_name,last_name,email,picture.width(100).height(100)", cancellationToken);
            if (result == null)
            {
                throw new Exception("User from this token not exist");
            }

            var account = new ExternalUser()
            {
                Email = result.email,
                FirstName = result.first_name,
                LastName = result.last_name,
                Picture = result.picture.data.url
            };

            return account;
        }

        private async Task<T> Get<T>(string accessToken, string endpoint, string args = null, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}", cancellationToken);
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}

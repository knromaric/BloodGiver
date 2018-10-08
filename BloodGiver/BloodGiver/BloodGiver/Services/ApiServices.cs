using BloodGiver.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BloodGiver.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "applicaton/json");
            var response = await httpClient.PostAsync("bloodgiver.gear.host/api/Account/Register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            var keyvalues =new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username", email), 
                new KeyValuePair<string, string>("password", password), 
                new KeyValuePair<string, string>("grant_type", password), 
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "bloodgiver.gear.host/Token")
            {
                Content = new FormUrlEncodedContent(keyvalues)
            };
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);
            var content = response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<List<BloodUser>> FindBloodGivers(string country, string bloodtype)
        {
            var bloodApiUrl = "bloodgiver.gear.host/api/BloodUsers";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "we will it later");
            var json = await httpClient.GetStringAsync($"{bloodApiUrl}?bloodGroup={bloodtype}&country={country}");
            return JsonConvert.DeserializeObject<List<BloodUser>>(json);
        }

        public async Task<List<BloodUser>> LatestBloodGivers()
        {
            var bloodApiUrl = "bloodgiver.gear.host/api/BloodUsers";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "we will it later");
            var json = await httpClient.GetStringAsync(bloodApiUrl);
            return JsonConvert.DeserializeObject<List<BloodUser>>(json);
        }

        public async Task<bool> RegisterGiver(BloodUser bloodUser)
        {
            var bloodApiUrl = "bloodgiver.gear.host/api/BloodUsers";
            var json = JsonConvert.SerializeObject(bloodUser);        
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "applicaton/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "we will it later");
            var response = await httpClient.PostAsync(bloodApiUrl, content);
            return response.IsSuccessStatusCode;
        }

    }
}

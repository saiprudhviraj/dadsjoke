using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dadsjoke.Services
{
    public class JokeService
    {
        private readonly HttpClient _httpClient;

        public JokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRandomJoke()
        {
            var response = await _httpClient.GetAsync("https://icanhazdadjoke.com/");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<JokeData>(json);

            return data.joke;
        }

        public async Task<int> GetJokeCount()
        {
            var response = await _httpClient.GetAsync("https://icanhazdadjoke.com/");
            response.EnsureSuccessStatusCode();

            var headers = response.Headers;
            if (headers.Contains("X-Total-Count"))
            {
                return int.Parse(headers.GetValues("X-Total-Count").FirstOrDefault());
            }
            else
            {
                throw new Exception("X-Total-Count header not found.");
            }
        }
    }

    public class JokeData
    {
        public string id { get; set; }
        public string joke { get; set; }
        public List<string> categories { get; set; }
    }
}

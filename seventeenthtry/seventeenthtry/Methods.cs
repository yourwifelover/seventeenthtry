using APIKEY;
using Microsoft.Extensions.Configuration;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace neCURSACH.Services
{
    public class MovieTMDB
    {

        private readonly HttpClient client = new HttpClient();
        private readonly API api = new API();
        public string apiKey;
        public MovieTMDB()
        {
            apiKey = api.apiKey;
        }

        public async Task<MainOne> PopularMovies(int page)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/popular?api_key={apiKey}&page={page}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MainOne>(content);
        }
        public async Task<Details> GetDetails(int movieId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key={apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Details>(content);
        }

        public async Task<MainOne> FindMovie(string name)
        {

            var response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={name}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MainOne>(content);
        }
    }
}

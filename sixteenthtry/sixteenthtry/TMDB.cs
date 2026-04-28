using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sixteenthtry;
namespace MASHINA
{
    public class MainOne
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class Result
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public int[] genre_ids { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }
    public class Details
    {
        public int id { get; set; }
        public string title { get; set; }
        public int runtime { get; set; }
        public int budget { get; set; }
        public string overview { get; set; }
    }

    public class MovieTMDB
    {

        private readonly HttpClient client = new HttpClient();
        private readonly APIKEY api = new APIKEY();
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

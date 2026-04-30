using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
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
        public Details(int id, string title, int runtime, int budget, string overview)
        {
            this.id = id;
            this.title = title;
            this.runtime = runtime;
            this.budget = budget;
            this.overview = overview;
        }
        public int id { get; set; }
        public string title { get; set; }
        public int runtime { get; set; }
        public int budget { get; set; }
        public string overview { get; set; }
    }


}

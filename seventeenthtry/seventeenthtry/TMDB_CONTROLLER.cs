using Microsoft.AspNetCore.Mvc;
using neCURSACH.Services;
using Model;
using Memory;
using System.Threading.Tasks;

namespace neCURSACH.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TMDB_ : ControllerBase
    {
        private readonly ILogger<TMDB_> _logger;
        private MovieTMDB service = new MovieTMDB();
        Storage storage = new Storage();

        public TMDB_(ILogger<TMDB_> logger)
        {
            _logger = logger;
        }

        [HttpGet("PopularMovies")]
        public async Task<ActionResult<MainOne>> PopularMovies(int page)
        {
            try
            {
                var result = await service.PopularMovies(page);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error, something wrong");
            }
        }

        [HttpGet("Details")]
        public async Task<ActionResult<Details>> GetDetails(int movieId)
        {
            try
            {
                var result = await service.GetDetails(movieId);
                storage.Save(result);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error, something wrong");
            }
        }

        [HttpGet("Search")]
        public async Task<ActionResult<MainOne>> SearchMovies(string query)
        {
            try
            {
                var result = await service.FindMovie(query);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error, something wrong");
            }
        }
        // POST, PUT, DELETE
        [HttpPost("Favorite")]
        public async Task<ActionResult> Favorite(int movieid)
        {

            try
            {
                var result = await service.GetDetails(movieid);
                storage.Save(result);
                return StatusCode(201, "Movie added to favorites successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error, something wrong");
            }
        }


        [HttpDelete("Delete")]
        public ActionResult DeleteMovie(int id)
        {
            try
            {
                storage.Delete(id);
                return StatusCode(204, "Movie deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error, something wrong");
            }
        }
        [HttpPut("Update")]
        public ActionResult UpdateMovie(int id, Details updatedMovie)
        {
            try
            {
                storage.Update(id, updatedMovie);
                return StatusCode(204, "Movie updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error, something wrong");
            }
        }
    }
}
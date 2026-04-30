using Newtonsoft.Json;
using Model;
namespace Memory;

public class Storage
{
    string path = "movies.json";
    public Storage() { }
    public List<Details> GetAll()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var movies = JsonConvert.DeserializeObject<List<Details>>(json);
            if (movies == null)
                movies = new List<Details>();
            return movies;
        }
        else
        {
            var movies = new List<Details>();
            return movies;
        }
    }
    public void Save(Details movie)
    {
        var movies = GetAll();
        movies.Add(movie);
        string json = JsonConvert.SerializeObject(movies);
        File.WriteAllText(path, json);
        //foreach (var m in movies)
        //{
        //    Console.WriteLine($"ID: {m.id}, Title: {m.title}, Runtime: {m.runtime}, Budget: {m.budget}, Overview: {m.overview}");
        //}
    }
    public void Delete(int id)
    {
        var movies = GetAll();
        var movieToDelete = movies.FirstOrDefault(m => m.id == id);
        if (movieToDelete != null)
        {
            movies.Remove(movieToDelete);
            string json = JsonConvert.SerializeObject(movies);
            File.WriteAllText(path, json);
        }
    }
    public void Update(int id, Details updatedMovie)
    {
        var movies = GetAll();
        var movieToUpdate = movies.FirstOrDefault(m => m.id == id);

        if (movieToUpdate != null)
        {
            movieToUpdate.title = updatedMovie.title;
            movieToUpdate.runtime = updatedMovie.runtime;
            movieToUpdate.budget = updatedMovie.budget;
            movieToUpdate.overview = updatedMovie.overview;
            string json = JsonConvert.SerializeObject(movies);
            File.WriteAllText(path, json);
        }
    }



}

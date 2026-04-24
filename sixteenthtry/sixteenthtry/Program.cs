using Newtonsoft.Json;
using sixteenthtry;

Console.OutputEncoding = System.Text.Encoding.UTF8;



var movie = new MovieTMDB();
//Top

while (true)
{
    try
    {
        Console.WriteLine("What do you want to see?");
        Console.WriteLine("popular | search | details | exit");
        var choice = Console.ReadLine();

        if (choice == "popular")
        {
            Console.WriteLine("How many pages do you want to see?");
            var pages = int.Parse(Console.ReadLine());
            for (int i = 1; i <= pages; i++)
            {
                var result = await movie.PopularMovies(i);
                foreach (var item in result.results)
                {
                    Console.WriteLine($"ID: {item.id}, Title: {item.title}");
                }
            }
        }

        else if (choice == "details")
        {
            Console.WriteLine("Enter the movie ID:");
            var movieId = int.Parse(Console.ReadLine());
            var result = await movie.GetDetails(movieId);
            Console.WriteLine($"ID: {result.id}, Title: {result.title}, Runtime: {result.runtime}, Budget: {result.budget}, Overview: {result.overview}");
        }
        else if (choice == "search")
        {
            Console.WriteLine("Enter the movie name:");
            var name = Console.ReadLine();
            var result = await movie.FindMovie(name);
            result.results = result.results.Where(x => x.vote_average > 7).OrderByDescending(x => x.vote_average).ToArray();
            foreach (var item in result.results)
                Console.WriteLine($"ID: {item.id}, Title: {item.original_title}");
        }
        else if (choice == "exit")
        {
            Console.WriteLine("Exiting the program.");
            break;
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
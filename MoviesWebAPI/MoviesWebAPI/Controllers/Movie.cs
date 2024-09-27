using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace MoviesWebAPI.Controllers
{
    //this is supposed to be first, probably.
    //this is standardizing the url?
    //it will only have the 1st word of the class and will be lowercase.
    //(we will need to add other mappings for each method)
    //add "/api/movie" to the local host address
    [Route("api/[controller]")]
    //adding this in will add the using statement
    //we also need to add in the inheritance from controllerbase
    //Movie is going to get extra methods and such from controller
    [ApiController]
    public class Movie : ControllerBase
    {
        static List<string> Movies = new List<string> { "The Shawshank Redemption", "Cloud Atlas", "Hot Fuzz", };

        //CRUDs
        [HttpGet] //this will make a distinct url for this function
        public List<string> GetAllMovies()
        {
            return Movies;
        }

        [HttpGet("{idx}")]
        public string GetMovieByIdx(int idx)
        {
            if (!ValidIndex(idx)) { return "No movie at that index position. try again."; }
            else
            {
                return Movies[idx];
            }
        }

        [HttpPost]
        public string AddMovie(string movie)
        {
            Movies.Add(movie);
            return "Movie added";
        }

        [HttpDelete("{idx}")]
        public string RemoveMovie(int idx)
        {
            Movies.RemoveAt(idx);
            return "Movie removed.";
        }

        [HttpPut]
        public string UpdateMovie(int idx, string newTitle)
        {
            if (!ValidIndex(idx)) { return "No movie at that index position. try again."; }
            else {
                Movies[idx] = newTitle;
                return "Movie updated.";
            }
        }

        private bool ValidIndex(int idx)
        {
            bool isValid = true;
            if (idx < 0 || idx >= Movies.Count)
            {
                isValid = false;
            }
            return isValid;
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebRepositoriesSearch.Models;

namespace WebRepositoriesSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    { 
        private readonly HttpClient client;        

        public RepositoriesController()
        {
            client = new HttpClient();            
        }      
                
        // GET: api/Repositories/the-rep/2        
        [HttpGet("{repositoryName}/{pageNumber}", Name = "Get")]
        public async Task<IActionResult> Get(string repositoryName, int? pageNumber)
        {
            string url = $"https://api.github.com/search/repositories?q={repositoryName}";

            if (pageNumber.HasValue)
            {
                url = $"https://api.github.com/search/repositories?q={repositoryName}&page={pageNumber}";
            }            

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders
              .Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "oksanab");

            var stringTask = client.GetStringAsync(url);
            var repositories = await stringTask;

            return Ok(repositories);
        }

        // POST: api/Repositories - save bookmarks to Session
        [HttpPost]
        public IActionResult Post([FromBody] Repository repository)
        {
            const string RepBookmarksSession = "RepBookmarks";

            var bookmarks = HttpContext.Session.GetObjectFromJson<Repositories>(RepBookmarksSession);
            if (bookmarks == null)
            {
                bookmarks = new Repositories();
            }

            bookmarks.AddBookmark(repository);

            HttpContext.Session.SetObjectAsJson(RepBookmarksSession, bookmarks);

            var updatedBookmarks = HttpContext.Session.GetObjectFromJson<Repositories>(RepBookmarksSession);
            
            return Ok(updatedBookmarks);
        }
    }
}

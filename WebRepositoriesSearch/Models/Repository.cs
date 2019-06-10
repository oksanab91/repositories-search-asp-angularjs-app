using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace WebRepositoriesSearch.Models
{
    public class Repository
    {
        public Repository()
        {
        }
       
        public long ID { get; set; }
        public string RepositoryName { get; set; }
        public string RepositoryLink { get; set; }
        public string OwnerAvatar { get; set; }
    }

    public class Repositories
    {
        public IList<Repository> Bookmarks { get; set; } = new List<Repository>();

        public void AddBookmark(Repository repository)
        {
            var existingRepository = Bookmarks.FirstOrDefault(e => e.ID == repository.ID);
            if (existingRepository == null)
            {                
                Bookmarks.Add(repository);
            }
        }
        
    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }    
}

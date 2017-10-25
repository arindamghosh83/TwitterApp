using System.Collections.Generic;
using System.Threading.Tasks;
using MyTwitterApp.Models;

namespace MyTwitterApp.Repository
{
    /// <summary>
    /// Repository Interface to retrieve access tokens and tweets from Twitter API
    /// </summary>
    public interface ITwitterRepo
    {
        Task<string> GetAccessToken();
        Task<List<RootObject>> GetTweets();
    }
}
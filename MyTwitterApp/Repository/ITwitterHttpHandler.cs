using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MyTwitterApp.Models;

namespace MyTwitterApp.Repository
{
    public interface ITwitterHttpHandler
    {
 
        Task<string> GetAccessTokenAsync(string url);
        Task<List<RootObject>> GetTweetsAsync(string accessToken, string url);
    }
}
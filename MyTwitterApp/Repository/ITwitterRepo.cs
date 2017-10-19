using System.Collections.Generic;
using System.Threading.Tasks;
using MyTwitterApp.Models;

namespace MyTwitterApp.Repository
{
    public interface ITwitterRepo
    {
        Task<string> GetAccessToken();
        Task<List<RootObject>> GetTwitts();
    }
}
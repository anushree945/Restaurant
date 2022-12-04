using Restaurant.Models;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services
{
    public class BaseService : IBaseService
    {
        public Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            throw new NotImplementedException();
        }
    }
}

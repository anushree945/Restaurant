using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Restaurant.Models;

namespace Restaurant.Services.Interfaces
{
    public interface IBaseService
    {
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}

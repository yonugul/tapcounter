using System.Threading.Tasks;

namespace MyCounter.Core.AppContext
{
    public interface IAppSettings
    {
        Task<TResult> GetValue<TResult>(string key) where TResult : struct;
        Task SaveValue<TResult>(string key, TResult value) where TResult : struct;
    }
}
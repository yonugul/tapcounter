using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyCounter.Core.AppContext
{
    public class AppSettings : IAppSettings
    {
        public Task<TResult> GetValue<TResult>(string key) where TResult : struct
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                var result = (TResult)Application.Current.Properties[key];
                return Task.FromResult(result);
            }

            return null;
        }
        public async Task SaveValue<TResult>(string key, TResult value) where TResult : struct
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                Application.Current.Properties[key] = value;
            }
            else
            {
                Application.Current.Properties.Add(key, value);
            }
            await Application.Current.SavePropertiesAsync();
        }
    }
}

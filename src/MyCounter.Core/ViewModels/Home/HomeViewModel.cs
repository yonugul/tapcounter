using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MyCounter.Core.AppContext;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyCounter.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private const string TapCountKey = "my.tapcount";
        private const string TargetCountKey = "my.targetcount";
        private readonly IAppSettings _appSettings;
        public HomeViewModel(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public MvxNotifyTask LoadInitialDataTask { get; private set; }
        public override Task Initialize()
        {
            LoadInitialDataTask = MvxNotifyTask.Create(LoadInitialData);

            return Task.FromResult(0);
        }
        private async Task LoadInitialData()
        {
            var tapCount = await GetTapCount();
            var targetCount = await GetTargetCount();
            Remaining = targetCount - tapCount;
            Target = targetCount;
            TapCount = tapCount;
            Application.Current.MainPage.dis
        }
        private int _tapCount;
        public int TapCount
        {
            get => _tapCount;
            set
            {
                SetProperty(ref _tapCount, value);
                Remaining = Target - value;
            }
        }
        private int _target;
        public int Target
        {
            get => _target;
            set
            {
                SetProperty(ref _target, value);
                Remaining = value - TapCount;
            }
        }
        private int _remaining;
        public int Remaining
        {
            get => _remaining;
            set => SetProperty(ref _remaining, value);
        }
        public IMvxCommand AddTapCommand => new MvxAsyncCommand(async () => await AddTapCount());
        public IMvxCommand RemoveTapCommand => new MvxAsyncCommand(async () => await RemoveTapCount());
        public IMvxCommand ResetCommand => new MvxAsyncCommand(async () => await ResetTapCount());

        private async Task AddTapCount()
        {
            if (TapCount >= Target)
            {
                await Application.Current.MainPage.DisplayAlert("Hedefe ulaşıldı", $"Tebrikler hedefiniz olan {Target} sayısına ulaştınız .", "Tamam");
            }
            else
            {
                TapCount++;
                await SaveTapCount();
                await SaveTargetCount();
            }

        }
        private async Task RemoveTapCount()
        {
            if (TapCount <= 0)
            {
                await Task.FromResult(0);
            }
            else
            {
                TapCount--;
                await SaveTapCount();
                await SaveTargetCount();
            }
           
        }
        private async Task ResetTapCount()
        {
            TapCount = 0;
            await SaveTapCount();
            await SaveTargetCount();
        }
        private async Task SaveTapCount()
        {
            await _appSettings.SaveValue(TapCountKey, TapCount);
        }

        private async Task<int> GetTapCount()
        {
            var res = await _appSettings.GetValue<int>(TapCountKey);
            return res;
        }
        private async Task SaveTargetCount()
        {
            await _appSettings.SaveValue(TargetCountKey, Target);
        }

        private async Task<int> GetTargetCount()
        {
            var res = await _appSettings.GetValue<int>(TargetCountKey);
            return res;
        }


    }
}

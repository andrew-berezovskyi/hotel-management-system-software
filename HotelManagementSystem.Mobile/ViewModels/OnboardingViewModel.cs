using System.Windows.Input;

namespace HotelMobileApp.ViewModels;

public class OnboardingViewModel : BaseViewModel
{
    public ICommand GetStartedCommand { get; }
    public ICommand SignInCommand { get; }

    public OnboardingViewModel()
    {
        GetStartedCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//Register"));

        SignInCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//SignIn"));
    }
}

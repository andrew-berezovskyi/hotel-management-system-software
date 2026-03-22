using System.Windows.Input;

namespace HotelMobileApp.ViewModels;

public class AuthViewModel : BaseViewModel
{
    string email = string.Empty;
    string password = string.Empty;

    public string Email
    {
        get => email;
        set => SetProperty(ref email, value);
    }

    public string Password
    {
        get => password;
        set => SetProperty(ref password, value);
    }

    public ICommand SignInCommand { get; }
    public ICommand RegisterCommand { get; }

    public AuthViewModel()
    {
        SignInCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//Home"));

        RegisterCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//Home"));
    }
}

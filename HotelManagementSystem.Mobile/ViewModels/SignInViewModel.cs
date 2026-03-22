using System.Text.RegularExpressions;
using System.Windows.Input;

namespace HotelMobileApp.ViewModels;

public class SignInViewModel : BaseViewModel
{
    private string _name;
    private string _email;
    private string _password;
    private bool _rememberMe;
    private bool _hasError;
    private string _errorMessage;
    private bool _isBusy;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public bool RememberMe
    {
        get => _rememberMe;
        set => SetProperty(ref _rememberMe, value);
    }

    public bool HasError
    {
        get => _hasError;
        set => SetProperty(ref _hasError, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public ICommand GoogleCommand { get; }
    public ICommand FacebookCommand { get; }
    public ICommand AppleCommand { get; }
    public ICommand GoToRegisterCommand { get; }
    public ICommand ForgotPasswordCommand { get; }

    public SignInViewModel()
    {
        GoToRegisterCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("//Register");
        });

        ForgotPasswordCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Forgot password",
                "Password recovery flow will be implemented later.",
                "OK");
        });

        GoogleCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Google",
                "Google sign-in will be here later.",
                "OK");
        });

        FacebookCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Facebook",
                "Facebook sign-in will be here later.",
                "OK");
        });

        AppleCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Apple",
                "Apple sign-in will be here later.",
                "OK");
        });
    }

    public async Task TryLoginAsync()
    {
        if (IsBusy) return;

        IsBusy = true;
        HasError = false;
        ErrorMessage = string.Empty;

        var error = Validate();
        if (error != null)
        {
            HasError = true;
            ErrorMessage = error;
            IsBusy = false;
            return;
        }

        // тут буде реальний логін (API/БД)
        await Task.Delay(600);

        IsBusy = false;

        await Shell.Current.DisplayAlert("Welcome",
            "You have successfully signed in.",
            "OK");

        // після логіну – наприклад, на Home
        await Shell.Current.GoToAsync("//Home");
    }

    private string Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return "Please enter your name.";

        if (string.IsNullOrWhiteSpace(Email))
            return "Please enter your email.";

        if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return "Please enter a valid email address.";

        if (string.IsNullOrWhiteSpace(Password))
            return "Please enter your password.";

        return null;
    }
}


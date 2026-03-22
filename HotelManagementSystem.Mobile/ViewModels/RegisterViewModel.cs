using System.Text.RegularExpressions;
using System.Windows.Input;

namespace HotelMobileApp.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    private string _name;
    private string _phone;
    private string _email;
    private string _password;
    private string _confirmPassword;
    private string _errorMessage;
    private bool _hasError;
    private bool _isBusy;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
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

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool HasError
    {
        get => _hasError;
        set => SetProperty(ref _hasError, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public ICommand GoToSignInCommand { get; }
    public ICommand GoogleCommand { get; }
    public ICommand FacebookCommand { get; }
    public ICommand AppleCommand { get; }

    public RegisterViewModel()
    {
        GoToSignInCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("//SignIn");
        });

        GoogleCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Google", "Google sign-in will be here later.", "OK");
        });

        FacebookCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Facebook", "Facebook sign-in will be here later.", "OK");
        });

        AppleCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Apple", "Apple sign-in will be here later.", "OK");
        });
    }

    public async Task TryRegisterAsync()
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

        // Тут буде справжній виклик API / збереження в БД
        await Task.Delay(600); // імітація запиту

        IsBusy = false;

        await Shell.Current.DisplayAlert("Success",
            "Your account has been created.",
            "OK");

        // після успішної реєстрації кидаємо, наприклад, на Home або SignIn
        await Shell.Current.GoToAsync("//SignIn");
    }

    private string Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return "Please enter your name.";

        if (string.IsNullOrWhiteSpace(Phone))
            return "Please enter your phone number.";

        if (string.IsNullOrWhiteSpace(Email))
            return "Please enter your email.";

        if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return "Please enter a valid email address.";

        if (string.IsNullOrWhiteSpace(Password))
            return "Please enter a password.";

        if (Password.Length < 6)
            return "Password should be at least 6 characters.";

        if (Password != ConfirmPassword)
            return "Passwords do not match.";

        return null;
    }
}

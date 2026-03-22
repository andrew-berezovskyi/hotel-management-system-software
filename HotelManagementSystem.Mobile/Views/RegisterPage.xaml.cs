using HotelMobileApp.ViewModels;

namespace HotelMobileApp.Views;

public partial class RegisterPage : ContentPage
{
    private readonly RegisterViewModel _vm;

    public RegisterPage()
    {
        InitializeComponent();
        _vm = new RegisterViewModel();
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // стартові стани (FormContainer уже має Opacity/TranslationY в XAML)
        ErrorLabel.Opacity = _vm.HasError ? 1 : 0;

        // м’яка анімація появи форми
        await FormContainer.FadeTo(1, 300, Easing.SinOut);
        await FormContainer.TranslateTo(0, 0, 300, Easing.SinOut);
    }

    private async void OnCreateTapped(object sender, TappedEventArgs e)
    {
        // невелика анімація "натискання"
        await CreateButtonFrame.ScaleTo(0.97, 80);
        await CreateButtonFrame.ScaleTo(1.0, 80);

        await _vm.TryRegisterAsync();

        // оновлюємо прозорість повідомлення про помилку
        await ErrorLabel.FadeTo(_vm.HasError ? 1 : 0, 200);
    }
}

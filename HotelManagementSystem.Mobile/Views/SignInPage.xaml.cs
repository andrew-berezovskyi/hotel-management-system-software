using HotelMobileApp.ViewModels;

namespace HotelMobileApp.Views;

public partial class SignInPage : ContentPage
{
    private readonly SignInViewModel _vm;

    public SignInPage()
    {
        InitializeComponent();
        _vm = new SignInViewModel();
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Початкові стани
        ErrorLabel.Opacity = _vm.HasError ? 1 : 0;
        FormContainer.Opacity = 0;
        FormContainer.TranslationY = 40;

        // Анімація м’якої появи
        await FormContainer.FadeTo(1, 300, Easing.SinOut);
        await FormContainer.TranslateTo(0, 0, 300, Easing.SinOut);
    }

    private async void OnLoginTapped(object sender, TappedEventArgs e)
    {
        // невелике "натискання" на кнопку
        await LoginButtonFrame.ScaleTo(0.97, 80);
        await LoginButtonFrame.ScaleTo(1.0, 80);

        await _vm.TryLoginAsync();

        await ErrorLabel.FadeTo(_vm.HasError ? 1 : 0, 200);
    }
}

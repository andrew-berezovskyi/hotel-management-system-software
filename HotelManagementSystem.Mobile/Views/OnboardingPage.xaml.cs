using HotelMobileApp.ViewModels;

namespace HotelMobileApp.Views;

public partial class OnboardingPage : ContentPage
{
    private readonly OnboardingViewModel _vm;

    // Для свайпу
    private double _thumbStartX;
    private double _maxThumbTranslation;
    private bool _layoutReady;

    public OnboardingPage()
    {
        InitializeComponent();
        _vm = new OnboardingViewModel();
        BindingContext = _vm;

        ButtonContainer.SizeChanged += OnButtonContainerSizeChanged;
    }

    private void OnButtonContainerSizeChanged(object? sender, EventArgs e)
    {
        // Коли відомі реальні розміри – рахуємо, доки можна тягнути кружечок
        if (ButtonFrame.Width <= 0 || Thumb.Width <= 0)
            return;

        // 20 лівий паддінг + ~20 запас біля тексту
        _maxThumbTranslation = Math.Max(0, ButtonFrame.Width - Thumb.Width - 40);
        _layoutReady = true;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        TitleLabel.Opacity = 0;
        ButtonContainer.Opacity = 0;
        ButtonContainer.TranslationY = 80;
        Thumb.TranslationX = 0;

        await TitleLabel.FadeTo(1, 600, Easing.SinOut);

        await Task.WhenAll(
            ButtonContainer.FadeTo(1, 400, Easing.SinOut),
            ButtonContainer.TranslateTo(0, 0, 400, Easing.SinOut)
        );
    }

    // Простий тап (на випадок, якщо людині лінь тягнути)
    private void OnGetStartedTapped(object sender, TappedEventArgs e)
    {
        NavigateNext();
    }

    // Свайп по колу
    private void OnThumbPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (!_layoutReady)
            return;

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _thumbStartX = Thumb.TranslationX;
                break;

            case GestureStatus.Running:
                var newX = _thumbStartX + e.TotalX;
                newX = Math.Max(0, Math.Min(_maxThumbTranslation, newX));
                Thumb.TranslationX = newX;
                break;

            case GestureStatus.Canceled:
            case GestureStatus.Completed:
                HandleThumbReleased();
                break;
        }
    }

    private async void HandleThumbReleased()
    {
        if (_maxThumbTranslation > 0 &&
            Thumb.TranslationX >= _maxThumbTranslation * 0.7)
        {
            // трошки дотягуємо до кінця для краси
            await Thumb.TranslateTo(_maxThumbTranslation, 0, 120, Easing.SinOut);
            NavigateNext();
        }
        else
        {
            // повертаємо назад
            await Thumb.TranslateTo(0, 0, 180, Easing.SinOut);
        }
    }

    private void NavigateNext()
    {
        if (_vm.GetStartedCommand.CanExecute(null))
            _vm.GetStartedCommand.Execute(null);
    }
}

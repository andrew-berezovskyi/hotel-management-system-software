using System.Windows.Input;

namespace HotelMobileApp.ViewModels;

[QueryProperty(nameof(Amount), "amount")]
public class PaymentViewModel : BaseViewModel
{
    private decimal amount;

    public decimal Amount
    {
        get => amount;
        set => SetProperty(ref amount, value);
    }

    public ICommand PayCommand { get; }

    public PaymentViewModel()
    {
        PayCommand = new Command(async () =>
        {
            // тут буде реальна оплата, поки просто success
            await Shell.Current.GoToAsync("//PaymentSuccess");
        });
    }
}

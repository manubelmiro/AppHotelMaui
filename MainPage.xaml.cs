namespace AppHotel;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnBotaoSobreClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SobrePage());
    }

    private async void OnBotaoCalcularClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CalculoDiariaPage());
    }
}
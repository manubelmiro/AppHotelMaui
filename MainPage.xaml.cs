namespace AppHotel;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnBotaoSobreClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SobrePage());
    }
}
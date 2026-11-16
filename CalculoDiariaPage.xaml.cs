
using AppHotel.Models;

namespace AppHotel;

public partial class CalculoDiariaPage : ContentPage
{
    List<Quarto> listaQuartos = new List<Quarto>
    {
        new Quarto { Descricao = "Suíte Super Luxo", ValorDiariaAdulto = 250.0, ValorDiariaCrianca = 125.0 },
        new Quarto { Descricao = "Suíte Luxo", ValorDiariaAdulto = 180.0, ValorDiariaCrianca = 90.0 },
        new Quarto { Descricao = "Suíte Single", ValorDiariaAdulto = 110.0, ValorDiariaCrianca = 55.0 },
        new Quarto { Descricao = "Suíte Crise", ValorDiariaAdulto = 65.0, ValorDiariaCrianca = 32.5 }
    };

    public CalculoDiariaPage()
    {
        InitializeComponent();

        pck_quarto.ItemsSource = listaQuartos;

        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkin.MaximumDate = DateTime.Today.AddMonths(1);
        dtpck_checkin.Date = DateTime.Today;

        dtpck_checkout.MinimumDate = DateTime.Today.AddDays(1);
        dtpck_checkout.MaximumDate = DateTime.Today.AddMonths(6);
        dtpck_checkout.Date = DateTime.Today.AddDays(1);
    }

    private void dtpck_checkin_DateSelected(object? sender, DateChangedEventArgs e)
    {
        DateTime dataSelecionada = e.NewDate ?? DateTime.Today;

        dtpck_checkout.MinimumDate = dataSelecionada.AddDays(1);
        dtpck_checkout.MaximumDate = dataSelecionada.AddMonths(6);

        if (dtpck_checkout.Date == null ||
            dtpck_checkout.Date <= dataSelecionada)
        {
            dtpck_checkout.Date = dataSelecionada.AddDays(1);
        }
    }

    private async void OnCalcularClicked(object sender, EventArgs e)
    {
        if (pck_quarto.SelectedItem == null)
        {
            await DisplayAlertAsync("Atenção", "Por favor, selecione uma suíte antes de calcular.", "OK");
            return;
        }

        Quarto quartoSelecionado = (Quarto)pck_quarto.SelectedItem;

        int adultos = (int)stp_adultos.Value;
        int criancas = (int)stp_criancas.Value;

        DateTime dataCheckin = dtpck_checkin.Date ?? DateTime.Today;
        DateTime dataCheckout = dtpck_checkout.Date ?? dataCheckin.AddDays(1);

        int totalDias = (dataCheckout - dataCheckin).Days;

        if (totalDias <= 0)
        {
            await DisplayAlertAsync("Data Inválida", "A data de Check-out deve ser posterior ao Check-in.", "OK");
            return;
        }

        double valorPorDia =
            (adultos * quartoSelecionado.ValorDiariaAdulto) +
            (criancas * quartoSelecionado.ValorDiariaCrianca);

        double valorTotal = valorPorDia * totalDias;

        lbl_resultado.Text = $"Suíte: {quartoSelecionado.Descricao}\n" +
                             $"Estadia: {totalDias} diária(s)\n" +
                             $"Hóspedes: {adultos} adulto(s) e {criancas} criança(s)\n\n" +
                             $"Valor Total: R$ {valorTotal:F2}";

        frame_resultado.IsVisible = true;
    }
}

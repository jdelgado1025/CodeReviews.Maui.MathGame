using MathGame.Maui.Views;

namespace MathGame.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnGameChosen(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Navigation.PushAsync(new Game(btn.Text));
    }
    
    private void OnViewGamesHistory(object sender, EventArgs e)
    {
        Navigation.PushAsync(new GameHistory());
    }
}


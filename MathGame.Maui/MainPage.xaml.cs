using MathGame.Maui.Views;
using MathGame.Maui.Models;

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
        var gameType = btn.Text switch
        {
            "Addition" => Operation.Addition,
            "Subtraction" => Operation.Subtraction,
            "Multiplication" => Operation.Multiplication,
            "Division" => Operation.Division,
            "Random" => Operation.Random,
            _ => Operation.Random
        };

        Navigation.PushAsync(new Game(gameType));
    }
    
    private void OnViewGamesHistory(object sender, EventArgs e)
    {
        Navigation.PushAsync(new GameHistory());
    }
}


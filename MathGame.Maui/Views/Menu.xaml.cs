using MathGame.Maui.Models;
using DataAccess;

namespace MathGame.Maui.Views;

public partial class Menu : ContentPage
{
    public Difficulty Level { get; set; }

    public Menu(Difficulty level)
	{
		InitializeComponent();
        Level = level;
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

        Navigation.PushAsync(new Game(gameType, Level));
    }

    private void OnViewGamesHistory(object sender, EventArgs e)
    {
        Navigation.PushAsync(new GameHistory());
    }
}
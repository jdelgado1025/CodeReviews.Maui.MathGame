using MathGame.Maui.Models;
using DataAccess;

namespace MathGame.Maui.Views;

[QueryProperty(nameof(LevelParameter), "difficulty")]
public partial class Menu : ContentPage
{
    private string _difficulty;
    public string LevelParameter
    {
        get => _difficulty;
        set
        {
            _difficulty = value;
            Level = Enum.TryParse(value, out Difficulty _level) ? _level : Difficulty.Easy;
        }
    }
    public Difficulty Level { get; set; }

    public Menu()
	{
		InitializeComponent();
	}

    private async void OnGameChosen(object sender, EventArgs e)
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

        //await Navigation.PushAsync(new Game(gameType, Level));
        await Shell.Current.GoToAsync($"Game?gameType={gameType}&level={Level}");
    }

    private async void OnViewGamesHistory(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GameHistory());
    }
}
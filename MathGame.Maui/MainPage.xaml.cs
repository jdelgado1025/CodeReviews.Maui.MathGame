using MathGame.Maui.Views;
using MathGame.Maui.Models;

namespace MathGame.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnDifficultyChosen(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var difficultyLevel = btn.Text switch
        {
            "Easy" => Difficulty.Easy,
            "Medium" => Difficulty.Medium,
            "Hard" => Difficulty.Hard,
            "Extreme" => Difficulty.Extreme,
            _ => Difficulty.Easy
        };

        //Navigation.PushAsync(new Menu(difficultyLevel));
        await Shell.Current.GoToAsync($"Menu?difficulty={difficultyLevel}");
    }
}


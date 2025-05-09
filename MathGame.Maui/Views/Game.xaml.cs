using MathGame.Maui.Models;

namespace MathGame.Maui.Views;

public partial class Game : ContentPage
{
    public Operation GameType { get; set; }
    public Difficulty Level { get; set; }

    public Game(Operation gameType, Difficulty level)
	{
		InitializeComponent();
		GameType = gameType;
        Level = level;
		BindingContext = this;

		//GenerateQuestion();
	}

    private void OnAnswerSubmitted(object sender, EventArgs e)
    {

    }
}
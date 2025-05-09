using MathGame.Maui.Models;

namespace MathGame.Maui.Views;

public partial class Game : ContentPage
{
    public Operation GameType { get; set; }

    public Game(Operation gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;

		GenerateQuestion();
	}

	private void GenerateQuestion()
	{

	}

	private void OnAnswerSubmitted(object sender, EventArgs e)
    {

    }
}
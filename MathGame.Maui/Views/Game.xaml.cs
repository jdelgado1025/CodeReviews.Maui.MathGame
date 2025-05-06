namespace MathGame.Maui.Views;

public partial class Game : ContentPage
{
    public string GameType { get; set; }
    public Game(string gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;
	}

	private void OnAnswerSubmitted(object sender, EventArgs e)
    {

    }
}
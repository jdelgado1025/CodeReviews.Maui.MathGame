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

        GenerateQuestion();
    }

    private void GenerateQuestion()
    {
        var (numberOne, numberTwo) = GenerateNumbers(GameType, Level);

        var operationSymbol = GameType switch
        {
            Operation.Addition => "+",
            Operation.Subtraction => "-",
            Operation.Multiplication => "*",
            Operation.Division => "/"
        };

        QuestionLabel.Text = $"{numberOne} {operationSymbol} {numberTwo}";
    }

    private (int, int) GenerateNumbers(Operation gameType, Difficulty difficultyLevel)
    {
        int numberOne, numberTwo;
        int upperLimit;

        const int lowerLimit = 0, divisorLowerLimit = 1;
        Random rand = new Random();

        switch (difficultyLevel)
        {
            case Difficulty.Easy:
                upperLimit = 100;
                break;
            case Difficulty.Medium:
                upperLimit = 300;
                break;
            case Difficulty.Hard:
                upperLimit = 500;
                break;
            case Difficulty.Extreme:
                upperLimit = 5000;
                break;
            default:
                upperLimit = 100;
                break;
        }

        if (gameType == Operation.Division)
        {
            /* Ensure the divisor is not 0 and result is a whole number */
            do
            {
                numberOne = rand.Next(lowerLimit, upperLimit);
                numberTwo = rand.Next(divisorLowerLimit, upperLimit);
            }
            while (numberOne % numberTwo != 0);
        }
        else
        {
            numberOne = rand.Next(lowerLimit, upperLimit);
            numberTwo = rand.Next(lowerLimit, upperLimit);
        }

        //Avoid negative number answers & higher number on the right side for Easy level
        if (difficultyLevel == Difficulty.Easy && numberTwo > numberOne)
        {
            int temp = numberOne;
            numberOne = numberTwo;
            numberTwo = temp;
        }

        /* Return two values using a Tuple */
        return (numberOne, numberTwo);
    }

    private void OnAnswerSubmitted(object sender, EventArgs e)
    {

    }
}
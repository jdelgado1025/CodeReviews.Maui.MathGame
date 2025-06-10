using MathGame.Maui.Models;
using DataAccess;

namespace MathGame.Maui.Views;

[QueryProperty(nameof(GameTypeParameter), "gameType")]
[QueryProperty(nameof(DifficultyParameter), "level")]
public partial class Game : ContentPage
{
    private string _gameType;

    public string GameTypeParameter
    {
        get => _gameType;
        set
        {
            _gameType = value;
            GameType = Enum.TryParse(value, out Operation gameType) ? gameType : default;
        }
    }

    private Operation gameType;
    public Operation GameType
    {
        get => gameType;
        set
        {
            gameType = value;
            OnPropertyChanged();
        }
    }

    private string _level;
    public string DifficultyParameter 
    {
        get => _level;
        set
        {
            _level = value;
            Level = Enum.TryParse(value, out Difficulty level) ? level : default;
        }
    }

    private Difficulty level;
    public Difficulty Level
    {
        get => level;
        set
        {
            level = value;
            OnPropertyChanged();
        }
    }

    public int Score { get; set; }
    private int _numberOne = 0;
    private int _numberTwo = 0;
    private const int _problemCount = 5;
    private int _questionNumber = 1;
    private int _questionsLeft = _problemCount;
    private Operation _currentOperation;

    public Game()
	{
		InitializeComponent();
        BindingContext = this;
        Score = 0;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        GenerateQuestion();
        UpdateScoreLabel();
    }

    private void GenerateQuestion()
    {
        _currentOperation = GameType;
        if (GameType == Operation.Random)
            _currentOperation = GetOperation();

        (_numberOne, _numberTwo) = GenerateNumbers(_currentOperation, Level);

        var operationSymbol = _currentOperation switch
        {
            Operation.Addition => "+",
            Operation.Subtraction => "-",
            Operation.Multiplication => "*",
            Operation.Division => "/",
            _ => ""
        };

        QuestionLabel.Text = $"{_numberOne} {operationSymbol} {_numberTwo}";
    }

    private Operation GetOperation()
    {
        Random rand = new Random();
        int getOperation = rand.Next(0, 4);

        return (Operation)getOperation;
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
                upperLimit = 15;
                break;
            case Difficulty.Medium:
                upperLimit = 50;
                break;
            case Difficulty.Hard:
                upperLimit = 100;
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
        if (difficultyLevel == Difficulty.Easy && numberTwo > numberOne && gameType != Operation.Division && numberOne != 0)
        {
            (numberOne, numberTwo) = (numberTwo, numberOne);
        }

        /* Return two values using a Tuple */
        return (numberOne, numberTwo);
    }

    private int CalculateAnswer()
    {
        int answer = 0;
        switch (_currentOperation)
        {
            case Operation.Addition:
                answer = _numberOne + _numberTwo;
                break;
            case Operation.Subtraction:
                answer = _numberOne - _numberTwo;
                break;
            case Operation.Multiplication:
                answer = _numberOne * _numberTwo;
                break;
            case Operation.Division:
                answer = _numberOne / _numberTwo;
                break;
        }

        return answer;
    }

    private void ValidateAnswer()
    {
        AnswerLabel.Text = "";
        var answer = CalculateAnswer();
        
        if (int.TryParse(AnswerEntry.Text, out var userAnswer) && userAnswer == answer)
        {
            AnswerLabel.Text = "Correct!";
            Score++;
        }
        else
            AnswerLabel.Text = "Incorrect!";

        UpdateScoreLabel();
    }

    private void GameOver()
    {
        GameOverLabel.Text = $"Thank you for playing!";
        BackButton.IsVisible = true;
    }

    private async void OnAnswerSubmitted(object sender, EventArgs e)
    {
        ValidateAnswer();

        //Reset Game Screen
        AnswerEntry.Text = "";

        //Set label animation
        AnswerLabel.Opacity = 0;
        await AnswerLabel.FadeTo(1, 500);

        //Keep track of questions left
        _questionsLeft--;

        if (_questionsLeft > 0)
        { 
            GenerateQuestion();
            _questionNumber++;
            QuestionNumberLabel.Text = $"Question {_questionNumber}";
        }
        else
            GameOver();

        await Task.Delay(2000);
        await AnswerLabel.FadeTo(0, 500);
        AnswerLabel.Text = "";
    }

    private async void OnBackToMenu(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        await Shell.Current.GoToAsync($"Menu?difficulty={Level}");
    }

    private void UpdateScoreLabel()
    {
        ScoreLabel.Text = $"Score: {Score} / {_problemCount}";
    }
}
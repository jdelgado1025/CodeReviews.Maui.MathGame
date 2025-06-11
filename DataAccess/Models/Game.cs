namespace DataAccess;
public class Game
{
    public int Id { get; set; }
    public Operation Operation { get; set; }
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime DatePlayed { get; set; }
}

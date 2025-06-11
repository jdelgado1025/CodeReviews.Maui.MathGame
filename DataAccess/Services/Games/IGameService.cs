namespace DataAccess.Services.Games;
public interface IGameService
{
    Task<List<Game>> GetAllGameRecords();
    Task CreateGameRecord(Game game);
}

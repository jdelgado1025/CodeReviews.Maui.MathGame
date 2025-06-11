
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DataAccess.Services.Games;
public class GameService : IGameService
{
    private readonly DataContext _context;

    public GameService(DataContext context)
    {
        _context = context;
    }

    public async Task CreateGameRecord(Game game)
    {
        if (game == null)
            throw new ArgumentNullException(nameof(game));

        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Game>> GetAllGameRecords()
    {
        return await _context.Games.ToListAsync();
    }
}

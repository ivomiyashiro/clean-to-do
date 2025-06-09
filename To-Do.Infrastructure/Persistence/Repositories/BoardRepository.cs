using Microsoft.EntityFrameworkCore;
using To_Do.Domain.Entities;
using To_Do.Domain.Respositories;

namespace To_Do.Infrastructure.Persistence.Repositories;

public class BoardRepository(ToDoDbContext context) : IBoardRepository
{
    private readonly ToDoDbContext _context = context;

    public async Task<Board> CreateBoardAsync(Board board, CancellationToken cancellationToken = default)
    {
        var createdBoard = await _context.Boards.AddAsync(board, cancellationToken);
        return createdBoard.Entity;
    }

    public async Task<Board?> GetBoardByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Boards.FirstOrDefaultAsync(b => b.Name == name, cancellationToken);
    }
}

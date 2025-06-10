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

    public async Task<Board?> GetBoardByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Boards.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<Board?> GetBoardByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Boards.FirstOrDefaultAsync(b => b.Name == name, cancellationToken);
    }

    public Board UpdateBoard(Board board)
    {
        var updatedBoard = _context.Boards.Update(board);
        return updatedBoard.Entity;
    }
}

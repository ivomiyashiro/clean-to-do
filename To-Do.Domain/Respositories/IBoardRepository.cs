using To_Do.Domain.Entities;

namespace To_Do.Domain.Respositories;

public interface IBoardRepository
{
    Task<Board> CreateBoardAsync(Board board, CancellationToken cancellationToken = default);
    Task<Board?> GetBoardByNameAsync(string name, CancellationToken cancellationToken = default);
}

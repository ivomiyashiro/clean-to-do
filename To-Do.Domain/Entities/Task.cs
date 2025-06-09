namespace To_Do.Domain.Entities;

public class Task
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid BoardId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public virtual Category? Category { get; private set; }
    public virtual Board? Board { get; private set; }

    public Task(string title, string description, bool isActive, Guid categoryId, Guid boardId)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new Exception("Title is required");
        }

        if (title.Length > 100)
        {
            throw new Exception("Title must be less than 100 characters");
        }

        if (description.Length > 2000)
        {
            throw new Exception("Description must be less than 2000 characters");
        }

        if (categoryId == Guid.Empty)
        {
            throw new Exception("Category is required");
        }

        if (boardId == Guid.Empty)
        {
            throw new Exception("Board is required");
        }

        Title = title;
        Description = description;
        IsActive = isActive;
        CategoryId = categoryId;
        BoardId = boardId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

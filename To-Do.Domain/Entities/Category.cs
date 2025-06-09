namespace To_Do.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public virtual HashSet<Task> Tasks { get; private set; } = [];

    public Category(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("Name is required");
        }

        if (name.Length > 50)
        {
            throw new Exception("Name must be less than 50 characters");
        }

        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
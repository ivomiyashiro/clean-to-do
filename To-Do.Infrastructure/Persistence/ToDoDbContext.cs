using Microsoft.EntityFrameworkCore;
using To_Do.Domain.Entities;
using TaskEntity = To_Do.Domain.Entities.Task;

namespace To_Do.Infrastructure.Persistence;

public class ToDoDbContext(DbContextOptions<ToDoDbContext> options) : DbContext(options)
{
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Board> Boards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

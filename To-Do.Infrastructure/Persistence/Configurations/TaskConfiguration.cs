using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEntity = To_Do.Domain.Entities.Task;

namespace To_Do.Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasMaxLength(2000);

        builder.Property(t => t.IsActive)
            .IsRequired();

        builder.Property(t => t.CategoryId)
            .IsRequired();

        builder.Property(t => t.BoardId)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .IsRequired();

        // Relationships
        builder.HasOne(t => t.Category)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Board)
            .WithMany(b => b.Tasks)
            .HasForeignKey(t => t.BoardId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(t => t.CategoryId);
        builder.HasIndex(t => t.BoardId);
        builder.HasIndex(t => t.CreatedAt);
    }
}
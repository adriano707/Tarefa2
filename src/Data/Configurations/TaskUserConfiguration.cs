using Domain.Aggregations.Task.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TaskUserConfiguration : IEntityTypeConfiguration<TaskUser>

    {
        public void Configure(EntityTypeBuilder<TaskUser> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Description);
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Status).IsRequired();
            builder.Property(t => t.UserId).IsRequired();

            builder.Property(t => t.CreateDate);
            builder.Property(t => t.UpdateDate);
        }
    }
}

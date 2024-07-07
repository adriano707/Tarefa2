using Domain.Aggregations.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.Password).IsRequired();

            builder.Property(u => u.CreateDate);
            builder.HasKey(u => u.UpdateDate);
        }
    }
}

using Domain.Aggregations.FinancialTransactionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AccountId).IsRequired();
            builder.Property(x => x.AccountDescription);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Type).IsRequired();
        }
    }
}

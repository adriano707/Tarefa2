using Domain.Aggregations.FinancialTransactionAggregate;

namespace Api.DTOs
{
    public class FinancialTransactionCreateDTO
    {
        public Guid AccountId { get; set; }
        public string AccountDescription { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
    }
}

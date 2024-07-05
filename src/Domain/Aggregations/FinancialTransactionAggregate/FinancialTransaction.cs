namespace Domain.Aggregations.FinancialTransactionAggregate;

public class FinancialTransaction
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public string AccountDescription { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private  set; }
    public decimal Amount { get; private  set; }
    public TransactionType Type { get; private  set; }
    
    public FinancialTransaction(DateTime date, Guid accountId, string accountDescription, string description, decimal amount, TransactionType type)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        AccountDescription = accountDescription;
        Date = date;
        Description = description;
        Amount = amount;
        Type = type;
    }
}
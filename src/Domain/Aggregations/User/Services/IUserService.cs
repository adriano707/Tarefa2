namespace Domain.Aggregations.User.Services
{
    public interface IUserService
    {
        Task<Entities.User> Get(Guid id);
        Task<Entities.User> GetAll();
        Task<Entities.User> Create(string username, string password);
    }
}

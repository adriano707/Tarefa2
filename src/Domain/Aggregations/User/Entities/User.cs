using Domain.Aggregations.Common;

namespace Domain.Aggregations.User.Entities
{
    public class User : EntityBase
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public User(string userName, string password)
        {
            Id = Guid.NewGuid();
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public void Update(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}

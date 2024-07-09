using Domain.Aggregations.Common;
using Domain.Aggregations.Task.Entities;

namespace Domain.Aggregations.User.Entities
{
    public class User : EntityBase
    {
        private List<TaskUser> _users = new List<TaskUser>();

        public Guid Id { get; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public IReadOnlyCollection<TaskUser> Tasks =>_users;

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

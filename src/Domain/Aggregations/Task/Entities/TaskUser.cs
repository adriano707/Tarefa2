using Domain.Aggregations.Common;
using Domain.Aggregations.Task.Enum;

namespace Domain.Aggregations.Task.Entities
{
    public class TaskUser : EntityBase
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatusEnum Status { get; private set; }

        public User.Entities.User User { get; private set; }
        public Guid UserId { get; private set; }

        public void Update(string title, string description, TaskStatusEnum status)
        {
            Title = title;
            Description = description;
            Status = status;
        }
    }
}

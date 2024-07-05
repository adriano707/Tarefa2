using Domain.Aggregations.Common;
using Domain.Aggregations.Task.Enum;

namespace Domain.Aggregations.Task.Entities
{
    public class Task : EntityBase
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatusEnum Status { get; private set; }
    }
}

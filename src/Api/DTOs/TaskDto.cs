using Domain.Aggregations.Task.Enum;

namespace Api.DTOs
{
    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}

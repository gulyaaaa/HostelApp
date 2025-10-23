namespace HostelApp.Models
{
    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    public enum TaskStatus
    {
        Open,
        InProgress,
        Done
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public TaskStatus Status { get; set; } = TaskStatus.Open;

        public string PriorityRu =>
            Priority == TaskPriority.Low ? "Низкий" :
            Priority == TaskPriority.Medium ? "Средний" :
            "Высокий";

        public string StatusRu =>
            Status == TaskStatus.Open ? "Открыта" :
            Status == TaskStatus.InProgress ? "В работе" :
            "Закрыта";

        public string TitleRu => $"Заявка #{Id} • Комн. {RoomNumber} • {PriorityRu} • {StatusRu}";
    }
}

namespace HostelApp.Models
{
    public enum RoomStatus
    {
        Ready,
        Cleaning,
        DoNotDisturb,
        NeedsRepair
    }

    public class Room
    {
        public int Number { get; set; }
        public string Type { get; set; } // Например: Частный / Общий 6/8
        public RoomStatus Status { get; set; }

        public string StatusRu =>
            Status == RoomStatus.Ready ? "Готов" :
            Status == RoomStatus.Cleaning ? "Уборка" :
            Status == RoomStatus.DoNotDisturb ? "Не беспокоить" :
            "Требует ремонта";
    }
}

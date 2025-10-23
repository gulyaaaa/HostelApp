using System;

namespace HostelApp.Models
{
    public enum BookingStatus
    {
        New,
        CheckedIn,
        CheckedOut
    }

    public class Booking
    {
        public string GuestName { get; set; }
        public int RoomNumber { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.New;

        public string Period => $"{From:dd.MM.yyyy} — {To:dd.MM.yyyy}";

        public string StatusRu =>
            Status == BookingStatus.New ? "Новая" :
            Status == BookingStatus.CheckedIn ? "Заселён" :
            "Выселен";

        public string TitleRu => $"{GuestName} • Комн. {RoomNumber} • {Period}";
    }
}

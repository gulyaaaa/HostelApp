using System;
using System.Collections.ObjectModel;
using HostelApp.Models;

namespace HostelApp.Services
{
    public class DataStore
    {
        public static DataStore Current { get; } = new DataStore();

        public ObservableCollection<Room> Rooms { get; } = new ObservableCollection<Room>();
        public ObservableCollection<Booking> Bookings { get; } = new ObservableCollection<Booking>();
        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();

        private int _taskId = 1;
        private DataStore() { }

        public void InitSampleData()
        {
            if (Rooms.Count > 0) return;

            Rooms.Add(new Room { Number = 101, Type = "Частный", Status = RoomStatus.Ready });
            Rooms.Add(new Room { Number = 102, Type = "Частный", Status = RoomStatus.Cleaning });
            Rooms.Add(new Room { Number = 201, Type = "Общий 6", Status = RoomStatus.NeedsRepair });
            Rooms.Add(new Room { Number = 202, Type = "Общий 8", Status = RoomStatus.DoNotDisturb });

            Bookings.Add(new Booking
            {
                GuestName = "Иван Петров",
                RoomNumber = 101,
                From = DateTime.Today,
                To = DateTime.Today.AddDays(2),
                Status = BookingStatus.New
            });

            Tasks.Add(new TaskItem
            {
                Id = _taskId++,
                RoomNumber = 102,
                Description = "Заменить лампочку",
                Priority = TaskPriority.Low,
                Status = TaskStatus.Open
            });
        }

        public TaskItem AddTask(int roomNumber, string description, TaskPriority priority)
        {
            var t = new TaskItem
            {
                Id = _taskId++,
                RoomNumber = roomNumber,
                Description = description,
                Priority = priority,
                Status = TaskStatus.Open
            };
            Tasks.Add(t);
            return t;
        }
    }
}

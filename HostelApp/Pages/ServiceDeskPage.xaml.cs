using System;
using System.Linq;
using Xamarin.Forms;
using HostelApp.Services;
using HostelApp.Models;

namespace HostelApp.Pages
{
    public partial class ServiceDeskPage : ContentPage
    {
        public ServiceDeskPage()
        {
            InitializeComponent();
            RoomPicker.ItemsSource = DataStore.Current.Rooms.Select(r => r.Number).ToList();
            PriorityPicker.ItemsSource = new[] { "Низкий", "Средний", "Высокий" };
            PriorityPicker.SelectedItem = "Средний";
            TasksList.ItemsSource = DataStore.Current.Tasks;
        }

        private async void OnCreateTask(object sender, EventArgs e)
        {
            if (RoomPicker.SelectedItem == null)
            {
                await DisplayAlert("Ошибка", "Выберите номер комнаты", "OK"); return;
            }
            var desc = DescEntry.Text?.Trim();
            if (string.IsNullOrEmpty(desc))
            {
                await DisplayAlert("Ошибка", "Введите описание", "OK"); return;
            }
            var room = (int)RoomPicker.SelectedItem;
            var p = (string)PriorityPicker.SelectedItem;
            var pr = p == "Низкий" ? TaskPriority.Low : p == "Средний" ? TaskPriority.Medium : TaskPriority.High;
            DataStore.Current.AddTask(room, desc, pr);
            DescEntry.Text = "";
            await DisplayAlert("Готово", "Заявка создана", "OK");
        }

        private void OnToInProgress(object s, EventArgs e)
        {
            var t = (s as Button)?.BindingContext as TaskItem;
            if (t == null) return;
            t.Status = TaskStatus.InProgress;
            Refresh();
        }

        private void OnDone(object s, EventArgs e)
        {
            var t = (s as Button)?.BindingContext as TaskItem;
            if (t == null) return;
            t.Status = TaskStatus.Done;
            Refresh();
        }

        private void Refresh()
        {
            var list = DataStore.Current.Tasks.ToList();
            DataStore.Current.Tasks.Clear();
            foreach (var x in list) DataStore.Current.Tasks.Add(x);
        }
    }
}

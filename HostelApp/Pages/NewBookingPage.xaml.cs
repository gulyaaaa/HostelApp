using System;
using System.Linq;
using Xamarin.Forms;
using HostelApp.Services;
using HostelApp.Models;

namespace HostelApp.Pages
{
    public partial class NewBookingPage : ContentPage
    {
        public NewBookingPage()
        {
            InitializeComponent();
            RoomPicker.ItemsSource = DataStore.Current.Rooms.Select(r => r.Number).ToList();
            FromDate.Date = DateTime.Today;
            ToDate.Date = DateTime.Today.AddDays(1);
        }

        private async void OnCreate(object sender, EventArgs e)
        {
            var guest = GuestEntry.Text?.Trim();
            if (string.IsNullOrEmpty(guest))
            {
                await DisplayAlert("Ошибка", "Введите имя гостя", "OK");
                return;
            }
            if (RoomPicker.SelectedItem == null)
            {
                await DisplayAlert("Ошибка", "Выберите номер комнаты", "OK");
                return;
            }
            if (ToDate.Date <= FromDate.Date)
            {
                await DisplayAlert("Ошибка", "Дата выезда должна быть позже даты заезда", "OK");
                return;
            }

            DataStore.Current.Bookings.Add(new Booking
            {
                GuestName = guest,
                RoomNumber = (int)RoomPicker.SelectedItem,
                From = FromDate.Date,
                To = ToDate.Date,
                Status = BookingStatus.New
            });

            await DisplayAlert("Готово", "Бронь создана", "OK");
            await Navigation.PopAsync();
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

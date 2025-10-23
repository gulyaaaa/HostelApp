using System.Linq;
using Xamarin.Forms;
using HostelApp.Services;
using HostelApp.Models;
using System.Collections.Generic;

namespace HostelApp.Pages
{
    public partial class BookingPage : ContentPage
    {
        private List<Booking> _all;

        public BookingPage()
        {
            InitializeComponent();
            _all = DataStore.Current.Bookings.ToList();
            Apply();
            SearchBar.TextChanged += (s, e) => Apply();
            SearchBar.SearchButtonPressed += (s, e) => Apply();
        }

        private void Apply()
        {
            var q = (SearchBar.Text ?? "").Trim().ToLowerInvariant();
            var list = _all.AsEnumerable();
            if (!string.IsNullOrEmpty(q))
                list = list.Where(b =>
                    (b.GuestName ?? "").ToLowerInvariant().Contains(q) ||
                    b.RoomNumber.ToString().Contains(q) ||
                    b.StatusRu.ToLowerInvariant().Contains(q));
            BookingsList.ItemsSource = list.ToList();
        }

        private async void OnNewBooking(object sender, System.EventArgs e)
            => await Navigation.PushAsync(new NewBookingPage());

        private async void OnCheckIn(object sender, System.EventArgs e)
        {
            var b = (sender as Button)?.BindingContext as Booking;
            if (b == null) return;
            var ok = await DisplayAlert("Подтверждение", $"Заселить гостя {b.GuestName}?", "Да", "Отмена");
            if (!ok) return;
            b.Status = BookingStatus.CheckedIn; _all = DataStore.Current.Bookings.ToList(); Apply();
        }

        private async void OnCheckOut(object sender, System.EventArgs e)
        {
            var b = (sender as Button)?.BindingContext as Booking;
            if (b == null) return;
            var ok = await DisplayAlert("Подтверждение", $"Выселить гостя {b.GuestName}?", "Да", "Отмена");
            if (!ok) return;
            b.Status = BookingStatus.CheckedOut; _all = DataStore.Current.Bookings.ToList(); Apply();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _all = DataStore.Current.Bookings.ToList(); Apply();
        }
    }
}

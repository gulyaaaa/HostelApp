using System.Linq;
using Xamarin.Forms;
using HostelApp.Services;
using HostelApp.Models;
using System.Collections.Generic;

namespace HostelApp.Pages
{
    public partial class HousekeepingPage : ContentPage
    {
        private List<Room> _all;

        public HousekeepingPage()
        {
            InitializeComponent();
            _all = DataStore.Current.Rooms.ToList();
            Apply();
            SearchBar.TextChanged += (s, e) => Apply();
            SearchBar.SearchButtonPressed += (s, e) => Apply();
        }

        private void Apply()
        {
            var q = (SearchBar.Text ?? "").Trim().ToLowerInvariant();
            var list = _all.AsEnumerable();
            if (!string.IsNullOrEmpty(q))
                list = list.Where(r =>
                    r.Number.ToString().Contains(q) ||
                    (r.Type ?? "").ToLowerInvariant().Contains(q) ||
                    r.StatusRu.ToLowerInvariant().Contains(q));
            RoomsList.ItemsSource = list.ToList();
        }

        private void Touch(System.Action<Room> act, object sender)
        {
            var room = (sender as Button)?.BindingContext as Room;
            if (room == null) return;
            act(room);
            _all = DataStore.Current.Rooms.ToList();
            Apply();
        }

        private void OnSetReady(object s, System.EventArgs e) => Touch(r => r.Status = RoomStatus.Ready, s);
        private void OnSetCleaning(object s, System.EventArgs e) => Touch(r => r.Status = RoomStatus.Cleaning, s);
        private void OnSetDnd(object s, System.EventArgs e) => Touch(r => r.Status = RoomStatus.DoNotDisturb, s);
        private void OnSetRepair(object s, System.EventArgs e) => Touch(r => r.Status = RoomStatus.NeedsRepair, s);
    }
}
   
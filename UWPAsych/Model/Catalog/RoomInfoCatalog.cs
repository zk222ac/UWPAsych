using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;
using Newtonsoft.Json;
using UWPAsych.Handler;
using UWPAsych.ViewModel;

namespace UWPAsych.Model.Catalog
{
   public class RoomInfoCatalog : IRequestHttpHandler<Hotel>
    {
        private const string Uri = "http://localhost:6454/api/VdistinctRoomNoes";
        public ObservableCollection<Room> Rooms { get; set; }
        public ViewModel.ViewModel BookingInfoVm { get; set; }

        public RoomInfoCatalog(ViewModel.ViewModel vm)
        {
            BookingInfoVm = vm;
            Rooms = new ObservableCollection<Room>();
            // Web API Uri link .........
            FetchAllData();
        }
        public async void FetchAllData()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = "";
                    Task task = Task.Run(async () =>
                    {
                        // sends GET request
                        // ReSharper disable once AccessToDisposedClosure
                        response = await client.GetStringAsync(new Uri(Uri));
                    });
                    // Wait
                    task.Wait();
                    // convert Json into Objects
                    if (response != null)
                    {
                        Rooms = JsonConvert.DeserializeObject<ObservableCollection<Room>>(response);
                        // call on property change Interface
                    }
                }
                catch (Exception ex)
                {
                    //// Display successful message
                    var messageDialog = new MessageDialog(ex.Message);
                    await messageDialog.ShowAsync();

                }

            }
        }
        public void Post()
        {
            
        }
    }
}

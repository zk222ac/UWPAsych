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
   
   public class GuestInfoCatalog : IRequestHttpHandler<Guest>
    {
        private const string Uri = "http://localhost:50659/api/Guests";
        public ObservableCollection<Guest> Guests { get; set; }
        public BookingInfoVm BookingInfoVm { get; set; }
        public GuestInfoCatalog(BookingInfoVm vm)
        {
            BookingInfoVm = vm;
            Guests = new ObservableCollection<Guest>();
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
                        Guests = JsonConvert.DeserializeObject<ObservableCollection<Guest>>(response);
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
            throw new NotImplementedException();
        }
    }
}

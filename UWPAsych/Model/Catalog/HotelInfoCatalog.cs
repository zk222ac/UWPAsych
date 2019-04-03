using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;
using Newtonsoft.Json;
using UWPAsych.Handler;
using UWPAsych.ViewModel;

namespace UWPAsych.Model.Catalog
{
    public class HotelInfoCatalog : IRequestHttpHandler<Hotel>
    {
        private const string Uri = "http://localhost:5676/api/Hotels";
        public ObservableCollection<Hotel> Hotels { get; set; }
        public BookingInfoVm BookingInfoVm { get; set; }

        public HotelInfoCatalog()
        {

        }

        // inject View Model into HotelInfoCatalog
        public HotelInfoCatalog(BookingInfoVm vm)
        {
            BookingInfoVm = vm;
            Hotels = new ObservableCollection<Hotel>();
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
                        Hotels = JsonConvert.DeserializeObject<ObservableCollection<Hotel>>(response);
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
            //Event newEvent = new Event()
            //{
            //    Id = EventVm.AddEvent.Id,
            //    Name = EventVm.AddEvent.Name,
            //    Place = EventVm.AddEvent.Place,
            //    Description = EventVm.AddEvent.Description,
            //    DateTime = DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventVm.Date, EventVm.Time)
            //};
            //// EventVm --> EventCatalogSingleton --> Add
            //EventVm.EventCatalogSingleton.Add(newEvent);

            //// Display successful message
            //var messageDialog = new MessageDialog("Your have been successfully Added your new Event in calender: " + EventVm);
            //await messageDialog.ShowAsync();

        }
    }
}

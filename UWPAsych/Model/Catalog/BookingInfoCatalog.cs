using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;
using Newtonsoft.Json;
using UWPAsych.Handler;
using UWPAsych.ViewModel;

namespace UWPAsych.Model.Catalog
{
    public class BookingInfoCatalog : IRequestHttpHandler<BookingInfo>
    {
        private const string Uri = "http://localhost:5676/api/BookingInfoes"; 
        public ObservableCollection<BookingInfo> BookingInfo { get; set; }
        public  BookingInfoVm BookingInfoVm { get; set; }
        public BookingInfoCatalog()
        {
            
        }

        // check selectedItem property 
        public void SelectedItem(BookingInfo bInfo)
        {
            BookingInfoVm.SelectedItem = bInfo;
        }

        public BookingInfoCatalog(BookingInfoVm vm)
        {
            BookingInfoVm = vm;
            BookingInfo = new ObservableCollection<BookingInfo>();
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
                        BookingInfo = JsonConvert.DeserializeObject<ObservableCollection<BookingInfo>>(response);
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
            BookingInfo newEvent = new BookingInfo
            {
                //BookingId = EventVm.AddEvent.Id,
                //Name = EventVm.AddEvent.Name,
                //Place = EventVm.AddEvent.Place,
                //Description = EventVm.AddEvent.Description,
                //DateTime = DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventVm.Date, EventVm.Time)
            };
            //// EventVm --> EventCatalogSingleton --> Add
            //EventVm.EventCatalogSingleton.Add(newEvent);

            //// Display successful message
            //var messageDialog = new MessageDialog("Your have been successfully Added your new Event in calender: " + EventVm);
            //await messageDialog.ShowAsync();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Newtonsoft.Json;
using UWPAsych.Handler;
using UWPAsych.ViewModel;
using HttpClient = Windows.Web.Http.HttpClient;
using HttpResponseMessage = Windows.Web.Http.HttpResponseMessage;
using UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding;

namespace UWPAsych.Model.Catalog
{
    public class BookingInfoCatalog : IRequestHttpHandler<BookingInfo>
    {
        private const string Uri = "http://localhost:50659/api/BookingInfoes";
        private const string PostUri = "http://localhost:50659/api/Bookings";
        public ObservableCollection<BookingInfo> BookingInfo { get; set; }

        public ObservableCollection<string> RoomType { get; set; }

        public BookingInfoVm BookingInfoVm { get; set; }
        public BookingInfoCatalog()
        {
            
        }
        public BookingInfoCatalog(BookingInfoVm vm)
        {
            BookingInfoVm = vm;
            // fixed Room Type : S , D , F
            RoomType = new ObservableCollection<string>() {"D", "S", "F"};
           
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

        public async void Post()
        {
            
            BookingInfo bookingInfo = new BookingInfo
            {
                HotelNo = BookingInfoVm.SelectedValueHotelNo.HotelNo,
                RoomNo = BookingInfoVm.SelectedValueRoomNo.RoomNo,
                GuestNo = BookingInfoVm.SelectedValueGuestNo.GuestNo,
                RoomPrice = BookingInfoVm.AddBookingInfo.RoomPrice,
                RoomType = BookingInfoVm.AddBookingInfo.RoomType,
                DateFrom = DateTimeOffsetAndTimeSetToDateTime(BookingInfoVm.DateFrom),
                DateTo =  DateTimeOffsetAndTimeSetToDateTime(BookingInfoVm.DateTo)
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
                    // In Post Method first we convert New object into Json format , then format it to Json string form
                    var jsonStr = JsonConvert.SerializeObject(bookingInfo);
                    var content = new HttpStringContent(jsonStr, UnicodeEncoding.Utf8, "application/json");
                    HttpResponseMessage response = null;
                    Task task = Task.Run(async () =>
                    {
                        // send a Post request and Get response with newly added resource
                        // ReSharper disable once AccessToDisposedClosure
                        response = await client.PostAsync(new Uri(PostUri), content);
                    });
                    // Wait ........ for Post outcomes
                    task.Wait();
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        throw new Exception("Hotel already exist:");
                    }
                    // if response is success
                    //response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        // Read response in a Json Format
                        string jsonFormat = await response.Content.ReadAsStringAsync();
                        var newBooking = JsonConvert.DeserializeObject<BookingInfo>(jsonFormat);
                        string booking = $"BookingId:{newBooking.BookingId}, HotelNo: {newBooking.HotelNo}, GuestNo:{newBooking.GuestNo}, RoomNo:{newBooking.RoomNo}, DateFrom:{newBooking.DateFrom}, DateTo:{newBooking.DateTo} , Price:{newBooking.RoomPrice}, Type:{newBooking.RoomType}";
                        var messageDialog = new MessageDialog("$ Congratulation New Booking has been Made. " + booking);
                        await messageDialog.ShowAsync();
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            // Try to read response, log error etc.
                            var errorJson = await response.Content.ReadAsStringAsync();
                            var messageDialog = new MessageDialog(errorJson);
                            await messageDialog.ShowAsync();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               
                //// Display successful message
                var messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();

            }


            //// Display successful message
            //var messageDialog = new MessageDialog("Your have been successfully Added your new Event in calender: " + EventVm);
            //await messageDialog.ShowAsync();
        }

        public static  void Delete()
        {

            // Create the message dialog and set its content
            //var messageDialog = new MessageDialog("Are you sure you want to Delete the Event: " + EventVm.SelectedEvent.Name + " ?");

            //// Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            //messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler)));
            //messageDialog.Commands.Add(new UICommand("No", null));

            //// Set the command that will be invoked by default
            //messageDialog.DefaultCommandIndex = 0;

            //// Set the command to be invoked when escape is pressed
            //messageDialog.CancelCommandIndex = 1;

            //// Show the message dialog
            //await messageDialog.ShowAsync();

        }

        //private static void CommandInvokedHandler(IUICommand command)
        //{
        //    // EventVm.EventCatalogSingleton.Remove(EventVm.SelectedEvent);
        //}
        public static DateTime DateTimeOffsetAndTimeSetToDateTime(DateTimeOffset date)
        { 
            DateTime dt = new DateTime();
            return new DateTime(date.Year, date.Month, date.Day , dt.Hour,dt.Minute,dt.Minute);
        }
    }
}

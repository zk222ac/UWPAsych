using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UWPAsych.Common;
using UWPAsych.Handler;
using UWPAsych.Model;
using UWPAsych.Model.Catalog;

namespace UWPAsych.ViewModel
{
   public class BookingInfoVm
    {
        public int BookingId { get; set; }
        public char RoomType { get; set; }
        public int RoomNo { get; set; }
        public float RoomPrice { get; set; }
        public string HotelName { get; set; }
        public string GuestName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public BookingInfo SelectedItem { get; set; }
        public ICommand FetchDataCommand { get; set; }
        public BookingInfoCatalog BookingInfoCatalog { get; set; }
        public HotelInfoCatalog HotelInfoCatalog { get; set; }
        public ICommand SelectedBookingCommand { get; set; }
        public ICommand CreateBookingCommand { get; set; }
        //public ICommand DeleteBookingCommand { get; set; }
        // Display day and time in Coordinated Universal time (UTC)
        public DateTimeOffset Date { get; set; }
        // Time interval code
        public TimeSpan Time { get; set; }

        public BookingInfoVm()
        {
            // when click Refresh DataSet
            FetchDataCommand = new RelayArgCommand<BookingInfo>(s => BookingInfoCatalog.FetchAllData());

            SelectedItem = new BookingInfo();
            
            // Inject BookingInfoVm into Request Handler
            BookingInfoCatalog = new BookingInfoCatalog(this);
            HotelInfoCatalog = new HotelInfoCatalog(this);
            //// Invoke SelectedEvent delegate method inside EventHandler
            SelectedBookingCommand = new RelayArgCommand<BookingInfo>(s => BookingInfoCatalog.SelectedItem(SelectedItem));

            // Get current datetime now
            DateTime dt = DateTime.Now;
            // represent current date in Years , Months , Day, hours , min , sec
            Date = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, new TimeSpan());
            Time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);

            // Invoke CreateEvent delegate method inside EvenHandler Class
             CreateBookingCommand = new RelayArgCommand<BookingInfo>(s => BookingInfoCatalog.Post());
            //// Invoke DeleteEvent delegate method inside EvenHandler Class
            //DeleteBookingCommand = new RelayArgCommand<BookingInfo>(s => RequestHandler<BookingInfo>.Delete());
        }



    }
}

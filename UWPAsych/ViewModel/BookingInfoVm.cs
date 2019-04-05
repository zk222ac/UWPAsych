﻿using System;
using System.Windows.Input;
using UWPAsych.Common;
using UWPAsych.Model;
using UWPAsych.Model.Catalog;

namespace UWPAsych.ViewModel
{
   public class BookingInfoVm : NotifyPropertyChangeClass
    {
        public int BookingId { get; set; }
        public string RoomType { get; set; }
        public int RoomNo { get; set; }
        public float RoomPrice { get; set; }
        public string HotelName { get; set; }
        public string GuestName { get; set; }
        public ICommand FetchDataCommand { get; set; }
        public BookingInfo AddRoomType { get; set; }
        public BookingInfoCatalog BookingInfoCatalog { get; set; }
        public HotelInfoCatalog HotelInfoCatalog { get; set; }
        public GuestInfoCatalog GuestInfoCatalog { get; set; }
        public RoomInfoCatalog RoomInfoCatalog { get; set; }
        public ICommand CreateBookingCommand { get; set; }
        //public ICommand DeleteBookingCommand { get; set; }
        // Display day and time in Coordinated Universal time (UTC)
        private DateTimeOffset _dateFrom;
        private DateTimeOffset _dateTo;
        // call on property Change method
        public DateTimeOffset DateFrom
        {
            get => _dateFrom;
            set
            {
                _dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }
        public DateTimeOffset DateTo
        {
            get => _dateTo;
            set
            {
                _dateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }
        // Selected item (key)
        public Hotel SelectedValueHotelNo { get; set; }
        public Room SelectedValueRoomNo { get; set; }
        public Guest SelectedValueGuestNo { get; set; }
        

        public BookingInfoVm()
        {
            //.... selected ComboBox Value............................................

            SelectedValueHotelNo = new Hotel();
            SelectedValueGuestNo = new Guest();
            SelectedValueRoomNo = new Room();
           // SelectedValueRoomType = new string();
            AddRoomType = new BookingInfo();

            // Inject BookingInfoVm into Request Handler..............
            BookingInfoCatalog = new BookingInfoCatalog(this);
            HotelInfoCatalog = new HotelInfoCatalog(this);
            GuestInfoCatalog = new GuestInfoCatalog(this);
            RoomInfoCatalog = new RoomInfoCatalog(this);
           
            
            // when click Refresh DataSet ....................
            FetchDataCommand = new RelayArgCommand<BookingInfo>(s => BookingInfoCatalog.FetchAllData());
            
            // Get current datetime now
            DateTime dt = DateTime.Now;
            // represent current date in Years , Months , Day, hours , min , sec
            DateFrom = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, new TimeSpan());
            DateTo = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, new TimeSpan());

            // Invoke CreateEvent delegate method inside EvenHandler Class
            CreateBookingCommand = new RelayArgCommand<BookingInfo>(s => BookingInfoCatalog.Post());
            //// Invoke DeleteEvent delegate method inside EvenHandler Class
            //DeleteBookingCommand = new RelayArgCommand<BookingInfo>(s => RequestHandler<BookingInfo>.Delete());
        }



    }
}

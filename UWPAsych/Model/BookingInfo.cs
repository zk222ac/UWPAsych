using System;

namespace UWPAsych.Model
{
    public class BookingInfo
    {
        private int _bookingId;
        public int BookingId
        {
            get => _bookingId;
            set => _bookingId = value;
        }

        private string _roomType;
        public string RoomType
        {
            get => _roomType;
            set => _roomType = value;
        }

        private int _roomNo;
        public int RoomNo
        {
            get => _roomNo;
            set => _roomNo = value;
        }

        private float _roomPrice;
        public float RoomPrice
        {
            get => _roomPrice;
            set => _roomPrice = value;
        }

        private string _hotelName;
        public string HotelName
        {
            get => _hotelName;
            set => _hotelName = value;
        }

        private string _guestName;
        public string GuestName
        {
            get => _guestName;
            set => _guestName = value;
        }

        private DateTime _dateFrom;
        public DateTime DateFrom
        {
            get => _dateFrom;
            set => _dateFrom = value;
        }

        private DateTime _dateTo;
        public DateTime DateTo
        {
            get => _dateTo;
            set => _dateTo = value;
        }

        private int _hotelNo;
        public int HotelNo
        {
            get => _hotelNo;
            set => _hotelNo = value;
        }

        private int _guestNo;
        public int GuestNo
        {
            get => _guestNo;
            set => _guestNo = value;
        }

        public override string ToString()
        {
            return $"{nameof(BookingId)}: {BookingId}, {nameof(RoomType)}: {RoomType}, {nameof(RoomNo)}: {RoomNo}, {nameof(RoomPrice)}: {RoomPrice}, {nameof(HotelName)}: {HotelName}, {nameof(GuestName)}: {GuestName}, {nameof(DateFrom)}: {DateFrom}, {nameof(DateTo)}: {DateTo}";
        }
    }
}

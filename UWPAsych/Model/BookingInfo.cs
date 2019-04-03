using System;

namespace UWPAsych.Model
{
    public class BookingInfo
    {
        public int BookingId { get; set; }
        public char RoomType { get; set; }
        public int RoomNo { get; set; }
        public float RoomPrice { get; set; }
        public string HotelName { get; set; }
        public string GuestName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public BookingInfo(int bookingId, char roomType, int roomNo, float roomPrice, string hotelName, string guestName, DateTime dateFrom, DateTime dateTo)
        {
            BookingId = bookingId;
            RoomType = roomType;
            RoomNo = roomNo;
            RoomPrice = roomPrice;
            HotelName = hotelName;
            GuestName = guestName;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public BookingInfo()
        {

        }

        public override string ToString()
        {
            return $"{nameof(BookingId)}: {BookingId}, {nameof(RoomType)}: {RoomType}, {nameof(RoomNo)}: {RoomNo}, {nameof(RoomPrice)}: {RoomPrice}, {nameof(HotelName)}: {HotelName}, {nameof(GuestName)}: {GuestName}, {nameof(DateFrom)}: {DateFrom}, {nameof(DateTo)}: {DateTo}";
        }
    }
}

﻿using System;

namespace UWPAsych.Model
{
    public class BookingInfo
    {
        public int BookingId { get; set; }
        public string RoomType { get; set; }
        public int RoomNo { get; set; }
        public float RoomPrice { get; set; }
        public string HotelName { get; set; }
        public string GuestName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int HotelNo { get; set; }
        public int GuestNo { get; set; }
        public override string ToString()
        {
            return $"{nameof(BookingId)}: {BookingId}, {nameof(RoomType)}: {RoomType}, {nameof(RoomNo)}: {RoomNo}, {nameof(RoomPrice)}: {RoomPrice}, {nameof(HotelName)}: {HotelName}, {nameof(GuestName)}: {GuestName}, {nameof(DateFrom)}: {DateFrom}, {nameof(DateTo)}: {DateTo}";
        }
    }
}

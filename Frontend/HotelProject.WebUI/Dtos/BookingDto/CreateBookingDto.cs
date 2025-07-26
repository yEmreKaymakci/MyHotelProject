namespace HotelProject.WebUI.Dtos.BookingDto
{
    public class CreateBookingDto
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Checkin { get; set; }     // Orijinal DateTime
        public string Checkout { get; set; }    // Orijinal DateTime
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public string RoomCount { get; set; }
        public string? SpecialRequest { get; set; } // Optional
        public string? Description { get; set; }    // Optional
        public string? Status { get; set; }
    }
}

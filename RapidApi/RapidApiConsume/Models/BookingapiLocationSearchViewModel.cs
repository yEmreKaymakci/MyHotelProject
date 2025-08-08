namespace RapidApiConsume.Models
{
    public class BookingApiLocationSearchViewModel
    {

        public class BookingApiResponse
        {
            public List<LocationItem> data { get; set; }
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class LocationItem
        {
            public string id { get; set; }
        }


    }
}
//public class BookingApiResponse
//{
//    public List<LocationItem> data { get; set; }
//    public bool status { get; set; }
//    public string message { get; set; }
//}

//// Sadece ihtiyacınız olan alanlar
//public class LocationItem
//{
//    public string dest_id { get; set; }
//}
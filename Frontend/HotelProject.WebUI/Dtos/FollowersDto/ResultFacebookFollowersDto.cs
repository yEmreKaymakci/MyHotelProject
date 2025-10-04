namespace HotelProject.WebUI.Dtos.FollowersDto
{
    public class ResultFacebookFollowersDto
    {
        public Results results { get; set; }

        public class Results
        {
            public int likes { get; set; }
            public int followers { get; set; }
        }

    }
}

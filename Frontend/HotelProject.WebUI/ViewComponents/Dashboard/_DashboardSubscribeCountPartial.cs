using HotelProject.WebUI.Dtos.FollowersDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelProject.WebUI.ViewComponents.Dashboard
{
    public class _DashboardSubscribeCountPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://instagram-profile1.p.rapidapi.com/getprofile/_yekaa"),
                Headers =
    {
        { "x-rapidapi-key", "eaa8321078msh8aa48935ef5e1d8p1cb46fjsn6493e1885e8e" },
        { "x-rapidapi-host", "instagram-profile1.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                ResultInstagramFollowersDto resultInstagramFollowersDtos = JsonConvert.DeserializeObject<ResultInstagramFollowersDto>(body);
                ViewBag.v1 = resultInstagramFollowersDtos.followers;
                ViewBag.v2 = resultInstagramFollowersDtos.following;
            }

            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://twitter241.p.rapidapi.com/user?username=MurattYucedag"),
                Headers =
    {
        { "x-rapidapi-key", "eaa8321078msh8aa48935ef5e1d8p1cb46fjsn6493e1885e8e" },
        { "x-rapidapi-host", "twitter241.p.rapidapi.com" },
    },
            };
            using (var response2 = await client2.SendAsync(request2))
            {
                response2.EnsureSuccessStatusCode();
                var body2 = await response2.Content.ReadAsStringAsync();
                ResultXFollowersDto resultXFollowersDtos = JsonConvert.DeserializeObject<ResultXFollowersDto>(body2);

                ViewBag.v3 = resultXFollowersDtos?.result?.data?.user?.result?.legacy?.followers_count ?? 0;
                ViewBag.v4 = resultXFollowersDtos?.result?.data?.user?.result?.legacy?.friends_count ?? 0;
            }



            var client3 = new HttpClient();
            var request3 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://fresh-linkedin-scraper-api.p.rapidapi.com/api/v1/user/follower-and-connection?username=yunusemrekaymakci"),
                Headers =
    {
        { "x-rapidapi-key", "eaa8321078msh8aa48935ef5e1d8p1cb46fjsn6493e1885e8e" },
        { "x-rapidapi-host", "fresh-linkedin-scraper-api.p.rapidapi.com" },
    },
            };
            using (var response3 = await client3.SendAsync(request3))
            {
                response3.EnsureSuccessStatusCode();
                var body3 = await response3.Content.ReadAsStringAsync();
                ResultLinkedinFollowersDto resultLinkedinFollowersDtos = JsonConvert.DeserializeObject<ResultLinkedinFollowersDto>(body3);

                ViewBag.v5 = resultLinkedinFollowersDtos?.data?.follower_count ?? 0;
                ViewBag.v6 = resultLinkedinFollowersDtos?.data?.connection_count ?? 0;
            }




            var client4 = new HttpClient();
            var request4 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://facebook-scraper3.p.rapidapi.com/page/details?url=https%3A%2F%2Fwww.facebook.com%2Fkanyewest"),
                Headers =
    {
        { "x-rapidapi-key", "eaa8321078msh8aa48935ef5e1d8p1cb46fjsn6493e1885e8e" },
        { "x-rapidapi-host", "facebook-scraper3.p.rapidapi.com" },
    },
            };
            using (var response4 = await client4.SendAsync(request4))
            {
                response4.EnsureSuccessStatusCode();
                var body4 = await response4.Content.ReadAsStringAsync();
                ResultFacebookFollowersDto resultFacebookFollowersDtos = JsonConvert.DeserializeObject<ResultFacebookFollowersDto>(body4);

                ViewBag.v7 = resultFacebookFollowersDtos?.results?.followers ?? 0;
                ViewBag.v8 = resultFacebookFollowersDtos?.results?.likes ?? 0;
            }




            return View();
        }
    }
}


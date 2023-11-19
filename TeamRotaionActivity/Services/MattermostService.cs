using System.Net;
using System.Net.Http.Headers;

namespace TeamRotationActivity.Services
{
    public class MattermostService
    {
        public static void SendMessage(string message)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sztoyqk4a3gk3cf1monfxhodgo");
                var uri = "https://talk.directum.ru/api/v4/posts";
                var body = new
                {
                    channel_id = "ascufsthxibpiq9x7bnqbycimc",
                    message = message
                };
                var result = client.PostAsJsonAsync(uri, body).Result;                
            }
        }
    }
}

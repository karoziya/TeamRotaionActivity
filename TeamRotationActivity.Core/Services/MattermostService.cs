using System.Net.Http.Headers;
using System.Net.Http.Json;
using TeamRotationActivity.Domain.Interfaces.Services;

namespace TeamRotationActivity.Core.Services
{
    public class MattermostService: IMessageSenderService
    {
        public async void SendMessage(string message)
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
                await client.PostAsJsonAsync(uri, body);                
            }
        }
    }
}

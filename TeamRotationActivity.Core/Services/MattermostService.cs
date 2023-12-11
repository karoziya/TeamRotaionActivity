using System.Net.Http.Headers;
using System.Net.Http.Json;
using TeamRotationActivity.Domain.Interfaces.Services;

namespace TeamRotationActivity.Core.Services;

/// <summary>
/// Сервис отправки сообщений.
/// </summary>
public class MattermostService: IMessageSenderService
{
    /// <summary>
    /// Отправить сообщение.
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="Exception"></exception>
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
            var response = await client.PostAsJsonAsync(uri, body);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Send message error. StatusCode: {response.StatusCode}");
            }
        }
    }
}

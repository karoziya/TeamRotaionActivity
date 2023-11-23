using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using TeamRotationActivity.Domain.Enums;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TeamRotationActivity.Core.Services;

public class ActivitySaverService : IActivitySaverService
{
    private const string DataFileName = "data2.json";

    public async Task<IEnumerable<ActivityWork>> LoadActivitiesFromFileAsync()
    {
        await using var fileStream = new FileStream(DataFileName, FileMode.Open);
        var result = await JsonSerializer.DeserializeAsync<IEnumerable<ActivityWork>>(fileStream);
        fileStream.Close();
        return result as IEnumerable<ActivityWork> ?? Enumerable.Empty<ActivityWork>();
    }

    public async Task SaveActivitiesAsync(IEnumerable<ActivityWork> activities)
    {
        await using var createStream = new FileStream(DataFileName, FileMode.OpenOrCreate);
        var option = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        await JsonSerializer.SerializeAsync(createStream, activities, option);
        createStream.Close();
    }

    public ActivityWork CreateActivities()
    {
        var activities = new ActivityWork
        {
            Id = Guid.NewGuid(),
            Name = "Daily Meeting",
            Description = "Дейли митинг ежедневный",
            RotationPeriod = RotationPeriod.Weekly,
            Members = new List<Member>
            {
                new() { Id = Guid.NewGuid(), Name = "Максим", LastName = "Минаев" },
                new() { Id = Guid.NewGuid(), Name = "Сергей", LastName = "Камышев" },
                new() { Id = Guid.NewGuid(), Name = "Сергей", LastName = "Полянских" }
            },
            MemberId = Guid.NewGuid(),
            ActivityPeriod = ActivityPeriod.EveryDay,
            ActivityAnnouncementMessage = "@here Митинг в 9:20  :eyes: \r\nhttps://directum.ktalk.ru/redteam\r\n Проводит Максим, далее Сергей",
            NextRotation = DateTime.Now,
            ActivityDate = new DateTime(2023, 11, 23, 22, 28, 00),
            LastRotation = DateTime.Now
        };

        return activities;
    }
}
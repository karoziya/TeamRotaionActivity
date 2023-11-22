using System.Text.Json;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Core.Services;

public class ActivitySaverService : IActivitySaverService
{
    private const string DataFileName = "data.json";

    public async Task<IEnumerable<ActivityWork>> LoadActivitiesFromFileAsync()
    {
        await using var fileStream = new FileStream(DataFileName, FileMode.Open);
        var result = await JsonSerializer.DeserializeAsync(fileStream, typeof(IEnumerable<ActivityWork>));
        return result as IEnumerable<ActivityWork> ?? Enumerable.Empty<ActivityWork>();
    }

    public async Task SaveActivitiesAsync(IEnumerable<ActivityWork> activities)
    {
        await using var createStream = new FileStream(DataFileName, FileMode.OpenOrCreate);
        var option = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        await JsonSerializer.SerializeAsync(createStream, activities, option);
    }
}
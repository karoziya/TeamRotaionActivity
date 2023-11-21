using System.Text.Json;
using TeamRotationActivity.Model;

namespace TeamRotationActivity.Services
{
    public class ActivityService
    {
        private const string DataFileName = "data.json";

        public static async Task<IEnumerable<ActivityWork>> LoadActivitiesFromFileAsync()
        {
            await using FileStream fileStream = new FileStream(DataFileName, FileMode.Open);
            var result = await JsonSerializer.DeserializeAsync(fileStream, typeof(IEnumerable<ActivityWork>));
            return result as IEnumerable<ActivityWork> ?? Enumerable.Empty<ActivityWork>();
        }

        public static async Task SaveActivitiesAsync(IEnumerable<ActivityWork> activities)
        {
            await using FileStream createStream = new FileStream(DataFileName, FileMode.OpenOrCreate);
            var option = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            await JsonSerializer.SerializeAsync(createStream, activities, option);
        }
    }
}

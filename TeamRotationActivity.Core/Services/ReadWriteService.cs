using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TeamRotationActivity.Core.Services;

/// <summary>
/// Сервис сохранения/загрузки данных.
/// </summary>
public class ReadWriteService : IReadWriteService
{
    /// <summary>
    /// Имя файла сохранения.
    /// </summary>
    private const string DataFileName = "data.json";

    /// <summary>
    /// Загрузить активности из JSON-файла.
    /// </summary>
    /// <returns>Активности.</returns>
    public async Task<IEnumerable<ActivityWork>> LoadActivitiesFromFileAsync()
    {
        await using var fileStream = new FileStream(DataFileName, FileMode.Open);
        var result = await JsonSerializer.DeserializeAsync<IEnumerable<ActivityWork>>(fileStream);
        fileStream.Close();
        return result ?? Enumerable.Empty<ActivityWork>();
    }

    /// <summary>
    /// Сохранить активности в JSON-файл.
    /// </summary>
    /// <param name="activities">Активности, которые будут сохранены.</param>
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
}
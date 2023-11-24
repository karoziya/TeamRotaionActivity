using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Domain.Interfaces.Services;

/// <summary>
/// Сервис сохранения.
/// </summary>
public interface ISaverService
{
    /// <summary>
    /// Загрузить активности из JSON-файла.
    /// </summary>
    /// <returns>Активности.</returns>
    public Task<IEnumerable<ActivityWork>> LoadActivitiesFromFileAsync();

    /// <summary>
    /// Сохранить активности в JSON-файл.
    /// </summary>
    /// <param name="activities">Активности, которые будут сохранены.</param>
    public Task SaveActivitiesAsync(IEnumerable<ActivityWork> activities);
}
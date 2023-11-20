using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Domain.Interfaces.Services;

/// <summary>
/// Сервис активности.
/// </summary>
public interface IActivityService
{
    List<ActivityWork> GetAll();
    ActivityWork? GetById(Guid id);
    ActivityWork Create(ActivityWork entity);
}


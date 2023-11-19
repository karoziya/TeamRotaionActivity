using TeamRotationActivity.Domain.Enums;
using TeamRotationActivity.Domain.Models.Abstractions;

namespace TeamRotationActivity.Domain.Models;

/// <summary>
/// Активность.
/// </summary>
public class ActivityWork : Entity
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// Описание активности.
    /// </summary>
    public string Description { get; set; } = String.Empty;

    /// <summary>
    /// Коллекция членов команды.
    /// </summary>
    public IList<Member>? Members { get; set; }

    /// <summary>
    /// Период ротации.
    /// </summary>
    public RotationPeriod RotationPeriod { get; set; }

    /// <summary>
    /// Последняя активность.
    /// </summary>
    public DateTime LastChangeActivity { get; set; }
}


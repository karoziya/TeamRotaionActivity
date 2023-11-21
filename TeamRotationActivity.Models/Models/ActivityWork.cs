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
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание активности.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Коллекция членов команды.
    /// </summary>
    public IList<Member>? Members { get; set; }

    /// <summary>
    /// Период ротации.
    /// </summary>
    public RotationPeriod RotationPeriod { get; set; }

    /// <summary>
    /// Дата последней смены ведущего.
    /// </summary>
    public DateTime LastRotation { get; set; }
    
    /// <summary>
    /// Дата следующей смены ведущего.
    /// </summary>
    public DateTime NextRotation { get; set; }
    
    /// <summary>
    /// Дата проведения.
    /// </summary>
    public DateTime ActivityDate { get; set; }
}


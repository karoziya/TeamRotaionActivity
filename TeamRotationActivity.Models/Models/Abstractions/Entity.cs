using TeamRotationActivity.Domain.Interfaces;

namespace TeamRotationActivity.Domain.Models.Abstractions;

/// <summary>
/// Сущность.
/// </summary>
public class Entity : IEntity<Guid>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
}

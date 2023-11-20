using TeamRotationActivity.Domain.Interfaces;

namespace TeamRotationActivity.Domain.Models.Abstractions;

/// <summary>
/// Сущность.
/// </summary>
public class Entity : IEntity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
}

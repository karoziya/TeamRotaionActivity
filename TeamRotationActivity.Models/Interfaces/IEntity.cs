namespace TeamRotationActivity.Domain.Interfaces;

/// <summary>
/// Интерфейс сущности.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    Guid Id { get; set; }
}


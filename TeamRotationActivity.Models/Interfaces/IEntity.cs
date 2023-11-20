namespace TeamRotationActivity.Domain.Interfaces;

/// <summary>
/// Интерфейс сущности.
/// </summary>
/// <typeparam name="TId">Тип Id.</typeparam>
public interface IEntity<TId> where TId : struct
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    TId Id { get; set; }
}


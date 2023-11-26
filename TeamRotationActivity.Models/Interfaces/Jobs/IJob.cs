namespace TeamRotationActivity.Domain.Interfaces.Jobs;

/// <summary>
/// Интерфейс фоновой работы.
/// </summary>
/// /// <typeparam name="TJob">Класс джобы.</typeparam>
public interface IJob<T>
{
    /// <summary>
    /// Работа выполняемая фоновой службой.
    /// </summary>
    /// <param name="token"><see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    public Task ExecuteAsync(CancellationToken token = default);
}


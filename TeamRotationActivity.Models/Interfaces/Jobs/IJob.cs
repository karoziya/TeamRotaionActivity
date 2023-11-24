namespace TeamRotationActivity.Domain.Interfaces.Jobs;

/// <summary>
/// Интерфейс фоновой работы.
/// </summary>
/// /// <typeparam name="TJob">Класс джобы.</typeparam>
public interface IJob<TJob> where TJob : class
{
    /// <summary>
    /// Работа выполняемая фоновой службой.
    /// </summary>
    /// <param name="jobId">id фоновой службы.</param>
    /// <param name="token"><see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    public Task ExecuteAsync(string jobId, CancellationToken token = default);
}


namespace TeamRotationActivity.Jobs.Jobs.Interfaces;

/// <summary>
/// Интерфейс фоновой службы.
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


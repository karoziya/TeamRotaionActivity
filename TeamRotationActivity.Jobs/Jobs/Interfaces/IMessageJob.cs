namespace TeamRotationActivity.Jobs.Jobs.Interfaces;

public interface IMessageJob
{
    /// <summary>
    /// Работа выполняемая фоновой службой.
    /// </summary>
    /// <param name="jobId">id фоновой службы.</param>
    /// <param name="message">Отправляемое сообщение.</param>
    void ExecuteAsync(string jobId, string message);
}


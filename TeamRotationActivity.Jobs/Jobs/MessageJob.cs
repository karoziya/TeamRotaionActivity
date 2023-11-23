using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Jobs.Jobs.Interfaces;

namespace TeamRotationActivity.Jobs.Jobs;

public class MessageJob : IMessageJob
{
    private readonly IMessageSenderService _messageSenderService;

    public MessageJob(IMessageSenderService messageSenderService)
    {
        _messageSenderService = messageSenderService;
    }

    public void ExecuteAsync(string jobId, string message)
    {
        _messageSenderService.SendMessage(message);
    }
}

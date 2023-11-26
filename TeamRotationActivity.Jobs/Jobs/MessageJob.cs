using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamRotationActivity.Domain.Interfaces.Jobs;
using TeamRotationActivity.Domain.Interfaces.Services;

namespace TeamRotationActivity.Jobs.Jobs
{
    internal class MessageJob : IJob<MessageJob>
    {
        private string message;

        private IMessageSenderService messageSenderService;
        public Task ExecuteAsync(CancellationToken token = default)
        {
            messageSenderService.SendMessage(message);
            return Task.CompletedTask;
        }

        public MessageJob(IMessageSenderService messageService) 
        { 
            this.message = "Message";
            this.messageSenderService = messageService;
        }
    }
}

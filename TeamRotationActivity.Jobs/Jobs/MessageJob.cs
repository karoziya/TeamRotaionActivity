using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamRotationActivity.Domain.Interfaces.Jobs;

namespace TeamRotationActivity.Jobs.Jobs
{
    internal class MessageJob : IJob<MessageJob>
    {
        private string message;
        public Task ExecuteAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public MessageJob(string Message) 
        { 
            this.message = Message;
        }
    }
}

using System.Collections.Generic;

namespace Futures.Infrastructure.MessageQueue
{
    public interface IMessage
    {
        string From { get; set; }

        string To { get; set; }

        Dictionary<string, dynamic> Body { get; set; }
    }
}
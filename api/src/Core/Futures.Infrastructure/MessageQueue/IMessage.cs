﻿using System.Collections.Generic;

namespace Futures.Infrastructure.MessageQueue
{
    public interface IMessage
    {
        string System { get; set; }

        string Source { get; set; }

        string Destination { get; set; }

        Dictionary<string, dynamic> Body { get; set; }
    }
}
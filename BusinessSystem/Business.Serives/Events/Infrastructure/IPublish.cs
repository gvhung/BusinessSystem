﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Serives.Events
{
    public interface IEventPublisher
    {
        void Publish<T>(T eventMessage);
    }
}

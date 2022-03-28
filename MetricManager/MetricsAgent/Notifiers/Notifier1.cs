using System;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MetricsAgent.Notifiers
{
    public class Notifier1 : INotifier
    {
        public void Notify()
        {
            Debug.WriteLine("Debugging from Notifier 1");
        }

        public bool CanRun()
        {
            return true;
        }
    }
}

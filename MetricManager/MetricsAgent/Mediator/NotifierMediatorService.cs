using System;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Mediator
{
    public class NotifierMediatorService : INotifierMediatorService
    {
        private readonly IEnumerable<INotifier> _notifiers;
        public NotifierMediatorService(IEnumerable<INotifier> notifiers)
        {
            _notifiers = notifiers;
        }
        public void Notify()
        {
            _notifiers.Where(x => x.CanRun()).ToList().ForEach(x => x.Notify());
        }
    }
}

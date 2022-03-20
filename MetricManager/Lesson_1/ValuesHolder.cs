using System;
using System.Collections.Generic;

namespace MetricsManager
{
    public class ValuesHolder
    {
        private List<AgentInfo> _values = new List<AgentInfo>();
        public List<AgentInfo> Values
        {
            get => _values;
            set => _values = value;
        }
    }
}
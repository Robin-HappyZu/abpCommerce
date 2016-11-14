using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;

namespace HappyZu.CloudStore.StatisticalAnalysis.Events
{
    public class StatisticsEventHandler:
        IEventHandler<StatisticsSalesEventData>,
        IEventHandler<StatisticsUserEventData>, ITransientDependency
    {

        public void HandleEvent(StatisticsSalesEventData eventData)
        {

        }

        public void HandleEvent(StatisticsUserEventData eventData)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCQRS.Commands;
using SimpleCQRS.Events;
using SimpleCQRS.Queries;

namespace SimpleCQRS
{
    public class EventBroker
    {
        public List<BaseEvent> AllEvents = new List<BaseEvent>();

        public event EventHandler<BaseCommand> Commands;

        public event EventHandler<BaseQuery> Queries;

        public void ExecuteCommand(BaseCommand command)
        {
            Commands?.Invoke(this, command);
        }

        public T ExecuteQuery<T>(BaseQuery query)
        {
            Queries?.Invoke(this, query);
            return (T)query.Result;
        }

        public void UndoLastCommand()
        {
            var lastEvent = AllEvents.LastOrDefault();

            if(lastEvent != null)
            {
                var e = lastEvent as AgeChangedEvent;
                ExecuteCommand(new ChangeAgeCommand(e.Target, e.OldAge, false));

                AllEvents.Remove(lastEvent);
            }
        }
    }
}

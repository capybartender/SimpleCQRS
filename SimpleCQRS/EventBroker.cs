using SimpleCQRS.Commands;
using SimpleCQRS.Events;
using SimpleCQRS.Queries;

namespace SimpleCQRS;

public class EventBroker
{
    public List<BaseEvent> AllEvents = [];

    public event EventHandler<BaseCommand<Person>> Commands;

    public event EventHandler<BaseQuery> Queries;

    public void ExecuteCommand(BaseCommand<Person> command)
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

        if(lastEvent != null && lastEvent is StatusChangedEvent e)
        {
            ExecuteCommand(new ChangeStatusCommand(e.Target, e.OldStatus, register: false));

            AllEvents.Remove(lastEvent);
        }
    }
}

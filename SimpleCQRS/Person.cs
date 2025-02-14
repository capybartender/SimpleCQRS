using SimpleCQRS.Commands;
using SimpleCQRS.Events;
using SimpleCQRS.Queries;

namespace SimpleCQRS;

public class Person
{
    private int _status;

    private EventBroker broker;

    public Person(EventBroker eventBroker)
    {
        broker = eventBroker;

        broker.Commands += OnCommand;
        broker.Queries += OnQuery;
    }

    private void OnQuery(object sender, BaseQuery query)
    {
        if(query is GetStatusQuery && query.Target == this)
        {
            query.Result = _status;
        }
    }

    private void OnCommand(object sender, BaseCommand<Person> command)
    {
        if(command is ChangeStatusCommand cmd && command.Target == this)
        {
            var newStatus = cmd.Status;

            if (cmd.Register)
            {
                broker.AllEvents.Add(new StatusChangedEvent(this, _status, newStatus)); 
            }

            _status = newStatus;
        }
    }
}

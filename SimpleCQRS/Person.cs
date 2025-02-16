using SimpleCQRS.Commands;
using SimpleCQRS.Events;
using SimpleCQRS.Queries;

namespace SimpleCQRS;

public class Person
{
    private Status _status;

    private EventBroker _broker;

    public Person(EventBroker eventBroker)
    {
        _broker = eventBroker;
        _status = Status.ApplicationSent;

        _broker.Commands += OnCommand;
        _broker.Queries += OnQuery;
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
                _broker.AllEvents.Add(new StatusChangedEvent(this, _status, newStatus)); 
            }

            _status = newStatus;
        }
    }
}

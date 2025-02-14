using SimpleCQRS.Commands;
using SimpleCQRS.Events;
using SimpleCQRS.Queries;

namespace SimpleCQRS;

public class Person
{
    private int _age;

    private EventBroker broker;

    public Person(EventBroker eventBroker)
    {
        broker = eventBroker;

        broker.Commands += OnCommand;
        broker.Queries += OnQuery;
    }

    private void OnQuery(object sender, BaseQuery query)
    {
        if(query is GetAgeQuery && query.Target == this)
        {
            query.Result = _age;
        }
    }

    private void OnCommand(object sender, BaseCommand<Person> command)
    {
        if(command is ChangeAgeCommand cmd && command.Target == this)
        {
            var newAge = cmd.Age;

            if (cmd.Register)
            {
                broker.AllEvents.Add(new AgeChangedEvent(this, _age, newAge)); 
            }

            _age = newAge;
        }
    }
}

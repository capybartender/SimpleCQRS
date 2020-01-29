using SimpleCQRS.Commands;
using SimpleCQRS.Events;
using SimpleCQRS.Queries;

namespace SimpleCQRS
{
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

        private void OnCommand(object sender, BaseCommand command)
        {
            if(command is ChangeAgeCommand && command.Target == this)
            {
                var c = (ChangeAgeCommand)command;
                var newAge = c.Age;

                if (c.Register)
                {
                    broker.AllEvents.Add(new AgeChangedEvent(this, _age, newAge)); 
                }

                _age = newAge;
            }
        }
    }
}

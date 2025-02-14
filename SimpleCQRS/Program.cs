using SimpleCQRS.Commands;
using SimpleCQRS.Events;
using SimpleCQRS.Queries;

namespace SimpleCQRS;

public class Program
{
    static void Main(string[] args)
    {
        var eventBroker = new EventBroker();
        var person = new Person(eventBroker);

        var age = eventBroker.ExecuteQuery<int>(new GetAgeQuery(person));
        Console.WriteLine($"Age is {age}\n");

        eventBroker.ExecuteCommand(new ChangeAgeCommand(person, 2));
        eventBroker.ExecuteCommand(new ChangeAgeCommand(person, 4));
        eventBroker.ExecuteCommand(new ChangeAgeCommand(person, 8));
        eventBroker.ExecuteCommand(new ChangeAgeCommand(person, 16));
        eventBroker.ExecuteCommand(new ChangeAgeCommand(person, 32));
        eventBroker.ExecuteCommand(new ChangeAgeCommand(person, 64));

        PrintCommands(eventBroker.AllEvents);

        age = eventBroker.ExecuteQuery<int>(new GetAgeQuery(person));
        Console.WriteLine($"Age is {age}\n");

        eventBroker.UndoLastCommand();

        age = eventBroker.ExecuteQuery<int>(new GetAgeQuery(person));
        Console.WriteLine($"Age is {age}\n");

        PrintCommands(eventBroker.AllEvents);
    }

    private static void PrintAge(int age)
    {
        Console.WriteLine($"Current age is {age}");
    }

    private static void PrintCommands(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            var ageChangedEvent = @event as AgeChangedEvent;
            Console.WriteLine($"Old age: {ageChangedEvent.OldAge}, new age: {ageChangedEvent.NewAge}");
        }
        Console.WriteLine();
    }
}

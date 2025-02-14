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

        var status = eventBroker.ExecuteQuery<int>(new GetStatusQuery(person));
        Console.WriteLine($"Status is {status}\n");

        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, 2));
        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, 4));
        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, 8));
        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, 16));
        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, 32));
        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, 64));

        PrintCommands(eventBroker.AllEvents);

        status = eventBroker.ExecuteQuery<int>(new GetStatusQuery(person));
        Console.WriteLine($"Status is {status}\n");

        eventBroker.UndoLastCommand();

        status = eventBroker.ExecuteQuery<int>(new GetStatusQuery(person));
        Console.WriteLine($"Status is {status}\n");

        PrintCommands(eventBroker.AllEvents);
    }

    private static void PrintStatus(int status)
    {
        Console.WriteLine($"Current status is {status}");
    }

    private static void PrintCommands(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            var statusChangedEvent = @event as StatusChangedEvent;
            Console.WriteLine($"Old status: {statusChangedEvent.OldStatus}, new status: {statusChangedEvent.NewStatus}");
        }
        Console.WriteLine();
    }
}

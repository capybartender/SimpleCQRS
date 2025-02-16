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

        var status = eventBroker.ExecuteQuery<Status>(new GetStatusQuery(person));
        Console.WriteLine($"Status is {status}\n");

        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, Status.InvitedToJobShadowing));
        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, Status.BookedJobShadowing));
        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, Status.OnJobShadowing));
        eventBroker.ExecuteCommand(new ChangeStatusCommand(person, Status.Approved));

        PrintCommands(eventBroker.AllEvents);

        status = eventBroker.ExecuteQuery<Status>(new GetStatusQuery(person));
        Console.WriteLine($"\nLast status is {status}");

        Console.WriteLine("Undo last changes...");
        eventBroker.UndoLastCommand();

        status = eventBroker.ExecuteQuery<Status>(new GetStatusQuery(person));
        Console.WriteLine($"Last status is {status}\n");

        PrintCommands(eventBroker.AllEvents);
    }

    private static void PrintCommands(IEnumerable<BaseEvent> events)
    {
        Console.WriteLine("### All events: ###");
        for (int i = 0; i < events.Count(); i++)
        {
            var statusChangedEvent = events.ElementAt(i) as StatusChangedEvent;
            Console.WriteLine($"{i+1}: Old status: {statusChangedEvent.OldStatus}, new status: {statusChangedEvent.NewStatus}");
        }
        Console.WriteLine("###");
    }
}

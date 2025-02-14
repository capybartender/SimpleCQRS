namespace SimpleCQRS.Events;

public sealed class StatusChangedEvent(Person target, int oldStatus, int newStatus) : BaseEvent
{
    public Person Target { get; } = target;
    public int OldStatus { get; } = oldStatus;
    public int NewStatus { get; } = newStatus;
}

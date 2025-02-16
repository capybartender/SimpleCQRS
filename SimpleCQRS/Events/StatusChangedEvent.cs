namespace SimpleCQRS.Events;

public sealed class StatusChangedEvent(Person target, Status oldStatus, Status newStatus) : BaseEvent
{
    public Person Target { get; } = target;
    public Status OldStatus { get; } = oldStatus;
    public Status NewStatus { get; } = newStatus;
}

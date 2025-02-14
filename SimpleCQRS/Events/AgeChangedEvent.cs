namespace SimpleCQRS.Events;

public sealed class AgeChangedEvent(Person target, int oldAge, int newAge) : BaseEvent
{
    public Person Target { get; } = target;
    public int OldAge { get; } = oldAge;
    public int NewAge { get; } = newAge;
}

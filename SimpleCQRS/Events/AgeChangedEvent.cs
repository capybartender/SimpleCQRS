namespace SimpleCQRS.Events
{
    public class AgeChangedEvent : BaseEvent
    {
        public AgeChangedEvent(Person target, int oldAge, int newAge)
        {
            Target = target;
            OldAge = oldAge;
            NewAge = newAge;
        }

        public Person Target { get; }
        public int OldAge { get; }
        public int NewAge { get; }
    }
}

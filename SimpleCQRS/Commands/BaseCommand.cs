namespace SimpleCQRS.Commands;

public abstract class BaseCommand<T> : EventArgs where T : class
{
    public T Target { get; set; }
}

namespace SimpleCQRS.Commands;

public class ChangeStatusCommand : BaseCommand<Person>
{
    public ChangeStatusCommand(Person target, Status status, bool register = true)
    {
        Target = target;
        Status = status;
        Register = register;
    }

    public Status Status { get; }

    public bool Register { get; }
}

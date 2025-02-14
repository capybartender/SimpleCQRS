namespace SimpleCQRS.Commands;

public class ChangeStatusCommand : BaseCommand<Person>
{
    public ChangeStatusCommand(Person target, int status, bool register = true)
    {
        Target = target;
        Status = status;
        Register = register;
    }

    public int Status { get; }

    public bool Register { get; }
}

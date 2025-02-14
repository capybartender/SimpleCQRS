namespace SimpleCQRS.Commands;

public class ChangeAgeCommand : BaseCommand<Person>
{
    public ChangeAgeCommand(Person target, int age, bool register = true)
    {
        Target = target;
        Age = age;
        Register = register;
    }

    public int Age { get; }

    public bool Register { get; }
}

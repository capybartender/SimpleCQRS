namespace SimpleCQRS.Commands
{
    public class ChangeAgeCommand : BaseCommand
    {
        public ChangeAgeCommand(Person target, int age, bool register = true)
        {
            Target = target;
            Age = age;
            Register = register;
        }

        public int Age;

        public bool Register { get; }
    }
}

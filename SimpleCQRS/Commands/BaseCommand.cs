using System;

namespace SimpleCQRS.Commands
{
    public class BaseCommand : EventArgs
    {
        public Person Target;
    }
}

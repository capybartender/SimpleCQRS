namespace SimpleCQRS.Queries;

public abstract class BaseQuery
{
    public Person Target { get; set; }

    public object Result { get; set;  }
}

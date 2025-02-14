namespace SimpleCQRS.Queries;

public class GetStatusQuery : BaseQuery
{
    public GetStatusQuery(Person target)
    {
        Target = target;
    }
}

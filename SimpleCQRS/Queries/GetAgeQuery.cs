namespace SimpleCQRS.Queries
{
    public class GetAgeQuery : BaseQuery
    {
        public GetAgeQuery(Person target)
        {
            Target = target;
        }
    }
}

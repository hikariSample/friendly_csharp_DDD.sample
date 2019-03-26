namespace DDD.Core.Sample.Domain.Entity
{
    public class Teacher : IAggregateRoot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StudentCount { get; set; }
    }
}

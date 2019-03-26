namespace DDD.Core.Sample.Domain.Entity
{
    public class Student : IAggregateRoot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TeacherId { get; set; }
    }
}

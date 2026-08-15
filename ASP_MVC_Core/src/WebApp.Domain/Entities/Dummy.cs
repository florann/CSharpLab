namespace WebApp.Domain.Entities
{
    public record Dummy
    {
        public long Id { get; set; }
        public required string Field { get; set; }
        public required DateTime CreationDate { get; set; }
    }
}

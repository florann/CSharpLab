namespace CodeEditor.Domain.Entities.Base
{
    public class Entity
    {
        public long Id { get; }

        public long RowVersion { get; }

        public DateTimeOffset CreationDate { get; }

        public DateTimeOffset LastModificationDate { get; }
    }
}

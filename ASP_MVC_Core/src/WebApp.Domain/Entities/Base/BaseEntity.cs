namespace WebApp.Domain.Entities.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public long RowVersion { get; set; }
        public bool IsActive { get; set; }
    }
}

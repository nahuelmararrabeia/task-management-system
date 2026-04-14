namespace TaskManagement.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public Guid? ModifiedByUserId { get; private set; }

        public void SetCreated(Guid userId)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedByUserId = userId;
        }

        public void SetModified(Guid userId)
        {
            ModifiedAt = DateTime.UtcNow;
            ModifiedByUserId = userId;
        }
    }
}

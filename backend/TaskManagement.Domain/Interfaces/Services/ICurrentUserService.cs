namespace TaskManagement.Domain.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
    }
}

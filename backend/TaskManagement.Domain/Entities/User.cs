using TaskManagement.Domain.Common;

public class User : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public User(string email, string name, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException("Email is required");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Name is required");

        Id = Guid.NewGuid();
        Email = email;
        Name = name;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Name is required");

        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException("Email is required");

        Name = name;
        Email = email;
    }

    public void Delete()
    {
        if (IsDeleted)
            return;

        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}
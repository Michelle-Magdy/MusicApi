using MusicApi.MusicApi.Domain.Exceptions;

namespace MusicApi.MusicApi.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<PlayList> PlayLists { get; set; } = [];

        private User() { }

        public static User create(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new DomainException("A valid email is required.");

            return new User
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Email = email.Trim().ToLowerInvariant(),
                CreatedAt = DateTime.UtcNow
            };
        }

    }
}

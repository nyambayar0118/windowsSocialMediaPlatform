using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.User
{
    /// <summary>
    /// User-ийн үндсэн абстракт класс
    /// </summary>
    public abstract class UserBase
    {
        /// <summary>ID дугаар</summary>
        public required UserId Id { get; init; }
        /// <summary>Хэрэглэгчийн нэр</summary>
        public required string Username { get; set; }
        /// <summary>Цахим шуудан хаяг</summary>
        public required string Email { get; set; }
        /// <summary>Нууц үг</summary>
        public required string Password { get; set; }
        /// <summary>Хэрэглэгч үүсгэсэн огноо</summary>
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}
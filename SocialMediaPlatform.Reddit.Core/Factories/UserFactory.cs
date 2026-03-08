using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.User;
using SocialMediaPlatform.Reddit.Core.Domain.User;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Factories
{
    /// <summary>
    /// Reddit платформын User объектуудыг үүсгэх Factory класс
    /// </summary>
    public class UserFactory
    {
        /// <summary>
        /// User төрлөөр User объект үүсгэх
        /// </summary>
        /// <param name="type">Хэрэглэгчийн төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="email">Цахим шуудан</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Үүсгэгдсэн User объект</returns>
        public User<UserType> Create(
            UserType type,
            UserId userId,
            string username,
            string email,
            string password) => type switch
            {
                UserType.Normal => new NormalUser
                {
                    Id = userId,
                    Username = username,
                    Email = email,
                    Password = password,
                    Type = UserType.Normal
                },
                UserType.Admin => new AdminUser
                {
                    Id = userId,
                    Username = username,
                    Email = email,
                    Password = password,
                    Type = UserType.Admin,
                    Privileges = new List<Privilege> { Privilege.Admin }
                },
                _ => throw new ArgumentException($"Тодорхойгүй User төрөл: {type}")
            };
    }
}
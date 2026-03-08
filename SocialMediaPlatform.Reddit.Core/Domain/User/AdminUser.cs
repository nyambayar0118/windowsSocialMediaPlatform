using SocialMediaPlatform.Core.Domain.User;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Domain.User
{
    /// <summary>
    /// Админ хэрэглэгчийн класс
    /// </summary>
    public class AdminUser : User<UserType>
    {
        /// <summary>Админы эрхүүд</summary>
        public List<Privilege> Privileges { get; set; } = new List<Privilege>();
    }
}
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.User
{
    /// <summary>
    /// Post-ийн үндсэн абстракт ерөнхий төрлийн класс
    /// </summary>
    public abstract class User<TUserType> : UserBase
    {
        public required TUserType Type { get; init; }
    }
}
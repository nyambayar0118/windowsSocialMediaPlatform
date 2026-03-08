using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.DTO
{
    /// <summary>
    /// User классын объектын Data Transfer Object
    /// </summary>
    /// <param name="Id">ID дугаар</param>
    /// <param name="Username">Хэрэглэгчийн нэр</param>
    /// <param name="Email">Хэрэглэгчийн цахим шуудан хаяг</param>
    /// <param name="Type">Хэрэглэгчийн төрөл</param>
    /// <param name="CreatedAt">Хэрэглэгч үүссэн огноо</param>
    public record UserDTO(
        UserId Id,
        string Username,
        string Email,
        string Type,
        DateTime CreatedAt
    );
}

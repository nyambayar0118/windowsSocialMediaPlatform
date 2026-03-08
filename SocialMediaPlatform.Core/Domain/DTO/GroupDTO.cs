using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.DTO
{
    /// <summary>
    /// Group классын объектын Data Transfer Object
    /// </summary>
    /// <param name="Id">ID дугаар</param>
    /// <param name="Name">Нэр</param>
    /// <param name="Description">Дэлгэрэнгүй</param>
    /// <param name="OwnerId">Group-ийн эзэмшигчийн ID дугаар</param>
    public record GroupDTO(
        GroupId Id,
        string Name,
        string Description,
        UserId OwnerId
    );
}

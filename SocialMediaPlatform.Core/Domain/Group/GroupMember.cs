using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Group
{
    /// <summary>
    /// Group-ийн гишүүнчлэлийн үндсэн класс
    /// </summary>
    public class GroupMember<TPrivilege>
    {
        /// <summary>Group-ийн ID дугаар</summary>
        public required GroupId GroupId { get; init; }

        /// <summary>Гишүүний ID дугаар</summary>
        public required UserId UserId { get; init; }

        /// <summary>Гишүүний эрх</summary>
        public required TPrivilege Role { get; set; }

        /// <summary>Group-т элссэн огноо</summary>
        public DateTime JoinedAt { get; init; } = DateTime.Now;
    }
}
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Group
{
    /// <summary>
    /// Group-ийн гишүүнчлэлийн үндсэн класс
    /// </summary>
    public abstract class GroupMemberBase
    {
        /// <summary>Group-ийн ID дугаар</summary>
        public required GroupId GroupId { get; init; }
        /// <summary>Гишүүний ID дугаар</summary>
        public required UserId UserId { get; init; }
        /// <summary>Group-т элссэн огноо</summary>
        public DateTime JoinedAt { get; init; } = DateTime.Now;
    }
}
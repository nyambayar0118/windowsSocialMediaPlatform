using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Group
{
    /// <summary>
    /// Group-ийн үндсэн класс
    /// </summary>
    public class Group
    {
        /// <summary>
        /// ID дугаар
        /// </summary>
        public required GroupId Id { get; init; }
        /// <summary>
        /// Group-ийн нэр
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Group-ийн дэлгэрэнгүй
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        /// Group-ийн үүсгэсэн огноо
        /// </summary>
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        /// <summary>
        /// Group-ийн эзэмшигчийн ID дугаар
        /// </summary>
        public required UserId OwnerId { get; set; }
    }
}

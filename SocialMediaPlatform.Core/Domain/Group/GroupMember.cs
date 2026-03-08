using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Group
{
    /// <summary>
    /// Group-ийн гишүүнчлэлийн үндсэн ерөнхий төрлийн класс
    /// </summary>
    public class GroupMember<TPrivilege> : GroupMemberBase
    {
        /// <summary>Гишүүний эрх</summary>
        public required TPrivilege Role { get; set; }
    }
}
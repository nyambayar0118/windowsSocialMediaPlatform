using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Reddit.Core.Enum;
using CoreGroup = SocialMediaPlatform.Core.Domain.Group;

namespace SocialMediaPlatform.Reddit.Core.Domain.Group
{
    /// <summary>
    /// Reddit платформын групп - Subreddit класс
    /// </summary>
    public class Subreddit : CoreGroup.Group
    {
        /// <summary>Subreddit-ийн дүрэм журам</summary>
        public string Rules { get; set; } = string.Empty;

        /// <summary>Насанд хүрэгчдэд зориулсан контент мөн эсэх</summary>
        public bool IsNSFW { get; set; } = false;

        /// <summary>Гишүүнчлэлийн доод насны шаардлага (хоног)</summary>
        public int MinAccountAgeDays { get; set; } = 0;
    }
}
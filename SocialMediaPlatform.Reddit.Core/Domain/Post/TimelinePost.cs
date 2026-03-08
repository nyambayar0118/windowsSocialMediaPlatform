using SocialMediaPlatform.Core.Domain.Post;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Domain.Post
{
    /// <summary>
    /// Хэрэглэгчийн timeline-д нийтлэгдэх Post класс
    /// </summary>
    public class TimelinePost : Post<PostType>
    {
        /// <summary>Гарчиг</summary>
        public required string Title { get; set; }

        /// <summary>Агуулга</summary>
        public string Content { get; set; } = string.Empty;
    }
}
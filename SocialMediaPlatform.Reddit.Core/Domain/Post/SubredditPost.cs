using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Post;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Domain.Post
{
    /// <summary>
    /// Subreddit-д нийтлэгдэх Post класс
    /// </summary>
    public class SubredditPost : Post<PostType>
    {
        /// <summary>Харьяалагдах Subreddit-ийн ID дугаар</summary>
        public required GroupId SubredditId { get; init; }

        /// <summary>Гарчиг</summary>
        public required string Title { get; set; }

        /// <summary>Агуулга</summary>
        public string Content { get; set; } = string.Empty;
    }
}
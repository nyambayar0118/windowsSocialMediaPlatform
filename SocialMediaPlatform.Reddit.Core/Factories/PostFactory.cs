using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Post;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Reddit.Core.Domain.Post;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Factories
{
    /// <summary>
    /// Reddit платформын Post объектуудыг үүсгэх Factory класс
    /// </summary>
    public class PostFactory
    {
        /// <summary>
        /// Post төрлөөр Post объект үүсгэх
        /// </summary>
        /// <param name="type">Post-ийн төрөл</param>
        /// <param name="authorId">Бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <param name="title">Гарчиг</param>
        /// <param name="content">Агуулга</param>
        /// <param name="subredditId">Subreddit-ийн ID дугаар (зөвхөн SubredditPost-д)</param>
        /// <returns>Үүсгэгдсэн Post объект</returns>
        public Post<PostType> Create(
            PostType type,
            UserId authorId,
            PostId postId,
            string title,
            string content,
            GroupId? subredditId = null) => type switch
            {
                PostType.Timeline => new TimelinePost
                {
                    Id = postId,
                    AuthorId = authorId,
                    Title = title,
                    Content = content,
                    Type = PostType.Timeline,
                    Visibility = VisibilityType.Public
                },
                PostType.Subreddit => new SubredditPost
                {
                    Id = postId,
                    AuthorId = authorId,
                    Title = title,
                    Content = content,
                    Type = PostType.Subreddit,
                    Visibility = VisibilityType.Public,
                    SubredditId = subredditId ?? throw new ArgumentException("SubredditPost үүсгэхэд SubredditId шаардлагатай")
                },
                _ => throw new ArgumentException($"Тодорхойгүй Post төрөл: {type}")
            };
    }
}
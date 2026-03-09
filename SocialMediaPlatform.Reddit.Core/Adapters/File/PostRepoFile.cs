using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Post;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.Post;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Post-ийн файл дээр суурилсан Repository адаптер
    /// </summary>
    public class PostRepoFile : IPostRepoPort
    {
        private readonly string _filePath;

        public PostRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Post хадгалах</summary>
        public void Save(PostBase post)
        {
            var line = Serialize(post);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>ID-аар Post хайх</summary>
        public PostBase FindById(PostId postId)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var post = Deserialize(line);
                if (post.Id.Value == postId.Value)
                    return post;
            }
            throw new KeyNotFoundException($"Post ID {postId.Value} not found");
        }

        public List<PostBase> GetAll()
        {
            var results = new List<PostBase>();
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var post = Deserialize(line);
                results.Add(post);
            }
            return results;
        }
        

        /// <summary>Хэрэглэгчийн Post-уудыг авах</summary>
        public List<PostBase> FindByAuthor(UserId userId)
        {
            var results = new List<PostBase>();
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var post = Deserialize(line);
                if (post.AuthorId.Value == userId.Value)
                    results.Add(post);
            }
            return results;
        }

        /// <summary>Post шинэчлэх</summary>
        public void Update(PostBase post)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) == post.Id.Value
                        ? Serialize(post)
                        : line;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Post устгах</summary>
        public void Delete(PostId postId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) != postId.Value;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Post объектыг мөр болгох</summary>
        private static string Serialize(PostBase post)
        {
            if (post is TimelinePost tp)
                return $"{tp.Id.Value}|{tp.AuthorId.Value}|{tp.Visibility}|{tp.CreatedAt:O}|Timeline|{tp.Title}|{tp.Content}|";

            if (post is SubredditPost sp)
                return $"{sp.Id.Value}|{sp.AuthorId.Value}|{sp.Visibility}|{sp.CreatedAt:O}|Subreddit|{sp.Title}|{sp.Content}|{sp.SubredditId.Value}";

            throw new ArgumentException($"Undefined Post type: {post.GetType().Name}");
        }

        /// <summary>Мөрийг Post объект болгох</summary>
        private static PostBase Deserialize(string line)
        {
            var parts = line.Split('|');
            var id = new PostId { Value = uint.Parse(parts[0]) };
            var authorId = new UserId { Value = uint.Parse(parts[1]) };
            var visibility = System.Enum.Parse<VisibilityType>(parts[2]);
            var createdAt = DateTime.Parse(parts[3]);
            var type = parts[4];
            var title = parts[5];
            var content = parts[6];

            if (type == "Timeline")
                return new TimelinePost
                {
                    Id = id,
                    AuthorId = authorId,
                    Visibility = visibility,
                    CreatedAt = createdAt,
                    Type = PostType.Timeline,
                    Title = title,
                    Content = content
                };

            if (type == "Subreddit")
                return new SubredditPost
                {
                    Id = id,
                    AuthorId = authorId,
                    Visibility = visibility,
                    CreatedAt = createdAt,
                    Type = PostType.Subreddit,
                    Title = title,
                    Content = content,
                    SubredditId = new GroupId { Value = uint.Parse(parts[7]) }
                };

            throw new ArgumentException($"Undefined Post type: {type}");
        }
    }
}
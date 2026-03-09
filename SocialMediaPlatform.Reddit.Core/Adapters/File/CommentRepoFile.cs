using SocialMediaPlatform.Core.Domain.Comment;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.Comment;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Comment-ийн файл дээр суурилсан Repository адаптер
    /// </summary>
    public class CommentRepoFile : ICommentRepoPort
    {
        private readonly string _filePath;

        public CommentRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Comment хадгалах</summary>
        public void Save(CommentBase comment)
        {
            var line = Serialize(comment);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>ID-аар Comment хайх</summary>
        public CommentBase FindById(CommentId commentId)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var comment = Deserialize(line);
                if (comment.Id.Value == commentId.Value)
                    return comment;
            }
            throw new KeyNotFoundException($"Comment ID {commentId.Value} not found");
        }

        /// <summary>Post-ийн үндсэн Comment-уудыг авах</summary>
        public List<CommentBase> FindByPost(PostId postId)
        {
            var results = new List<CommentBase>();
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var comment = Deserialize(line);
                if (comment is MainComment mc && mc.PostId.Value == postId.Value)
                    results.Add(comment);
            }
            return results;
        }

        /// <summary>Comment-ийн хариунуудыг авах</summary>
        public List<CommentBase> FindByParent(CommentId commentId)
        {
            var results = new List<CommentBase>();
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var comment = Deserialize(line);
                if (comment is ReplyComment rc && rc.ParentCommentId.Value == commentId.Value)
                    results.Add(comment);
            }
            return results;
        }

        /// <summary>Comment устгах</summary>
        public void Delete(CommentId commentId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) != commentId.Value;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Comment объектыг мөр болгох</summary>
        private static string Serialize(CommentBase comment)
        {
            if (comment is MainComment mc)
                return $"{mc.Id.Value}|{mc.AuthorId.Value}|{mc.Content}|{mc.CreatedAt:O}|Main|{mc.PostId.Value}|";

            if (comment is ReplyComment rc)
                return $"{rc.Id.Value}|{rc.AuthorId.Value}|{rc.Content}|{rc.CreatedAt:O}|Reply||{rc.ParentCommentId.Value}";

            throw new ArgumentException($"Undefined Comment type: {comment.GetType().Name}");
        }

        /// <summary>Мөрийг Comment объект болгох</summary>
        private static CommentBase Deserialize(string line)
        {
            var parts = line.Split('|');
            var id = new CommentId { Value = uint.Parse(parts[0]) };
            var authorId = new UserId { Value = uint.Parse(parts[1]) };
            var content = parts[2];
            var createdAt = DateTime.Parse(parts[3]);
            var type = parts[4];

            if (type == "Main")
                return new MainComment
                {
                    Id = id,
                    AuthorId = authorId,
                    Content = content,
                    CreatedAt = createdAt,
                    Type = CommentType.Main,
                    PostId = new PostId { Value = uint.Parse(parts[5]) }
                };

            if (type == "Reply")
                return new ReplyComment
                {
                    Id = id,
                    AuthorId = authorId,
                    Content = content,
                    CreatedAt = createdAt,
                    Type = CommentType.Reply,
                    ParentCommentId = new CommentId { Value = uint.Parse(parts[6]) }
                };

            throw new ArgumentException($"Undefined Comment type: {type}");
        }
    }
}
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Reaction;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.Reaction;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Reaction-ий файл дээр суурилсан Repository адаптер
    /// </summary>
    public class ReactionRepoFile : IReactionRepoPort
    {
        private readonly string _filePath;

        public ReactionRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Reaction хадгалах</summary>
        public void Save(ReactionBase reaction)
        {
            var line = Serialize(reaction);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>Reaction устгах</summary>
        public void Delete(uint targetId, UserId authorId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return !(uint.Parse(parts[0]) == targetId &&
                             uint.Parse(parts[2]) == authorId.Value);
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Зорилтот объектын reaction-ийн тоог авах</summary>
        public Dictionary<string, uint> CountByTarget(uint targetId, ReactionTargetType targetType)
        {
            var counts = new Dictionary<string, uint>();
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (uint.Parse(parts[0]) == targetId &&
                    System.Enum.Parse<ReactionTargetType>(parts[1]) == targetType)
                {
                    var reactionType = parts[4];
                    if (!counts.ContainsKey(reactionType))
                        counts[reactionType] = 0;
                    counts[reactionType]++;
                }
            }
            return counts;
        }

        /// <summary>Хэрэглэгч reaction хийсэн эсэхийг шалгах</summary>
        public bool ExistsByUserAndTarget(UserId userId, uint targetId, ReactionTargetType targetType)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (uint.Parse(parts[0]) == targetId &&
                    System.Enum.Parse<ReactionTargetType>(parts[1]) == targetType &&
                    uint.Parse(parts[2]) == userId.Value)
                    return true;
            }
            return false;
        }

        /// <summary>Reaction объектыг мөр болгох</summary>
        private static string Serialize(ReactionBase reaction)
        {
            if (reaction is Upvote up)
                return $"{up.TargetId}|{up.TargetType}|{up.AuthorId.Value}|{up.CreatedAt:O}|Upvote";

            if (reaction is Downvote down)
                return $"{down.TargetId}|{down.TargetType}|{down.AuthorId.Value}|{down.CreatedAt:O}|Downvote";

            throw new ArgumentException($"Тодорхойгүй Reaction төрөл: {reaction.GetType().Name}");
        }

        /// <summary>Мөрийг Reaction объект болгох</summary>
        private static ReactionBase Deserialize(string line)
        {
            var parts = line.Split('|');
            var targetId = uint.Parse(parts[0]);
            var targetType = System.Enum.Parse<ReactionTargetType>(parts[1]);
            var authorId = new UserId { Value = uint.Parse(parts[2]) };
            var createdAt = DateTime.Parse(parts[3]);
            var reactionType = parts[4];

            if (reactionType == "Upvote")
                return new Upvote
                {
                    TargetId = targetId,
                    TargetType = targetType,
                    AuthorId = authorId,
                    CreatedAt = createdAt,
                    Type = ReactionType.Upvote
                };

            if (reactionType == "Downvote")
                return new Downvote
                {
                    TargetId = targetId,
                    TargetType = targetType,
                    AuthorId = authorId,
                    CreatedAt = createdAt,
                    Type = ReactionType.Downvote
                };

            throw new ArgumentException($"Тодорхойгүй Reaction төрөл: {reactionType}");
        }
    }
}
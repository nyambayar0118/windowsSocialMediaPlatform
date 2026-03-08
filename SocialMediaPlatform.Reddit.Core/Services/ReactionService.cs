using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Reaction;
using SocialMediaPlatform.Core.Ports.Input;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.Reaction;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Services
{
    /// <summary>
    /// Reaction-ий үйлдлүүдийг гүйцэтгэх Service класс
    /// </summary>
    public class ReactionService : IReactionServicePort
    {
        private readonly IReactionRepoPort _repo;

        /// <summary>
        /// ReactionService үүсгэх
        /// </summary>
        /// <param name="repo">Reaction-ий Repository адаптер</param>
        public ReactionService(IReactionRepoPort repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Reaction нэмэх
        /// </summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="reactionType">Reaction-ий төрөл</param>
        public void React(uint targetId, ReactionTargetType targetType, UserId userId, string reactionType)
        {
            if (_repo.ExistsByUserAndTarget(userId, targetId, targetType))
                _repo.Delete(targetId, userId);

            var type = System.Enum.Parse<ReactionType>(reactionType);
            ReactionBase reaction = type switch
            {
                ReactionType.Upvote => new Upvote
                {
                    TargetId = targetId,
                    TargetType = targetType,
                    AuthorId = userId,
                    Type = ReactionType.Upvote
                },
                ReactionType.Downvote => new Downvote
                {
                    TargetId = targetId,
                    TargetType = targetType,
                    AuthorId = userId,
                    Type = ReactionType.Downvote
                },
                _ => throw new ArgumentException($"Undefined Reaction type: {reactionType}")
            };

            _repo.Save(reaction);
        }

        /// <summary>
        /// Reaction устгах
        /// </summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void Unreact(uint targetId, ReactionTargetType targetType, UserId userId)
        {
            if (!_repo.ExistsByUserAndTarget(userId, targetId, targetType))
                throw new InvalidOperationException("Reaction олдсонгүй");
            _repo.Delete(targetId, userId);
        }

        /// <summary>
        /// Reaction-ий тоог авах
        /// </summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <returns>Reaction-ий төрөл тус бүрийн тоо</returns>
        public Dictionary<string, uint> GetReactionCount(uint targetId, ReactionTargetType targetType)
        {
            return _repo.CountByTarget(targetId, targetType);
        }

        /// <summary>
        /// Хэрэглэгч Reaction хийсэн эсэхийг шалгах
        /// </summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Reaction хийсэн бол true, үгүй бол false</returns>
        public bool HasReacted(uint targetId, ReactionTargetType targetType, UserId userId)
        {
            return _repo.ExistsByUserAndTarget(userId, targetId, targetType);
        }
    }
}
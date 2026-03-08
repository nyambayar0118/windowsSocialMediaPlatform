using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Adapters.IdGenerator
{
    /// <summary>
    /// Дараалсан ID үүсгэгч адаптер
    /// </summary>
    public class SequentialIdGenerator : IIdGeneratorPort
    {
        private readonly ISequentialIdRepoPort _repo;

        public SequentialIdGenerator(ISequentialIdRepoPort repo)
        {
            _repo = repo;
        }

        /// <summary>Хэрэглэгчийн ID үүсгэх</summary>
        public UserId NextUserId()
        {
            var next = _repo.GetLastId(IdEntityType.User) + 1;
            _repo.SaveLastId(IdEntityType.User, next);
            return new UserId { Value = next };
        }

        /// <summary>Post-ийн ID үүсгэх</summary>
        public PostId NextPostId()
        {
            var next = _repo.GetLastId(IdEntityType.Post) + 1;
            _repo.SaveLastId(IdEntityType.Post, next);
            return new PostId { Value = next };
        }

        /// <summary>Группын ID үүсгэх</summary>
        public GroupId NextGroupId()
        {
            var next = _repo.GetLastId(IdEntityType.Group) + 1;
            _repo.SaveLastId(IdEntityType.Group, next);
            return new GroupId { Value = next };
        }

        /// <summary>Comment-ийн ID үүсгэх</summary>
        public CommentId NextCommentId()
        {
            var next = _repo.GetLastId(IdEntityType.Comment) + 1;
            _repo.SaveLastId(IdEntityType.Comment, next);
            return new CommentId { Value = next };
        }
    }
}
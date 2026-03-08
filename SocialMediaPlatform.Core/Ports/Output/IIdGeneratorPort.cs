using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Output
{
    /// <summary>
    /// ID үүсгэгчийн Output Port интерфейс
    /// </summary>
    public interface IIdGeneratorPort
    {
        /// <summary>Хэрэглэгчийн ID үүсгэх</summary>
        /// <returns>Шинэ UserId</returns>
        public UserId NextUserId();

        /// <summary>Post-ийн ID үүсгэх</summary>
        /// <returns>Шинэ PostId</returns>
        public PostId NextPostId();

        /// <summary>Gruppийн ID үүсгэх</summary>
        /// <returns>Шинэ GroupId</returns>
        public GroupId NextGroupId();

        /// <summary>Comment-ийн ID үүсгэх</summary>
        /// <returns>Шинэ CommentId</returns>
        public CommentId NextCommentId();
    }
}
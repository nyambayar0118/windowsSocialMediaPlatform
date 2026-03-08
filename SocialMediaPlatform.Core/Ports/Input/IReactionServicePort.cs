using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Enum;

namespace SocialMediaPlatform.Core.Ports.Input
{
    /// <summary>
    /// Reaction-ий үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface IReactionServicePort
    {
        /// <summary>Reaction нэмэх</summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="reactionType">Reaction-ий төрөл</param>
        public void React(uint targetId, ReactionTargetType targetType, UserId userId, string reactionType);

        /// <summary>Reaction устгах</summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void Unreact(uint targetId, ReactionTargetType targetType, UserId userId);

        /// <summary>Reaction-ий тоог авах</summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <returns>Reaction-ий төрөл тус бүрийн тоо</returns>
        public Dictionary<string, uint> GetReactionCount(uint targetId, ReactionTargetType targetType);

        /// <summary>Хэрэглэгч reaction хийсэн эсэхийг шалгах</summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Reaction хийсэн бол true, үгүй бол false</returns>
        public bool HasReacted(uint targetId, ReactionTargetType targetType, UserId userId);
    }
}
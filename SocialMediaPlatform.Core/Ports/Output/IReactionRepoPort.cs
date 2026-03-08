using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Reaction;

namespace SocialMediaPlatform.Core.Ports.Output
{
    /// <summary>
    /// Reaction-ий Repository-н Output Port интерфейс
    /// </summary>
    public interface IReactionRepoPort
    {
        /// <summary>Reaction хадгалах</summary>
        /// <param name="reaction">Хадгалах reaction-ий объект</param>
        public void Save(Reaction<object> reaction);

        /// <summary>Reaction устгах</summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="authorId">Хэрэглэгчийн ID дугаар</param>
        public void Delete(uint targetId, UserId authorId);

        /// <summary>Зорилтот объектын reaction-ийн тоог авах</summary>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <returns>Reaction-ий төрөл тус бүрийн тоо</returns>
        public Dictionary<string, uint> CountByTarget(uint targetId, ReactionTargetType targetType);

        /// <summary>Хэрэглэгч reaction хийсэн эсэхийг шалгах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="targetId">Зорилтот объектын ID дугаар</param>
        /// <param name="targetType">Зорилтот объектын төрөл</param>
        /// <returns>Reaction хийсэн бол true, үгүй бол false</returns>
        public bool ExistsByUserAndTarget(UserId userId, uint targetId, ReactionTargetType targetType);
    }
}
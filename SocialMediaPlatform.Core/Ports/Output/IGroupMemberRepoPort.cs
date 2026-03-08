using SocialMediaPlatform.Core.Domain.Group;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Output
{
    /// <summary>
    /// Группын гишүүний Repository-н Output Port интерфейс
    /// </summary>
    public interface IGroupMemberRepoPort
    {
        /// <summary>Гишүүн хадгалах</summary>
        /// <param name="groupMember">Хадгалах гишүүний объект</param>
        public void Save(GroupMember<object> groupMember);

        /// <summary>Гишүүн устгах</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void Delete(GroupId groupId, UserId userId);

        /// <summary>Группын гишүүдийг авах</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <returns>Гишүүний объектын жагсаалт</returns>
        public List<GroupMember<object>> FindByGroup(GroupId groupId);

        /// <summary>Хэрэглэгчийн группуудыг авах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Гишүүний объектын жагсаалт</returns>
        public List<GroupMember<object>> FindByUser(UserId userId);
    }
}
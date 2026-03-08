using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Input
{
    /// <summary>
    /// Группын гишүүний үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface IGroupMemberServicePort
    {
        /// <summary>Групп нэгдэх</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void Join(GroupId groupId, UserId userId);

        /// <summary>Группаас гарах</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void Leave(GroupId groupId, UserId userId);

        /// <summary>Группын гишүүдийг авах</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <returns>Хэрэглэгчийн DTO жагсаалт</returns>
        public List<UserDTO> GetMembers(GroupId groupId);

        /// <summary>Хэрэглэгчийн группуудыг авах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Gruppийн DTO жагсаалт</returns>
        public List<GroupDTO> GetGroups(UserId userId);
    }
}
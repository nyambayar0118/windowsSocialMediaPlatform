using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Input
{
    /// <summary>
    /// Группын үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface IGroupServicePort
    {
        /// <summary>Групп үүсгэх</summary>
        /// <param name="name">Gruppийн нэр</param>
        /// <param name="description">Gruppийн тайлбар</param>
        /// <param name="ownerId">Эзэмшигчийн ID дугаар</param>
        /// <returns>Үүсгэгдсэн группын DTO</returns>
        public GroupDTO CreateGroup(string name, string description, UserId ownerId);

        /// <summary>Групп устгах</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        public void DeleteGroup(GroupId groupId);

        /// <summary>Группын мэдээлэл засах</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <param name="name">Шинэ нэр</param>
        /// <param name="description">Шинэ тайлбар</param>
        /// <returns>Засагдсан gruppийн DTO</returns>
        public GroupDTO EditGroup(GroupId groupId, string name, string description);

        /// <summary>Группын мэдээлэл авах</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <returns>Gruppийн DTO</returns>
        public GroupDTO GetGroup(GroupId groupId);
    }
}
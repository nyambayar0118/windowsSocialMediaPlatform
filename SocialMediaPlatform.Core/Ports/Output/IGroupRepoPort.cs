using SocialMediaPlatform.Core.Domain.Group;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Output
{
    /// <summary>
    /// Группын Repository-н Output Port интерфейс
    /// </summary>
    public interface IGroupRepoPort
    {
        /// <summary>Групп хадгалах</summary>
        /// <param name="group">Хадгалах группын объект</param>
        public void Save(Group group);

        /// <summary>ID-аар групп хайх</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <returns>Олдсон gruppийн объект</returns>
        public Group FindById(GroupId groupId);

        /// <summary>Gruppийн мэдээлэл шинэчлэх</summary>
        /// <param name="group">Шинэчлэх gruppийн объект</param>
        public void Update(Group group);

        /// <summary>Групп устгах</summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        public void Delete(GroupId groupId);
    }
}
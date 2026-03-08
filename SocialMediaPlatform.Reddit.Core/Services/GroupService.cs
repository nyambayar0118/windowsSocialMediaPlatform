using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Group;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Ports.Input;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.Group;

namespace SocialMediaPlatform.Reddit.Core.Services
{
    /// <summary>
    /// Группын үйлдлүүдийг гүйцэтгэх Service класс
    /// </summary>
    public class GroupService : IGroupServicePort
    {
        private readonly IGroupRepoPort _repo;
        private readonly IIdGeneratorPort _idGenerator;

        /// <summary>
        /// GroupService үүсгэх
        /// </summary>
        /// <param name="repo">Группын Repository адаптер</param>
        /// <param name="idGenerator">ID үүсгэгч</param>
        public GroupService(IGroupRepoPort repo, IIdGeneratorPort idGenerator)
        {
            _repo = repo;
            _idGenerator = idGenerator;
        }

        /// <summary>
        /// Групп үүсгэх
        /// </summary>
        /// <param name="name">Gruppийн нэр</param>
        /// <param name="description">Gruppийн тайлбар</param>
        /// <param name="ownerId">Эзэмшигчийн ID дугаар</param>
        /// <returns>Үүсгэгдсэн gruppийн DTO</returns>
        public GroupDTO CreateGroup(string name, string description, UserId ownerId)
        {
            var id = _idGenerator.NextGroupId();
            var group = new Subreddit
            {
                Id = id,
                Name = name,
                Description = description,
                OwnerId = ownerId
            };
            _repo.Save(group);
            return ToDTO(group);
        }

        /// <summary>
        /// Групп устгах
        /// </summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        public void DeleteGroup(GroupId groupId)
        {
            _repo.Delete(groupId);
        }

        /// <summary>
        /// Gruppийн мэдээлэл засах
        /// </summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <param name="name">Шинэ нэр</param>
        /// <param name="description">Шинэ тайлбар</param>
        /// <returns>Засагдсан gruppийн DTO</returns>
        public GroupDTO EditGroup(GroupId groupId, string name, string description)
        {
            var group = _repo.FindById(groupId);
            group.Name = name;
            group.Description = description;
            _repo.Update(group);
            return ToDTO(group);
        }

        /// <summary>
        /// Gruppийн мэдээлэл авах
        /// </summary>
        /// <param name="groupId">Gruppийн ID дугаар</param>
        /// <returns>Gruppийн DTO</returns>
        public GroupDTO GetGroup(GroupId groupId)
        {
            var group = _repo.FindById(groupId);
            return ToDTO(group);
        }

        /// <summary>
        /// Group объектыг DTO болгох
        /// </summary>
        /// <param name="group">Gruppийн объект</param>
        /// <returns>Gruppийн DTO</returns>
        private static GroupDTO ToDTO(Group group) =>
            new GroupDTO(group.Id, group.Name, group.Description, group.OwnerId, group.CreatedAt);
    }
}
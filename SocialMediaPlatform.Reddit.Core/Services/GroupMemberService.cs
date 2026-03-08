using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Group;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Ports.Input;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Services
{
    /// <summary>
    /// Группын гишүүний үйлдлүүдийг гүйцэтгэх Service класс
    /// </summary>
    public class GroupMemberService : IGroupMemberServicePort
    {
        private readonly IGroupMemberRepoPort _repo;
        private readonly IUserRepoPort _userRepo;
        private readonly IGroupRepoPort _groupRepo;

        /// <summary>
        /// GroupMemberService үүсгэх
        /// </summary>
        /// <param name="repo">Группын гишүүний Repository адаптер</param>
        /// <param name="userRepo">Хэрэглэгчийн Repository адаптер</param>
        /// <param name="groupRepo">Группын Repository адаптер</param>
        public GroupMemberService(IGroupMemberRepoPort repo, IUserRepoPort userRepo, IGroupRepoPort groupRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
            _groupRepo = groupRepo;
        }

        /// <summary>
        /// Групп нэгдэх
        /// </summary>
        /// <param name="groupId">Группийн ID дугаар</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void Join(GroupId groupId, UserId userId)
        {
            var member = new GroupMember<Privilege>
            {
                GroupId = groupId,
                UserId = userId,
                Role = Privilege.Member
            };
            _repo.Save(member);
        }

        /// <summary>
        /// Группаас гарах
        /// </summary>
        /// <param name="groupId">Группийн ID дугаар</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void Leave(GroupId groupId, UserId userId)
        {
            _repo.Delete(groupId, userId);
        }

        /// <summary>
        /// Группын гишүүдийг авах
        /// </summary>
        /// <param name="groupId">Группийн ID дугаар</param>
        /// <returns>Хэрэглэгчийн DTO жагсаалт</returns>
        public List<UserDTO> GetMembers(GroupId groupId)
        {
            return _repo.FindByGroup(groupId)
                .Select(m =>
                {
                    var user = _userRepo.FindById(m.UserId);
                    return new UserDTO(user.Id, user.Username, user.Email, user.GetType().Name, user.CreatedAt);
                })
                .ToList();
        }

        /// <summary>
        /// Хэрэглэгчийн группуудыг авах
        /// </summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Группийн DTO жагсаалт</returns>
        public List<GroupDTO> GetGroups(UserId userId)
        {
            return _repo.FindByUser(userId)
                .Select(m =>
                {
                    var group = _groupRepo.FindById(m.GroupId);
                    return new GroupDTO(group.Id, group.Name, group.Description, group.OwnerId, group.CreatedAt);
                })
                .ToList();
        }
    }
}
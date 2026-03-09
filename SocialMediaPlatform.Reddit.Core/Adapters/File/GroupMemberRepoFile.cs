using SocialMediaPlatform.Core.Domain.Group;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Группын гишүүний файл дээр суурилсан Repository адаптер
    /// </summary>
    public class GroupMemberRepoFile : IGroupMemberRepoPort
    {
        private readonly string _filePath;

        public GroupMemberRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Гишүүн хадгалах</summary>
        public void Save(GroupMemberBase groupMember)
        {
            var line = Serialize(groupMember);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>Гишүүн устгах</summary>
        public void Delete(GroupId groupId, UserId userId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return !(uint.Parse(parts[0]) == groupId.Value &&
                             uint.Parse(parts[1]) == userId.Value);
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Группын гишүүдийг авах</summary>
        public List<GroupMemberBase> FindByGroup(GroupId groupId)
        {
            var results = new List<GroupMemberBase>();
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var member = Deserialize(line);
                if (member.GroupId.Value == groupId.Value)
                    results.Add(member);
            }
            return results;
        }

        /// <summary>Хэрэглэгчийн группуудыг авах</summary>
        public List<GroupMemberBase> FindByUser(UserId userId)
        {
            var results = new List<GroupMemberBase>();
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var member = Deserialize(line);
                if (member.UserId.Value == userId.Value)
                    results.Add(member);
            }
            return results;
        }

        /// <summary>GroupMember объектыг мөр болгох</summary>
        private static string Serialize(GroupMemberBase member)
        {
            if (member is GroupMember<Privilege> gm)
                return $"{gm.GroupId.Value}|{gm.UserId.Value}|{gm.JoinedAt:O}|{gm.Role}";

            throw new ArgumentException($"Undefined GroupMember type: {member.GetType().Name}");
        }

        /// <summary>Мөрийг GroupMember объект болгох</summary>
        private static GroupMemberBase Deserialize(string line)
        {
            var parts = line.Split('|');
            return new GroupMember<Privilege>
            {
                GroupId = new GroupId { Value = uint.Parse(parts[0]) },
                UserId = new UserId { Value = uint.Parse(parts[1]) },
                JoinedAt = DateTime.Parse(parts[2]),
                Role = System.Enum.Parse<Privilege>(parts[3])
            };
        }
    }
}
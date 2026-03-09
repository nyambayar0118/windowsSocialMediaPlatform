using SocialMediaPlatform.Core.Domain.Group;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.Group;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Группын файл дээр суурилсан Repository адаптер
    /// </summary>
    public class GroupRepoFile : IGroupRepoPort
    {
        private readonly string _filePath;

        public GroupRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Групп хадгалах</summary>
        public void Save(Group group)
        {
            var line = Serialize(group);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>ID-аар групп хайх</summary>
        public Group FindById(GroupId groupId)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var group = Deserialize(line);
                if (group.Id.Value == groupId.Value)
                    return group;
            }
            throw new KeyNotFoundException($"Group ID {groupId.Value} not found");
        }

        /// <summary>Группын мэдээлэл шинэчлэх</summary>
        public void Update(Group group)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) == group.Id.Value
                        ? Serialize(group)
                        : line;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Групп устгах</summary>
        public void Delete(GroupId groupId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) != groupId.Value;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Group объектыг мөр болгох</summary>
        private static string Serialize(Group group)
        {
            if (group is Subreddit sr)
                return $"{sr.Id.Value}|{sr.Name}|{sr.Description}|{sr.CreatedAt:O}|{sr.OwnerId.Value}|{sr.Rules}|{sr.IsNSFW}|{sr.MinAccountAgeDays}";

            return $"{group.Id.Value}|{group.Name}|{group.Description}|{group.CreatedAt:O}|{group.OwnerId.Value}|||0";
        }

        /// <summary>Мөрийг Group объект болгох</summary>
        private static Group Deserialize(string line)
        {
            var parts = line.Split('|');
            return new Subreddit
            {
                Id = new GroupId { Value = uint.Parse(parts[0]) },
                Name = parts[1],
                Description = parts[2],
                CreatedAt = DateTime.Parse(parts[3]),
                OwnerId = new UserId { Value = uint.Parse(parts[4]) },
                Rules = parts[5],
                IsNSFW = bool.Parse(parts[6]),
                MinAccountAgeDays = int.Parse(parts[7])
            };
        }
    }
}
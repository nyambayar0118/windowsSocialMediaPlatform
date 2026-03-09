using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.User;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.User;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Хэрэглэгчийн файл дээр суурилсан Repository адаптер
    /// </summary>
    public class UserRepoFile : IUserRepoPort
    {
        private readonly string _filePath;

        public UserRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Хэрэглэгч хадгалах</summary>
        public void Save(UserBase user)
        {
            var line = Serialize(user);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>ID-аар хэрэглэгч хайх</summary>
        public UserBase FindById(UserId userId)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var user = Deserialize(line);
                if (user.Id.Value == userId.Value)
                    return user;
            }
            throw new KeyNotFoundException($"User ID {userId.Value} not found");
        }

        /// <summary>Хэрэглэгчийн нэрээр хэрэглэгч хайх</summary>
        public UserBase FindByUsername(string username)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var user = Deserialize(line);
                if (user.Username == username)
                    return user;
            }
            throw new KeyNotFoundException($"Username '{username}' not found");
        }

        /// <summary>Хэрэглэгчийн мэдээлэл шинэчлэх</summary>
        public void Update(UserBase user)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) == user.Id.Value
                        ? Serialize(user)
                        : line;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Хэрэглэгч устгах</summary>
        public void Delete(UserId userId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) != userId.Value;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>User объектыг мөр болгох</summary>
        private static string Serialize(UserBase user)
        {
            if (user is AdminUser admin)
            {
                var privileges = string.Join(",", admin.Privileges);
                return $"{admin.Id.Value}|{admin.Username}|{admin.Email}|{admin.Password}|{admin.CreatedAt:O}|Admin|{privileges}";
            }

            if (user is NormalUser normal)
                return $"{normal.Id.Value}|{normal.Username}|{normal.Email}|{normal.Password}|{normal.CreatedAt:O}|Normal|";

            throw new ArgumentException($"Undefined User type: {user.GetType().Name}");
        }

        /// <summary>Мөрийг User объект болгох</summary>
        private static UserBase Deserialize(string line)
        {
            var parts = line.Split('|');
            var id = new UserId { Value = uint.Parse(parts[0]) };
            var username = parts[1];
            var email = parts[2];
            var password = parts[3];
            var createdAt = DateTime.Parse(parts[4]);
            var type = parts[5];

            if (type == "Normal")
                return new NormalUser
                {
                    Id = id,
                    Username = username,
                    Email = email,
                    Password = password,
                    CreatedAt = createdAt,
                    Type = UserType.Normal
                };

            if (type == "Admin")
            {
                var privileges = string.IsNullOrWhiteSpace(parts[6])
                    ? new List<Privilege>()
                    : parts[6].Split(',')
                        .Select(p => System.Enum.Parse<Privilege>(p))
                        .ToList();

                return new AdminUser
                {
                    Id = id,
                    Username = username,
                    Email = email,
                    Password = password,
                    CreatedAt = createdAt,
                    Type = UserType.Admin,
                    Privileges = privileges
                };
            }

            throw new ArgumentException($"Undfined User type: {type}");
        }
    }
}
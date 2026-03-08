using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Дараалсан ID-ийн файл дээр суурилсан Repository адаптер
    /// </summary>
    public class SequentialIdRepoFile : ISequentialIdRepoPort
    {
        private readonly string _filePath;

        public SequentialIdRepoFile(string filePath)
        {
            _filePath = filePath;
            InitializeFile();
        }

        /// <summary>Сүүлийн ID утгыг авах</summary>
        public uint GetLastId(IdEntityType entityType)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('=');
                if (parts[0].Trim() == entityType.ToString())
                    return uint.Parse(parts[1].Trim());
            }
            return 0;
        }

        /// <summary>Сүүлийн ID утгыг хадгалах</summary>
        public void SaveLastId(IdEntityType entityType, uint value)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split('=');
                    return parts[0].Trim() == entityType.ToString()
                        ? $"{entityType}={value}"
                        : line;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Файл байхгүй бол үүсгэж анхны утгуудыг бичих</summary>
        private void InitializeFile()
        {
            if (!System.IO.File.Exists(_filePath) ||
                new FileInfo(_filePath).Length == 0)
            {
                var lines = System.Enum.GetValues<IdEntityType>()
                    .Select(e => $"{e}=0")
                    .ToArray();
                System.IO.File.WriteAllLines(_filePath, lines);
            }
        }
    }
}
using SocialMediaPlatform.Core.Domain.Enum;

namespace SocialMediaPlatform.Core.Ports.Output
{
    /// <summary>
    /// Дараалсан ID-ийн Repository-н Output Port интерфейс
    /// </summary>
    public interface ISequentialIdRepoPort
    {
        /// <summary>Сүүлийн ID утгыг авах</summary>
        /// <param name="entityType">Entity-н төрөл</param>
        /// <returns>Сүүлийн ID утга</returns>
        public uint GetLastId(IdEntityType entityType);

        /// <summary>Сүүлийн ID утгыг хадгалах</summary>
        /// <param name="entityType">Entity-н төрөл</param>
        /// <param name="value">Хадгалах ID утга</param>
        public void SaveLastId(IdEntityType entityType, uint value);
    }
}
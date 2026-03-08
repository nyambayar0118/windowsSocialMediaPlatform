using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Input
{
    /// <summary>
    /// Хэрэглэгчийн үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface IUserServicePort
    {
        /// <summary>Хэрэглэгч бүртгэх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="email">Цахим шуудан</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Бүртгэгдсэн хэрэглэгчийн DTO</returns>
        public UserDTO Register(string username, string email, string password);

        /// <summary>Хэрэглэгч нэвтрэх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Нэвтэрсэн хэрэглэгчийн DTO</returns>
        public UserDTO Login(string username, string password);

        /// <summary>Хэрэглэгч гарах</summary>
        public void Logout();

        /// <summary>Хэрэглэгчийн мэдээлэл авах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Хэрэглэгчийн DTO</returns>
        public UserDTO GetUser(UserId userId);

        /// <summary>Хэрэглэгчийн мэдээлэл засах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="username">Шинэ хэрэглэгчийн нэр</param>
        /// <param name="email">Шинэ цахим шуудан</param>
        /// <returns>Засагдсан хэрэглэгчийн DTO</returns>
        public UserDTO EditUser(UserId userId, string username, string email);

        /// <summary>Хэрэглэгч устгах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void DeleteUser(UserId userId);
    }
}
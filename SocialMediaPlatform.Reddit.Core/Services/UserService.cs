using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.User;
using SocialMediaPlatform.Core.Ports.Input;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.User;
using SocialMediaPlatform.Reddit.Core.Enum;
using SocialMediaPlatform.Reddit.Core.Factories;

namespace SocialMediaPlatform.Reddit.Core.Services
{
    /// <summary>
    /// Хэрэглэгчийн үйлдлүүдийг гүйцэтгэх Service класс
    /// </summary>
    public class UserService : IUserServicePort
    {
        private readonly IUserRepoPort _repo;
        private readonly IIdGeneratorPort _idGenerator;
        private readonly UserFactory _factory;

        /// <summary>
        /// UserService үүсгэх
        /// </summary>
        /// <param name="repo">Хэрэглэгчийн Repository адаптер</param>
        /// <param name="idGenerator">ID үүсгэгч</param>
        /// <param name="factory">Хэрэглэгч үүсгэх Factory</param>
        public UserService(IUserRepoPort repo, IIdGeneratorPort idGenerator, UserFactory factory)
        {
            _repo = repo;
            _idGenerator = idGenerator;
            _factory = factory;
        }

        /// <summary>
        /// Хэрэглэгч бүртгэх
        /// </summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="email">Цахим шуудан</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Бүртгэгдсэн хэрэглэгчийн DTO</returns>
        public UserDTO Register(string username, string email, string password)
        {
            var id = _idGenerator.NextUserId();
            var user = _factory.Create(UserType.Normal, id, username, email, password);
            _repo.Save(user);
            return ToDTO(user);
        }

        /// <summary>
        /// Хэрэглэгч нэвтрэх
        /// </summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Нэвтэрсэн хэрэглэгчийн DTO</returns>
        /// <exception cref="UnauthorizedAccessException">Нууц үг буруу үед</exception>
        public UserDTO Login(string username, string password)
        {
            var user = _repo.FindByUsername(username);
            if (user.Password != password)
                throw new UnauthorizedAccessException("Incorrect password");
            return ToDTO(user);
        }

        /// <summary>
        /// Хэрэглэгч гарах
        /// </summary>
        public void Logout() { }

        /// <summary>
        /// Хэрэглэгчийн мэдээлэл авах
        /// </summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Хэрэглэгчийн DTO</returns>
        public UserDTO GetUser(UserId userId)
        {
            var user = _repo.FindById(userId);
            return ToDTO(user);
        }

        /// <summary>
        /// Хэрэглэгчийн мэдээлэл засах
        /// </summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="username">Шинэ хэрэглэгчийн нэр</param>
        /// <param name="email">Шинэ цахим шуудан</param>
        /// <returns>Засагдсан хэрэглэгчийн DTO</returns>
        public UserDTO EditUser(UserId userId, string username, string email)
        {
            var user = _repo.FindById(userId);
            user.Username = username;
            user.Email = email;
            _repo.Update(user);
            return ToDTO(user);
        }

        /// <summary>
        /// Хэрэглэгч устгах
        /// </summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void DeleteUser(UserId userId)
        {
            _repo.Delete(userId);
        }

        /// <summary>
        /// Хэрэглэгч объектыг DTO болгох
        /// </summary>
        /// <param name="user">Хэрэглэгчийн объект</param>
        /// <returns>Хэрэглэгчийн DTO</returns>
        private static UserDTO ToDTO(UserBase user) =>
            new UserDTO(user.Id, user.Username, user.Email, user.GetType().Name, user.CreatedAt);
    }
}
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.User;

namespace SocialMediaPlatform.Core.Ports.Output
{
    /// <summary>
    /// Хэрэглэгчийн Repository-н Output Port интерфейс
    /// </summary>
    public interface IUserRepoPort
    {
        /// <summary>Хэрэглэгч хадгалах</summary>
        /// <param name="user">Хадгалах хэрэглэгчийн объект</param>
        public void Save(User<object> user);

        /// <summary>ID-аар хэрэглэгч хайх</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Олдсон хэрэглэгчийн объект</returns>
        public User<object> FindById(UserId userId);

        /// <summary>Хэрэглэгчийн нэрээр хэрэглэгч хайх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <returns>Олдсон хэрэглэгчийн объект</returns>
        public User<object> FindByUsername(string username);

        /// <summary>Хэрэглэгчийн мэдээлэл шинэчлэх</summary>
        /// <param name="user">Шинэчлэх хэрэглэгчийн объект</param>
        public void Update(User<object> user);

        /// <summary>Хэрэглэгч устгах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public void Delete(UserId userId);
    }
}
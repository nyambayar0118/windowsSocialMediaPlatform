using SocialMediaPlatform.Core.Ports.Input;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Core.Services;

namespace SocialMediaPlatform.Core.Infrastructure
{
    /// <summary>
    /// Service объектуудыг үүсгэх Service factory класс
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// User Service үүсгэх метод
        /// </summary>
        /// <param name="repo">User Service-ийн Repo-той ажиллах Port-ийг хэрэгжүүлсэн адаптер</param>
        /// <param name="idGenerator">ID үүсгэгч</param>
        /// <returns>IUserServicePort интерфейсийг хэрэгжүүлсэн UserService объект</returns>
        public static IUserServicePort CreateUserService(IUserRepoPort repo, IIdGeneratorPort idGenerator) => new UserService(repo, idGenerator);

        /// <summary>
        /// Post Service үүсгэх метод
        /// </summary>
        /// <param name="repo">Post Service-ийн Repo-той ажиллах Port-ийг хэрэгжүүлсэн адаптер</param>
        /// <param name="idGenerator">ID үүсгэгч</param>
        /// <returns>IPostServicePort интерфейсийг хэрэгжүүлсэн PostService объект</returns>
        public static IPostServicePort CreatePostService(IPostRepoPort repo, IIdGeneratorPort idGenerator) => new PostService(repo, idGenerator);

        /// <summary>
        /// Group Service үүсгэх метод
        /// </summary>
        /// <param name="repo">Group Service-ийн Repo-той ажиллах Port-ийг хэрэгжүүлсэн адаптер</param>
        /// <param name="idGenerator">ID үүсгэгч</param>
        /// <returns>IGroupServicePort интерфейсийг хэрэгжүүлсэн GroupService объект</returns>
        public static IGroupServicePort CreateGroupService(IGroupRepoPort repo, IIdGeneratorPort idGenerator) => new GroupService(repo, idGenerator);

        /// <summary>
        /// Comment Service үүсгэх метод
        /// </summary>
        /// <param name="repo">Comment Service-ийн Repo-той ажиллах Port-ийг хэрэгжүүлсэн адаптер</param>
        /// <param name="idGenerator">ID үүсгэгч</param>
        /// <returns>ICommentServicePort интерфейсийг хэрэгжүүлсэн CommentService объект</returns>
        public static ICommentServicePort CreateCommentService(ICommentRepoPort repo, IIdGeneratorPort idGenerator) => new CommentService(repo, idGenerator);

        /// <summary>
        /// Reaction Service үүсгэх метод
        /// </summary>
        /// <param name="repo">Reaction Service-ийн Repo-той ажиллах Port-ийг хэрэгжүүлсэн адаптер</param>
        /// <returns>IReactionServicePort интерфейсийг хэрэгжүүлсэн ReactionService объект</returns>
        public static IReactionServicePort CreateReactionService(IReactionRepoPort repo) => new ReactionService(repo);

        /// <summary>
        /// GroupMember Service үүсгэх метод
        /// </summary>
        /// <param name="repo">GroupMember Service-ийн Repo-той ажиллах Port-ийг хэрэгжүүлсэн адаптер</param>
        /// <returns>IGroupMemberServicePort интерфейсийг хэрэгжүүлсэн GroupMemberService объект</returns>
        public static IGroupMemberServicePort CreateGroupMemberService(IGroupMemberRepoPort repo) => new GroupMemberService(repo);
    }
}
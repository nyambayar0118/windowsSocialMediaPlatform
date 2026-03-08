using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Adapters.File;
using SocialMediaPlatform.Reddit.Core.Adapters.IdGenerator;
using SocialMediaPlatform.Reddit.Core.Factories;
using SocialMediaPlatform.Reddit.Core.Services;

namespace SocialMediaPlatform.Reddit.Core
{
    /// <summary>
    /// Системийн бүх хэсгүүдийг тохиргооны дагуу холбох AppConfig класс
    /// </summary>
    public class AppConfig
    {
        private readonly Controller _controller;

        /// <summary>
        /// AppConfig үүсгэж бүх хэсгүүдийг холбох
        /// </summary>
        /// <param name="configPath">Config файлын зам</param>
        public AppConfig(string configPath)
        {
            var cfg = ReadConfig(configPath);

            // Adapters
            var idRepoFile = new SequentialIdRepoFile(cfg["ids"]);
            var idGenerator = new SequentialIdGenerator(idRepoFile);
            var userRepo = new UserRepoFile(cfg["users"]);
            var postRepo = new PostRepoFile(cfg["posts"]);
            var groupRepo = new GroupRepoFile(cfg["groups"]);
            var groupMemberRepo = new GroupMemberRepoFile(cfg["group_members"]);
            var commentRepo = new CommentRepoFile(cfg["comments"]);
            var reactionRepo = new ReactionRepoFile(cfg["reactions"]);

            // Factories
            var userFactory = new UserFactory();
            var postFactory = new PostFactory();

            // Services
            var userService = new UserService(userRepo, idGenerator, userFactory);
            var postService = new PostService(postRepo, idGenerator, postFactory);
            var groupService = new GroupService(groupRepo, idGenerator);
            var groupMemberService = new GroupMemberService(groupMemberRepo, userRepo, groupRepo);
            var commentService = new CommentService(commentRepo, idGenerator);
            var reactionService = new ReactionService(reactionRepo);

            // Session
            var session = Session.GetInstance();

            // Controller
            _controller = new Controller(
                userService,
                postService,
                groupService,
                commentService,
                reactionService,
                groupMemberService,
                session
            );
        }

        /// <summary>
        /// Controller авах
        /// </summary>
        public Controller GetController() => _controller;

        /// <summary>
        /// Config файл уншиж Dictionary буцаах
        /// </summary>
        /// <param name="path">Config файлын зам</param>
        private static Dictionary<string, string> ReadConfig(string path)
        {
            var config = new Dictionary<string, string>();
            foreach (var line in System.IO.File.ReadAllLines(path))
            {
                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                    config[parts[0].Trim()] = parts[1].Trim();
            }
            return config;
        }
    }
}
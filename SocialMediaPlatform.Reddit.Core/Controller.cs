using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Core.Ports.Input;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core
{
    /// <summary>
    /// Хэрэглэгчийн үйлдлүүдийг удирдах Controller класс
    /// </summary>
    public class Controller
    {
        private readonly IUserServicePort _userService;
        private readonly IPostServicePort _postService;
        private readonly IGroupServicePort _groupService;
        private readonly ICommentServicePort _commentService;
        private readonly IReactionServicePort _reactionService;
        private readonly IGroupMemberServicePort _groupMemberService;
        private readonly Session _session;

        public Controller(
            IUserServicePort userService,
            IPostServicePort postService,
            IGroupServicePort groupService,
            ICommentServicePort commentService,
            IReactionServicePort reactionService,
            IGroupMemberServicePort groupMemberService,
            Session session)
        {
            _userService = userService;
            _postService = postService;
            _groupService = groupService;
            _commentService = commentService;
            _reactionService = reactionService;
            _groupMemberService = groupMemberService;
            _session = session;
        }

        // ─── AUTH ────────────────────────────────────────────────

        /// <summary>Хэрэглэгч бүртгэх</summary>
        public void Register(string username, string email, string password)
        {
            var user = _userService.Register(username, email, password);
            _session.Login(user);
        }

        /// <summary>Хэрэглэгч нэвтрэх</summary>
        public void Login(string username, string password)
        {
            var user = _userService.Login(username, password);
            _session.Login(user);
        }

        /// <summary>Хэрэглэгч гарах</summary>
        public void Logout()
        {
            _userService.Logout();
            _session.Logout();
        }

        // ─── POST ────────────────────────────────────────────────

        /// <summary>Post үүсгэх</summary>
        public PostDTO CreatePost(PostType type, string content)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            return _postService.CreatePost(type.ToString(), currentUser.Id, content);
        }

        /// <summary>Post устгах</summary>
        public void DeletePost(PostId postId) =>
            _postService.DeletePost(postId);

        /// <summary>Post засах</summary>
        public PostDTO EditPost(PostId postId, string content) =>
            _postService.EditPost(postId, content);

        /// <summary>Timeline авах</summary>
        public List<PostDTO> GetTimeline()
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            return _postService.GetTimeline();
        }

        // ─── GROUP ───────────────────────────────────────────────

        /// <summary>Групп үүсгэх</summary>
        public GroupDTO CreateGroup(string name, string description)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            var result = _groupService.CreateGroup(name, description, currentUser.Id);
            JoinGroup(result.Id); // Групп үүсгэсний дараа өөрөө нэгдэнэ   
            return result;
        }

        /// <summary>Групп устгах</summary>
        public void DeleteGroup(GroupId groupId) =>
            _groupService.DeleteGroup(groupId);

        /// <summary>Групп засах</summary>
        public GroupDTO EditGroup(GroupId groupId, string name, string description) =>
            _groupService.EditGroup(groupId, name, description);

        /// <summary>Группийг ID дугаараар хайх</summary>
        public GroupDTO GetGroup(GroupId groupId) =>
            _groupService.GetGroup(groupId);

        /// <summary>Групп нэгдэх</summary>
        public void JoinGroup(GroupId groupId)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            _groupService.GetGroup(groupId); // Групп байгаа эсэхийг шалгах
            _groupMemberService.Join(groupId, currentUser.Id);
        }

        /// <summary>Группаас гарах</summary>
        public void LeaveGroup(GroupId groupId)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            _groupService.GetGroup(groupId);
            _groupMemberService.Leave(groupId, currentUser.Id);
        }

        // ─── COMMENT ─────────────────────────────────────────────

        /// <summary>Comment нэмэх</summary>
        public CommentDTO AddComment(PostId postId, string content)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            _postService.GetPost(postId); // Post байгаа эсэхийг шалгах
            return _commentService.AddComment(postId, currentUser.Id, content);
        }

        /// <summary>Comment-д хариу бичих</summary>
        public CommentDTO ReplyToComment(CommentId commentId, string content)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            _commentService.GetComment(commentId); // Comment байгаа эсэхийг шалгах
            return _commentService.ReplyToComment(commentId, currentUser.Id, content);
        }

        /// <summary>Comment устгах</summary>
        public void DeleteComment(CommentId commentId) =>
            _commentService.DeleteComment(commentId);

        /// <summary>Post-ийн comment-уудыг авах</summary>
        public List<CommentDTO> GetComments(PostId postId) =>
            _commentService.GetComments(postId);

        /// <summary>Comment-ийн хариунуудыг авах</summary>
        public List<CommentDTO> GetReplies(CommentId commentId) =>
            _commentService.GetReplies(commentId);

        // ─── REACTION ────────────────────────────────────────────

        /// <summary>Post-д reaction хийх</summary>
        public void ReactPost(PostId postId, ReactionType type)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            _reactionService.React(postId.Value, ReactionTargetType.Post, currentUser.Id, type.ToString());
        }

        /// <summary>Comment-д reaction хийх</summary>
        public void ReactComment(CommentId commentId, ReactionType type)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            _reactionService.React(commentId.Value, ReactionTargetType.Comment, currentUser.Id, type.ToString());
        }

        /// <summary>Reaction устгах</summary>
        public void Unreact(uint targetId, ReactionTargetType targetType)
        {
            var currentUser = _session.GetCurrentUser()
                ?? throw new InvalidOperationException("Session expired");
            _reactionService.Unreact(targetId, targetType, currentUser.Id);
        }
    }
}
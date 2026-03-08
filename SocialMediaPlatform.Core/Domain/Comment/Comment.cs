using SocialMediaPlatform.Core.Domain.ID;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Core.Domain.Comment
{
    /// <summary>
    /// Comment-ийн үндсэн абстракт ерөнхий төрлийн класс
    /// </summary>
    public abstract class Comment<TCommentType> : CommentBase
    {
        public required TCommentType Type { get; init; }
    }
}

using SocialMediaPlatform.Core.Domain.ID;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Core.Domain.Comment
{
    /// <summary>
    /// Comment-ийн үндсэн абстракт класс
    /// </summary>
    public abstract class Comment<TCommentType>
    {
        /// <summary>ID дугаар</summary>
        public required CommentId Id { get; init; }

        /// <summary>Бичсэн хэрэглэгчийн ID дугаар</summary>
        public required UserId AuthorId { get; init; }

        /// <summary>Агуулга</summary>
        public required string Content { get; init; }

        /// <summary>Үүсгэсэн огноо</summary>
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        /// <summary>Comment-ийн төрөл</summary>
        public required TCommentType Type { get; init; }
    }
}

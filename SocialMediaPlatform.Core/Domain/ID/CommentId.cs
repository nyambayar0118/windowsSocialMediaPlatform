namespace SocialMediaPlatform.Core.Domain.ID
{
    /// <summary>
    /// Комментийн ID дугаарыг ялгахад зориулсан wrapper класс
    /// </summary>
    public class CommentId
    {
        /// <summary>
        /// Комметийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init;  }

        public override string ToString() => Value.ToString();
    }
}

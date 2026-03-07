namespace SocialMediaPlatform.Core.Domain.ID
{
    /// <summary>
    /// Comment-ийн ID дугаарыг ялгахад зориулсан wrapper класс
    /// </summary>
    public class CommentId
    {
        /// <summary>
        /// Comment-ийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init;  }

        public override string ToString() => Value.ToString();
    }
}

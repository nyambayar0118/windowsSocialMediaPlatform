namespace SocialMediaPlatform.Core.Domain.ID
{
    /// <summary>
    /// Постийн ID дугаарыг ялгахад зориулсан wrapper класс
    /// </summary>
    public class PostId
    {
        /// <summary>
        /// Постийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init; }

        public override string ToString() => Value.ToString();
    }
}

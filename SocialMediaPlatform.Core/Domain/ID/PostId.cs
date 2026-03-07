namespace SocialMediaPlatform.Core.Domain.ID
{
    /// <summary>
    /// Post-ийн ID дугаарыг ялгахад зориулсан wrapper класс
    /// </summary>
    public class PostId
    {
        /// <summary>
        /// Post-ийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init; }

        public override string ToString() => Value.ToString();
    }
}

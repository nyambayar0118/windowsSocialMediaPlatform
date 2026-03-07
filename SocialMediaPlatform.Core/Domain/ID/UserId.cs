namespace SocialMediaPlatform.Core.Domain.ID
{
    /// <summary>
    /// User-ийн ID дугаарыг ялгахад зориулсан wrapper класс
    /// </summary>
    public class UserId
    {
        /// <summary>
        /// User-ийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init; }

        public override string ToString() => Value.ToString();
    }
}

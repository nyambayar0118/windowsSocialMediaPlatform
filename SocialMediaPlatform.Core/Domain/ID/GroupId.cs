namespace SocialMediaPlatform.Core.Domain.ID
{
    /// <summary>
    /// Group-ийн ID дугаарыг ялгахад зориулсан wrapper класс
    /// </summary>
    public class GroupId
    {
        /// <summary>
        /// Group-ийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init; }

        public override string ToString() => Value.ToString();
    }
}

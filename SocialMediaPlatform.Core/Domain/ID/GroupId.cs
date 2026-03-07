namespace SocialMediaPlatform.Core.Domain.ID
{
    /// <summary>
    /// Грүпийн ID дугаарыг ялгахад зориулсан wrapper класс
    /// </summary>
    public class GroupId
    {
        /// <summary>
        /// Грүпийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init; }

        public override string ToString() => Value.ToString();
    }
}

namespace Task2
{
    internal static class Exceptions
    {
        [Serializable]
        public class NotCorrectPointPosition(Position position) : Exception
        {
            public override string Message => $"NotCorrectPointPosition: point at coordinates {position} not found";
        }
    }
}

namespace Task2
{
    internal struct Position(float x, float y)
    {
        public float X => x;
        public float Y => y;

        public override string ToString()
        {
            return $"(x: {X}, y: {Y})";
        }
    }
}

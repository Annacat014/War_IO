namespace LearnGame.Enemy
{
    internal class NavMashQueryFilter
    {
        internal int areaMask;

        public NavMashQueryFilter(object value)
        {
            Value = value;
        }

        public object Value { get; }
    }
}
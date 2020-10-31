namespace CommandResolver.Helpers
{
    public class MutableKeyValuePair<Key, Val>
    {
        public Key Id { get; set; }
        public Val Value { get; set; }

        public MutableKeyValuePair() { }

        public MutableKeyValuePair(Key key, Val val)
        {
            Id = key;
            Value = val;
        }
    }
}

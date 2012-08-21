namespace ALinq
{
    public sealed class AsyncLoopContext<T>
    {
        public long     Index { get; internal set; }
        public T        Item  { get; internal set; }

        internal bool   WasBreakCalled { get; private set; }

        public void Break()
        {
            WasBreakCalled = true;
        }
    }
}
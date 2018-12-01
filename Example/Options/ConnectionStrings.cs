namespace Example.Options
{
    public class ConnectionStrings
    {
        public ConnectionStrings(string @default)
        {
            Default = @default;
        }
        public string Default { get; }
    }
}

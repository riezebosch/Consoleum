namespace Consoleum.PageObjects
{
    [System.Serializable]
    public class NavigationFailedException : System.Exception
    {
        public NavigationFailedException() { }
        public NavigationFailedException(string message) : base(message) { }
        public NavigationFailedException(string message, System.Exception inner) : base(message, inner) { }
        protected NavigationFailedException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public string Output { get; internal set; }
    }
}
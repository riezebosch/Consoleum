namespace Consoleum.PageObjects
{
    [System.Serializable]
    public class OutputNotFoundException : System.Exception
    {
        public OutputNotFoundException() { }
        public OutputNotFoundException(string message) : base(message) { }
        public OutputNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected OutputNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public string Output { get; internal set; }
        public string Pattern { get; internal set; }
    }
}
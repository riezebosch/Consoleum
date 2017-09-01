namespace Consoleum
{
    [System.Serializable]
    public class CaptureOutputException : System.Exception
    {
        public CaptureOutputException() { }
        public CaptureOutputException(string message) : base(message) { }
        public CaptureOutputException(string message, System.Exception inner) : base(message, inner) { }
        protected CaptureOutputException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
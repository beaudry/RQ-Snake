using System;

namespace Serpent
{
    [Serializable]
    public class SnakeIsDeadException : Exception
    {
        public SnakeIsDeadException() { }
        public SnakeIsDeadException(string message) : base(message) { }
        public SnakeIsDeadException(string message, Exception inner) : base(message, inner) { }
        protected SnakeIsDeadException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

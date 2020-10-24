using System;
using System.Collections.Generic;
using System.Text;

namespace BookJournal.FunctionApp.Exceptions
{
    [Serializable]
    public class NoUpnException : Exception
    {
        public NoUpnException() { }
        public NoUpnException(string message) : base(message) { }
        public NoUpnException(string message, Exception inner) : base(message, inner) { }
        protected NoUpnException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BookJournal.FunctionApp.Exceptions
{
    [Serializable]
    public class BookNameNullException : Exception
    {
        public BookNameNullException() { }
        public BookNameNullException(string message) : base(message) { }
        public BookNameNullException(string message, Exception inner) : base(message, inner) { }
        protected BookNameNullException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

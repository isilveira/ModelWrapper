using System;
using System.Runtime.Serialization;

namespace BAYSOFT.Abstractions.Core.Domain.Exceptions
{
    public class BaysoftException : Exception
    {
        public int ExceptionCode { get; set; }
        public int ExceptionInternalCode { get; set; }
        public BaysoftException(int exceptionCode, int exceptionInternalCode)
        {
            ExceptionCode = exceptionCode;
            ExceptionInternalCode = exceptionInternalCode;
        }

        public BaysoftException(int exceptionCode, int exceptionInternalCode, string message) : base(message)
        {
            ExceptionCode = exceptionCode;
            ExceptionInternalCode = exceptionInternalCode;
        }

        public BaysoftException(int exceptionCode, int exceptionInternalCode, string message, Exception innerException) : base(message, innerException)
        {
            ExceptionCode = exceptionCode;
            ExceptionInternalCode = exceptionInternalCode;
        }

        protected BaysoftException(int exceptionCode, int exceptionInternalCode, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ExceptionCode = exceptionCode;
            ExceptionInternalCode = exceptionInternalCode;
        }
    }
}
